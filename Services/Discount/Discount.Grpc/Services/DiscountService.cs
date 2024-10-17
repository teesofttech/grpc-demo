namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(d => d.ProductCode == request.ProdcutCode);

        if (coupon is null)
        {
            logger.LogInformation(
            "Coupon is not found for Product Code: {productCode}",
            request.ProdcutCode);

            return new CouponModel
            {
                Desciption = "No Discount",
                ProdcutCode = "No Product",
                DiscountPercentage = 0
            };
        }

        logger.LogInformation(
            "Coupon is found for Product Code: {productCode}",
            request.ProdcutCode);

        return new CouponModel
        {
            Id = coupon.Id,
            Desciption = coupon.Description,
            ProdcutCode = coupon.ProductCode,
            DiscountPercentage = coupon.DiscountPercentage
        };
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon
        {
            ProductCode = request.Coupon.ProdcutCode,
            Description = request.Coupon.Desciption,
            DiscountPercentage = request.Coupon.DiscountPercentage
        };

        dbContext.Coupons.Add(coupon);

        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Coupon is created for Product Code: {productCode}",
            request.Coupon.ProdcutCode);

        return new CouponModel
        {
            Id = coupon.Id,
            Desciption = coupon.Description,
            ProdcutCode = coupon.ProductCode,
            DiscountPercentage = coupon.DiscountPercentage
        };
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
           .Coupons
           .FirstOrDefaultAsync(d => d.ProductCode == request.Coupon.ProdcutCode);

        if (coupon is null)
        {
            logger.LogInformation(
            "Coupon is not found for Product Code: {productCode}",
            request.Coupon.ProdcutCode);

            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Invalid request")
                );
        }

        coupon.Description = request.Coupon.Desciption;
        coupon.DiscountPercentage = request.Coupon.DiscountPercentage;

        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Coupon is updated by Product Code: {productCode}",
            request.Coupon.ProdcutCode);

        return new CouponModel
        {
            Id = coupon.Id,
            Desciption = coupon.Description,
            ProdcutCode = coupon.ProductCode,
            DiscountPercentage = coupon.DiscountPercentage
        };
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
           .Coupons
           .FirstOrDefaultAsync(d => d.ProductCode == request.Coupon.ProdcutCode);

        if (coupon is null)
        {
            logger.LogInformation(
            "Coupon is not found for Product Code: {productCode}",
            request.Coupon.ProdcutCode);

            return new DeleteDiscountResponse
            {
                Succes = false
            };
        }


        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Coupon is deleted by Product Code: {productCode}",
            request.Coupon.ProdcutCode);

        return new DeleteDiscountResponse
        {
            Succes = true
        };
    }
}
