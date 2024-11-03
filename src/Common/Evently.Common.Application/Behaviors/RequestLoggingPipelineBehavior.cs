using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Evently.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Evently.Common.Application.Behaviors;
internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : Result
{

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        string moduleName = GetModuleName(typeof(TRequest).FullName!);

        using (LogContext.PushProperty("Module", moduleName))
        {
            logger.LogInformation("Executing request {RequestName}", requestName);

            TResponse result = await next();

            if (result.IsSuccess)
            {
                string jsonResponse = JsonSerializer.Serialize(result, _jsonSerializerOptions);
                logger.LogInformation("Request {RequestName} processed successfully with response: {Response}", requestName, jsonResponse);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogError("Request {RequestName} processed with error", requestName);
                }
            }

            return result;
        }

    }

    private static string GetModuleName(string requestName) => requestName.Split('.')[2];
}
