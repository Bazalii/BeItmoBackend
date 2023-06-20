using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeItmoBackend.Data.Interests.Models;

public class InterestStatisticDbModel
{
    public Guid InterestId { get; set; }
    public int UserId { get; set; }
    public int TapCounter { get; set; }
    public int PrizeCounter { get; set; }

    internal class Map : IEntityTypeConfiguration<InterestStatisticDbModel>
    {
        public void Configure(EntityTypeBuilder<InterestStatisticDbModel> builder)
        {
            builder.HasKey(entity => new { entity.InterestId, entity.UserId });
        }
    }
}