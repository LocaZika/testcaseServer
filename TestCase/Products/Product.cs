using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestCase.Products.Enums;

namespace TestCase.Products;

public class Product
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public short Id { get; set; }

  [Column(TypeName = "varchar(10)")]
  public required string Name { get; set; }

  [Column(TypeName = "varchar(10)")]
  public required string Brand { get; set; }

  [Column(TypeName = "varchar(4)")]
  public required string TradeType { get; set; }

  [Column(TypeName = "timestamptz")]
  public DateTime Created_at { get; set; }

  [Column(TypeName = "timestamptz")]
  public DateTime Updated_at { get; set; }
}
