namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(
        GetDiscountRequest request,
        ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(d => d.ProductCode == request.ProductCode);

        if (coupon is null)
        {
            logger.LogInformation(
            "Coupon is not found for Product Code: {productCode}",
            request.ProductCode);

            return new CouponModel
            {
                Desciption = "No Discount",
                ProductCode = "No Product",
                DiscountPercentage = 0
            };
        }

        logger.LogInformation(
            "Coupon is found for Product Code: {productCode}",
            request.ProductCode);

        return coupon.ToModel();
    }

    public override async Task<CouponModel> CreateDiscount(
        CreateDiscountRequest request,
        ServerCallContext context)
    {
        var coupon = request.Coupon.ToEntity();

        dbContext.Coupons.Add(coupon);

        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Coupon is created for Product Code: {productCode}",
            request.Coupon.ProductCode);

        return coupon.ToModel();
    }

    public override async Task<CouponModel> UpdateDiscount(
        UpdateDiscountRequest request,
        ServerCallContext context)
    {
        var coupon = await dbContext
           .Coupons
           .FirstOrDefaultAsync(d => d.ProductCode == request.Coupon.ProductCode);

        if (coupon is null)
        {
            logger.LogInformation(
            "Coupon is not found for Product Code: {productCode}",
            request.Coupon.ProductCode);

            throw new RpcException(
                new Status(
                    StatusCode.NotFound,
                    $"Coupon is not found for Product Code: {request.Coupon.ProductCode}")
                );
        }

        coupon.Description = request.Coupon.Desciption;
        coupon.DiscountPercentage = request.Coupon.DiscountPercentage;

        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Coupon is updated by Product Code: {productCode}",
            request.Coupon.ProductCode);

        return coupon.ToModel();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var coupon = await dbContext
           .Coupons           
           .FirstOrDefaultAsync(d => d.ProductCode == request.Coupon.ProductCode);

        if (coupon is null)
        {
            logger.LogInformation(
            "Coupon is not found for Product Code: {productCode}",
            request.Coupon.ProductCode);

            throw new RpcException(
                new Status(
                    StatusCode.NotFound,
                    $"Coupon is not found for Product Code: {request.Coupon.ProductCode}")
                );
        }


        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Coupon is deleted by Product Code: {productCode}",
            request.Coupon.ProductCode);

        return new DeleteDiscountResponse { Succes = true };
    }
}
