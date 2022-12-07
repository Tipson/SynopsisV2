using AutoMapper;
using MediatR;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using Synopsis.Models;
using Synopsis.Models.Partners;
using SynopsisV2.Application.Common.Models;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Partners.Commands.CreatePartner;

public record CreatePartnerCommand(
    string Name, 
    string Logo,
    PartnerType Type,
    SynopsisVersionType SynopsisType,
    ImportanceType Importance,
    int IsShow,
    string Url) : IRequest<PartnerListDto>;

public class CreatePartnerCommandHandle : IRequestHandler<CreatePartnerCommand, PartnerListDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreatePartnerCommandHandle(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PartnerListDto> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
    {
        var row = new Partner(
            request.Name,
            request.Logo,
            (int)request.Type, 
            (int)request.SynopsisType,
            (int)request.Importance, 
            request.IsShow,
            request.Url
        );

        await _dbContext.Partners
            .AddAsync(row, cancellationToken)
            .ConfigureAwait(false);
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        return _mapper.Map<PartnerListDto>(row);    
    }
}