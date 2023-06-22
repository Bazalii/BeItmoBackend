using BeItmoBackend.Core;
using BeItmoBackend.Data;
using BeItmoBackend.Web;
using BeItmoBackend.Web.Middlewares;

const string allowedOrigins = "allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddData(builder.Configuration)
    .AddCore()
    .AddWeb();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                              .WithMethods("GET", "POST", "PUT", "OPTIONS", "DELETE")
                              .AllowAnyHeader();
                      });
});

var app = builder.Build();

app.UseCors(allowedOrigins);

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();

app.MapControllers();

await app.RunAsync();