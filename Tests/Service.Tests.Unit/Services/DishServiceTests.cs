using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyDishesApp.Repository.Repositories.Interfaces;
using MyDishesApp.Service.Services;
using System;
using Xunit;

namespace Service.Tests.Unit.Services
{
    public class DishServiceTests
    {
        public class Constructor : DishServiceTestsBase
        {
            [Fact]
            public void InitializeDishServiceCorrectly() => Assert.NotNull(
                new DishService(LoggerMock.Object, MapperMock.Object, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenLoggerIsNull() => Assert.Throws<ArgumentNullException>("logger",
                () => new DishService(null, MapperMock.Object, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenMapperIsNull() => Assert.Throws<ArgumentNullException>("mapper",
                () => new DishService(LoggerMock.Object, null, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenDishRepositoryIsNull() => Assert.Throws<ArgumentNullException>("dishRepository",
                () => new DishService(LoggerMock.Object, MapperMock.Object, null, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenIngredientRepositoryIsNull() => Assert.Throws<ArgumentNullException>("ingredientRepository",
                () => new DishService(LoggerMock.Object, MapperMock.Object, DishRepositoryMock.Object, null));
        }

        public abstract class DishServiceTestsBase
        {
            protected readonly Mock<ILogger<DishService>> LoggerMock = new Mock<ILogger<DishService>>();
            protected readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
            protected readonly Mock<IDishRepository> DishRepositoryMock = new Mock<IDishRepository>();
            protected readonly Mock<IIngredientRepository> IngredientRepositoryMock = new Mock<IIngredientRepository>();


        }
    }
}
