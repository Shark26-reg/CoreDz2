namespace Application.Products.Cache;

public static class ProductKeys
{
    public static string All() => "ProductKeys.All";
    public static string Id(Guid id) => $"Product.{id}";
}
