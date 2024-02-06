namespace Application.Products.Responses;

public class ProductResultResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public Guid? CategoryId { get; set; }
}
