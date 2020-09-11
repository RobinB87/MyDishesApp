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
        public DbSet<DishIngredient> DishIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>()
                .HasIndex(d => d.Name)
                .IsUnique();

            modelBuilder.Entity<Ingredient>()
                .HasIndex(d => d.Name)
                .IsUnique();

            modelBuilder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });
            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId);
            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId);
        }
    }
}
