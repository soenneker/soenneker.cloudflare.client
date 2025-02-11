using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cloudflare.Client.Abstract;

namespace Soenneker.Cloudflare.Client.Registrars;

/// <summary>
/// An async thread-safe singleton for the Cloudflare library, CloudFlare.Client
/// </summary>
public static class CloudflareClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ICloudflareClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareClientUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<ICloudflareClientUtil, CloudflareClientUtil>();
        return services;
    }

    /// <summary>
    /// Adds <see cref="ICloudflareClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareClientUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<ICloudflareClientUtil, CloudflareClientUtil>();
        return services;
    }
}