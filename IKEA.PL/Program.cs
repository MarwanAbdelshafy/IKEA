using IKEA.BLL.Common.services.Attachments;
using IKEA.BLL.Services.DepartmentServise;
using IKEA.BLL.Services.EmployeeServise;
using IKEA.DAL.Models.Identity;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Reposatories.Departments;
using IKEA.DAL.Persistance.Reposatories.Employee;
using IKEA.DAL.Persistance.UnitOfWork;
using IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
           // builder.Services.AddScoped<IDepartmentReposatory, DepartmentReposatory>();
            builder.Services.AddScoped<IDepartementServices, DepartementServices>();
            //builder.Services.AddScoped<IEmployeeReposatory, EmployeeReposatory>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();

            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfile)));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAttachmetService, AttachmentService>();
            //allow DI user manger //signinmanger rolemanger
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;//#$%
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            }).AddEntityFrameworkStores<ApplcationDBContext>();



            builder.Services.AddAuthentication().AddCookie(Options =>
            {
                Options.LogoutPath = "/Account/LogOut";
                Options.AccessDeniedPath = "/Account/Error ";
                Options.SlidingExpiration = true;
                Options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                Options.ForwardSignOut = "/Account/LogIn";
            });
            //builder.Services.AddDbContext<ApplcationDBContext>(Options =>
            //{
            //    Options.UseSqlServer("Server=.;Database=IKEA_DB;trusted_Connection=true;TrustServerCertificate=true");
            //});


            builder.Services.AddDbContext<ApplcationDBContext>(Options =>
            {
                Options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
