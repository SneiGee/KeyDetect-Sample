namespace Contracts.Authentication.Dto;

public class UserDto
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Occupation { get; set; } = null!;
    public string Token { get; set; } = null!;
}