namespace EPR.Accreditation.Portal.Extensions
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddPortalDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<MaterialTypeViewModel>();
            services.AddHttpClient("HttpClient");
            services.AddScoped<WasteTypeActionFilter>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<ICultureHelper, CultureHelper>();
            services.AddScoped<IQueryStringHelper, QueryStringHelper>();
            services.AddScoped<IAccreditationSiteMaterialService, AccreditationSiteMaterialService>();
            services.AddScoped<ISaveAndComeBackService, SaveAndComeBackService>();
            services.AddScoped<IPermitExemptionService, PermitExemptionService>();
            services
                .Configure<ServicesConfiguration>(configuration.GetSection(ServicesConfiguration.SectionName));

            services
                .AddScoped<IHttpSiteMaterialService>(s =>
                    new HttpSiteMaterialService(
                        s.GetRequiredService<IHttpContextAccessor>(),
                        s.GetRequiredService<IHttpClientFactory>(),
                        s.GetRequiredService<IOptions<ServicesConfiguration>>().Value.AccreditationFacade.Url,
                        "Accreditation"
                    )
            );

            services
                .AddScoped<IHttpSaveAndComeBackService>(s =>
                    new HttpSaveAndComeBackService(
                        s.GetRequiredService<IHttpContextAccessor>(),
                        s.GetRequiredService<IHttpClientFactory>(),
                        s.GetRequiredService<IOptions<ServicesConfiguration>>().Value.AccreditationFacade.Url,
                        "SaveAndComeBack"
                    )
            );

            return services;
        }
    }
}
