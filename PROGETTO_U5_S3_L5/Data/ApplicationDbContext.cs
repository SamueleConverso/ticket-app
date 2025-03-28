using PROGETTO_U5_S3_L5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PROGETTO_U5_S3_L5.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>> {

        public ApplicationDbContext() {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<Artista> Artisti {
            get; set;
        }

        public DbSet<Evento> Eventi {
            get; set;
        }

        public DbSet<Biglietto> Biglietti {
            get; set;
        }

        public DbSet<ApplicationUser> ApplicationUsers {
            get; set;
        }

        public DbSet<ApplicationRole> ApplicationRoles {
            get; set;
        }

        public DbSet<ApplicationUserRole> ApplicationUserRoles {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>().HasOne(e => e.Artista).WithMany(a => a.Eventi).HasForeignKey(e => e.ArtistaId);

            modelBuilder.Entity<Biglietto>().HasOne(b => b.Evento).WithMany(e => e.Biglietti).HasForeignKey(b => b.EventoId);

            modelBuilder.Entity<Biglietto>().HasOne(b => b.ApplicationUser).WithMany(u => u.Biglietti).HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Biglietto>().Property(p => p.DataAcquisto).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.User).WithMany(u => u.ApplicationUserRoles).HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.Role).WithMany(r => r.ApplicationUserRole).HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<ApplicationUserRole>().Property(p => p.Date).HasDefaultValueSql("GETDATE()").IsRequired(true);
        }
    }
}
