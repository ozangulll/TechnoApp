
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using SendGrid.Extensions.DependencyInjection;
using ShopApp.Business.Abstract;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.DataAccess.Concrete.EFCore;
using ShopApp.WebUI.Identity;

using ShopApp.WebUI.Middlewares;
using ShopApp.WebUI.Services;
using ShopApp.WebUI.SettingsForMail;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("IdentityConnection");
builder.Services.AddDbContext<AppIdentityDbContext>(options=>options.UseSqlServer(connectionString));
// Services configuration
builder.Services.Configure<Settings>(builder.Configuration.GetSection("SendGridSettings"));
builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options=>{
   options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
});

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                     HttpOnly = true,
                     Name=".ShopApp.Security.Cookie",
                     SameSite=SameSiteMode.Strict
                };

            });
builder.Services.AddScoped<IProductDAL, EFCoreProductDAL>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryDAL, EFCoreCategoryDAL>();
builder.Services.AddScoped<ICartDAL,EFCoreCartDAL>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IEmailSender,EmailSenderService>();
builder.Services.AddScoped<ICartService,CartManager>();
builder.Services.AddControllersWithViews(); // Changed from AddMvc()
builder.Services.AddSendGrid(options => {
    options.ApiKey = builder.Configuration.GetSection("SendGridSettings")
    .GetValue<string>("ApiKey");
});
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
app.UseAuthentication();
app.UseAuthorization();

 app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/products",
        defaults: new { controller = "Admin", action = "ProductList" }
    );

    endpoints.MapControllerRoute(
        name: "adminEditProduct",
        pattern: "admin/products/{id?}",
        defaults: new { controller = "Admin", action = "EditProduct" }
    );

    endpoints.MapControllerRoute(
        name: "products",
        pattern: "products/{category?}",
        defaults: new { controller = "Shop", action = "List" }
    );
     endpoints.MapControllerRoute(
        name: "cart",
        pattern: "cart",
        defaults: new { controller = "Cart", action = "Index" }
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var configuration1 = services.GetRequiredService<IConfiguration>(); // Add this for accessing configuration

    SeedIdentity.Seed(userManager, roleManager, configuration1).Wait(); 
}

app.Run();

