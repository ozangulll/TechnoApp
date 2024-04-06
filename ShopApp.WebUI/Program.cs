using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApp.Business.Abstract;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.Memory;

var builder = WebApplication.CreateBuilder(args);

// Services configuration
builder.Services.AddScoped<IProductDAL, MemoryProductDAL>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddMvc();

var app = builder.Build();
app.MapDefaultControllerRoute();
// HTTP request pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.MapGet("/", () => "Hello World!");

app.Run();
