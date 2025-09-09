using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relier.Application.DTOs;
using Relier.Application.Interfaces;

namespace Relier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Recumpera todos os Produtos cadastrados
        /// </summary>
        /// <returns>Todos produtos cadastrado,caso contrario notfound</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetAll();
            if (products is null)
                return NotFound("Produtos não encontrado");
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetById(id);
            if (product is null)
                return NotFound("Produto não encontrado");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO product)
        {
            if (product is null)
                return BadRequest("Dados invalidos");

            await _productService.Add(product);

            return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO product)
        {
            if (id != product.Id)
                return BadRequest();

            if (product is null)
                return BadRequest();
            await _productService.Update(product);

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var response = await _productService.GetById(id);
            if (response is null || response.Id == 0)
                return NotFound("Produto não encontrado");

            await _productService.Delete(id);

            return Ok(response);
        }

    }
}
