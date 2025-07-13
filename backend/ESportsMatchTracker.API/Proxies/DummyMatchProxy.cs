using ESportsMatchTracker.API.Enums;

namespace ESportsMatchTracker.API.Proxies;

public interface IDummyMatchProxy
{
    public Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class;
}

public class DummyDummyMatchProxy(IHttpClientFactory httpClientFactory) : IDummyMatchProxy
{
    public async Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class
    {
        var httpClient = httpClientFactory.CreateClient($"DummyMatchClient:{status.ToString()}");
        return await httpClient.GetFromJsonAsync<List<T>>($"/api/dummy/{status.ToString()}") ?? [];
    }
}