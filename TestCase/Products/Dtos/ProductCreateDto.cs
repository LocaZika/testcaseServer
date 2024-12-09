using System.ComponentModel.DataAnnotations;
using TestCase.Products.Enums;

namespace TestCase.Products.Dtos;

public class ProductCreateDto
{
  [Required(AllowEmptyStrings = false, ErrorMessage = "Product name must not be empty")]
  public required string Name { get; set; }

  [Required(AllowEmptyStrings = false, ErrorMessage = "Product name must not be empty")]
  public required string Brand { get; set; }

  [Required(AllowEmptyStrings = false, ErrorMessage = "Product name must not be empty")]
  public required string TradeType { get; set; } 
}
