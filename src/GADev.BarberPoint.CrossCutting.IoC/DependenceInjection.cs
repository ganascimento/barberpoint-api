using GADev.BarberPoint.Application.Repositories.Core;
using GADev.BarberPoint.Identity.Models;
using GADev.BarberPoint.Identity.Repositories;
using GADev.BarberPoint.Infrastructure.Repositories.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GADev.BarberPoint.CrossCutting.IoC
{
    public static class DependenceInjection
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserRepository>();
	        services.AddTransient<IRoleStore<ApplicationRole>, RoleRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}