using BeItmoBackend.Data.Categories.Models;
using BeItmoBackend.Data.Interests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeItmoBackend.Data.Users.Models;

public class UserDbModel
{
    public int Id { get; set; }
    public int FriendlinessScore { get; set; }
    public int HealthScore { get; set; }
    public int FitScore { get; set; }
    public int EcoScore { get; set; }
    public int OpenScore { get; set; }
    public int ProScore { get; set; }
    public virtual List<CategoryDbModel> Categories { get; set; } = new();
    public virtual List<InterestDbModel> Interests { get; set; } = new();
    
    internal class Map : IEntityTypeConfiguration<UserDbModel>
    {
        public void Configure(EntityTypeBuilder<UserDbModel> builder)
        {
            builder
                .HasMany(dbModel => dbModel.Categories)
                .WithMany();

            builder
                .HasMany(dbModel => dbModel.Interests)
                .WithMany();
        }
    }
}