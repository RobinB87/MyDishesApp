using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Service.Dtos;
using MyDishesApp.Service.Service.Interfaces;
using MyDishesApp.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDishesApp.WebApi.Controllers
{
    /// <summary>
    /// The dish controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DishController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IDishService _dishService;

        /// <summary>
        /// Initializes a new instance of <see cref="DishController" />
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="mapper">The mapper to use</param>
        /// <param name="dishService">The dish service</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishService" /> is null.</exception>
        public DishController(ILogger<DishController> logger, IMapper mapper, IDishService dishService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dishService = dishService ?? throw new ArgumentNullException(nameof(dishService));
        }

        /// <summary>
        /// Get all dishes
        /// </summary>
        /// <returns>A list of dishes</returns>
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        //[Authorize(Policy = Policies.User)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetDishes()
        {
            return (await _dishService.GetDishListAsync()).ToList();
        }

        ///// <summary>
        ///// Get a dish
        ///// </summary>
        ///// <returns>A dish</returns>
        //[HttpGet("{id}", Name = "GetDish")]
        ////[Authorize(Policy = Policies.Admin)]
        ////[Authorize(Policy = Policies.User)]
        //public async Task<ActionResult<DishDto>> GetDish(int id)
        //{
        //    var dishEntity = await _dishRepository.GetDishAsync(id);
        //    if (dishEntity == null)
        //    {
        //        return BadRequest();
        //    }

        //    return _mapper.Map<DishDto>(dishEntity);
        //}

        ///// <summary>
        ///// Add a dish to the database
        ///// </summary>
        ///// <param name="dish">The dish</param>
        ///// <returns>A 201 created when succesful</returns>
        //[HttpPost]
        ////[Authorize(Policy = Policies.Admin)]
        ////[Authorize(Policy = Policies.User)]
        //public async Task<ActionResult> AddDish(DishDto dish)
        //{
        //    if (dish == null)
        //    {
        //        return BadRequest();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return new UnprocessableEntityObjectResult(ModelState);
        //    }

        //    if (await _dishRepository.DishExists(dish.Name))
        //    {
        //        ModelState.AddModelError("UniqueDishName", "UniqueDish|Dish name already exists. Please provide a different name.");
        //        return new UnprocessableEntityObjectResult(ModelState);
        //    }

        //    // Map the dish to an entity
        //    var dishEntity = _mapper.Map<Dish>(dish);

        //    foreach (var ingredient in dish.Ingredients)
        //    {
        //        // Check if the ingredient already exists
        //        var ingredientEntity = await _ingredientRepository.GetIngredientAsync(ingredient.Name);
        //        if (ingredientEntity == null)
        //        {
        //            ingredientEntity = _mapper.Map<Ingredient>(ingredient);
        //        }

        //        // Create the dish ingredient link
        //        var dishIngredient = new DishIngredient
        //        {
        //            Dish = dishEntity,
        //            Ingredient = ingredientEntity,
        //            Quantity = ingredient.Quantity
        //        };

        //        // Add the link to the dish
        //        dishEntity.DishIngredients.Add(dishIngredient);
        //    }

        //    // Save the dish
        //    await _dishRepository.AddDishAsync(dishEntity);
        //    try
        //    {
        //        await _dishRepository.SaveAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Log(LogLevel.Error, "Adding a dish failed on save", e);
        //        throw;
        //    }

        //    // Map the new entity back to a dto, to be able to create a 201 response
        //    var dishToReturn = _mapper.Map<DishDto>(dishEntity);
        //    return CreatedAtRoute("GetDish",
        //        new
        //        {
        //            id = dishToReturn.DishId
        //        },
        //        dishToReturn);
        //}

        ///// <summary>
        ///// Delete a dish from the database
        ///// </summary>
        ///// <param name="id">The dish id</param>
        ///// <returns>204 when successful</returns>
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteDish(int id)
        //{
        //    var dishEntity = await _dishRepository.GetDishAsync(id);
        //    if (dishEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    _dishRepository.DeleteDish(dishEntity);

        //    // Try to save database
        //    try
        //    {
        //        await _dishRepository.SaveAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Log(LogLevel.Error, "Deleting a dish failed on save.", e);
        //        throw;
        //    }

        //    // The response has no body, so therefore a 204 No Content.
        //    return NoContent();
        //}








        //[HttpGet]
        //public async Task<ActionResult> GetDishes()
        //{
        // TODO: Fix authentication
        //    //if (_userInfoService.Role != "Administrator" || !Guid.TryParse(_userInfoService.UserId, out Guid userIdAsGuid))
        //    //{
        //    //    return Forbid();
        //    //}

        //    // TODO: Add GetDishesForManager

        //    var dishEntities = await _dishRepository.GetDishesAsync();
        //    var dishes = _mapper.Map<IEnumerable<DishDto>>(dishEntities);
        //    return Ok(dishes);
        //}

        //[HttpPatch("{dishId}")]
        //public async Task<ActionResult> PartiallyUpdateDish(int dishId,
        //    [FromBody] JsonPatchDocument<DishForUpdateDto> jsonPatchDocument)
        //{
        //    if (jsonPatchDocument == null)
        //    {
        //        return BadRequest();
        //    }

        //    var dishFromRepo = await _dishRepository.GetDishAsync(dishId);
        //    if (dishFromRepo == null)
        //    {
        //        return BadRequest();
        //    }

        //    var dishToPatch = _mapper.Map<DishForUpdateDto>(dishFromRepo);

        //    // if patchDocument is malformed, this is still a client error. Use ModelState
        //    jsonPatchDocument.ApplyTo(dishToPatch, ModelState);
        //    if (!ModelState.IsValid)
        //    {
        //        return new UnprocessableEntityObjectResult(ModelState);
        //    }

        //    if (!TryValidateModel(dishToPatch))
        //    {
        //        return new UnprocessableEntityObjectResult(ModelState);
        //    }

        //    _mapper.Map(dishToPatch, dishFromRepo);

        //    if (!await _dishRepository.SaveAsync())
        //    {
        //        throw new Exception("Updating a dish failed on save.");
        //    }

        //    return NoContent();
        //}
    }
}