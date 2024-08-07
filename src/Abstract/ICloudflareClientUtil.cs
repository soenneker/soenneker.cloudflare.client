using System.Threading.Tasks;
using System;
using CloudFlare.Client;
using System.Threading;

namespace Soenneker.Cloudflare.Client.Abstract;

/// <summary>
/// An async thread-safe singleton for the Cloudflare library, CloudFlare.Client
/// </summary>
public interface ICloudflareClientUtil : IDisposable, IAsyncDisposable
{
    ValueTask<CloudFlareClient> Get(CancellationToken cancellationToken = default);
}