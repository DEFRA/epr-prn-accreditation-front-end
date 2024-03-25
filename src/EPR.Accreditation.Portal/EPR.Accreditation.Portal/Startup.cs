using EPR.Accreditation.Portal.Constants;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Helpers;
using EPR.Accreditation.Portal.Helpers.Interfaces;
using EPR.Accreditation.Portal.Middleware;
using EPR.Accreditation.Portal.Options;
using EPR.Accreditation.Portal.Services.Accreditation;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EPR.Accreditation.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services
                    .AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
            }

            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddPortalDependencies(Configuration);

            var supportedCultures = new[]
            {
                    CultureConstants.English,
                    CultureConstants.Welsh
                };
            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });
            services
                .Configure<RequestLocalizationOptions>(opts =>
                {
                    opts.DefaultRequestCulture = new RequestCulture(CultureConstants.English);
                    opts.SupportedCultures = supportedCultures;
                    opts.SupportedUICultures = supportedCultures;
                });

            // Register configuration options
            services.Configure<AppSettingsConfigOptions>(
                Configuration.GetSection(AppSettingsConfigOptions.ConfigSection)
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                        ctx.Context.Response.Headers["Pragma"] = "no-cache";
                        ctx.Context.Response.Headers["Expires"] = "0";
                    }
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/500");
                app.UseHsts();
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseStaticFiles();
            }

            //app.UsePrnMiddleware();
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

            
        }
    }
}
