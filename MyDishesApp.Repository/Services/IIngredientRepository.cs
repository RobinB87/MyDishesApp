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
        Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId);
        Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId, IEnumerable<int> ingredientIds);
        Task<Ingredient> GetIngredientForDish(int dishId, int ingredientId);
        void DeleteIngredientFromDish(Ingredient ingredient);
        Task AddIngredientOrIngredientCollectionToDishAndSumUpDuplicateQuantities(IEnumerable<Ingredient> newIngredientEntities, int dishId);

        /// <summary>
        /// Method for saving the context
        /// </summary>
        Task<bool> SaveAsync();
    }
}
