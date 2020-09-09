using System.ComponentModel.DataAnnotations;

namespace MyDishesApp.WebApi.Dtos
{
    public class IngredientDto : IngredientAbstractBaseDto
    {
        public int IngredientId { get; set; }
    }
}
