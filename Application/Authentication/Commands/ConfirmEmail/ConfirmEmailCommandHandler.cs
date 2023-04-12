using Application.Authentication.Common;
using Domain.Identity;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ErrorOr<AuthenticationResult>>
{
    private readonly UserManager<AppUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(ConfirmEmailCommand command,
        CancellationToken cancellationToken)
    {
        // Find and validate if user id exist / valid
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user is null)
        {
            return Errors.AuthUserIdExists.UserIdExists;
        }

        // Confirm user email 
        var confirm = await _userManager.ConfirmEmailAsync(user, command.Token);
        if (!confirm.Succeeded) 
        {
            return Errors.FailedToConfirmEmail.FailedToConfirmUser;
        }

        // Create JWT Token
        return new AuthenticationResult(
            user,
            command.Token);
    }
}