using Gikslab.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gikslab.Repository.Data
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SkillData());

            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<UserSkill>().HasKey(us => new { us.UserId, us.SkillId });
            //modelBuilder.Entity<UserActivity>().HasKey(ua => new { ua.UserId, ua.ActivityId });
            modelBuilder.Entity<Skill>().HasMany(s => s.Users).WithMany(s => s.Skills).UsingEntity<UserSkill>();
            modelBuilder.Entity<Activity>().HasMany(a => a.Users).WithMany(a => a.Activities).UsingEntity<UserActivity>();
        }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
    }
}
