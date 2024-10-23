namespace ShoppingCart.Api.Data;

public sealed class ShoppingCartRepository(IDocumentSession session)
    : IShoppingCartRepository
{
    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);

        return cart;
    }

    public async Task<bool> DeleteAsync(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<Cart>(userName);
        await session.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Cart> GetAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<Cart>(userName, cancellationToken);

        return cart is null ? throw new CartNotFoundException("") : cart;
    }
}
