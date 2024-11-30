using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publications.Main.Domain.Entities;

namespace Publications.Main.Infrastructure.Database.EntityConfigurations;

public class PublicationConfig : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {
        builder.ToTable(nameof(Publication));
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.CountOfLikes);
    }
}
