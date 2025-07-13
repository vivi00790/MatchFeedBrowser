using ESportsMatchTracker.API.Enums;
using ESportsMatchTracker.API.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ESportsMatchTracker.API.Decorators;

public class MatchServiceCache(IMatchService inner, IMemoryCache cache) : IMatchService
{
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public async Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class
    {
        var cacheKey = $"matches:{status}";

        if (cache.TryGetValue(cacheKey, out List<T> cached))
        {
            return cached ?? [];
        }

        var result = await inner.FetchMatchesAsync<T>(status);

        cache.Set(cacheKey, result, _cacheDuration);

        return result;
    }
}