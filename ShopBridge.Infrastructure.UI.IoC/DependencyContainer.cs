using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Application.Logic.Interface;
using ShopBridge.Application.Logic.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Infrastructure.UI.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAppClient, AppClient>();

            services.AddScoped<IAppUtility, AppUtility>();
        }
    }
}
