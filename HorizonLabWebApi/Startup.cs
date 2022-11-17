using HorizonLabWebApi.Models;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HorizonLabWebApi.ApiFilter;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddConfiguration(configuration)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IConfiguration>(_configuration);
            services.AddDbContextPool<HorizonLabDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("HorizonLabDbConn")));
            services.AddHttpContextAccessor();
            services.AddScoped<Interface_hlab_users, HlabUserRepository>();
            services.AddScoped<Interface_hlab_services, HlabServicesRepository>();
            services.AddScoped<Interface_hlab_api_keys, HlabApiKeyRepository>();
            services.AddScoped<Interface_hlab_service_details, HlabServiceDetailRepository>();
            services.AddScoped<Interface_test_transactions, HlabTestTransactionRepository>();            
            services.AddScoped<Interface_test_results, HlabTestResultRepository>();
            services.AddScoped<Interface_hlab_customers, HlabCustomerRepository>();
            services.AddScoped<Interface_hlab_invoice, HlabInvoiceRepository>();
            services.AddScoped<Interface_hlab_test_coupon_logs, HlabCouponLogRepository>();
            services.AddScoped<Interface_hlab_test_payments, HlabTestPaymentRepository>();
            services.AddScoped<Interface_hlab_orders, HlabOrderRepository>();
            services.AddScoped<Interface_water_chem_result, HlabWaterChemRepository>();
            services.AddScoped<Interface_hlab_supplies, HlabSupplies>();
            services.AddScoped<Interface_test_package, HlabTestPackages>();
            services.AddScoped<Interface_EmailSender, HlabEmailSender>();
            services.AddScoped<Interface_hlab_test_default_pkg_params, HlabDefaultParameter>();
            services.AddScoped<Interface_hlab_cities, City>();
            services.AddScoped<Interface_receivers, HlabReceivers>();
            services.AddScoped<Interface_rural_municipality, Municipality>();                        
            services.AddScoped<Interface_test_projects, HlabTestProject>();
            services.AddScoped<Interface_test_sample_types, HlabTestSampleType>();            
            services.AddScoped<Interface_test_parameter, HlabTestParameter>();            
            services.AddScoped<Interface_test_report_types, HlabTestReport>();            
            services.AddScoped<Interface_test_class, HlabTestClass>();            
            services.AddScoped<Interface_test_description, HlabTestDescription>();            
            services.AddScoped<Interface_unit_of_measurement, UnitOfMeasurement>();            
            services.AddScoped<Interface_hlab_provinces, Province>();            
            services.AddScoped<Interface_hlab_project_supply, HlabTestProjectSupplies>();            
            services.AddScoped<Interface_hlab_test_project_form, HlabTestProjectForm>();            
            services.AddScoped<APIKeyHandlers>();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //use to show detailed error messages
            app.UseDeveloperExceptionPage(); 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
