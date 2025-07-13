namespace ESportsMatchTracker.API.Models;

public class MapScore
{
    public string Map { get; set; }
    public Dictionary<string, int> Score { get; set; }
}