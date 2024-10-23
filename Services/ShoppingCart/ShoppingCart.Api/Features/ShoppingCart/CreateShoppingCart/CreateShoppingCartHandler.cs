using Discount.Grpc;
using System.Security.Cryptography;

namespace ShoppingCart.Api.Features.ShoppingCart.CreateShoppingCart;

public sealed record CreateShoppingCartCommand(string UserName, List<CartItem> Items)
    : IRequest<CreateShoppingCartResult>;
public sealed record CreateShoppingCartResult(string UserName);

public sealed class CreateShoppingCartCommandValidator
    : AbstractValidator<CreateShoppingCartCommand>
{
    public CreateShoppingCartCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is requred!");
    }
}

public sealed class CreateShoppingCartCommandHandler(
    IShoppingCartRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountService
  ) : IRequestHandler<CreateShoppingCartCommand, CreateShoppingCartResult>
{
    public async Task<CreateShoppingCartResult> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Adapt<Cart>();

        await DedcutDiscount(cart, cancellationToken);

        await repository.CreateAsync(cart, cancellationToken);

        return new CreateShoppingCartResult(cart.UserName);
    }

    private async Task DedcutDiscount(Cart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountService.GetDiscountAsync(
                new GetDiscountRequest
                {
                    ProdcutCode = item.ProductCode
                },
                 cancellationToken: cancellationToken);

            if (coupon is not null)
            {
                item.Price -= (item.Price * coupon.DiscountPercentage) / 100;
            }
        }
    }
}
