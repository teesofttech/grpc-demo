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
            request.Coupon.ProdcutCode);

        return coupon.ToModel();
    }

    public override async Task<CouponModel> UpdateDiscount(
        UpdateDiscountRequest request,
        ServerCallContext context)
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
                    StatusCode.NotFound,
                    $"Coupon is not found for Product Code: {request.Coupon.ProdcutCode}")
                );
        }

        coupon.Description = request.Coupon.Desciption;
        coupon.DiscountPercentage = request.Coupon.DiscountPercentage;

        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Coupon is updated by Product Code: {productCode}",
            request.Coupon.ProdcutCode);

        return coupon.ToModel();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context)
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
                    StatusCode.NotFound,
                    $"Coupon is not found for Product Code: {request.Coupon.ProdcutCode}")
                );
        }


        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Coupon is deleted by Product Code: {productCode}",
            request.Coupon.ProdcutCode);

        return new DeleteDiscountResponse { Succes = true };
    }
}
