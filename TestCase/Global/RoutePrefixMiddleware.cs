namespace TestCase.Global;

public class RoutePrefixMiddleware
{
  private readonly RequestDelegate _next;
  private readonly string _routePrefix;
  public RoutePrefixMiddleware(RequestDelegate next, string routePrefix){
    _next = next;
    _routePrefix = routePrefix;
  }
  public async Task InvokeAsync(HttpContext httpContext){
    httpContext.Request.PathBase = new PathString(_routePrefix);
    await _next(httpContext);
  }
}
