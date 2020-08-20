using Microsoft.EntityFrameworkCore;
using MyDishesApp.API.Database.Entities;

/* This class is for database creation and migrations.
 *
 * Intuitively, a DbContext corresponds to your database (or a collection of tables and views 
 * in your database) whereas a DbSet corresponds to a table or view in your database. 
 * 
 * You will be using a DbContext object to GET ACCESS to your tables and views 
 * (which will be represented by DbSet's) and you will be using your DbSet's to 
 * get access, create, update, delete and modify your table data. */

namespace MyDishesApp.API.Database
{
    public class DishInfoContext : DbContext
    {
        public DishInfoContext(DbContextOptions<DishInfoContext> options) : base(options)
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
