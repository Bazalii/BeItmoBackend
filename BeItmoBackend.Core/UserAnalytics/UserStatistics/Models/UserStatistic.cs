using BeItmoBackend.Core.UserAnalytics.UserStatistics.Enums;

namespace BeItmoBackend.Core.UserAnalytics.UserStatistics.Models;

public class UserStatistic
{
    public Guid TypeValueId { get; set; }
    public int UserId { get; set; }
    public StatisticType Type { get; set; }
    public int TapCounter { get; set; }
    public int PrizeCounter { get; set; }
}