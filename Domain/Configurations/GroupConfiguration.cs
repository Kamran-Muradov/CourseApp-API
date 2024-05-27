using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Capacity)
                .IsRequired();
        }
    }
}
