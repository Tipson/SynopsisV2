namespace SynopsisV2.Application.Common.Models;

public record SendSmtpCommand(string Subject, string Body, string Recipient);