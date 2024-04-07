using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApp.Business.Abstract;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EFCore;
using ShopApp.DataAccess.Concrete.Memory;
using ShopApp.WebUI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Services configuration
builder.Services.AddScoped<IProductDAL, EFCoreProductDAL>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddMvc();

var app = builder.Build();

// HTTP request pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.CustomStaticFiles();
app.MapDefaultControllerRoute();
//app.MapGet("/", () => "Hello World!");

app.Run();
