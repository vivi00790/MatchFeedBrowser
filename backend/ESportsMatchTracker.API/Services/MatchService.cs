using System.Text.Json;
using ESportsMatchTracker.API.Enums;
using ESportsMatchTracker.API.Models;
using ESportsMatchTracker.API.Proxies;

namespace ESportsMatchTracker.API.Services;

public interface IMatchService
{
    Task<List<MatchInfo>> FetchMatchesAsync(MatchStatus status);
}

public class MatchService(IDummyMatchProxy dummyMatchProxy) : IMatchService
{
    public async Task<List<MatchInfo>> FetchMatchesAsync(MatchStatus status)
    {
        return status switch
        {
            //TODO: use different model for different match status
            MatchStatus.Scheduled => await dummyMatchProxy.FetchMatchesAsync<MatchInfo>(status),
            MatchStatus.Live => await dummyMatchProxy.FetchMatchesAsync<MatchInfo>(status),
            MatchStatus.Ended => await dummyMatchProxy.FetchMatchesAsync<MatchInfo>(status),
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}