using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Application.Partners.Commands.UpdatePartner;

namespace SynopsisV2.Application.Partners.Commands.UpdateVisibilityPartner;

public record UpdateVisibilityPartnerCommand(
    int Id,
    bool IsShow) : IRequest<PartnerDto>;

public class UpdateVisibilityPartnerCommandHandler : IRequestHandler<UpdateVisibilityPartnerCommand, PartnerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateVisibilityPartnerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PartnerDto> Handle(UpdateVisibilityPartnerCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Partners
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        if (row is null)
        {
            throw new InvalidOperationException($"The Speaker with id = {request.Id} not found");
        }
        row.IsShow = request.IsShow ? 1:0;
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return _mapper.Map<PartnerDto>(row);
    }
}