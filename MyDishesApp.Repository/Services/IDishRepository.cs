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
        /// <summary>
        /// Get all dishes
        /// </summary>
        Task<IEnumerable<Dish>> GetDishesAsync();

        /// <summary>
        /// Get a dish by id
        /// </summary>
        /// <param name="id">The dish id</param>
        Task<Dish> GetDishAsync(int id);

        //Task AddDishAsync(Dish dish);

        //void DeleteDish(Dish dish);

        /// <summary>
        /// Method for saving the context
        /// </summary>
        Task<bool> SaveAsync();
    }
}
