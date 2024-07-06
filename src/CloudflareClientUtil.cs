using System;
using System.Threading;
using System.Threading.Tasks;
using CloudFlare.Client;
using CloudFlare.Client.Api.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Cloudflare.Client.Abstract;
using Soenneker.Extensions.Configuration;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Cloudflare.Client;

/// <inheritdoc cref="ICloudflareClientUtil"/>
public class CloudflareClientUtil : ICloudflareClientUtil
{
    private readonly AsyncSingleton<CloudFlareClient> _client;

    public CloudflareClientUtil(ILogger<CloudflareClientUtil> logger, IConfiguration config)
    {
        _client = new AsyncSingleton<CloudFlareClient>(() =>
        {
            var email = config.GetValueStrict<string>("Cloudflare:Email");
            var apiKey = config.GetValueStrict<string>("Cloudflare:ApiKey");

            var authentication = new ApiKeyAuthentication(email, apiKey);

            logger.LogInformation("Connecting to Cloudflare...");

            var client = new CloudFlareClient(authentication);

            return client;
        });
    }

    public ValueTask<CloudFlareClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _client.DisposeAsync();
    }
}