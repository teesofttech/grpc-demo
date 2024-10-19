namespace Basket.Api.Features.Basket.CreateBasket;
public sealed record CreateBasketRequest(
    );
public sealed record CreateBasketResponse(Guid Id);


public sealed class CreateBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async () =>
        {

        })
        .WithName("CreateBasket")
        .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Basket")
        .WithDescription("Create Basket");
    }
}
