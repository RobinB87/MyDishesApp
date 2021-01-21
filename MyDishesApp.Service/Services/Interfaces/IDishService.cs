using MyDishesApp.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Service.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishDto>> GetDishListAsync();
    }
}
