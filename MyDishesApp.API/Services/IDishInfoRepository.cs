using MyDishesApp.API.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

/* IDishInfoRepository creates abstraction layer between controller and database context. 
 * It contains the methods from the controller. */

namespace MyDishesApp.API.Services
{
    public interface IDishInfoRepository
    {
        Task<bool> DishExists(int dishId);
        Task<IEnumerable<Dish>> GetDishes();
        Task<Dish> GetDish(int dishId);

        Task AddDish(Dish dish);
        Task UpdateDish(Dish dish);
        Task DeleteDish(Dish dish);

        Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId);
        Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId, IEnumerable<int> ingredientIds);

        Task<Ingredient> GetIngredientForDish(int dishId, int ingredientId);
        Task AddIngredientToDish(int dishId, Ingredient ingredient);
        // PUT UpdateIngredient
        Task DeleteIngredientFromDish(Ingredient ingredient);

        Task<bool> SaveAsync();

        Task AddIngredientOrIngredientCollectionToDishAndSumUpDuplicateQuantities(IEnumerable<Ingredient> newIngredientEntities, int dishId);
    }
}
