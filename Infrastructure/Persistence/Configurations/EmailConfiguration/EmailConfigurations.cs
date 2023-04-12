namespace Infrastructure.Persistence.Configurations.EmailConfiguration;

public class EmailConfigurations
{
    public const string SectionName = "EmailConfigurations";
    public string From { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool EnableSSL { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public bool IsBodyHTML { get; set; }
}