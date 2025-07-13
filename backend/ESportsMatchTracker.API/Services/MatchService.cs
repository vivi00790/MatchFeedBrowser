using System.Text.Json;
using ESportsMatchTracker.API.Enums;
using ESportsMatchTracker.API.Models;

namespace ESportsMatchTracker.API.Services;

public interface IMatchService
{
    Task<List<MatchInfo>> FetchMatchesAsync(MatchStatus status);
}

public class MatchService(HttpClient httpClient) : IMatchService
{
    public async Task<List<MatchInfo>> FetchMatchesAsync(MatchStatus status)
    {
        var url = $"/api/dummy/{status.ToString().ToLower()}";

        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        var matches = await JsonSerializer.DeserializeAsync<List<MatchInfo>>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return matches ?? [];
    }
}