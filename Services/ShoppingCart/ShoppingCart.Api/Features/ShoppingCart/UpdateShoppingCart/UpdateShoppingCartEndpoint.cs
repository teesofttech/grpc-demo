namespace ShoppingCart.Api.Features.ShoppingCart.UpdateShoppingCart;

public sealed record UpdateShoppingCartRequest(string UserName, List<UpdateShoppingCartItem> Items);
public sealed record UpdateShoppingCartItem(string ProductCode, int Quantity, decimal Price);
public sealed record UpdateShoppingCartResponse(string UserName);

public sealed class UpdateShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/shopping-cart", async (UpdateShoppingCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateShoppingCartCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateShoppingCartResponse>();

            return Results.Created($"/api/shopping-cart/{response.UserName}",
                response);
        })
        .WithName("UpdateShoppingCart")
        .Produces<UpdateShoppingCartResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Shopping Cart")
        .WithDescription("Update Shopping Cart");
    }
}
