namespace ShoppingCart.Api.Exceptions;

public sealed class CartNotFoundException(string userName)
    : Exception(userName)
{
}
