using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OrderTracking.Business
{
    public static class Registration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            var ass = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(ass);
            return services;
        }
    }
}
