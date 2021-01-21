using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyDishesApp.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Service.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishDto>> GetAllAsync();
        Task<DishDto> GetById(int id);
        Task<bool> DishExists(string name, ModelStateDictionary modelState);
        Task<DishDto> PostAsync(DishDto dish);
        Task DeleteAsync(int id);
    }
}
