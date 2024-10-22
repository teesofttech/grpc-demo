namespace ShoppingCart.Api.Features.ShoppingCart.GetShoppingCart;

public sealed record GetShoppingCartQuery(string UserName)
    : IRequest<GetShoppingCartResult>;
public sealed record GetShoppingCartResult(string UserName, List<CartItem> Items);
public sealed class GetShoppingCartQueryHandler(IShoppingCartRepository repository)
    : IRequestHandler<GetShoppingCartQuery, GetShoppingCartResult>
{
    public async Task<GetShoppingCartResult> Handle(GetShoppingCartQuery query, CancellationToken cancellationToken)
    {
        var cart = await repository.GetAsync(query.UserName, cancellationToken);

        var result = cart.Adapt<GetShoppingCartResult>();

        return result;
    }
}
