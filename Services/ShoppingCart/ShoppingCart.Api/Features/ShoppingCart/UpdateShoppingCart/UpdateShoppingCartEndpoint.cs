namespace ShoppingCart.Api.Features.ShoppingCart.UpdateShoppingCart;

public sealed record UpdateShoppingCartRequest(string UserName, List<UpdateShoppingCartItem> Items);
public sealed record UpdateShoppingCartItem(string ProductCode, int Quantity, decimal Price);
public sealed record UpdateShoppingCartResponse(string UserName);

public sealed class UpdateShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/shopping-cart/{userName}", async (UpdateShoppingCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateShoppingCartCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateShoppingCartResponse>();

            return Results.Ok();
        })
        .WithName("UpdateShoppingCart")
        .Produces<UpdateShoppingCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Shopping Cart")
        .WithDescription("Update Shopping Cart");
    }
}
