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
        var user = await _userManager.FindByEmailAsync(query.Email);

        // 1. Validate the user exists
        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);

        // 2. Validate / show error if the login/password is not correct
        if (!result.Succeeded)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
        }

        return new AuthenticationResult(
            user,
            _jwtTokenGenerator.GenerateToken(user)); // Create Jwt token
    }
}
