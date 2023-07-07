using CareerLinkServer.models.Products;
using CareerLinkServer.records;
using CareerLinkServer.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerLinkServer.Controllers;

/**
 * Note that , the list DataSet<T>  from the  database can be converted to an array using .ToArray() method ,
 * or to a  list using the ToList() method.
 * public WeatherForecastController(ILogger<ProductsController> logger)
 */

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [Authorize]
    [HttpPost(Name = "AddProduct")]
    public  Product AddProduct(RProduct rProduct)
    {
        Console.WriteLine(rProduct.ToString());
        var category = _productService.CreateAndSaveCategory();
        Console.WriteLine(category);

        var product = new Product()
        {
            CategoryId = rProduct.categoryId,
            DateCreated = new DateOnly(),
            ProductId = new Guid(),
            ProductCategory = category
        };
        Console.WriteLine(product.ToString());
        
        if (_productService is  null) {
            throw new Exception("the _productService  was null !!");
        }else
        {
            return _productService.SaveProduct(product);

        }


        


    }

    [HttpGet(Name = "GetProducts")]
    public IEnumerable<string> GetProducts()
    {
        return new List<string>() { "apple", "maize"};
        

    }
    
}
