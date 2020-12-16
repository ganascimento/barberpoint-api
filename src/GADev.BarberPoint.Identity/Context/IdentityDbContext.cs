using GADev.BarberPoint.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GADev.BarberPoint.Identity.Context
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> optionsBuilder) : base(optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("tb_user");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.UserName).HasColumnName("user_name");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.NormalizedUserName).HasColumnName("normalized_user_name");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.Email).HasColumnName("email");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.NormalizedEmail).HasColumnName("normalized_email");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.EmailConfirmed).HasColumnName("email_confirmed");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.CreateAt).HasColumnName("create_at");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.UpdateAt).HasColumnName("update_at");
                
            modelBuilder.Entity<ApplicationRole>().ToTable("tb_role");
            modelBuilder.Entity<ApplicationRole>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<ApplicationRole>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<ApplicationRole>().Property(p => p.NormalizedName).HasColumnName("normalized_name");
        }
    }
}