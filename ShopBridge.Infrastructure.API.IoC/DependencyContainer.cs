using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Application.Interface;
using ShopBridge.Application.Logic.Interface;
using ShopBridge.Application.Logic.Service;
using ShopBridge.Application.Service;
using ShopBridge.Domain.Interface;
using ShopBridge.Infrastructure.Data.Context;
using ShopBridge.Infrastructure.Data.Repository;
using ShopBridge.Infrastructure.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Infrastructure.API.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppClient, AppClient>();

            services.AddScoped<IAppUtility, AppUtility>();

            services.AddSingleton(new ConnectionString(configuration.GetConnectionString("SWMasterApp")));

            services.AddScoped<IDbContext, DbContext>();

            services.AddScoped<IInventoryRepository, InventoryRepository>();

            services.AddScoped<IInventoryService, InventoryService>();
        }
    }
}
