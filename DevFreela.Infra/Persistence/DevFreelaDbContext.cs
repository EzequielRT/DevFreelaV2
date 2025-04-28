using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

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

                    p.HasOne(p => p.Client)
                        .WithMany(u => u.OwnedProjects)
                        .HasForeignKey(p => p.ClientId)
                        .OnDelete(DeleteBehavior.Restrict);

                    p.HasOne(p => p.Freelancer)
                        .WithMany(u => u.FreelanceProjects)
                        .HasForeignKey(p => p.FreelancerId)
                        .OnDelete(DeleteBehavior.Restrict);

                    p.HasMany(p => p.Comments)
                        .WithOne(pc => pc.Project)
                        .HasForeignKey(pc => pc.ProjectId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<ProjectComment>(pc =>
                {
                    pc.HasKey(pc => pc.Id);

                    pc.Property(pc => pc.Id)
                        .HasColumnType("bigint")
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                    pc.Property(pc => pc.CreatedAt)
                        .HasColumnType("datetime2")
                        .IsRequired();

                    pc.Property(pc => pc.DeletedAt)
                        .HasColumnType("datetime2")
                        .IsRequired(false);

                    pc.Property(pc => pc.Content)
                        .HasColumnType("varchar(2000)")
                        .IsRequired();

                    pc.Property(pc => pc.ProjectId)
                        .HasColumnType("bigint")
                        .IsRequired();

                    pc.Property(pc => pc.UserId)
                        .HasColumnType("bigint")
                        .IsRequired();
                });

            builder
                .Entity<Skill>(s =>
                {
                    s.HasKey(s => s.Id);

                    s.Property(s => s.Id)
                        .HasColumnType("bigint")
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                    s.Property(s => s.CreatedAt)
                        .HasColumnType("datetime2")
                        .IsRequired();

                    s.Property(s => s.DeletedAt)
                        .HasColumnType("datetime2")
                        .IsRequired(false);

                    s.Property(s => s.Description)
                        .HasColumnType("varchar(250)")
                        .IsRequired();

                    s.HasMany(s => s.UserSkills)
                        .WithOne(us => us.Skill)
                        .HasForeignKey(us => us.SkillId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<User>(u =>
                {
                    u.HasKey(u => u.Id);

                    u.Property(u => u.Id)
                        .HasColumnType("bigint")
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                    u.Property(u => u.CreatedAt)
                        .HasColumnType("datetime2")
                        .IsRequired();

                    u.Property(u => u.DeletedAt)
                        .HasColumnType("datetime2")
                        .IsRequired(false);

                    u.Property(u => u.FullName)
                        .HasColumnType("varchar(250)")
                        .IsRequired();

                    u.Property(u => u.Email)
                        .HasColumnType("varchar(100)")
                        .IsRequired();

                    u.Property(u => u.BirthDate)
                        .HasColumnType("datetime2")
                        .IsRequired();

                    u.Property(u => u.Active)
                        .HasColumnType("bit")
                        .IsRequired();

                    u.HasMany(u => u.Skills)
                        .WithOne(us => us.User)
                        .HasForeignKey(us => us.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

                    u.HasMany(u => u.Comments)
                        .WithOne(pc => pc.User)
                        .HasForeignKey(pc => pc.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<UserSkill>(us =>
                {
                    us.HasKey(us => us.Id);

                    us.Property(us => us.Id)
                        .HasColumnType("bigint")
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                    us.Property(us => us.CreatedAt)
                        .HasColumnType("datetime2")
                        .IsRequired();

                    us.Property(us => us.DeletedAt)
                        .HasColumnType("datetime2")
                        .IsRequired(false);

                    us.Property(us => us.UserId)
                        .HasColumnType("bigint")
                        .IsRequired();

                    us.Property(us => us.SkillId)
                        .HasColumnType("bigint")
                        .IsRequired();
                });  

            base.OnModelCreating(builder);
        }
    }
}