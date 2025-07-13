using System.Text.Json;
using ESportsMatchTracker.API.Enums;
using ESportsMatchTracker.API.Models;
using ESportsMatchTracker.API.Proxies;

namespace ESportsMatchTracker.API.Services;

public interface IMatchService
{
    Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class;
}

public class MatchService(IDummyMatchProxy dummyMatchProxy) : IMatchService
{
    public async Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class
    {
        return await dummyMatchProxy.FetchMatchesAsync<T>(status);
    }
}