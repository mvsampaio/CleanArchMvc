using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
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

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null) 
            {
                return BadRequest("Data invalid");
            }

            await _productService.Add(productDTO);

            return new CreatedAtRouteResult("GetProduct",
                new { id = productDTO.Id },
                productDTO);
        }
        #endregion

        #region PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if ( (id != productDTO.Id) || (productDTO == null) )
            {
                return BadRequest("Data invalid");
            }

            await _productService.Update(productDTO);
            return Ok(productDTO);
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {            
            var productDTO = await _productService.GetById(id);
            if (productDTO == null)
            {
                return NotFound("Product not found");
            }

            await _productService.Remove(id);
            return Ok(productDTO);
        }
        #endregion

    }
}
