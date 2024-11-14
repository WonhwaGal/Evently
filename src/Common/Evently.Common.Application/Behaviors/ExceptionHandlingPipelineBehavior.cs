using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Evently.Common.Application.Behaviors;

public class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return next(); // Вызов следующего обработчика в конвейере
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception for {RequestName}", typeof(TRequest).Name);
            throw new EventlyException(typeof(TRequest).Name, innerException: exception); // Проброс исключения
        }
    }
}
