using AutoMapper;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.WebApi.Dtos;
using System.Linq;

namespace MyDishesApp.WebApi
{
    /// <summary>
    /// Provides configuration for mappings between entities and dtos
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            // Source, Destination
            CreateMap<Dish, DishDto>()
                .ForMember(d => d.Ingredients,  o => o.MapFrom(s => s.DishIngredients.Select(y => y.Ingredient).ToList()));

            CreateMap<DishDto, Dish>()
                .ForMember(d => d.DishIngredients, o => o.Ignore());

            CreateMap<IngredientDto, Ingredient>()
                .ForMember(d => d.DishIngredients, o => o.Ignore());
        }

        //CreateMap<Ingredient, IngredientDto>()
        //    .ForMember(d => d.Quantity, o => o.MapFrom(s => s.DishIngredients.Select(i => i.Quantity).FirstOrDefault()));
    }
}
