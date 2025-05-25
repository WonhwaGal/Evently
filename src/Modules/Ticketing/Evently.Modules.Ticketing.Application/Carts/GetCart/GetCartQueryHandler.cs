using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Users.PublicApi;

namespace Evently.Modules.Ticketing.Application.Carts.GetCart;
internal sealed class GetCartQueryHandler(
    CartService cartService,
    ICustomerRepository customerRepository) : IQueryHandler<GetCartQuery, CartResponse>
{
    public async Task<Result<CartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        Customer? customer = 
            await customerRepository.GetAsync(request.CustomerId, cancellationToken);
        if(customer is null)
        {
            return Result.Failure<CartResponse>(CustomerErrors.NotFound(request.CustomerId));
        }

        Cart cart = await cartService.GetAsync(request.CustomerId, cancellationToken);

        var cartResponse = new CartResponse
        {
            CustomerId = cart.CustomerId,
            Items = cart.Items
        };

        return cartResponse;
    }
}
