using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Errors;

public static class ProductErrors
{
    public static NotFound NotFound(Guid id)
        => new("Product", "Product with id " + id + " not found");

    public static Conflict AlreadyExist(string name)
        => new("Product", "Product with name " + name + " already exist");

    internal static Conflict PriceCanNotBeLessZero(decimal price)
        => new("Product", "Price can not be less than zero (current price is " + price + ")");

    internal static Conflict ProductContainsInStocks(Guid id)
        => new("Product", "Product with id " + id + " contains in stocks");
}
