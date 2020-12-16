using GADev.BarberPoint.Identity.Context;
using GADev.BarberPoint.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GADev.BarberPoint.CrossCutting.Identity
{
    public static class IdentityConfig
    {
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<IdentityDbContext>(options => {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => 
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
        }
    }
}