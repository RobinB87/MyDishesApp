using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDishesApp.API.Database;
using MyDishesApp.API.Database.Entities;

namespace MyDishesApp.API.Services
{
    public class DishInfoRepository : IDishInfoRepository
    {
        // Instanciate DishInfoContext, which enables connection of the entities with the database
        private readonly DishInfoContext _context;

        public DishInfoRepository(DishInfoContext context)
        {
            _context = context;
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


        // Ingredients CRUD
        public async Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId)
        {
            return await _context.Ingredients.Where(i => i.DishId == dishId).ToListAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId, IEnumerable<int> ingredientIds)
        {
            return await _context.Ingredients
                 .Where(i => i.DishId == dishId && ingredientIds.Contains(i.IngredientId)).ToListAsync();
        }

        public async Task<Ingredient> GetIngredientForDish(int dishId, int ingredientId)
        {
            return await _context.Ingredients
                .Where(i => i.DishId == dishId && i.IngredientId == ingredientId).FirstOrDefaultAsync();
        }

        public async Task AddIngredientToDish(int dishId, Ingredient ingredient)
        {
            var dish = await GetDish(dishId);
            if (dish == null)
            {
                // throw an exception - this is a race condition
                // that's mostly caught by checking if the tour exists
                // right before calling into this method - if that method is not
                // called the condition can happen, otherwise the tour
                // will already be loaded on the context
                throw new Exception($"Cannot add ingredient to dish with id {dishId}: dish not found.");
            }
            dish.Ingredients.Add(ingredient);
        }

        // PUT UpdateIngredient
        // PATCH PartiallyUpdateIngredient

#pragma warning disable 1998
        public async Task DeleteIngredientFromDish(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
        }
#pragma warning restore 1998

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task AddIngredientOrIngredientCollectionToDishAndSumUpDuplicateQuantities(IEnumerable<Ingredient> newIngredientEntities, int dishId)
        {
            // Get ingredients for dish and transform IEnumerable to List (for modification of list)
            IEnumerable<Ingredient> existingIngredients = await GetIngredientsForDish(dishId);
            List<Ingredient> existingIngredientsToBeModified = existingIngredients.ToList();

            // Loop over new Ingredients and compare with existing ingredients.
            foreach (Ingredient newIngredient in newIngredientEntities)
            {
                bool ingredientExists = false;
                foreach (Ingredient existingIngredient in existingIngredientsToBeModified)
                {
                    if (newIngredient.Name != existingIngredient.Name)
                    {
                        continue;
                    }
                    // If new and existing name match, sum up quantitiy and save.
                    existingIngredient.Quantity += newIngredient.Quantity;
                    ingredientExists = true;
                    if (!await SaveAsync())
                    {
                        throw new Exception("Adding a collection of ingredients failed on save.");
                    }
                }

                if (!ingredientExists)
                {
                    existingIngredientsToBeModified.Add(newIngredient);
                    await AddIngredientToDish(dishId, newIngredient);
                    if (!await SaveAsync())
                    {
                        throw new Exception("Adding a collection of ingredients failed on save.");
                    }
                }
            }
        }
    }
}
