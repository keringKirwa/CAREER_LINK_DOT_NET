using CareerLinkServer.DataBaseContext;
using CareerLinkServer.models.Products;


namespace CareerLinkServer.services;

public interface IProductService
{
    public Product SaveProduct(Product product);
    public void DeleteProduct(Guid productId);
    public Category CreateAndSaveCategory();

}

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public Product SaveProduct(Product product)
    {
        
        var category = CreateAndSaveCategory();
        product.ProductCategory = category;
        
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return product;
    }

    public void DeleteProduct(Guid productId)
    {
        var product = _dbContext.Products.Find(productId);
        if (product != null)
        { 
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            
        }
    }
    
    public Category CreateAndSaveCategory()
    {
        try
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
              
            
            };
            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();
            Console.WriteLine("++++++++++>>>>>> ++++++ saved successfully to the database" +  newCategory);
            return newCategory;

        }
        catch (Exception e)
        {
            Console.WriteLine("++++++++++>>>>>> ++++++ Exception HERE ::: " +  e);
            throw;
        }
        
        
    }

}