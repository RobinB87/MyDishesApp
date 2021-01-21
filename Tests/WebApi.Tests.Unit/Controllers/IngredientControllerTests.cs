//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using Moq;
//using MyDishesApp.Repository.Data.Entities;
//using MyDishesApp.Repository.Services;
//using MyDishesApp.WebApi.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace WebApi.Tests.Unit.Controllers
//{
//    public class IngredientControllerTests
//    {
//        public class Constructor : IngredientControllerTestBase
//        {
//            [Fact]
//            public void InitializeIngredientControllerCorrectly() => Assert.NotNull(
//                new IngredientController(LoggerMock.Object, MapperMock.Object, IngredientRepositoryMock.Object, DishRepositoryMock.Object));

//            [Fact]
//            public void ThrowsArgumentNullExceptionWhenLoggerIsNull() => Assert.Throws<ArgumentNullException>("logger",
//                () => new IngredientController(null, MapperMock.Object, IngredientRepositoryMock.Object, DishRepositoryMock.Object));

//            [Fact]
//            public void ThrowsArgumentNullExceptionWhenMapperIsNull() => Assert.Throws<ArgumentNullException>("mapper",
//                () => new IngredientController(LoggerMock.Object, null, IngredientRepositoryMock.Object, DishRepositoryMock.Object));

//            [Fact]
//            public void ThrowsArgumentNullExceptionWhenIngredientRepositoryIsNull() => Assert.Throws<ArgumentNullException>("ingredientRepository",
//                () => new IngredientController(LoggerMock.Object, MapperMock.Object, null, DishRepositoryMock.Object));

//            [Fact]
//            public void ThrowsArgumentNullExceptionWhenDishRepositoryIsNull() => Assert.Throws<ArgumentNullException>("dishRepository",
//                () => new IngredientController(LoggerMock.Object, MapperMock.Object, IngredientRepositoryMock.Object, null));
//        }

//        public class GetIngredients : IngredientControllerTestBase
//        {
//            private const string Input = "Pizza";

//            [Fact]
//            public async Task CallsServiceCorrectly()
//            {
//                // Arrange
//                var serviceResponse = new List<Ingredient> { new Ingredient { Name = Input } };
//                var mappedResult = new List<IngredientDto> { new IngredientDto { Name = Input } };

//                IngredientRepositoryMock
//                    .Setup(s => s.GetIngredientsAsync())
//                    .ReturnsAsync(serviceResponse)
//                    .Verifiable();

//                MapperMock
//                    .Setup(m => m.Map<IEnumerable<IngredientDto>>(serviceResponse))
//                    .Returns(mappedResult)
//                    .Verifiable();

//                var sut = CreateController();

//                // Act
//                var result = await sut.GetIngredients();

//                // Assert
//                VerifyMocks();
//                Assert.NotNull(result);
//                Assert.Equal(mappedResult, result.Value);
//            }

//            [Fact]
//            public void LogsAndRethrowsException()
//            {
//                var sut = CreateController();
//                Assert.ThrowsAsync<Exception>(() => sut.GetIngredients());
//            }
//        }

//        public abstract class IngredientControllerTestBase
//        {
//            protected readonly Mock<ILogger<IngredientController>> LoggerMock = new Mock<ILogger<IngredientController>>();
//            protected readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
//            protected readonly Mock<IIngredientRepository> IngredientRepositoryMock = new Mock<IIngredientRepository>();
//            protected readonly Mock<IDishRepository> DishRepositoryMock = new Mock<IDishRepository>();

//            protected IngredientController CreateController()
//            {
//                return new IngredientController(
//                    LoggerMock.Object,
//                    MapperMock.Object,
//                    IngredientRepositoryMock.Object,
//                    DishRepositoryMock.Object);
//            }

//            protected void VerifyMocks()
//            {
//                LoggerMock.Verify();
//                MapperMock.Verify();
//                IngredientRepositoryMock.Verify();
//                DishRepositoryMock.Verify();
//            }
//        }
//    }
//}
