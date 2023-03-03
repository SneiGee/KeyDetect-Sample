namespace Contracts.Authentication;

public record AuthenticationResponse(
    string FirstName,
    string LastName,
    string Occupation,
    string Email,
    string Token);