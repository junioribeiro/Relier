using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Relier.Application.Interfaces;
using Relier.Application.Mappings;
using Relier.Application.Services;
using Relier.Domain.Account;
using Relier.Domain.Interfaces;
using Relier.Infra.Data.Account;
using Relier.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relier.Infra.IOC.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Dependencias de construtores
            services.AddScoped<IProductRepository, ProductRepository>(s => new ProductRepository(configuration.GetConnectionString("DefaultConnection") ?? ""));
            services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>(s => new AuthenticateRepository(configuration.GetConnectionString("DefaultConnection") ?? ""));
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            // IOC AutoMapper
            services.AddAutoMapper(typeof(MappingProfile), typeof(MappingProfile));            
            return services;
        }
    }
}
