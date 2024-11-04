namespace ShoppingCart.Api.Data;

public sealed class CachedShoppingCartRepository(
    IShoppingCartRepository repository,
    IDistributedCache cache) : IShoppingCartRepository
{
    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await repository.CreateAsync(cart, cancellationToken);

        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));

        return cart;
    }
    public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await repository.UpdateAsync(cart, cancellationToken);

        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));

        return cart;
    }

    public async Task<bool> DeleteAsync(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync(userName, cancellationToken);

        await cache.RemoveAsync(userName);

        return true;
    }

    public async Task<Cart> GetAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cachedCart = await cache.GetStringAsync(userName, cancellationToken);

        if (!string.IsNullOrEmpty(cachedCart))
        {
            return JsonSerializer.Deserialize<Cart>(cachedCart)!;
        }

        var cart = await repository.GetAsync(userName, cancellationToken);

        await cache.SetStringAsync(userName, JsonSerializer.Serialize(cart));

        return cart;
    }

}
