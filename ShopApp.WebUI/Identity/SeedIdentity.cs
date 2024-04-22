using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShopApp.WebUI.Identity
{
    public class SeedIdentity
    {
        public static async Task Seed(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration  ){
            var username=configuration["Data:AdminUser:username"];
            var email=configuration["Data:Adminuser:email"];
         var password=configuration["Data:Adminuser:password"];
          var role=configuration["Data:Adminuser:role"];

          if(await userManager.FindByNameAsync(username)==null){
            await roleManager.CreateAsync(new IdentityRole(role));
            var user=new AppUser(){
                UserName=username,
                Email=email,
                FullName="Admin User",
                EmailConfirmed=true
            };
            var result= await userManager.CreateAsync(user,password);
            if(result.Succeeded){
                await userManager.AddToRoleAsync(user,role);
            }
          }
        }
    }
}