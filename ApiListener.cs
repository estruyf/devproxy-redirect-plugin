using Microsoft.DevProxy.Abstractions;
using Microsoft.Extensions.Configuration;

namespace DevProxyCustomPlugin;

public class RedirectCallsConfiguration
{
  public string? FromUrl { get; set; }
  public string? ToUrl { get; set; }
}

public class RedirectCalls : BaseProxyPlugin
{
  public override string Name => nameof(RedirectCalls);
  private readonly RedirectCallsConfiguration _configuration = new();

  public override void Register(IPluginEvents pluginEvents,
                                IProxyContext context,
                                ISet<UrlToWatch> urlsToWatch,
                                IConfigurationSection? configSection = null)
  {
    base.Register(pluginEvents, context, urlsToWatch, configSection);

    configSection?.Bind(_configuration);

    pluginEvents.BeforeRequest += OnBeforeRequest;
  }

  private Task OnBeforeRequest(object sender, ProxyRequestArgs e)
  {
    if (_urlsToWatch is null ||
      !e.HasRequestUrlMatch(_urlsToWatch))
    {
      // No match for the URL, so we don't need to do anything
      return Task.CompletedTask;
    }

    var fromUrl = _configuration?.FromUrl ?? string.Empty;
    var toUrl = _configuration?.ToUrl ?? string.Empty;

    if (string.IsNullOrEmpty(fromUrl) || string.IsNullOrEmpty(toUrl))
    {
      return Task.CompletedTask;
    }

    if (e.Session.HttpClient.Request.RequestUri.AbsoluteUri.Contains(fromUrl))
    {
      var url = e.Session.HttpClient.Request.RequestUri.AbsoluteUri;
      e.Session.HttpClient.Request.RequestUri = new Uri(url.Replace(fromUrl, toUrl));
    }

    return Task.CompletedTask;
  }
}
