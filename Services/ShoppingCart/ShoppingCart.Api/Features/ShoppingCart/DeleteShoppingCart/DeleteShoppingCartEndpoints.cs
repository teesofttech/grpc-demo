namespace ShoppingCart.Api.Features.ShoppingCart.GetShoppingCart;

public sealed record DeleteShoppingCartResponse(bool IsSuccess);

public sealed class DeleteShoppingCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/shopping-cart/{userName}", async (string userName, ISender sender) =>
        {
            var command = new DeleteShoppingCartCommand(userName);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteShoppingCartResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteShoppingCart")
        .Produces<DeleteShoppingCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Shopping Cart")
        .WithDescription("Delete Shopping Cart");
    }
}
