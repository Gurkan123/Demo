
using Demo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<Role> Roles { get; set; }
        public DbSet<Perm> Perms { get; set; } 
        public DbSet<RolePerm> RolePerms { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
            .HasOne(a => a.Roles)
            .WithMany(c => c.Users)
            .HasForeignKey(c => c.RoleId);

            builder.Entity<RolePerm>()
            .HasKey(t => new { t.RoleId, t.PermId});

            builder.Entity<RolePerm>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePerms)
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePerm>()
                .HasOne(rp => rp.Perm)
                .WithMany(p => p.RolePerms)
                .HasForeignKey(rp => rp.PermId);
        }
        
    }
}