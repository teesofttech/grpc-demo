using Discount.Grpc.Entities;

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
                    ProductCode = "BOOK0001",
                    Description = "Book 1 discount",
                    DiscountPercentage = 10
                },
                new Coupon
                {
                    Id = 2,
                    ProductCode = "BOOK0002",
                    Description = "Book 2 discount",
                    DiscountPercentage = 5
                },
                new Coupon
                {
                    Id = 3,
                    ProductCode = "BOOK0003",
                    Description = "Book 3 discount",
                    DiscountPercentage = 10
                }
                );
        });
    }
}
