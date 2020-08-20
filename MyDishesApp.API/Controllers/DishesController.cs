using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDishesApp.API.Dtos;
using MyDishesApp.API.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyDishesApp.API.Controllers
{
    [Route("api/dishes")]
    [Authorize]
    public class DishesController : Controller
    {
        private readonly IDishInfoRepository _dishInfoRepository;
        private readonly IUserInfoService _userInfoService;

        public DishesController(IDishInfoRepository dishInfoRepository, IUserInfoService userInfoService)
        {
            _dishInfoRepository = dishInfoRepository;
            _userInfoService = userInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishes()
        {
            IEnumerable<Database.Entities.Dish> dishesFromRepo = new List<Database.Entities.Dish>();

            if (_userInfoService.Role == "Administrator")
            {
                dishesFromRepo = await _dishInfoRepository.GetDishes();
            }
            else
            {
                if (!Guid.TryParse(_userInfoService.UserId, out Guid userIdAsGuid))
                {
                    return Forbid();
                }

                // TODO: Add GetDishesForManager...
                dishesFromRepo = await _dishInfoRepository.GetDishes();
            }

            var dishes = Mapper.Map<IEnumerable<DishDto>>(dishesFromRepo);
            return Ok(dishes);
        }

        [HttpGet("{dishId}", Name = "GetDish")]
        [Authorize(Policy = "UserMustBeDishManager")]
        [Authorize(Policy = "UserMustBeAdministrator")]
        public async Task<IActionResult> GetDish(int dishId)
        {
            var dishFromRepo = await _dishInfoRepository.GetDish(dishId);

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

            List<string> ingredientNames = new List<string>();
            foreach (IngredientDto ingredient in dish.Ingredients)
            {
                ingredientNames.Add(ingredient.Name);
            }

            var dishEntity = Mapper.Map<Database.Entities.Dish>(dish);

            // TODO: add code below
            //if (dishEntity.ManagerId == Guid.Empty)
            //{
            //    if (!Guid.TryParse(_userInfoService.UserId, out Guid userIdAsGuid))
            //    {
            //        return Forbid();
            //    }

            //    dishEntity.ManagerId = userIdAsGuid;
            //}

            await _dishInfoRepository.AddDish(dishEntity);

            try
            {
                await _dishInfoRepository.SaveAsync();
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

            var dishFromRepo = await _dishInfoRepository.GetDish(dishId);
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

            await _dishInfoRepository.UpdateDish(dishFromRepo);

            if (!await _dishInfoRepository.SaveAsync())
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
            if (!await _dishInfoRepository.DishExists(dishId))
            {
                return NotFound();
            }

            // If dish (entity) exists, get it and then remove it.
            var dishEntity = await _dishInfoRepository.GetDish(dishId);
            await _dishInfoRepository.DeleteDish(dishEntity);

            // Try to save database
            if (!await _dishInfoRepository.SaveAsync())
            {
                throw new Exception("Deleting a dish failed on save.");
            }

            // TODO: add mailservice which sends an e-mail that the dish is deleted.

            // The response has no body, so therefore a 202 No Content.
            return NoContent();
        }
    }
}