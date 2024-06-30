using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(m => m.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Age)
                .IsRequired();
        }
    }
}
