using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLayerApp.Application.MainBoundedContext.BankingModule.Services;
using NLayerApp.Application.MainBoundedContext.ERPModule.Services;
using NLayerApp.DistributedServices.MainBoundedContext.Filters;
using NLayerApp.Domain.MainBoundedContext.Aggregates.ImageAgg;
using NLayerApp.Domain.MainBoundedContext.Aggregates.PostAgg;
using NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
using NLayerApp.Domain.MainBoundedContext.BankingModule.Services;
using NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
using NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
using NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
using NLayerApp.Infrastructure.Crosscutting.Adapter;
using NLayerApp.Infrastructure.Crosscutting.Localization;
using NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;
using NLayerApp.Infrastructure.Crosscutting.NetFramework.Localization;
using NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator;
using NLayerApp.Infrastructure.Crosscutting.Validator;
using NLayerApp.Infrastructure.Data.MainBoundedContext.BankingModule.Repositories;
using NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories;
using NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;

namespace Tech.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainBCUnitOfWork>();
            //Custom Exception and validation Filter
            services.AddScoped<CustomExceptionFilterAttribute>();

            //Repositories

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Domain Services
            services.AddScoped<IBankTransferService, BankTransferService>();

            services.AddScoped<ISalesAppService, SalesAppService>();
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IBankAppService, BankAppService>();

            //Adapters
            services.AddScoped<ITypeAdapterFactory, AutomapperTypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(services.BuildServiceProvider().GetService<ITypeAdapterFactory>());

            //Validator
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());

            //Localization
            LocalizationFactory.SetCurrent(new ResourcesManagerFactory());


            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidateModelAttribute()); // an instance
                options.Filters.Add(typeof(LoggerAttribute));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MainBCUnitOfWork context2)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
            DbInitializer.Initialize(context2);
        }
    }
}
