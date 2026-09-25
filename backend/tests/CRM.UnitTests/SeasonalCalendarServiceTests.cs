using CRM.Domain.Enums;
using CRM.Infrastructure.Services;

namespace CRM.UnitTests;

public class SeasonalCalendarServiceTests
{
    private readonly SeasonalCalendarService service = new();

    [Fact]
    public void February_28_Is_Winter()
    {
        var state = service.GetSeasonState(new DateTime(2026, 2, 28));

        Assert.Equal(Season.Winter, state.Season);
        Assert.Equal(SeasonalEvent.None, state.Event);
    }

    [Fact]
    public void March_1_Starts_Spring()
    {
        var state = service.GetSeasonState(new DateTime(2026, 3, 1));

        Assert.Equal(Season.Spring, state.Season);
        Assert.Equal(0d, state.Transition);
    }

    [Fact]
    public void December_20_Starts_New_Year_Event()
    {
        var state = service.GetSeasonState(new DateTime(2026, 12, 20));

        Assert.Equal(Season.Winter, state.Season);
        Assert.Equal(SeasonalEvent.NewYear, state.Event);
    }

    [Fact]
    public void January_10_Is_Still_New_Year_Event()
    {
        var state = service.GetSeasonState(new DateTime(2027, 1, 10));

        Assert.Equal(SeasonalEvent.NewYear, state.Event);
    }

    [Fact]
    public void January_11_Ends_New_Year_Event()
    {
        var state = service.GetSeasonState(new DateTime(2027, 1, 11));

        Assert.Equal(SeasonalEvent.None, state.Event);
    }
}