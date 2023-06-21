using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeItmoBackend.Data.UniversityEvents.Models;

public class AttendedUniversityEventDbModel
{
    public int UserId { get; set; }
    public Guid EventId { get; set; }
    public int Score { get; set; }

    internal class Map : IEntityTypeConfiguration<AttendedUniversityEventDbModel>
    {
        public void Configure(EntityTypeBuilder<AttendedUniversityEventDbModel> builder)
        {
            builder.HasKey(entity => new { entity.UserId, entity.EventId });
        }
    }
}