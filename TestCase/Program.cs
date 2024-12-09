using Microsoft.EntityFrameworkCore;
using TestCase.Database;
using TestCase.Global;
using TestCase.Products;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ProductsService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(o =>
  o.UseNpgsql(config.GetConnectionString("Default"), o => {
    o.MigrationsHistoryTable("_EfMigrations", "TestCaseSchema");
    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
  })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI(o =>
  {
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "TestCase v1");
    o.RoutePrefix = string.Empty;
  });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RoutePrefixMiddleware>("/api");

app.UsePathBase(new PathString("/api"));

app.Run();
