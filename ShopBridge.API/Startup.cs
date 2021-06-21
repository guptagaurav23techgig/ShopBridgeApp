using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShopBridge.Infrastructure.API.IoC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private string _corspolicy = "ShopBridgeAppCorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(Configuration["GlobalizationString:culture"]);

                options.SupportedCultures = new List<CultureInfo> { new CultureInfo(Configuration["GlobalizationString:culture"]), new CultureInfo(Configuration["GlobalizationString:culture"]) };

                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo(Configuration["GlobalizationString:culture"]), new CultureInfo(Configuration["GlobalizationString:culture"]) };

                options.RequestCultureProviders.Clear();
            });

            services.AddCors(options =>
            {
                options.AddPolicy(_corspolicy,
                builder => builder.WithOrigins(Configuration["GlobalizationString:UIUrl"]).AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopBridge.API", Version = "v1" });
            });

            RegisterServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(Configuration["GlobalizationString:APIUrl"] + "/swagger/v1/swagger.json", "ShopBridge.API v1"));
            }

            app.UseRouting();

            app.UseCors(_corspolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DependencyContainer.RegisterServices(services, configuration);
        }
    }
}
