using System.Net;
using System.Text;
using ESportsMatchTracker.API.Enums;
using ESportsMatchTracker.API.Models;
using ESportsMatchTracker.API.Proxies;
using Newtonsoft.Json;
using NSubstitute;

namespace ESportsMatchTracker.API.Test;

public class DummyMatchProxyTest
{
    private IHttpClientFactory _httpClientFactory;

    [SetUp]
    public void Setup()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
    }

    // Tests showcase for how to test easily with NSubstitute and DI
    [Test]
    public async Task ProxyShouldCreateCorrectHttpClient()
    {
        var handler = new MockHttpMessageHandler(request =>
        {
            var json = JsonConvert.SerializeObject(new List<LiveMatchInfo>
            {
                new LiveMatchInfo { Id = 10001 }
            });

            return Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });
        });

        var mockHttpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost")
        };

        _httpClientFactory.CreateClient(Arg.Is<string>(x => x == "DummyMatchClient:Live")).Returns(mockHttpClient);
        var result = await new DummyMatchProxy(_httpClientFactory).FetchMatchesAsync<LiveMatchInfo>(MatchStatus.Live);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(10001));
    }

    private class MockHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> handlerFunc)
        : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
            => handlerFunc(request);
    }
}