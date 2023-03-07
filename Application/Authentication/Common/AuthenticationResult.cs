using Domain.Entities;

namespace Application.Authentication.Common;

public record AuthenticationResult(
    AppUser AppUser,
    string Token);