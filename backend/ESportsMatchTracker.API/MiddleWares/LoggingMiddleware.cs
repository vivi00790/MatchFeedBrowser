using ESportsMatchTracker.API.DbContexts;
using ESportsMatchTracker.API.Models;

namespace ESportsMatchTracker.API.MiddleWares;

public class LoggingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, LoggingDbContext dbContext)
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        var log = new ApiLog
        {
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path,
            Method = context.Request.Method,
            StatusCode = context.Response.StatusCode,
            RequestBody = requestBody,
            ResponseBody = responseText
        };

        dbContext.ApiLogs.Add(log);
        await dbContext.SaveChangesAsync();

        await responseBody.CopyToAsync(originalBodyStream);
    }
}