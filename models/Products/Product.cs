using CareerLinkServer.models.Products;

namespace CareerLinkServer.models.Products;

public class Product
{
    public Guid ProductId { get; set; }
    
    private string ProductName { get; set; }
    private string Price { get; set; }
    
    public DateOnly DateCreated { get; set; }
    public Category ProductCategory { get; set; }
    public Guid CategoryId { get; set; }
}