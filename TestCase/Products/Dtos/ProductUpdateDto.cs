using TestCase.Products.Enums;

namespace TestCase.Products.Dtos;

public class ProductUpdateDto
{
  public required short Id { get; set; }
  public string? Name { get; set; }
  public string? Brand { get; set; }
  public ETradeType? TradeType { get; set; }
}
