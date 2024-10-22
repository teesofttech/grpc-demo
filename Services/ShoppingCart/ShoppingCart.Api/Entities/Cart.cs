namespace ShoppingCart.Api.Entities;

public sealed class Cart
{
    public string UserName { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = new();
}