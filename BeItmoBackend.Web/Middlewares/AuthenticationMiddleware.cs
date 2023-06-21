using System.IdentityModel.Tokens.Jwt;
using BeItmoBackend.Core.Exceptions;

namespace BeItmoBackend.Web.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var token = httpContext.Request.Headers.Authorization.ToString();

        if (token.Length == 0)
        {
            throw new NotAuthorizedException("You are not authorized!");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        var isuNumber = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "isu") ??
                        throw new NotAuthorizedException("You are not authorized!");

        httpContext.Items["isuNumber"] = int.Parse(isuNumber.Value);

        await _next(httpContext);
    }
}