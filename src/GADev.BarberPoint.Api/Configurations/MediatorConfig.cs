using System;
using FluentValidation;
using GADev.BarberPoint.Api.Filters;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GADev.BarberPoint.Api.Configurations
{
    public static class MediatorConfig
    {
        public static void ConfigureMediator(this IServiceCollection services) {
            var assembly = AppDomain.CurrentDomain.Load("GADev.BarberPoint.Application");

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            services.AddMediatR(assembly);
        }
    }
}