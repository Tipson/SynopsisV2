using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Common.Interfaces;


public interface ISmtpService
{
    Task SendAsync(SendSmtpCommand command, CancellationToken cancellationToken);
}
