using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Services;
using MyDishesApp.WebApi.Dtos;
using System;
using System.Collections.Generic;
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
        private readonly IDishRepository _dishRepository;
        private readonly IUserInfoService _userInfoService;

        /// <summary>
        /// Initializes a new instance of <see cref="DishController" />
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="mapper">The mapper to use</param>
        /// <param name="dishRepository">The repository to use</param>
        /// <param name="userInfoService">The user info service</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishRepository" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="userInfoService" /> is null.</exception>
        public DishController(ILogger<DishController> logger, IMapper mapper, IDishRepository dishRepository, IUserInfoService userInfoService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
            _userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
        }

        [HttpGet]
        public async Task<IActionResult> GetDishes()
        {
            var dishEntities = new List<Dish>();

            if (_userInfoService.Role == "Administrator")
            {
                dishEntities = await _dishRepository.GetDishesAsync().ToList();
            }
            else
            {
                if (!Guid.TryParse(_userInfoService.UserId, out Guid userIdAsGuid))
                {
                    return Forbid();
                }

                // TODO: Add GetDishesForManager...
                dishEntities = await _dishRepository.GetDishesAsync().ToList();
            }

            var dishes = Mapper.Map<IEnumerable<DishDto>>(dishEntities);
            return Ok(dishes);
        }

        [HttpGet("{dishId}", Name = "GetDish")]
        [Authorize(Policy = "UserMustBeDishManager")]
        [Authorize(Policy = "UserMustBeAdministrator")]
        public async Task<IActionResult> GetDish(int dishId)
        {
            var dishFromRepo = await _dishRepository.GetDishAsync(dishId);

            if (dishFromRepo == null)
            {
                return BadRequest();
            }

            return Ok(Mapper.Map<DishDto>(dishFromRepo));
        }

        [HttpPost]
        public async Task<IActionResult> AddDish([FromBody] DishDto dish)
        {
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            if (dish == null)
            {
                return BadRequest();
            }

            var ingredientNames = new List<string>();
            foreach (IngredientDto ingredient in dish.Ingredients)
            {
                ingredientNames.Add(ingredient.Name);
            }

            var dishEntity = Mapper.Map<Dish>(dish);

            // TODO: add code below
            //if (dishEntity.ManagerId == Guid.Empty)
            //{
            //    if (!Guid.TryParse(_userInfoService.UserId, out Guid userIdAsGuid))
            //    {
            //        return Forbid();
            //    }

            //    dishEntity.ManagerId = userIdAsGuid;
            //}

            await _dishRepository.AddDishAsync(dishEntity);

            try
            {
                await _dishRepository.SaveAsync();
            }
            catch (DbUpdateException dbEx)
            {
                SqlException sqlException = dbEx.InnerException as SqlException;
                if (sqlException.Number == 2601)
                {
                    // log dbEx
                    ModelState.AddModelError("UniqueDishName", "UniqueDishName|Dishname already exists. Please provide a different name.");
                    return new UnprocessableEntityObjectResult(ModelState);
                }
                throw new Exception("Adding a dish failed on save");
            }
            catch (Exception ex)
            {
                // log ex
                throw new Exception("Adding a dish failed on save");
            }

            var dishToReturn = Mapper.Map<DishDto>(dishEntity);

            return CreatedAtRoute("GetDish",
                new { dishId = dishToReturn.DishId },
                dishToReturn);
        }

        [HttpPatch("{dishId}")]
        public async Task<IActionResult> PartiallyUpdateDish(int dishId,
            [FromBody] JsonPatchDocument<DishForUpdateDto> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest();
            }

            var dishFromRepo = await _dishRepository.GetDishAsync(dishId);
            if (dishFromRepo == null)
            {
                return BadRequest();
            }

            var dishToPatch = Mapper.Map<DishForUpdateDto>(dishFromRepo);

            // if patchDocument is malformed, this is still a client error. Use ModelState
            jsonPatchDocument.ApplyTo(dishToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            if (!TryValidateModel(dishToPatch))
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            Mapper.Map(dishToPatch, dishFromRepo);

            if (!await _dishRepository.SaveAsync())
            {
                throw new Exception("Updating a dish failed on save.");
            }

            return NoContent();
        }
        
        // Delete dish
        [HttpDelete("{dishId}")]
        public async Task<IActionResult> DeleteDish(int dishId)
        {
            // Check if dish exists.
            if (!await _dishRepository.DishExists(dishId))
            {
                return NotFound();
            }

            // If dish (entity) exists, get it and then remove it.
            var dishEntity = await _dishRepository.GetDishAsync(dishId);
            _dishRepository.DeleteDish(dishEntity);

            // Try to save database
            if (!await _dishRepository.SaveAsync())
            {
                throw new Exception("Deleting a dish failed on save.");
            }

            // TODO: add mailservice which sends an e-mail that the dish is deleted.

            // The response has no body, so therefore a 202 No Content.
            return NoContent();
        }
    }
}