namespace ShoppingCart.Api.Entities;

public sealed class CartItem
{
    public string ProductCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
