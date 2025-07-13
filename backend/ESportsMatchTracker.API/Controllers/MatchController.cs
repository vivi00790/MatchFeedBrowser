using ESportsMatchTracker.API.Enums;
using ESportsMatchTracker.API.Models;
using ESportsMatchTracker.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESportsMatchTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController(IMatchService matchService) : ControllerBase
{
    [HttpGet("{status}")]
    public async Task<IActionResult> GetMatches(MatchStatus status)
    {
        try
        {
            return status switch
            {
                MatchStatus.Scheduled => Ok(await matchService.FetchMatchesAsync<ScheduledMatchInfo>(status)),
                MatchStatus.Live => Ok(await matchService.FetchMatchesAsync<LiveMatchInfo>(status)),
                MatchStatus.Ended => Ok(await matchService.FetchMatchesAsync<EndedMatchInfo>(status)),
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}