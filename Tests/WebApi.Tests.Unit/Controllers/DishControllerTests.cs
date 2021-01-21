using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Services;
using MyDishesApp.WebApi.Controllers;
using MyDishesApp.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Tests.Unit.Controllers
{
    public class DishControllerTests
    {
        public class Constructor : DishControllerTestBase
        {
            [Fact]
            public void InitializeDishControllerCorrectly() => Assert.NotNull(
                new DishController(LoggerMock.Object, MapperMock.Object, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenLoggerIsNull() => Assert.Throws<ArgumentNullException>("logger",
                () => new DishController(null, MapperMock.Object, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenMapperIsNull() => Assert.Throws<ArgumentNullException>("mapper",
                () => new DishController(LoggerMock.Object, null, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenDishRepositoryIsNull() => Assert.Throws<ArgumentNullException>("dishRepository",
                () => new DishController(LoggerMock.Object, MapperMock.Object, null, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenIngredientRepositoryIsNull() => Assert.Throws<ArgumentNullException>("ingredientRepository",
                () => new DishController(LoggerMock.Object, MapperMock.Object, DishRepositoryMock.Object, null));
        }

        public class GetDishes : DishControllerTestBase
        {
            private const string Input = "Pizza";

            [Fact]
            public async Task CallsServiceCorrectly()
            {
                // Arrange
                var serviceResponse = new List<Dish> { new Dish { Name = Input } };
                var mappedResult = new List<DishDto> { new DishDto { Name = Input } };

                DishRepositoryMock
                    .Setup(s => s.GetDishesAsync())
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();

                MapperMock
                    .Setup(m => m.Map<IEnumerable<DishDto>>(serviceResponse))
                    .Returns(mappedResult)
                    .Verifiable();

                var sut = CreateController();

                // Act
                var result = await sut.GetDishes();

                // Assert
                VerifyMocks();
                Assert.NotNull(result);
                Assert.Equal(mappedResult, result.Value);
            }

            [Fact]
            public void LogsAndRethrowsException()
            {
                var sut = CreateController();
                Assert.ThrowsAsync<Exception>(() => sut.GetDishes());
            }
        }

        public class GetDish : DishControllerTestBase
        {
            private const string Input = "Pizza";

            [Fact]
            public async Task CallsServiceCorrectly()
            {
                // Arrange
                var serviceResponse = new Dish { Name = Input };
                var mappedResult = new DishDto { Name = Input };

                DishRepositoryMock
                    .Setup(s => s.GetDishAsync(3))
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();

                MapperMock
                    .Setup(m => m.Map<DishDto>(serviceResponse))
                    .Returns(mappedResult)
                    .Verifiable();

                var sut = CreateController();

                // Act
                var result = await sut.GetDish(3);

                // Assert
                VerifyMocks();
                Assert.NotNull(result);
                Assert.Equal(mappedResult, result.Value);
            }

            [Fact]
            public void LogsAndRethrowsException()
            {
                var sut = CreateController();
                Assert.ThrowsAsync<Exception>(() => sut.GetDish(3));
            }
        }

        public class AddDish : DishControllerTestBase
        {
            [Fact]
            public async Task CallsServiceCorrectly()
            {
                // Arrange
                var ingredientToAdd = new IngredientDto {Name = "Ansjovis"};
                var dishToAdd = new DishDto
                {
                    Name = "Pizza",
                    Country = "Italy",
                    Ingredients = new[]
                    {
                        ingredientToAdd
                    }
                };

                var mappedIngredient = new Ingredient {Name = "Ansjovis"};
                var mappedDish = new Dish
                {
                    Name = "Pizza",
                    Country = "Italy",
                    DishIngredients =
                    {
                        new DishIngredient
                        {
                            Ingredient = mappedIngredient
                        }
                    }
                };

                DishRepositoryMock
                    .Setup(s => s.DishExists(dishToAdd.Name))
                    .ReturnsAsync(false)
                    .Verifiable();

                MapperMock
                    .Setup(m => m.Map<Dish>(dishToAdd))
                    .Returns(mappedDish)
                    .Verifiable();

                IngredientRepositoryMock
                    .Setup(s => s.GetIngredientAsync(ingredientToAdd.Name))
                    .ReturnsAsync(mappedIngredient)
                    .Verifiable();

                DishRepositoryMock
                    .Setup(s => s.AddDishAsync(mappedDish))
                    .Returns(Task.CompletedTask)
                    .Verifiable();

                MapperMock
                    .Setup(m => m.Map<DishDto>(mappedDish))
                    .Returns(dishToAdd)
                    .Verifiable();

                var sut = CreateController();

                // Act
                var result = await sut.AddDish(dishToAdd);

                // Assert
                VerifyMocks();
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Fact]
            public void ReturnsUnprocessableEntityObjectResultWhenDishAlreadyExists()
            {
                DishRepositoryMock
                    .Setup(s => s.DishExists("Pizza"))
                    .ReturnsAsync(true)
                    .Verifiable();

                var sut = CreateController();
                var result = sut.AddDish(new DishDto { Name = "Pizza" });
                Assert.IsType<UnprocessableEntityObjectResult>(result.Result);
            }

            [Fact]
            public void LogsAndRethrowsException()
            {
                var sut = CreateController();
                Assert.ThrowsAsync<Exception>(() => sut.AddDish(new DishDto()));
            }
        }

        //public class GetDishByName : DishControllerTestBase
        //{
        //    private const string Input = "Efilnikufesin (N.F.L.)";

        //    [Fact]
        //    public async Task CallsServiceCorrectly()
        //    {
        //        // Arrange
        //        var serviceResponse = new Dish { Name = Input };
        //        var mappedResult = new DishDto { Name = Input };

        //        DishRepositoryMock
        //            .Setup(s => s.GetDishAsync(Input))
        //            .ReturnsAsync(serviceResponse)
        //            .Verifiable();

        //        MapperMock
        //            .Setup(m => m.Map<DishDto>(serviceResponse))
        //            .Returns(mappedResult)
        //            .Verifiable();

        //        var sut = CreateController();

        //        // Act
        //        var result = await sut.GetDishByName(Input);

        //        // Assert
        //        VerifyMocks();
        //        Assert.NotNull(result);
        //        Assert.Equal(mappedResult, result.Value);
        //    }

        //    [Fact]
        //    public void LogsAndRethrowsException()
        //    {
        //        var sut = CreateController();
        //        Assert.ThrowsAsync<Exception>(() => sut.GetDishByName(Input));
        //    }
        //}

        //public class GetDishsByArtist : DishControllerTestBase
        //{
        //    [Fact]
        //    public async Task CallsServiceCorrectly()
        //    {
        //        // Arrange
        //        var serviceResponse = new List<Dish> { new Dish { Name = "Freak On A Leash" }, new Dish { Name = "Here To Stay" } };
        //        var mappedResult = new List<DishDto> { new DishDto { Name = "Freak On A Leash" }, new DishDto { Name = "Here To Stay" } };

        //        DishRepositoryMock
        //            .Setup(s => s.GetDishsByArtist("Korn"))
        //            .ReturnsAsync(serviceResponse)
        //            .Verifiable();

        //        MapperMock
        //            .Setup(m => m.Map<IEnumerable<DishDto>>(serviceResponse))
        //            .Returns(mappedResult)
        //            .Verifiable();

        //        var sut = CreateController();

        //        // Act
        //        var result = await sut.GetDishsByArtist("Korn");

        //        // Assert
        //        VerifyMocks();
        //        Assert.NotNull(result);
        //        Assert.Equal(mappedResult, result.Value);
        //    }

        //    [Fact]
        //    public void LogsAndRethrowsException()
        //    {
        //        var sut = CreateController();
        //        Assert.ThrowsAsync<Exception>(() => sut.GetDishsByArtist("Korn"));
        //    }
        //}

        //public class SearchDishsByGenre : DishControllerTestBase
        //{
        //    [Fact]
        //    public async Task CallsServiceCorrectly()
        //    {
        //        // Arrange
        //        var serviceResponse = new List<Dish> { new Dish { Name = "Zeig Dich" }, new Dish { Name = "Äuslander" } };
        //        var mappedResult = new List<DishDto> { new DishDto { Name = "Zeig Dich" }, new DishDto { Name = "Äuslander" } };

        //        DishRepositoryMock
        //            .Setup(s => s.SearchDishsByGenre("Tanzmetall"))
        //            .ReturnsAsync(serviceResponse)
        //            .Verifiable();

        //        MapperMock
        //            .Setup(m => m.Map<IEnumerable<DishDto>>(serviceResponse))
        //            .Returns(mappedResult)
        //            .Verifiable();

        //        var sut = CreateController();

        //        // Act
        //        var result = await sut.SearchDishsByGenre("Tanzmetall");

        //        // Assert
        //        VerifyMocks();
        //        Assert.NotNull(result);
        //        Assert.Equal(mappedResult, result.Value);
        //    }

        //    [Fact]
        //    public void LogsAndRethrowsException()
        //    {
        //        var sut = CreateController();
        //        Assert.ThrowsAsync<Exception>(() => sut.SearchDishsByGenre("Tanzmetall"));
        //    }
        //}



        //public class EditDish : DishControllerTestBase
        //{
        //    [Fact]
        //    public async Task CallsServiceCorrectly()
        //    {
        //        // Arrange
        //        var dishToEdit = new DishDto { Name = "Bottom", Album = "Undertow", Artist = "Tool", DishId = 5 };
        //        var mappedDish = new Dish { Name = "Bottom", Album = "Undertow", Id = 5, Artist = new Artist { Name = "Tool" } };

        //        MapperMock
        //            .Setup(m => m.Map<Dish>(dishToEdit))
        //            .Returns(mappedDish)
        //            .Verifiable();

        //        DishRepositoryMock
        //            .Setup(s => s.EditDishAsync(mappedDish))
        //            .Returns(Task.CompletedTask)
        //            .Verifiable();

        //        var sut = CreateController();

        //        // Act
        //        var result = await sut.EditDish(5, dishToEdit);

        //        // Assert
        //        VerifyMocks();
        //        Assert.IsType<NoContentResult>(result);
        //    }

        //    [Fact]
        //    public void ReturnsBadRequestWhenIdDoesNotMatch()
        //    {
        //        var sut = CreateController();
        //        var result = sut.EditDish(8, new DishDto { DishId = 10 });
        //        Assert.IsType<BadRequestResult>(result.Result);
        //    }
        //}

        //public class DeleteDish : DishControllerTestBase
        //{
        //    [Fact]
        //    public async Task CallsServiceCorrectly()
        //    {
        //        // Arrange
        //        var dishToDelete = new Dish { Name = "Bottom", Album = "Undertow", Id = 5, Artist = new Artist { Name = "Tool" } };

        //        DishRepositoryMock
        //            .Setup(s => s.GetDishAsync(3))
        //            .ReturnsAsync(dishToDelete)
        //            .Verifiable();

        //        DishRepositoryMock
        //            .Setup(s => s.DeleteDishAsync(dishToDelete))
        //            .Returns(Task.CompletedTask)
        //            .Verifiable();

        //        var sut = CreateController();

        //        // Act
        //        var result = await sut.DeleteDish(3);

        //        // Assert
        //        VerifyMocks();
        //        Assert.IsType<NoContentResult>(result);
        //    }

        //    [Fact]
        //    public void ReturnsNotFoundWhenDishIsNotFound()
        //    {
        //        var sut = CreateController();
        //        var result = sut.DeleteDish(5);
        //        Assert.IsType<NotFoundResult>(result.Result);
        //    }
        //}

        public abstract class DishControllerTestBase
        {
            protected readonly Mock<ILogger<DishController>> LoggerMock = new Mock<ILogger<DishController>>();
            protected readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
            protected readonly Mock<IDishRepository> DishRepositoryMock = new Mock<IDishRepository>();
            protected readonly Mock<IIngredientRepository> IngredientRepositoryMock = new Mock<IIngredientRepository>();

            protected DishController CreateController()
            {
                return new DishController(
                    LoggerMock.Object,
                    MapperMock.Object,
                    DishRepositoryMock.Object,
                    IngredientRepositoryMock.Object);
            }

            protected void VerifyMocks()
            {
                LoggerMock.Verify();
                MapperMock.Verify();
                DishRepositoryMock.Verify();
                IngredientRepositoryMock.Verify();
            }
        }
    }
}