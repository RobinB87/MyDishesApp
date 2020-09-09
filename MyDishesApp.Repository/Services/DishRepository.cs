using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDishesApp.Repository.Data;
using MyDishesApp.Repository.Data.Entities;

namespace MyDishesApp.Repository.Services
{
    /// <inheritdoc />
    public class DishRepository : IDishRepository
    {
        private readonly DishesContext _context;

        /// <summary>
        /// Constructor with the dishes context
        /// </summary>
        /// <param name="context">The dishes context</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="context"/> is null</exception>
        public DishRepository(DishesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> DishExists(int dishId)
        {
            return await _context.Dishes.AnyAsync(d => d.DishId == dishId);
        }

        // TODO: Niet ToListen, dat moet je pas in de controller doen ivm performance.
        // Dishes CRUD
        public async Task<IEnumerable<Dish>> GetDishes()
        {
            return await _context.Dishes.Include(i => i.Ingredients)
                .OrderBy(d => d.Name).ToListAsync();
        }

        public async Task<Dish> GetDish(int dishId)
        {
            return await _context.Dishes.Include(i => i.Ingredients)
                .Where(d => d.DishId == dishId).FirstOrDefaultAsync();
        }

        public async Task AddDish(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
        }

        // PUT UpdateDish - update fully
        // PATCH PartiallyUpdateDish - update partially

#pragma warning disable 1998
        // disable async warning - no code 
        public async Task UpdateDish(Dish dish)
        {
            // no code in this implementation
        }
#pragma warning restore 1998

#pragma warning disable 1998
        public async Task DeleteDish(Dish dish)
        {
            _context.Dishes.Remove(dish);
        }
#pragma warning restore 1998
        // pragma warning disable ivm await warning.

        /// <inheritdoc />
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
