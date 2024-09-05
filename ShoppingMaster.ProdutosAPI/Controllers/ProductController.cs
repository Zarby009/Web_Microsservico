using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShoppingMaster.ProdutosAPI.DTO;
using ShoppingMaster.ProdutosAPI.Services.ProductRepository;

namespace ShoppingMaster.ProdutosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface _product;
        public ProductController(IProductInterface product)
        {
            _product = product;
        }

        [HttpGet("GetAll")]

        public async Task<ActionResult<List<ProductDTO>>> FindAll()
        {


            var prod = await _product.FindAllAsync();
            return Ok(prod);


        }
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO prodDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(); };


            if (prodDTO == null) return BadRequest();
            var prod = await _product.Create(prodDTO);
            return Ok(prod);


        }
        [HttpGet("DeletarPorId/{id:int}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            var prod = await _product.Delete(id);
            return Ok(prod);


        }
        [HttpGet("BuscarPorId/{id:int}")]
        public async Task<ActionResult<ProductDTO>> Buscar(long id)
        {
            var prod = await _product.FindByIdAsync(id);
            if (prod == null) return BadRequest("Não Encontrado");

            return Ok(prod);


        }
        [HttpPut("Atualizar")]
        public async Task<ActionResult<ProductDTO>> Update(ProductDTO productDTO)
        {
            if(productDTO == null) return BadRequest("Atualizacoes nao feitas");
            var prod = await _product.Update(productDTO);
            return Ok(prod);


        }

    }
}
