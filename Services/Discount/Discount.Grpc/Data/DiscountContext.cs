namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options)
    : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>(e =>
        {
            e.HasData(
                new Coupon
                {
                    Id = 1,
                    Code = "WELCOME10",
                    ProductCode = "",
                    Description = "10% off your first purchase",
                    DiscountPercentage = 10
                },
                new Coupon
                {
                    Id = 2,
                    Code = "SAVE5",
                    ProductCode = "",
                    Description = "$5 off any purchase",
                    DiscountPercentage = 5
                },
                new Coupon
                {
                    Id = 3,
                    Code = "HOLIDAY10",
                    ProductCode = "BOOK001",
                    Description = "10% off during the holiday season",
                    DiscountPercentage = 10
                }
                );
        });
    }
}
