using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using Synopsis.Models.Partners;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Partners.Commands.UpdatePartner;

public record UpdatePartnerCommand(
    int Id,
    string Name, 
    string Logo,
    PartnerType Type,
    ImportanceType Importance,
    string Url) : IRequest<PartnerDto>;

public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand, PartnerDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdatePartnerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<PartnerDto> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
    {
        var row = await _dbContext.Partners
            .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (row is null)
        {
            throw new InvalidOperationException($"The Partners with id = {request.Id} not found");
        }

        row.Name = request.Name;
        row.Logo = request.Logo;
        row.Type = (int)request.Type;
        row.Important = (int)request.Importance;
        row.Url = request.Url;

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return _mapper.Map<PartnerDto>(row);    }
}