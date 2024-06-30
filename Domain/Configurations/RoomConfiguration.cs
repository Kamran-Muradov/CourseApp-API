using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.SeatCount)
                .IsRequired();
        }
    }
}
