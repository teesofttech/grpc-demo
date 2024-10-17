namespace Discount.Grpc.Models;

public class Coupon
{
    public int Id { get; set; }
    public string? ProductCode { get; set; } 
    public string Description { get; set; }
    public int DiscountPercentage { get; set; }        
}
