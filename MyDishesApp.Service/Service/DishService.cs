using AutoMapper;
using Microsoft.Extensions.Logging;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Repositories.Interfaces;
using MyDishesApp.Service.Service.Interfaces;
using MyDishesApp.Service.Dtos;
using System;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Service
{
    public class DishService : IDishService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IIngredientRepository _ingredientRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="DishService" />
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="mapper">The mapper to use</param>
        /// <param name="ingredientRepository">The repository to use</param>
        /// <param name="dishRepository">The repository to use</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ingredientRepository" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishRepository" /> is null.</exception>
        public DishService(ILogger<DishService> logger, IMapper mapper, IIngredientRepository ingredientRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        }

        public async Task AddDishIngredients(DishDto dish, Dish dishEntity)
        {
            foreach (var ingredient in dish.Ingredients)
            {
                // Check if the ingredient already exists
                var ingredientEntity = await _ingredientRepository.GetIngredientAsync(ingredient.Name);
                if (ingredientEntity == null)
                {
                    ingredientEntity = _mapper.Map<Ingredient>(ingredient);
                }

                // Create the dish ingredient link
                var dishIngredient = new DishIngredient
                {
                    Dish = dishEntity,
                    Ingredient = ingredientEntity,
                    Quantity = ingredient.Quantity
                };

                // Add the link to the dish
                dishEntity.DishIngredients.Add(dishIngredient);
            }
        }
    }
}
