using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository,
        UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // await Task.CompletedTask;

        // var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == query.Email);

        // 1. Validate the user exists
        if (await _userRepository.GetUserByEmail(query.Email) is not AppUser user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Validate the password is correct
        // if (user.Password != query.Password)
        // {
        //     return new[] { Errors.Authentication.InvalidCredentials };
        // }

        var result = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);

        // 2. Validate the password is correct
        if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
        }

        // Create Jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}