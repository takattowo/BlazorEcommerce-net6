global using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var product = await _productService.GetProductsAsync();

            return Ok(product);
        }

        [HttpGet("category/{stringUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string stringUrl)
        {
            var product = await _productService.GetProductsByCategoryAsync(stringUrl);

            return Ok(product);
        }

        [HttpGet("search/{text}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts(string text, int page)
        {
            var product = await _productService.SearchProducts(text, page);

            return Ok(product);
        }

        [HttpGet("searchsussgesstion/{text}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> SearchProductSuggestions(string text)
        {
            var product = await _productService.SearchProductSearchSuggestions(text);

            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);

            return Ok(product);
        }
    }
}
