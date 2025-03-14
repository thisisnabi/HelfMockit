using Microsoft.Extensions.Options;

namespace HelfMockit;

public class MockHttpClientHandler(IHostEnvironment hostEnvironment, IOptions<MockServerSettings> settings) : DelegatingHandler
{
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly MockServerSettings _settings = settings.Value;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_hostEnvironment.IsStaging() &&
            request.RequestUri is not null &&
            GlobalMockServerService.ContainsRoute(request.RequestUri.AbsoluteUri))
        {
            var mockServerUrl = new Uri(_settings.BaseUrl);
            var uriBuilder = new UriBuilder(request.RequestUri!)
            {
                Scheme = mockServerUrl.Scheme,
                Host = mockServerUrl.Host
            };

            request.RequestUri = uriBuilder.Uri;
        }

        return base.SendAsync(request, cancellationToken);
    }
}