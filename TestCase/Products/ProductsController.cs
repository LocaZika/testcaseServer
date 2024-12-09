using Microsoft.AspNetCore.Mvc;
using TestCase.Global;
using TestCase.Products.Dtos;
using TestCase.Products.Enums;

namespace TestCase.Products
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ProductsService _services;
    public ProductsController(ProductsService services){
      _services = services;
    }
    [HttpGet]
    public async Task<IActionResult> FindAll(){
      try{
        var products = await _services.FindAll();
        return Ok(products);
      } catch {
        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
      }
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> FindById(short Id){
      try {
        var product = await _services.FindById(Id);
        return Ok(product);
      } catch {
        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto productDto){
      try {
        var serviceStatus = await _services.Create(productDto);
        if (serviceStatus == ServiceStatus.Existed){
          return Conflict("Product name already exists");
        }
        if (serviceStatus == ServiceStatus.InvalidValue){
          return BadRequest($"Product trade type must be one of {Enum.GetNames<ETradeType>()}");
        }
        return Created();
      } catch {
        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
      }
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] ProductUpdateDto productDto){
      try
      {
        var serviceStatus = await _services.Update(productDto);
        if (serviceStatus == ServiceStatus.NotFound) return NotFound("Product was not found");
        return Ok(ServiceStatus.Successful);
      }
      catch
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
      }
    }
  }
}
