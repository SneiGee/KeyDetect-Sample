using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand(
    string UserId,
    string Token) : IRequest<ErrorOr<AuthenticationResult>>;