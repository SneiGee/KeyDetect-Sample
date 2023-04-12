using Application.Authentication.Common;
using Application.Common.Helpers;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using Domain.Identity;
using ErrorOr;
using FluentEmail.Core;
using FluentEmail.Razor;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;
    private readonly IFluentEmail _fluentEmail;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository,
        UserManager<AppUser> userManager, IMapper mapper,
        IConfiguration config, IFluentEmail fluentEmail)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _userManager = userManager;
        _mapper = mapper;
        _config = config;
        _fluentEmail = fluentEmail;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        // Validate the user doesn't exist
        if (await _userRepository.CheckEmailExists(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        // Create User (generate unique ID) and Persist to Db.
        var user = _mapper.Map<AppUser>(command);

        user.UserName = command.Email;
        user.Email = command.Email;

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            return Errors.AddUser.FailedAddingUser;
        }

        // Generate email confirmation token and confirmation link
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        string urlPath = "";
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env?.ToLower() == "development")
        {
            urlPath = _config["returnPaths:ConfirmEmail"]!;
        }
        else
        {
            urlPath = Environment.GetEnvironmentVariable("ReturnPaths:ConfirmEmail")!;
        }

        var confirmationLink = URLBuilder.BuildUrl(urlPath, token, user.Id);

         // Build email message and send it
        _fluentEmail.Renderer = new RazorRenderer();

        await _fluentEmail
            .To(user.Email)
            .Subject("Confirm your email")
            .UsingTemplateFromFile(
                "EmailTemplates/Authentication/ConfirmEmail.cshtml",
                new { Username = user.FirstName, ConfirmationLink = confirmationLink })
            .SendAsync();

        // 3. Create JWT Token
        return new AuthenticationResult(
            user,
            _jwtTokenGenerator.GenerateToken(user));
    }
}