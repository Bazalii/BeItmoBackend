using BeItmoBackend.Data.Categories.Models;
using BeItmoBackend.Data.Interests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeItmoBackend.Data.UniversityEvents.Models;

public class UniversityEventDbModel
{
    public Guid Id { get; set; }
    public int CreatorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Place { get; set; } = string.Empty;
    public string Contacts { get; set; } = string.Empty;
    public string PictureLink { get; set; } = string.Empty;
    public virtual CategoryDbModel Category { get; set; } = new();
    public virtual List<InterestDbModel> Interests { get; set; } = new();

    internal class Map : IEntityTypeConfiguration<UniversityEventDbModel>
    {
        public void Configure(EntityTypeBuilder<UniversityEventDbModel> builder)
        {
            builder
                .HasOne(dbModel => dbModel.Category)
                .WithMany();

            builder
                .HasMany(dbModel => dbModel.Interests)
                .WithMany()
                .UsingEntity("events_interests");
        }
    }
}