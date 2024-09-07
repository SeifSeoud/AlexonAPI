using AlexonTask.Data;
using AlexonTask.Models;
using AlexonTask.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlexonTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _dbProduct;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository dbProduct, IMapper mapper)
        {
            _mapper=mapper;
            _dbProduct = dbProduct;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            IEnumerable<Product>productList=await _dbProduct.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(productList);
            return Ok(productDtos);
        }
        [HttpGet("{id:int}",Name ="GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if(id== 0)
            {
                return BadRequest();
            }
            var product=await _dbProduct.GetFirstOrDefaultAsync(x => x.Id == id);
            if(product== null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody]ProductCreateDto productCreateDto)
        {
            if (productCreateDto.Name == "")
            {
                return BadRequest(ModelState);
            }
            if (productCreateDto == null)
            {
                return BadRequest(ModelState);
            }
            var dbModel = _mapper.Map<Product>(productCreateDto);
            await _dbProduct.CreateAsync(dbModel);
            return CreatedAtRoute("GetProduct", new { id = dbModel.Id }, dbModel);
        }
        [HttpDelete("{id:int}",Name ="DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product=await _dbProduct.GetFirstOrDefaultAsync(x=>x.Id == id);
            if(product== null)
            {
                return NotFound();
            }
            _dbProduct.RemoveAsync(product);
            return NoContent();
        }
        [HttpPut("{id:int}",Name ="UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>UpdateProduct(int id, [FromBody]ProductUpdateDto productUpdateDto)
        {
            if(productUpdateDto == null || id != productUpdateDto.Id)
            {
                return BadRequest();
            }
            var dbModel = _mapper.Map<Product>(productUpdateDto);
            await _dbProduct.UpdateAsync(dbModel);
            return NoContent();
        }
    }
}
