using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Ticketing.Presentation.Carts;

public static class CartEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        AddToCart.MapEndpoint(app);
    }
}
