using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            string firstName = "Адміністратор";
            string secondName = "Іван";
            string phoneNumber = "0970000000";
            string country = "Україна";
            string city = "Вінниця";
            string aboutMe = "I love C#";

            DateTime birthday = new DateTime(2001, 1, 1);
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = firstName,
                    SecondName = secondName,
                    RegisterDate = birthday,
                    PhoneNumber = phoneNumber,
                    Country = country,
                    City = city,
                    AboutMe = aboutMe,
                    GenderId = 2,
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
