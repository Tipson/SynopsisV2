// using SynopsisV2.Application.Common.Interfaces;
// using MediatR.Pipeline;
// using Microsoft.Extensions.Logging;
//
// namespace SynopsisV2.Application.Common.Behaviours;
//
// public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
// {
//     private readonly ILogger _logger;
//     private readonly IIdentityService _identityService;
//
//     public LoggingBehaviour(ILogger logger, IIdentityService identityService)
//     {
//         _logger = logger;
//         _identityService = identityService;
//     }
//
//     public async Task Process(TRequest request, CancellationToken cancellationToken)
//     {
//         var requestName = typeof(TRequest).Name;
//         string? userName = string.Empty;
//         
//
//         _logger.LogInformation("SynopsisV2 Request: {Name} {@UserName} {@Request}",
//             requestName, userName, request);
//     }
// }
