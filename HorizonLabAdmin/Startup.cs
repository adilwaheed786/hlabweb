using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Helpers.Utilities.Session;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HorizonLabAdmin
{
    public class Startup
    {
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

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();
            services.AddScoped<Interface_hlab_users, HlabUserRepository>();
            services.AddScoped<Interface_hlab_services, HlabServicesRepository>();
            services.AddScoped<Interface_hlab_service_details, HlabServiceDetailsRepository>();
            services.AddScoped<Interface_test_transactions, HlabTestTransactionRepository>();
            services.AddScoped<Interface_test_package, HlabTestPackage>();
            services.AddScoped<Interface_test_results, HlabTestResultRepository>();
            services.AddScoped<Interface_hlab_customers, HlabCustomerRepository>();
            services.AddScoped<Interface_hlab_invoice, HlabInvoiceRepository>();
            services.AddScoped<Interface_hlab_test_coupon_logs, HlabCouponLogRepository>();
            services.AddScoped<Interface_hlab_orders, HlabOrderRepository>();
            services.AddScoped<Interface_hlab_test_payments, HlabTestPaymentRepository>();
            services.AddScoped<Interface_water_chem_result, HlabWaterChemRepository>();
            services.AddScoped<Interface_hlab_test_default_pkg_params, HlabDefaultParameterRepository>();
            services.AddScoped<Interface_EmailSender, HlabEmailSender>();
            services.AddScoped<Interface_test_projects, HlabTestProject>();

            services.AddScoped<Interface_rural_municipality, HlabTableReferenceRepository>(); 
            services.AddScoped<Interface_test_sample_types, HlabTableReferenceRepository>();
            services.AddScoped<Interface_test_parameter, HlabTableReferenceRepository>();
            services.AddScoped<Interface_test_report_types, HlabTableReferenceRepository>();
            services.AddScoped<Interface_test_class, HlabTableReferenceRepository>();
            services.AddScoped<Interface_test_description, HlabTableReferenceRepository>();
            services.AddScoped<Interface_unit_of_measurement, HlabTableReferenceRepository>();
            services.AddScoped<Interface_hlab_provinces, HlabTableReferenceRepository>();
            services.AddScoped<Interface_hlab_cities, HlabTableReferenceRepository>();
            services.AddScoped<Interface_receivers, HlabTableReferenceRepository>();
            services.AddScoped<Interface_hlab_supplies, HlabSuppliesRepository>();
            services.AddScoped<Interface_hlab_project_supply, HlabTestProjectSupplyRepo>();
            services.AddScoped<Interface_hlab_test_project_form, HlabTestProjectFormRepository>();

            //UTILITIES
            services.AddScoped<ILogin, HLogin>();            
            services.AddScoped<IUtility, HUtility>();            
            services.AddScoped<ICertificate, HCertificate>();
            services.AddScoped<IRequestSession, RequestSession>();
            services.AddScoped<ICustomerSession, CustomerSession>();
            services.AddScoped<INavigationSession, NavigationSession>();
            services.AddScoped<IUserSession, UserSession>();
            services.AddScoped<IUserAccount, UserAccount>();
            services.AddScoped<ITestTransactionSession, TestTransactionSession>();
            services.AddScoped<IRequest, HRequest>();
            services.AddScoped<IRequestItem, HRequestItem>();
            services.AddScoped<INavigation, HNavigation>();
            services.AddScoped<IEmail, HEmail>();
            services.AddScoped<ITransaction, HTransaction>();
            services.AddScoped<ICustomer, HCustomer>();
            services.AddScoped<ITestResult, HTestResult>();
            services.AddScoped<ITestPackage, HTestPackage>();
            services.AddScoped<IDashboard, HDashboard>();
            services.AddScoped<IPayment, HPayment>();
            services.AddScoped<IHLCode, HLCode>();
            services.AddScoped<Iinvoice, Hinvoice>();
            services.AddScoped<ISupply, HSupply>();
            services.AddScoped<ITestProject, HTestProject>();
            services.AddScoped<IWaterBacteria, HWaterBacteria>();
            services.AddScoped<ITestProjectSupply, HTestProjectSupply>();
            services.AddScoped<ITestProjectForm, HTestPorjectForm>();
            services.AddScoped<ISelectHtmlToPDFConverter, SelectHtmlToPDFConverter>();

            services.AddSingleton<HSession>();
            services.AddSingleton<IRequestSession>(x => (HSession)x.GetRequiredService<IUserSession>()); 
            services.AddSingleton<ICustomerSession>(x => x.GetRequiredService<HSession>());
            services.AddSingleton<INavigationSession>(x => x.GetRequiredService<HSession>());
            services.AddSingleton<IUserSession>(x => x.GetRequiredService<HSession>());
            services.AddSingleton<ITestTransactionSession>(x => x.GetRequiredService<HSession>());
            services.AddSingleton<IHorizonLabSession>(x => x.GetRequiredService<HSession>());
            

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var cultureInfo = new CultureInfo("en-US");

            cultureInfo.DateTimeFormat.DateSeparator = "/";
            cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            cultureInfo.DateTimeFormat.LongDatePattern = "dd/MM/yyyy hh:mm:ss tt";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}");
            });
        }
    }
}

