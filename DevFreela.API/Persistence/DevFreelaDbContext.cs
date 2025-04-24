using DevFreela.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Persistence
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

                    p.HasOne(p => p.Client)
                        .WithMany(u => u.OwnedProjects)
                        .HasForeignKey(p => p.ClientId)
                        .OnDelete(DeleteBehavior.Restrict);

                    p.HasOne(p => p.Freelancer)
                        .WithMany(u => u.FreelanceProjects)
                        .HasForeignKey(p => p.FreelancerId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<Skill>(s =>
                {
                    s.HasKey(s => s.Id);

                    s.HasMany(s => s.UserSkills)
                        .WithOne(us => us.Skill)
                        .HasForeignKey(us => us.SkillId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<User>(u =>
                {
                    u.HasKey(u => u.Id);

                    u.HasMany(u => u.Skills)
                        .WithOne(us => us.User)
                        .HasForeignKey(us => us.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<UserSkill>(us =>
                {
                    us.HasKey(us => us.Id);
                });

            builder
                .Entity<ProjectComment>(pc =>
                {
                    pc.HasKey(pc => pc.Id);

                    pc.HasOne(pc => pc.User)
                        .WithMany(u => u.Comments)
                        .HasForeignKey(pc => pc.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

                    pc.HasOne(pc => pc.Project)
                        .WithMany(p => p.Comments)
                        .HasForeignKey(pc => pc.ProjectId)
                        .OnDelete(DeleteBehavior.Restrict);
                });            

            base.OnModelCreating(builder);
        }
    }
}