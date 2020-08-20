using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyDishesApp.API.Database.Entities;
using MyDishesApp.API.Dtos;
using MyDishesApp.API.Services;
using System;
using System.Threading.Tasks;

namespace MyDishesApp.API.Controllers
{
    [Route("api/dishes/{dishId}/ingredients")]
    [Authorize]
    public class IngredientsController : Controller
    {
        // Call repository            
        private readonly IDishInfoRepository _dishInfoRepository;

        // Create constructor
        public IngredientsController(IDishInfoRepository dishInfoRepository)
        {
            _dishInfoRepository = dishInfoRepository;
        }

        // Get
        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> GetIngredientForDish(int dishId, int ingredientId)
        {
            if (!await _dishInfoRepository.DishExists(dishId))
            {
                return NotFound();
            }

            var ingredientFromRepo = await _dishInfoRepository.GetIngredientForDish(dishId, ingredientId);

            if (ingredientFromRepo == null)
            {
                return BadRequest();
            }

            return Ok(Mapper.Map<IngredientDto>(ingredientFromRepo));
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredientsForDish(int dishId)
        {
            if (!await _dishInfoRepository.DishExists(dishId))
            {
                return NotFound();
            }

            var ingredientsFromRepo = await _dishInfoRepository.GetIngredientsForDish(dishId);

            if (ingredientsFromRepo == null)
            {
                return BadRequest();
            }

            return Ok(Mapper.Map<IngredientDto[]>(ingredientsFromRepo));
        }
       
        // Patch
        [HttpPatch("{ingredientId}")]
        public async Task<IActionResult> PartiallyUpdateIngredient(int dishId, int ingredientId,
            [FromBody] JsonPatchDocument<IngredientForUpdateDto> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest();
            }

            if (!await _dishInfoRepository.DishExists(dishId))
            {
                return NotFound();
            }

            // Get ingredient entity from repo, to be patched.
            Ingredient ingredientFromRepo = await _dishInfoRepository.GetIngredientForDish(dishId, ingredientId);

            if (ingredientFromRepo == null)
            {
                return BadRequest();
            }

            IngredientForUpdateDto ingredientToPatch = Mapper.Map<IngredientForUpdateDto>(ingredientFromRepo);

            jsonPatchDocument.ApplyTo(ingredientToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(ingredientToPatch))
            {
                return BadRequest();
            }

            Mapper.Map(ingredientToPatch, ingredientFromRepo);

            if (!await _dishInfoRepository.SaveAsync())
            {
                throw new Exception("Updating an ingredient failed on save.");
            }

            return NoContent();
        }


        // Delete
        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteIngredientFromDish(int dishId, int ingredientId)
        {
            if (!await _dishInfoRepository.DishExists(dishId))
            {
                return NotFound();
            }

            var ingredientFromRepository = await _dishInfoRepository.GetIngredientForDish(dishId, ingredientId);
            if (ingredientFromRepository == null)
            {
                return BadRequest();
            }

            await _dishInfoRepository.DeleteIngredientFromDish(ingredientFromRepository);

            if (!await _dishInfoRepository.SaveAsync())
            {
                throw new Exception("Deleting an ingredient from dish failed on save.");
            }

            // add logger / mailservice?

            return NoContent();
        }
    }
}