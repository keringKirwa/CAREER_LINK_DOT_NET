namespace CareerLinkServer.models.Products;

public class Category
{
 public Guid CategoryId { get; set; }
 public List<Product> Products { get; set; } = new ();


}