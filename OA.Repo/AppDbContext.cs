using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OA.Data.Maps;
using OA.Data.Models;

namespace OA.Data
{
    public class AppDbContext: IdentityDbContext<AspNetUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AspNetUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new AspNetUserMap(modelBuilder.Entity<AspNetUser>());
        }
    }
}
