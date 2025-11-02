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
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISecurityServices, SecurityServices>();
            services.AddScoped<IStockServices, StockServices>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            services.AddScoped<IUserServices, UserServices>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserRerpository, UserRerpository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
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
