using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Domain.Entities;

namespace Synopsis.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<Error> Errors { get; }

    DbSet<Partner> Partners { get; }

    DbSet<Speaker> Speakers { get; }

    DbSet<Ticket> Tickets { get; }
    DbSet<Agenda> Agendas { get; }
    DbSet<Site> Sites { get; }
    DbSet<Logo> Logos { get; }
    DbSet<Feedback> Feedbacks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}