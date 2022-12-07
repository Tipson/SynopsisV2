using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Mappings;

namespace SynopsisV2.Application.Common.Models;

public class TicketDto : IMapFrom<Ticket>
{
    public int Id { get; set; }
    public string Mail { get; set; }
    public int Type { get; set; }
    public string Code { get; set; }
    public string Telegram { get; set; }    
    public string WalletBsc { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
}