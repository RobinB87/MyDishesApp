using System.Collections.Generic;
using System.Threading.Tasks;
using MyDishesApp.Repository.Data.Entities;

namespace MyDishesApp.Repository.Services
{
    /// <summary>
    /// The dish repository
    /// </summary>
    public interface IDishRepository
    {
        Task<bool> DishExists(int dishId);

        /// <summary>
        /// Get all dishes
        /// </summary>
        Task<IEnumerable<Dish>> GetDishesAsync();
        Task<Dish> GetDishAsync(int dishId);
        Task AddDishAsync(Dish dish);
        void DeleteDish(Dish dish);

        /// <summary>
        /// Method for saving the context
        /// </summary>
        Task<bool> SaveAsync();
    }
}
