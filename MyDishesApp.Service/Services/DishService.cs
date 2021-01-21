using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Repositories.Interfaces;
using MyDishesApp.Service.Dtos;
using MyDishesApp.Service.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Service
{
    public class DishService : IDishService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IDishRepository _dishRepository;
        private readonly IIngredientRepository _ingredientRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="DishService" />
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="dishRepository">The dish repository</param>
        /// <param name="ingredientRepository">The ingredient repository</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishRepository" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ingredientRepository" /> is null.</exception>
        public DishService(ILogger<DishService> logger, IMapper mapper, IDishRepository dishRepository, IIngredientRepository ingredientRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
            _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        }

        public async Task<IEnumerable<DishDto>> GetAllAsync()
        {
            var dishEntities = await _dishRepository.GetDishesAsync();
            return _mapper.Map<IEnumerable<DishDto>>(dishEntities);
        }

        public async Task<DishDto> GetById(int id)
        {
            var dishEntity = await _dishRepository.GetDishAsync(id);
            return _mapper.Map<DishDto>(dishEntity);
        }

        public async Task<bool> DishExists(string name, ModelStateDictionary modelState)
        {
            var exists = await _dishRepository.DishExists(name);
            if (exists)
            {
                modelState.AddModelError("UniqueDishName", "UniqueDish|Dish name already exists. Please provide a different name.");
            }

            return exists;
        }

        public async Task<DishDto> PostAsync(DishDto dish)
        {
            // Map the dish to an entity
            var dishEntity = _mapper.Map<Dish>(dish);

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

            // Save the dish
            await _dishRepository.AddDishAsync(dishEntity);
            try
            {
                await _dishRepository.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Adding a dish failed on save", e);
                throw;
            }

            // Map the new entity back to a dto, to be able to create a 201 response
            return _mapper.Map<DishDto>(dishEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var dishEntity = await _dishRepository.GetDishAsync(id);
            if (dishEntity == null)
            {
                throw new ArgumentException($"Dish with id: '{id}' does not exist.");
            }

            _dishRepository.DeleteDish(dishEntity);

            // Try to save database
            try
            {
                await _dishRepository.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Deleting a dish failed on save.", e);
                throw;
            }
        }
    }
}