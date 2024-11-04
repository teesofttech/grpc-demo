namespace ShoppingCart.Api.Data;

public interface IShoppingCartRepository
{
    Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);
    Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default);
    Task<Cart> GetAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string userName, CancellationToken cancellationToken = default);

}
