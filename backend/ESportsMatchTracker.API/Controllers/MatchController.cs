using ESportsMatchTracker.API.Enums;
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
            var matches = await matchService.FetchMatchesAsync(status);
            return Ok(matches);
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