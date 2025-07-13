using ESportsMatchTracker.API.Enums;
using ESportsMatchTracker.API.Proxies;

namespace ESportsMatchTracker.API.Services;

public interface IMatchService
{
    Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class;
}

public class MatchService(IDummyMatchProxy dummyMatchProxy) : IMatchService
{
    // Services should always, ideally, running business logic with company's domain.
    // How frontend display data is also not within the domain of service.
    // Which means, we should also separate model for display, and model for internal process. 
    // In current task, we can use the same model(LiveMatchInfo) for both, but in real-world scenarios,
    // they will become for instance LiveMatchDisplayData and LiveMatchInfo. 
    public async Task<List<T>> FetchMatchesAsync<T>(MatchStatus status) where T : class
    {
        return await dummyMatchProxy.FetchMatchesAsync<T>(status);
    }
}