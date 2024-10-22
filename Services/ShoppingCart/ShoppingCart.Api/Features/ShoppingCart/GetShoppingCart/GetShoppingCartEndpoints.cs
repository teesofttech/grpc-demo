namespace ShoppingCart.Api.Features.ShoppingCart.GetShoppingCart;

public sealed record GetShoppingCartResponse(string UserName, List<GetShoppingCartItem> Items);

public sealed record GetShoppingCartItem(string ProductCode, int Quantity, decimal Price);

public sealed class GetShoppingCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/shopping-cart/{userName}", async (string userName, ISender sender) =>
        {
            var query = new GetShoppingCartQuery(userName);

            var result = await sender.Send(query);

            var response = result.Adapt<GetShoppingCartResponse>();

            return Results.Ok(response);
        })
        .WithName("GetShoppingCart")
        .Produces<GetShoppingCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Shopping Cart")
        .WithDescription("Get Shopping Cart");
    }
}
