using CareerLinkServer.DTOs;
using CareerLinkServer.models.Products;
using CareerLinkServer.services;
using Microsoft.AspNetCore.Mvc;

namespace CareerLinkServer.Controllers;

/**
 * Note that , the list DataSet<T>  from the  database can be converted to an array using .ToArray() method ,
 * or to a  list using the ToList() method. 
 */

public class ProductsController : ApiController
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost(Name = "AddProduct")]
    public  async Task<IActionResult> AddProduct(ProductDto productDto)
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
        var result = await  _productService.SaveProduct(product);
        return Ok(result.Value);
    }

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
    [HttpPost("deleteProduct/{productId:guid}", Name = "DeleteProduct")]
    public IActionResult DeleteProduct([FromRoute] Guid productId)
    {
        var result = _productService.DeleteProduct(productId);
        
        return result.Match(
            onValue:product => Ok(result.Value), 
            onError:AppProblem
        );

    }
    
    
}
