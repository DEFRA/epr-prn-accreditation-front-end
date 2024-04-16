#pragma warning disable SA1005 // StyleCop error code you want to ignore
namespace EPR.Accreditation.Portal.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using EPR.Accreditation.Portal.Helpers.AuthHelpers;
    using EPR.Accreditation.Portal.Middleware.Auth;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.Services.AuthServices;
    using EPR.Accreditation.Portal.Services.AuthServices.Interfaces;
    using EPR.Accreditation.Portal.Sessions;
    using EPR.Common.Authorization.Extensions;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.Extensions.Options;
    using Microsoft.Identity.Web;
    using Microsoft.Identity.Web.TokenCacheProviders.Distributed;
    using StackExchange.Redis;
    using CookieOptions = EPR.Accreditation.Portal.Options.CookieOptions;
    using SessionOptions = EPR.Accreditation.Portal.Options.SessionOptions;

    /// <summary>
    /// Class definition.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// Register web components
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection RegisterWebComponents(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureOptions(services, configuration);
            ConfigureAuthentication(services, configuration);
            ConfigureAuthorization(services, configuration);
            ConfigureSession(services);
            RegisterAccountManagementServices(services);
            RegisterAccountServiceHttpClients(services);

            return services;
        }

        /// <summary>
        /// Configure Msal distributed token options
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection ConfigureMsalDistributedTokenOptions(this IServiceCollection services)
        {
            services.Configure<MsalDistributedTokenCacheAdapterOptions>(options =>
            {
                var msalOptions = services.BuildServiceProvider().GetRequiredService<IOptions<MsalOptions>>().Value;

                options.DisableL1Cache = msalOptions.DisableL1Cache;
                options.SlidingExpiration = TimeSpan.FromMinutes(msalOptions.L2SlidingExpiration);

                options.OnL2CacheFailure = exception =>
                {
                    if (exception is RedisConnectionException)
                    {
                        return true;
                    }

                    return false;
                };
            });

            return services;
        }

        private static void ConfigureAuthorization(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;

                var azureB2COptions = services.BuildServiceProvider().GetRequiredService<IOptions<AzureAdB2COptions>>().Value;

                options.LoginPath = azureB2COptions.SignedOutCallbackPath;
                options.AccessDeniedPath = azureB2COptions.SignedOutCallbackPath;

                options.SlidingExpiration = true;
            });

            services.RegisterPolicy<FrontendSchemeRegistrationSession>(configuration);
        }

        private static void ConfigureOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GlobalVariables>(configuration);
            services.Configure<PhaseBannerOptions>(configuration.GetSection(PhaseBannerOptions.Section));
            services.Configure<FrontEndAccountManagementOptions>(configuration.GetSection(FrontEndAccountManagementOptions.ConfigSection));
            services.Configure<FrontEndAccountCreationOptions>(configuration.GetSection(FrontEndAccountCreationOptions.ConfigSection));
            services.Configure<ExternalUrlOptions>(configuration.GetSection(ExternalUrlOptions.ConfigSection));
            services.Configure<CachingOptions>(configuration.GetSection(CachingOptions.ConfigSection));
            services.Configure<EmailAddressOptions>(configuration.GetSection(EmailAddressOptions.ConfigSection));
            services.Configure<SiteDateOptions>(configuration.GetSection(SiteDateOptions.ConfigSection));
            services.Configure<CookieOptions>(configuration.GetSection(CookieOptions.ConfigSection));
            services.Configure<GoogleAnalyticsOptions>(configuration.GetSection(GoogleAnalyticsOptions.ConfigSection));
            services.Configure<GuidanceLinkOptions>(configuration.GetSection(GuidanceLinkOptions.ConfigSection));
            services.Configure<MsalOptions>(configuration.GetSection(MsalOptions.ConfigSection));
            services.Configure<AzureAdB2COptions>(configuration.GetSection(AzureAdB2COptions.ConfigSection));
            services.Configure<HttpClientOptions>(configuration.GetSection(HttpClientOptions.ConfigSection));
            services.Configure<AccountsFacadeApiOptions>(configuration.GetSection(AccountsFacadeApiOptions.ConfigSection));
            services.Configure<WebApiOptions>(configuration.GetSection(WebApiOptions.ConfigSection));
            services.Configure<ValidationOptions>(configuration.GetSection(ValidationOptions.ConfigSection));
            services.Configure<RedisOptions>(configuration.GetSection(RedisOptions.ConfigSection));
            services.Configure<ComplianceSchemeMembersPaginationOptions>(configuration.GetSection(ComplianceSchemeMembersPaginationOptions.ConfigSection));
            services.Configure<SessionOptions>(configuration.GetSection(SessionOptions.ConfigSection));
        }

        private static void RegisterAccountManagementServices(IServiceCollection services)
        {
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IRoleManagementService, RoleManagementService>();
            services.AddScoped<IRoleManagementService, RoleManagementService>();
            services.AddTransient<UserDataCheckerMiddleware>();
            services.AddSingleton<ICorrelationIdProvider, CorrelationIdProvider>();
        }

        private static void RegisterAccountServiceHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<IAccountServiceApiClient, AccountServiceApiClient>((sp, client) =>
            {
                var facadeApiOptions = sp.GetRequiredService<IOptions<AccountsFacadeApiOptions>>().Value;
                var httpClientOptions = sp.GetRequiredService<IOptions<HttpClientOptions>>().Value;

                client.BaseAddress = new Uri(facadeApiOptions.BaseEndpoint);
                client.Timeout = TimeSpan.FromSeconds(httpClientOptions.TimeoutSeconds);
            });
        }

        private static void ConfigureSession(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var globalVariables = sp.GetRequiredService<IOptions<GlobalVariables>>().Value;

            if (!globalVariables.UseLocalSession)
            {
                var redisOptions = sp.GetRequiredService<IOptions<RedisOptions>>().Value;
                var redisConnectionString = redisOptions.ConnectionString;

                services.AddDataProtection()
                    .SetApplicationName("EprProducers")
                    .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(redisConnectionString), "DataProtection-Keys");

                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConnectionString;
                    options.InstanceName = redisOptions.InstanceName;
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            services.AddSession(options =>
            {
                var cookieOptions = sp.GetRequiredService<IOptions<CookieOptions>>().Value;
                var sessionOptions = sp.GetRequiredService<IOptions<SessionOptions>>().Value;

                options.Cookie.Name = cookieOptions.SessionCookieName;
                options.IdleTimeout = TimeSpan.FromMinutes(sessionOptions.IdleTimeoutMinutes);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.Path = "/";
            });
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var sp = services.BuildServiceProvider();
            var cookieOptions = sp.GetRequiredService<IOptions<CookieOptions>>().Value;
            var facadeApiOptions = sp.GetRequiredService<IOptions<AccountsFacadeApiOptions>>().Value;

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(
                    options =>
                    {
                        configuration.GetSection(AzureAdB2COptions.ConfigSection).Bind(options);

                        options.CorrelationCookie.Name = cookieOptions.CorrelationCookieName;
                        options.NonceCookie.Name = cookieOptions.OpenIdCookieName;
                        options.ErrorPath = "/error";
                        options.ClaimActions.Add(new CorrelationClaimAction());
                    },
                    options =>
                    {
                        options.Cookie.Name = cookieOptions.AuthenticationCookieName;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(cookieOptions.AuthenticationExpiryInMinutes);
                        options.SlidingExpiration = true;
                        options.Cookie.Path = "/";
                    })
                .EnableTokenAcquisitionToCallDownstreamApi(new[] { facadeApiOptions.DownstreamScope })
                .AddDistributedTokenCaches();
        }
    }
}
#pragma warning restore SA1005 // Restore StyleCop warnings