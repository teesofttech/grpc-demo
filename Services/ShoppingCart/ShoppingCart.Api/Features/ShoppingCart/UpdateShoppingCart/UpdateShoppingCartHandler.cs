using Discount.Grpc;

namespace ShoppingCart.Api.Features.ShoppingCart.UpdateShoppingCart;

public sealed record UpdateShoppingCartCommand(string UserName, List<CartItem> Items) 
    : IRequest<UpdateShoppingCartResult>;

public sealed record UpdateShoppingCartResult(string UserName);

public sealed class UpdateShoppingCartCommandValidator: AbstractValidator<UpdateShoppingCartCommand>
{
    public UpdateShoppingCartCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required!");
    }
}

public sealed record UpdateShoppingCartCommandHandler(
    IShoppingCartRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountService)
    : IRequestHandler<UpdateShoppingCartCommand, UpdateShoppingCartResult>
{
    public async Task<UpdateShoppingCartResult> Handle(UpdateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Adapt<Cart>();

        await DeductDiscount(cart, cancellationToken);

        await repository.UpdateAsync(cart, cancellationToken);

        return new UpdateShoppingCartResult(cart.UserName);
    }

    private async Task DeductDiscount(Cart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountService.GetDiscountAsync(
                new GetDiscountRequest
                {
                    ProductCode = item.ProductCode
                },
                cancellationToken: cancellationToken);

            if (coupon is not null)
            {
                item.Price -= (item.Price * coupon.DiscountPercentage) / 100;
            }
        }
    }
}