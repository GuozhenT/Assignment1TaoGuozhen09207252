using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZFishingPointManage.Models.Domain;

namespace NZFishingPointManage.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FishingPoint> FishingPoints { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}