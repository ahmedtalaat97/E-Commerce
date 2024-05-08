using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Interfaces.Services;
using E_Commerce_Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        [Cache(7)]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecificationParameters specParameter)
        {
            var products = await _productService.GetAllProductsAsync(specParameter);
            return  Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
         
            var product = await _productService.GetProductAsync(id);
            return product is not null? Ok():NotFound(new ApiResponse(404,$"product with Id {id} not found"));
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetBrands() => Ok(await _productService.GetAllBrandsAsync());




        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetTypes() => Ok(await _productService.GetAllTypesAsync());

    }
}
