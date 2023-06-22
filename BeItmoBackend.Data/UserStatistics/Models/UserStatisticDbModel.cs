using BeItmoBackend.Core.UserAnalytics.UserStatistics.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeItmoBackend.Data.UserStatistics.Models;

public class UserStatisticDbModel
{
    public Guid TypeValueId { get; set; }
    public int UserId { get; set; }
    public StatisticType Type { get; set; }
    public int TapCounter { get; set; }
    public int PrizeCounter { get; set; }

    internal class Map : IEntityTypeConfiguration<UserStatisticDbModel>
    {
        public void Configure(EntityTypeBuilder<UserStatisticDbModel> builder)
        {
            builder.HasKey(entity => new { entity.TypeValueId, entity.UserId });
        }
    }
}