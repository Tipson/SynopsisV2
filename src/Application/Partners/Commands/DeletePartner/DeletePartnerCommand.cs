using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Partners.Commands.DeletePartner;

public record DeletePartnerCommand : IRequest
{
    public int Id { get; set; } //Todo
}
public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeletePartnerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Partners
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        if (row is null)
        { 
            return Unit.Value;
        }

        _dbContext.Partners.Remove(row);
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return Unit.Value;
    }
}