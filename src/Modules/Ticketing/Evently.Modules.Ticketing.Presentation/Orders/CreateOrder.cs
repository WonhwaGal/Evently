using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Ticketing.Application.Orders.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Ticketing.Presentation.Orders;

internal sealed class CreateOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (
                [FromBody] Request request,
                [FromServices] /*ICustomerContext customerContext*/ ISender sender) =>
            {
                Result result = await sender.Send(new CreateOrderCommand(
                    request.CustomerId
                    //customerContext.CustomerId
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            //.RequireAuthorization(Permissions.CreateOrder)
            .WithTags(Tags.Orders);
    }
    
    /// <summary>
    /// Запрос на создание заказа
    /// </summary>
    internal sealed class Request
    {
        
        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public Guid CustomerId { get; init; }
    }
}
