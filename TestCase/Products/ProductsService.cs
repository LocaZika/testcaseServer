using Microsoft.EntityFrameworkCore;
using TestCase.Database;
using TestCase.Global;
using TestCase.Products.Dtos;
using TestCase.Products.Enums;
using TestCase.Utils;

namespace TestCase.Products;

public class ProductsService
{
  private readonly AppDbContext _context;
  public ProductsService(AppDbContext context){
    _context = context;
  }
  public async Task<List<Product>> FindAll(){
    List<Product> products = await _context.Products.ToListAsync();
    return products;
  }
  public async Task<Product?> FindById(short Id){
    Product? product = await _context.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
    return product;
  }
  public async Task<ServiceStatus> Create(ProductCreateDto ProductDto){
    int ExistedProduct = await _context.Products.Where(p => p.Name == ProductDto.Name).CountAsync();
    if (ExistedProduct > 0){
      return ServiceStatus.Existed;
    }
    if (DtoHelper.CompareEnumType<ETradeType>(ProductDto.TradeType)){
      return ServiceStatus.InvalidValue;
    }
    Product? product = new(){
      Name = ProductDto.Name,
      Brand = ProductDto.Brand,
      TradeType = ProductDto.TradeType,
    };
    await _context.Products.AddAsync(product);
    await _context.SaveChangesAsync();
    return ServiceStatus.Successful;
  }
  public async Task<ServiceStatus> Update(ProductUpdateDto ProductDto){
    Product? ExistedProduct = await _context.Products.Where(p => p.Id == ProductDto.Id).FirstOrDefaultAsync();
    if (ExistedProduct == null){
      return ServiceStatus.NotFound;
    }
    DtoHelper.MappingDtoToEntity<ProductUpdateDto, Product>(ProductDto, ExistedProduct);
    _context.Products.Update(ExistedProduct);
    await _context.SaveChangesAsync();
    return ServiceStatus.Successful;
  }
}
