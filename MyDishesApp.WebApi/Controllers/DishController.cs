using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDishesApp.Repository.Services;
using MyDishesApp.WebApi.Dtos;
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
    //[Authorize]
    public class DishController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IDishRepository _dishRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="DishController" />
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="mapper">The mapper to use</param>
        /// <param name="dishRepository">The repository to use</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishRepository" /> is null.</exception>
        public DishController(ILogger<DishController> logger, IMapper mapper, IDishRepository dishRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
        }

        /// <summary>
        /// Get all dishes
        /// </summary>
        /// <returns>A list of dishes</returns>
        [HttpGet]
        //[Authorize(Policy = Policies.Admin)]
        //[Authorize(Policy = Policies.User)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetDishes()
        {
            var dishEntities = await _dishRepository.GetDishesAsync();
            return _mapper.Map<IEnumerable<DishDto>>(dishEntities).ToList();
        }

        [HttpGet("{id}", Name = "GetDish")]
        //[Authorize(Policy = Policies.Admin)]
        //[Authorize(Policy = Policies.User)]
        public async Task<ActionResult<DishDto>> GetDish(int id)
        {
            var dishEntity = await _dishRepository.GetDishAsync(id);
            if (dishEntity == null)
            {
                return BadRequest();
            }

            return _mapper.Map<DishDto>(dishEntity);
        }







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

        //[HttpGet("{dishId}", Name = "GetDish")]
        //[Authorize(Policy = "UserMustBeDishManager")]
        //[Authorize(Policy = "UserMustBeAdministrator")]
        //public async Task<ActionResult> GetDish(int dishId)
        //{
        //    var dishFromRepo = await _dishRepository.GetDishAsync(dishId);

        //    if (dishFromRepo == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(_mapper.Map<DishDto>(dishFromRepo));
        //}

        //[HttpPost]
        //public async Task<ActionResult> AddDish([FromBody] DishDto dish)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new UnprocessableEntityObjectResult(ModelState);
        //    }

        //    if (dish == null)
        //    {
        //        return BadRequest();
        //    }

        //    var ingredientNames = new List<string>();
        //    foreach (IngredientDto ingredient in dish.Ingredients)
        //    {
        //        ingredientNames.Add(ingredient.Name);
        //    }

        //    var dishEntity = _mapper.Map<Dish>(dish);

        //    // TODO: add code below
        //    //if (dishEntity.ManagerId == Guid.Empty)
        //    //{
        //    //    if (!Guid.TryParse(_userInfoService.UserId, out Guid userIdAsGuid))
        //    //    {
        //    //        return Forbid();
        //    //    }

        //    //    dishEntity.ManagerId = userIdAsGuid;
        //    //}

        //    await _dishRepository.AddDishAsync(dishEntity);

        //    try
        //    {
        //        await _dishRepository.SaveAsync();
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        SqlException sqlException = dbEx.InnerException as SqlException;
        //        if (sqlException?.Number == 2601)
        //        {
        //            // log dbEx
        //            ModelState.AddModelError("UniqueDishName", "UniqueDishName|Dishname already exists. Please provide a different name.");
        //            return new UnprocessableEntityObjectResult(ModelState);
        //        }
        //        throw new Exception("Adding a dish failed on save");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Log(LogLevel.Error, "Adding a dish failed on save", ex);
        //        throw;
        //    }

        //    var dishToReturn = _mapper.Map<DishDto>(dishEntity);

        //    return CreatedAtRoute("GetDish",
        //        new { dishId = dishToReturn.DishId },
        //        dishToReturn);
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

        //// Delete dish
        //[HttpDelete("{dishId}")]
        //public async Task<ActionResult> DeleteDish(int dishId)
        //{
        //    // Check if dish exists.
        //    if (!await _dishRepository.DishExists(dishId))
        //    {
        //        return NotFound();
        //    }

        //    // If dish (entity) exists, get it and then remove it.
        //    var dishEntity = await _dishRepository.GetDishAsync(dishId);
        //    _dishRepository.DeleteDish(dishEntity);

        //    // Try to save database
        //    if (!await _dishRepository.SaveAsync())
        //    {
        //        throw new Exception("Deleting a dish failed on save.");
        //    }

        //    // TODO: add mailservice which sends an e-mail that the dish is deleted.

        //    // The response has no body, so therefore a 202 No Content.
        //    return NoContent();
        //}
    }
}