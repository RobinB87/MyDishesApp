using System.Collections.Generic;
using System.Threading.Tasks;
using MyDishesApp.Repository.Data.Entities;

namespace MyDishesApp.Repository.Services
{
    /// <summary>
    /// The dish repository
    /// </summary>
    public interface IIngredientRepository
    {
        //Task<IEnumerable<Ingredient>> GetIngredientsForDishAsync(int dishId);
        //Task<IEnumerable<Ingredient>> GetIngredientsForDishAsync(int dishId, IEnumerable<int> ingredientIds);
        //Task<Ingredient> GetIngredientForDishAsync(int dishId, int ingredientId);
        //void DeleteIngredientFromDish(Ingredient ingredient);
        //Task AddIngredientOrIngredientCollectionToDishAndSumUpDuplicateQuantitiesAsync(IEnumerable<Ingredient> newIngredientEntities, int dishId);

        /// <summary>
        /// Method for saving the context
        /// </summary>
        Task<bool> SaveAsync();
    }
}
