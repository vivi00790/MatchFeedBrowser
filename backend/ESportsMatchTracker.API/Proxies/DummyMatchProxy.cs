using ESportsMatchTracker.API.Enums;

namespace ESportsMatchTracker.API.Proxies;

public interface IDummyMatchProxy
{
    public Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class;
}

public class DummyMatchProxy(IHttpClientFactory httpClientFactory) : IDummyMatchProxy
{
    // Proxy is the place that external provider's domain merges with our internal domain.
    // In current case there's not much to do, however, in real-world scenarios,
    // we can convert external match id to our internal match id, or
    // map external match or tournament data to a form benefits us (for instance,
    // provider may use id or code to represent team, and provide another API to fetch team id/name mapping)
    // In that case, we can make 2 model: one for external provider, and one for our internal use, which normally basically a db row projection.
    // Then we can deserialize external provider's data to external model, and then map it to our internal model.
    // In current task, we can create DummyLiveMatchResponse as external model, and LiveMatchInfo as internal model, but the properties are the same.
    public async Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class
    {
        var httpClient = httpClientFactory.CreateClient($"DummyMatchClient:{status.ToString()}");
        return await httpClient.GetFromJsonAsync<List<T>>($"/api/dummy/{status.ToString()}") ?? [];
    }
}