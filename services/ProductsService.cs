using CareerLinkServer.DataBaseContext;
using CareerLinkServer.models.Products;
using CareerLinkServer.ServiceErrors;
using ErrorOr;


namespace CareerLinkServer.services;

public interface IProductService
{
    public Product SaveProduct(Product product);
    public void DeleteProduct(Guid productId);
    public Category CreateAndSaveCategory();
    public ErrorOr<Product> GetProduct(Guid productId);
    

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

    public ErrorOr<Product> GetProduct(Guid productId)
    {
        var product = _dbContext.Products.FirstOrDefault(prod => prod.ProductId == productId);
        /*
         * note that the errorOr library has 2 implicit converters .One to convert an Error object into an ErorOr object ,and the other
         * from the instance to  the ErrorOr.
         * Again the var prop at the same time has got an implicit converter , unlike the const property.
         * */
        
        if (product is null)
        {
            return DomainErrors.Products.NotFound; 
        }

        return product;

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
    
    public ErrorOr<Category> GetCategory(Guid categoryId)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

        if (category == null)
        {
            return new ErrorOr<Category>.Error("Not found");
        }

        return ErrorOr<Category>.Result(category);
    }

}