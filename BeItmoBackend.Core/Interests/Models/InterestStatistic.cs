namespace BeItmoBackend.Core.Interests.Models;

public class InterestStatistic
{
    public Guid InterestId { get; set; }
    public int UserId { get; set; }
    public int TapCounter { get; set; }
    public int PrizeCounter { get; set; }
}