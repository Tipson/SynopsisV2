using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure;
using SynopsisV2.Domain.Entities;
using SynopsisV2.Domain.Enums;

namespace SynopsisV2.Application.Agendas.Queries.GetListAgendas;

public record GetListAgendasCommand(SynopsisVersionType VersionType, string Lang) : IRequest<AgendaListDto>;

public class GetListAgendasCommandHandler : IRequestHandler<GetListAgendasCommand, AgendaListDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListAgendasCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<AgendaListDto> Handle(GetListAgendasCommand request, CancellationToken cancellationToken)
    {
        var rows = await _dbContext.Agendas
            .Include(r=>r.Speakers)
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        
        var models = _mapper.Map<List<Agenda>>(rows);
        foreach (var model in models)
        {
            if (request.Lang == "ru")
            {
                model.Topic = model.TopicRu;
                foreach (var speaker in model.Speakers)
                {
                    speaker.Name = speaker.NameRu;
                }
            }
            else
            {
                model.Topic = model.TopicEn;
                foreach (var speaker in model.Speakers)
                {
                    speaker.Name = speaker.NameEn;
                }            
            }
        }
        return _mapper.Map<AgendaListDto>(models);    
    }
}