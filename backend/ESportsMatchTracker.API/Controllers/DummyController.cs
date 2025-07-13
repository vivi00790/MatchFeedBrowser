using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ESportsMatchTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DummyController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public DummyController(IWebHostEnvironment env)
    {
        _env = env;
    }

    private async Task<IActionResult> ReturnJsonFileAsync(string fileName)
    {
        var path = Path.Combine(_env.ContentRootPath, "DummyData", fileName);
        if (!System.IO.File.Exists(path))
            return NotFound($"File {fileName} not found.");

        var json = await System.IO.File.ReadAllTextAsync(path);
        return Ok(json);
    }

    [HttpGet("scheduled")]
    public async Task<IActionResult> GetScheduled()
    {
        return await ReturnJsonFileAsync("matches-scheduled.json");
    }

    [HttpGet("live")]
    public async Task<IActionResult> GetLive()
    {
        return await ReturnJsonFileAsync("matches-live.json");
    }

    [HttpGet("ended")]
    public async Task<IActionResult> GetEnded()
    {
        return await ReturnJsonFileAsync("matches-ended.json");
    }
}