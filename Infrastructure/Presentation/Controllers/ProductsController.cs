using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")] // BaseUrl/api/Products
public class ProductsController(IServiceManager _serviceManager) : ControllerBase
{
    // Get All Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        => Ok(await _serviceManager.ProductService.GetAllProductsAsync());

    // Get Product By Id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
        => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));

    // Get All Types
    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        => Ok(await _serviceManager.ProductService.GetAllTypesAsync());

    // Get All Brands
    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());
}