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
            //Source, Destination
            CreateMap<Dish, DishDto>()
                .ForMember(d => d.Ingredients, opt => opt.MapFrom(s => s.DishIngredients.Select(y => y.Ingredient).ToList()));

            CreateMap<Ingredient, IngredientDto>();
        }

        //    config.CreateMap<Dish, DishDto>();
        //    config.CreateMap<Dish, DishForUpdateDto>().ReverseMap();
        //    config.CreateMap<IngredientForCreationDto, Ingredient>();
        //    config.CreateMap<Ingredient, IngredientForUpdateDto>().ReverseMap();
    }
}
