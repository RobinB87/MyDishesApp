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
        /// <summary>
        /// Get all ingredients
        /// </summary>
        Task<IEnumerable<Ingredient>> GetIngredients();

        /// <summary>
        /// Gets an engredient by name
        /// </summary>
        /// <param name="name">The ingredient name</param>
        Task<Ingredient> GetIngredient(string name);

        ///// <summary>
        ///// Add an ingredient to the database
        ///// </summary>
        ///// <param name="ingredient">The ingredient</param>
        //Task AddIngredient(Ingredient ingredient);

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
