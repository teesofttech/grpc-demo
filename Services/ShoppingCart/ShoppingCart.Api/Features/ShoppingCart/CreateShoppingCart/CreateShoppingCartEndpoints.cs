namespace ShoppingCart.Api.Features.ShoppingCart.CreateShoppingCart;
public sealed record CreateBasketRequest(
    );
public sealed record CreateBasketResponse(Guid Id);


public sealed class CreateShoppingCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/basket", async () =>
        {

        })
        .WithName("CreateBasket")
        .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Basket")
        .WithDescription("Create Basket");
    }
}
