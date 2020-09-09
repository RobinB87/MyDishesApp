using Microsoft.EntityFrameworkCore;
using MyDishesApp.Repository.Data.Entities;

namespace MyDishesApp.Repository.Data
{
    /// <summary>
    /// Class containing database context and methods for creation and migrations.
    /// </summary>
    public sealed class DishesContext : DbContext
    {
        public DishesContext(DbContextOptions<DishesContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>()
                .HasIndex(d => d.Name)
                .IsUnique();
        }
    }
}
