using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Ticketing.Application.Carts.ClearCart;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Ticketing.Presentation.Carts;
internal sealed class ClearCart: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("carts/clear", async (Guid customerId, ISender sender) =>
        {
            var command = new ClearCartCommand(customerId);

            Result result = await sender.Send(command);

            return result.Match(() => Results.Ok(), ApiResults.Problem);

        }).WithTags(Tags.Carts);
    }
}
