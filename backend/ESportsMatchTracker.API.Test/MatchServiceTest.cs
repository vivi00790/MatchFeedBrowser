using ESportsMatchTracker.API.Enums;
using ESportsMatchTracker.API.Models;
using ESportsMatchTracker.API.Proxies;
using ESportsMatchTracker.API.Services;
using NSubstitute;

namespace ESportsMatchTracker.API.Test;

public class MatchServiceTest
{
    private IDummyMatchProxy _proxy;

    [SetUp]
    public void Setup()
    {
        _proxy = Substitute.For<IDummyMatchProxy>();
    }

    // Tests showcase for how to test easily with NSubstitute and DI
    [Test]
    public async Task ServiceShouldCallProxy()
    {
        _proxy.FetchMatchesAsync<LiveMatchInfo>(Arg.Any<MatchStatus>()).Returns(Task.FromResult(new List<LiveMatchInfo>
        {
            new LiveMatchInfo
            {
                Id = 10001,
            }
        }));
        var result = await new MatchService(_proxy).FetchMatchesAsync<LiveMatchInfo>(MatchStatus.Live);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(10001));
        await _proxy.Received(1).FetchMatchesAsync<LiveMatchInfo>(MatchStatus.Live);
    }
}