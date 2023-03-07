namespace Domain.Entities;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Occupation { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}