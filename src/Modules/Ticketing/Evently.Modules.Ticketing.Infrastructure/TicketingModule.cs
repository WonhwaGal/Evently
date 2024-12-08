using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Ticketing.Application.Carts;
using Evently.Modules.Ticketing.Presentation.Carts;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CartEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddTicketingModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<CartService>();

        return services;
    }

}
