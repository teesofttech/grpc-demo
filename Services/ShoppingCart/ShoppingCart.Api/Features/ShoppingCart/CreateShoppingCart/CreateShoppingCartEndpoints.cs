namespace ShoppingCart.Api.Features.ShoppingCart.CreateShoppingCart;
public sealed record CreateShoppingCartRequest(string UserName, List<CreateShoppingCartItem> Items);
public sealed record CreateShoppingCartItem(string ProductCode, int Quantity, decimal Price);
public sealed record CreateShoppingCartResponse(string UserName);

public sealed class CreateShoppingCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/shopping-cart", async (CreateShoppingCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateShoppingCartCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateShoppingCartResponse>();

            return Results.Created($"/api/shopping-cart/{response.UserName}", response);
        })
        .WithName("CreateShoppingCart")
        .Produces<CreateShoppingCartResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Shopping Cart")
        .WithDescription("Create Shopping Cart");
    }
}
