using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MyForum.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyForum.Models;

namespace MyForum
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserValidator<User>, CustomUserValidator>();

            services.AddTransient<IPasswordValidator<User>,
                CustomPasswordValidator>(serv => new CustomPasswordValidator(6));


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(opts => {
                    opts.Password.RequiredLength = 6;   // ����������� �����
                    opts.Password.RequireNonAlphanumeric = true;   // ��������� �� �� ���������-�������� �������
                    opts.Password.RequireLowercase = true; // ��������� �� ������� � ������ ��������
                    opts.Password.RequireUppercase = true; // ��������� �� ������� � ������� ��������
                    opts.Password.RequireDigit = false; // ��������� �� �����
                    opts.User.RequireUniqueEmail = true;    // ���������� email
                    opts.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz"; // ���������� �������
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Topics}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
