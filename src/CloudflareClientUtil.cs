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
public sealed class CloudflareClientUtil : ICloudflareClientUtil
{
    private readonly AsyncSingleton<CloudFlareClient> _client;
    private readonly ILogger<CloudflareClientUtil> _logger;
    private readonly IConfiguration _config;

    public CloudflareClientUtil(ILogger<CloudflareClientUtil> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
        _client = new AsyncSingleton<CloudFlareClient>(CreateClient);
    }

    private CloudFlareClient CreateClient()
    {
        var email = _config.GetValueStrict<string>("Cloudflare:Email");
        var apiKey = _config.GetValueStrict<string>("Cloudflare:ApiKey");

        var authentication = new ApiKeyAuthentication(email, apiKey);

        _logger.LogInformation("Connecting to Cloudflare...");

        return new CloudFlareClient(authentication);
    }

    public ValueTask<CloudFlareClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}