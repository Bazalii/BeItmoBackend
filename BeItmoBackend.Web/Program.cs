using BeItmoBackend.Core;
using BeItmoBackend.Data;
using BeItmoBackend.Web;
using BeItmoBackend.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddData(builder.Configuration)
    .AddCore()
    .AddWeb();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();