using AutoMapper;
using MediatR;
using Synopsis.Infrastructure;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Interfaces;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Application.Errors.Commands.CreateError;

public record CreateErrorCommand(
    string Message,
    string Controller,
    string Method,
    string Source) : IRequest<ErrorDto>;

public class CreateErrorCommandHandler : IRequestHandler<CreateErrorCommand, ErrorDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateErrorCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<ErrorDto> Handle(CreateErrorCommand request, CancellationToken cancellationToken)
    {
        var row = new Error(
            request.Message,
            request.Controller,
            request.Method,
            request.Source
        );
        
        await _dbContext.Errors
            .AddAsync(row, cancellationToken)
            .ConfigureAwait(false);
        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
        
        return _mapper.Map<ErrorDto>(row);    
    }
}