﻿using AutoMapper;
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
            services.AddScoped<BackPageViewModel>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<ICultureHelper, CultureHelper>();
            services.AddScoped<IQueryStringHelper, QueryStringHelper>();
            services.AddScoped<IAccreditationSiteMaterialService, AccreditationSiteMaterialService>();
            services.AddScoped<ISaveAndComeBackService, SaveAndComeBackService>();
            services.AddScoped<IWastePermitService, WastePermitService>();
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

            return services;
        }
    }
}
