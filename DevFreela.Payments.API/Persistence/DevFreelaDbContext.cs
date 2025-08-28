using DevFreela.Payments.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Payments.API.Persistence;

public class DevFreelaDbContext : DbContext
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Project>(p =>
            {
                p.HasKey(p => p.Id);

                p.Property(p => p.Id)
                    .HasColumnType("bigint")
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                p.Property(p => p.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired();

                p.Property(p => p.DeletedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false);

                p.Property(p => p.Title)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                p.Property(p => p.Description)
                    .HasColumnType("varchar(5000)")
                    .IsRequired();

                p.Property(p => p.ClientId)
                    .HasColumnType("bigint")
                    .IsRequired();

                p.Property(p => p.FreelancerId)
                    .HasColumnType("bigint")
                    .IsRequired();

                p.Property(p => p.TotalCost)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                p.Property(p => p.StartedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false);

                p.Property(p => p.CompletedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false);

                p.Property(p => p.Status)
                    .HasConversion<string>()
                    .HasColumnType("varchar(20)")
                    .IsRequired();
            });

        base.OnModelCreating(builder);
    }
}
