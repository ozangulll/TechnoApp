using ShopApp.Business.Abstract;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.DataAccess.Concrete.EFCore;
using ShopApp.WebUI.Middlewares;
var builder = WebApplication.CreateBuilder(args);

// Services configuration
builder.Services.AddScoped<IProductDAL, EFCoreProductDAL>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryDAL, EFCoreCategoryDAL>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddControllersWithViews(); // Changed from AddMvc()

var app = builder.Build();

// HTTP request pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    SeedDatabase.Seed();
}

app.UseStaticFiles();
app.CustomStaticFiles();

app.UseRouting();

 app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/products",
        defaults: new { controller = "Admin", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "adminEditProduct",
        pattern: "admin/products/{id?}",
        defaults: new { controller = "Admin", action = "Edit" }
    );

    endpoints.MapControllerRoute(
        name: "products",
        pattern: "products/{category?}",
        defaults: new { controller = "Shop", action = "List" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});


app.Run();

