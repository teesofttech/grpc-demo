namespace Discount.Grpc.Extensions;

public static class CouponMappingExtensions
{
    public static Coupon ToEntity(this CouponModel couponModel)
    {
        return new Coupon
        {
            Id = couponModel.Id,
            ProductCode = couponModel.ProdcutCode,
            Description = couponModel.Desciption,
            DiscountPercentage = couponModel.DiscountPercentage
        };    
    }

    public static CouponModel ToModel(this Coupon coupon)
    {
        return new CouponModel
        {
            Id = coupon.Id,
            Desciption = coupon.Description,
            ProdcutCode = coupon.ProductCode,
            DiscountPercentage = coupon.DiscountPercentage
        };
    }
}