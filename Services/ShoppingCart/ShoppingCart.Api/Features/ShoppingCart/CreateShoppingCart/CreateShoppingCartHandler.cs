namespace ShoppingCart.Api.Features.ShoppingCart.CreateShoppingCart;

public sealed record CreateShoppingCartCommand(string UserName, List<CartItem> Items)
    : IRequest<CreateShoppingCartResult>;
public sealed record CreateShoppingCartResult(string UserName);

public sealed class CreateShoppingCartCommandValidator 
    : AbstractValidator<CreateShoppingCartCommand>
{
    public CreateShoppingCartCommandValidator()
    {
        RuleFor(x=>x.UserName)
            .NotEmpty()
            .WithMessage("UserName is requred!");
    }
}

public sealed class CreateShoppingCartCommandHandler(IShoppingCartRepository repository)
    : IRequestHandler<CreateShoppingCartCommand, CreateShoppingCartResult>
{
    public async Task<CreateShoppingCartResult> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Adapt<Cart>();

        await repository.CreateAsync(cart, cancellationToken);

        return new CreateShoppingCartResult(cart.UserName);
    }
}
