﻿namespace ESportsMatchTracker.API.Models;

public class EndedMatchInfo : IMatchInfo
{
    public int Id { get; set; }
    public string Game { get; set; }
    public string[] Teams { get; set; }
    public DateTime StartTime { get; set; }
    public string Status { get; set; }
    public string Stage { get; set; }
    public string Tournament { get; set; }
    public string StreamUrl { get; set; }
    public MatchDetails MatchDetails { get; set; }
    public Dictionary<string, int> Score { get; set; }
    public List<MapScore> MapScores { get; set; }
    public string Winner { get; set; }
    
}