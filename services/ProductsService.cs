using CareerLinkServer.DataBaseContext;
using CareerLinkServer.models.Products;
using CareerLinkServer.ServiceErrors;
using ErrorOr;


namespace CareerLinkServer.services;

public interface IProductService
{
     Task<ErrorOr<Created>> SaveProduct(Product product);
    public ErrorOr<Deleted> DeleteProduct(Guid productId);
    public Category CreateAndSaveCategory();
    public ErrorOr<Product> GetProduct(Guid productId);
    

}
/*
 * We are going to use NoContent() statusCode 204 for successful deletion and updating , in the case when we want to update
 * a product and we realize  that its not found in the database , we create it instead.
 */

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task<ErrorOr<Created>> SaveProduct(Product product)
    {
        /*
         * We are not expecting any error here .
         */
        
        var category = CreateAndSaveCategory();
        product.ProductCategory = category;
        
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteProduct(Guid productId)
    {
        var product = _dbContext.Products.Find(productId);
        // if (product is null) return DomainErrors.Products.NotFound;
        
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();

        return Result.Deleted;
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
            return DomainErrors.Products.NotFound;
        }

        return category;
    }

}