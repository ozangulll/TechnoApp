using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace ShopApp.WebUI.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder CustomStaticFiles(this IApplicationBuilder app){
            var path=Path.Combine(Directory.GetCurrentDirectory(),"node_modules");
            var options= new StaticFileOptions{
                FileProvider=new PhysicalFileProvider(path),
                RequestPath="/modules"
            };
            app.UseStaticFiles(options);
            return app;
        }
    }
}