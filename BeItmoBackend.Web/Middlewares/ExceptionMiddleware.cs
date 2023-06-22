using BeItmoBackend.Core.Exceptions;

namespace BeItmoBackend.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ObjectNotFoundException objectNotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(new { Error = $"{objectNotFoundException.Message}" });
        }
        catch (NotAuthorizedException notAuthorizedException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsJsonAsync(new { Error = $"{notAuthorizedException.Message}" });
        }
        catch (ServiceUnavailableException serviceNotAvailableException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await httpContext.Response.WriteAsJsonAsync(new { Error = $"{serviceNotAvailableException.Message}" });
        }
        catch (Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { Error = $"{exception.Message}" });
        }
    }
}