using Microsoft.EntityFrameworkCore;

namespace Mission08_Group2_14.Models
{
    public class QuadrantContext : DbContext
    {
        public QuadrantContext(DbContextOptions<QuadrantContext> options) : base(options)
        {

        }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Categories> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().HasData(
                new Categories { CategoryId = 1, CategoryName = "Home" },
                new Categories { CategoryId = 2, CategoryName = "School" },
                new Categories { CategoryId = 3, CategoryName = "Work" },
                new Categories { CategoryId = 4, CategoryName = "Church" }
                );
        }
    }
}
