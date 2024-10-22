namespace Basket.Api.Data;

public interface IShoppingCartRepository
{
    Task<ShoppingCart> CreateAsync(ShoppingCart cart, CancellationToken cancellationToken = default);
    Task<ShoppingCart> GetAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string userName, CancellationToken cancellationToken = default);
}
