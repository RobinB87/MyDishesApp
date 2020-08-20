using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDishesApp.API.Database.Entities;
using MyDishesApp.API.Dtos;
using MyDishesApp.API.Helpers;
using MyDishesApp.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDishesApp.API.Controllers
{
    [Route("api/dishes/{dishId}/ingredientcollections")]
    [Authorize]
    public class IngredientCollectionsController : Controller
    {
        private readonly IDishInfoRepository _dishInfoRepository;

        public IngredientCollectionsController(IDishInfoRepository dishInfoRepository)
        {
            _dishInfoRepository = dishInfoRepository;
        }

        // Get
        [HttpGet("({ingredientIds})", Name = "GetIngredientCollection")]
        public async Task<IActionResult> GetIngredientCollection(int dishId,
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ingredientIds)
        {
            if (ingredientIds == null || !ingredientIds.Any())
            {
                return BadRequest();
            }

            if (!await _dishInfoRepository.DishExists(dishId))
            {
                return NotFound();
            }

            var ingredientEntities = await _dishInfoRepository.GetIngredientsForDish(dishId, ingredientIds);

            if (ingredientIds.Count() != ingredientEntities.Count())
            {
                return NotFound();
            }

            var ingredientCollectionToReturn = Mapper.Map<IEnumerable<IngredientDto>>(ingredientEntities);
            return Ok(ingredientCollectionToReturn);
        }

        // Post IngredientCollection
        [HttpPost]
        public async Task<IActionResult> CreateIngredientCollection(
           int dishId,
           [FromBody] IEnumerable<IngredientForCreationDto> ingredientCollection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (ingredientCollection == null)
            {
                return BadRequest();
            }

            if (!await _dishInfoRepository.DishExists(dishId))
            {
                return NotFound();
            }

            // Map IngredientCollection (Dto) to Entity
            IEnumerable<Ingredient> newIngredientEntities = Mapper.Map<IEnumerable<Ingredient>>(ingredientCollection);

            // Add ingredients to dish. If ingredientname already exists, quantities will be summed up.
            try
            {
                await _dishInfoRepository.AddIngredientOrIngredientCollectionToDishAndSumUpDuplicateQuantities(newIngredientEntities, dishId);
            }
            catch (Exception ex)
            {
                throw new Exception("Adding a collection of ingredients failed.", ex);
            }

            // return 201 created
            var ingredientCollectionToReturn = Mapper.Map<IEnumerable<IngredientDto>>(newIngredientEntities);

            var ingredientIdsAsString = string.Join(",", ingredientCollectionToReturn.Select(a => a.IngredientId));

            return CreatedAtRoute("GetIngredientCollection",
                new { dishId, ingredientIds = ingredientIdsAsString },
                ingredientCollectionToReturn);
        }
    }
}
