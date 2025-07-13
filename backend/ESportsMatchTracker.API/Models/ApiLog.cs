namespace ESportsMatchTracker.API.Models;

public class ApiLog
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Path { get; set; }
    public string Method { get; set; }
    public int StatusCode { get; set; }
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
}