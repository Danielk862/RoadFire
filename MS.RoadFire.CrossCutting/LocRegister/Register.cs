using Microsoft.Extensions.DependencyInjection;
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

        }

        public static void AddRepositories(this IServiceCollection services)
        {

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
