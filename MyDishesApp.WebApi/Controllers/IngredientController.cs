using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyDishesApp.WebApi.Dtos;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Services;

namespace MyDishesApp.WebApi.Controllers
{
    /// <summary>
    /// The ingredient controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/dishes/{dishId}/ingredients")]
    [ApiController]
    [Authorize]
    public class IngredientController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IDishRepository _dishRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="IngredientController" />
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="mapper">The mapper to use</param>
        /// <param name="ingredientRepository">The repository to use</param>
        /// <param name="dishRepository">The repository to use</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ingredientRepository" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishRepository" /> is null.</exception>
        public IngredientController(ILogger<IngredientController> logger, IMapper mapper, IIngredientRepository ingredientRepository, IDishRepository dishRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
        }

        // Get
        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> GetIngredientForDish(int dishId, int ingredientId)
        {
            if (!await _dishRepository.DishExists(dishId))
            {
                return NotFound();
            }

            var ingredientFromRepo = await _ingredientRepository.GetIngredientForDishAsync(dishId, ingredientId);

            if (ingredientFromRepo == null)
            {
                return BadRequest();
            }

            return Ok(Mapper.Map<IngredientDto>(ingredientFromRepo));
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredientsForDish(int dishId)
        {
            if (!await _dishRepository.DishExists(dishId))
            {
                return NotFound();
            }

            var ingredientsFromRepo = await _ingredientRepository.GetIngredientsForDishAsync(dishId);

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

            if (!await _dishRepository.DishExists(dishId))
            {
                return NotFound();
            }

            // Get ingredient entity from repo, to be patched.
            Ingredient ingredientFromRepo = await _ingredientRepository.GetIngredientForDishAsync(dishId, ingredientId);

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

            if (!await _ingredientRepository.SaveAsync())
            {
                throw new Exception("Updating an ingredient failed on save.");
            }

            return NoContent();
        }

        // Delete
        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteIngredientFromDish(int dishId, int ingredientId)
        {
            if (!await _dishRepository.DishExists(dishId))
            {
                return NotFound();
            }

            var ingredientFromRepository = await _ingredientRepository.GetIngredientForDishAsync(dishId, ingredientId);
            if (ingredientFromRepository == null)
            {
                return BadRequest();
            }

            _ingredientRepository.DeleteIngredientFromDish(ingredientFromRepository);

            if (!await _ingredientRepository.SaveAsync())
            {
                throw new Exception("Deleting an ingredient from dish failed on save.");
            }

            // add logger / mailservice?

            return NoContent();
        }
    }
}