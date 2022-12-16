namespace SynopsisV2.Infrastructure.Configuration;

public class SmtpSecrets
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public bool UseSsl { get; set; }
    public string ManagerEmail { get; set; }
}