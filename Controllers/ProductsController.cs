using CareerLinkServer.DTOs;
using CareerLinkServer.models.Products;
using CareerLinkServer.services;
using Microsoft.AspNetCore.Mvc;

namespace CareerLinkServer.Controllers;

/**
 * Note that , the list DataSet<T>  from the  database can be converted to an array using .ToArray() method ,
 * or to a  list using the ToList() method.
 * public WeatherForecastController(ILogger<ProductsController> logger)
 */

public class ProductsController : ApiController
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost(Name = "AddProduct")]
    public  Product AddProduct(ProductDto productDto)
    {
        Console.WriteLine(productDto.ToString());

        var product = new Product()
        {
            CategoryId = productDto.CategoryId,
            DateCreated = new DateOnly(),
            ProductId = new Guid(),
            ProductCategory = null
        };
        
        Console.WriteLine(product.ToString());
        return _productService.SaveProduct(product); }

    [HttpGet(Name = "GetProducts")]
    public IEnumerable<string> GetProducts()
    {
        return new List<string>() { "apple", "maize"};
        

    }
    
    [HttpPost("getProduct/{productId:guid}", Name = "GetProduct")]
    public IActionResult GetProduct([FromRoute] Guid productId)
    {
        var result = _productService.GetProduct(productId);
        
        return result.Match(
            onValue:product => Ok(result.Value), 
            onError:AppProblem
        );

    }
    
    
}
