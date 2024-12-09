using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestCase.Products;
using TestCase.Products.Enums;

namespace TestCase.Database;

public class AppDbContext: DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
  public DbSet<Product> Products { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    var datetimeConverter = new ValueConverter<DateTime, DateTime>(v =>
      v.ToUniversalTime(), v =>
        DateTime.SpecifyKind(v, DateTimeKind.Utc));
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      foreach (var property in entityType.GetProperties())
      {
        if (property.ClrType == typeof(DateTime))
        {
          property.SetValueConverter(datetimeConverter);
        }
      }
    }
    modelBuilder.Entity<Product>()
      .ToTable("cars", "products")
      .Property(p => p.TradeType)
      .HasConversion<string>();
  }
}
