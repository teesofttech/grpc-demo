namespace ShoppingCart.Api.Features.ShoppingCart.GetShoppingCart;

public sealed record DeleteShoppingCartCommand(string UserName)
    : IRequest<DeleteShoppingCartResult>;
public sealed record DeleteShoppingCartResult(bool IsSuccess);

public sealed class DeleteShoppingCartCommandValidator
    : AbstractValidator<DeleteShoppingCartCommand>
{
    public DeleteShoppingCartCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is requred!");
    }
}
public sealed class DeleteShoppingCartCommandHandler(IShoppingCartRepository repository)
    : IRequestHandler<DeleteShoppingCartCommand, DeleteShoppingCartResult>
{
    public async Task<DeleteShoppingCartResult> Handle(DeleteShoppingCartCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(command.UserName, cancellationToken);
        return new DeleteShoppingCartResult(true);
    }
}
