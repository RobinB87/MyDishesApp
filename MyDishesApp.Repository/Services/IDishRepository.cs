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
        Task<IEnumerable<Dish>> GetDishes();
        Task<Dish> GetDish(int dishId);
        Task AddDish(Dish dish);
        Task UpdateDish(Dish dish);
        Task DeleteDish(Dish dish);

        /// <summary>
        /// Method for saving the context
        /// </summary>
        Task<bool> SaveAsync();
    }
}
