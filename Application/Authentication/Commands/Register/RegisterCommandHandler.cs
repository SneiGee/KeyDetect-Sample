using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using Domain.Identity;
using ErrorOr;
using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository,
        UserManager<AppUser> userManager, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        // Validate the user doesn't exist
        if (await CheckEmailExists(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        // Create User (generate unique ID) and Persist to Db.
        var user = new AppUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Occupation = command.Occupation,
            Email = command.Email,
            // PasswordHash = command.Password
        };

        // var user = _mapper.Map<AppUser>(command);

        var result = await _userManager.CreateAsync(user, command.Password);

        // 3. Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    private async Task<bool> CheckEmailExists(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }
}