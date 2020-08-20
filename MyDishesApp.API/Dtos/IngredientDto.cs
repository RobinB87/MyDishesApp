using System.ComponentModel.DataAnnotations;

namespace MyDishesApp.API.Dtos
{
    public class IngredientDto : IngredientAbstractBaseDto
    {
        public int IngredientId { get; set; }
    }
}
