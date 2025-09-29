using Microsoft.Extensions.DependencyInjection;
using MS.RoadFire.Application.Contracts.Interfaces;
using MS.RoadFire.Application.Services;
using MS.RoadFire.Business.Mappers;
using MS.RoadFire.DataAccess.Contracts.Interfaces;
using MS.RoadFire.DataAccess.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MS.RoadFire.CrossCutting.LocRegister
{
    public static class Register
    {
        public static IServiceCollection AddRegister(this IServiceCollection services)
        {
            services.AddServices();
            services.AddRepositories();
            AddJsonDefaultSettings();

            return services;
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericServices<,>), typeof(GenericServices<,>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ISecurityServices, SecurityServices>();
            services.AddScoped<IUserServices, UserServices>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISecurityRepository, SecurityRepository>();
        }

        private static void AddJsonDefaultSettings()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
        }
    }
}
