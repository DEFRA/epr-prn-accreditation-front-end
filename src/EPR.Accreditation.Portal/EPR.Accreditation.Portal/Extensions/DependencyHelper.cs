namespace EPR.Accreditation.Portal.Extensions
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Configuration;
    using EPR.Accreditation.Portal.Helpers;
    using EPR.Accreditation.Portal.Helpers.ActionFilters;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Profiles;
    using EPR.Accreditation.Portal.RESTservices;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Options;

    public static class ExtensionMethods
    {
        public static IServiceCollection AddPortalDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient("HttpClient");
            services
                .AddScoped<MaterialTypeViewModel>()
                .AddScoped<WasteTypeActionFilter>()
                .AddScoped<BackPageViewModel>()
                .AddScoped<IUrlHelperWrapper, UrlHelperWrapper>()
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddScoped<ICultureHelper, CultureHelper>()
                .AddScoped<IQueryStringHelper, QueryStringHelper>()
                .AddScoped<IAccreditationSiteMaterialService, AccreditationSiteMaterialService>()
                .AddScoped<ISaveAndComeBackService, SaveAndComeBackService>()
                .AddScoped<IWastePermitService, WastePermitService>()
                .AddScoped<IAccreditationService, AccreditationService>()
                .AddScoped<IUrlHelperWrapper, UrlHelperWrapper>()
                .Configure<ServicesConfiguration>(configuration.GetSection(ServicesConfiguration.SectionName));

            services.AddScoped<IHttpSiteMaterialService>(s =>
                    new HttpSiteMaterialService(
                        s.GetRequiredService<IHttpContextAccessor>(),
                        s.GetRequiredService<IHttpClientFactory>(),
                        s.GetRequiredService<IOptions<ServicesConfiguration>>().Value.AccreditationFacade.Url,
                        "Accreditation"
                    )

            );
            services
                .AddScoped<IAccreditationService, AccreditationService>()
                .AddScoped<IHttpAccreditationService>(s =>
                    new HttpAccreditionService(
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

            services
                .AddScoped<IHttpWastePermitService>(s =>
                    new HttpWastePermitService(
                        s.GetRequiredService<IHttpContextAccessor>(),
                        s.GetRequiredService<IHttpClientFactory>(),
                        s.GetRequiredService<IOptions<ServicesConfiguration>>().Value.AccreditationFacade.Url,
                        "Accreditation"
                    )
            );

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AccreditationProfile());
                mc.AllowNullCollections = true;
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // control client validation based on configuration
            services.AddRazorPages()
                .AddViewOptions(o =>
                {
                    o.HtmlHelperOptions.ClientValidationEnabled = configuration.GetValue<bool>("ClientValidationEnabled");
                });

            return services;
        }
    }
}
