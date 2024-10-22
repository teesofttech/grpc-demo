namespace Basket.Api.Entities;

public sealed class ShoppingCartItem
{
    public string ProductCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
