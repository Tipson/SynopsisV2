namespace SynopsisV2.Domain.Entities;

public partial class Ticket
{
    public int Id { get; set; }

    public string? Mail { get; set; }

    public int? Type { get; set; }

    public string? Code { get; set; }

    public string? Telegram { get; set; }

    public string? WalletBsc { get; set; }

    public string? Login { get; set; }

    public string? Name { get; set; }


    public Ticket(
        string tMail,
        int tType,
        string tCode,
        string tTelegram,
        string tWalletBsc,
        string tLogin,
        string tName
    )

    {
        Mail = tMail;
        Type = tType;
        Code = tCode;
        Telegram = tTelegram;
        WalletBsc = tWalletBsc;
        Login = tLogin;
        Name = tName;

    }

    public Ticket()
    {
        
    }
}