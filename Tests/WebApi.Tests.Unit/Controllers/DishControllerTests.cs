using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Services;
using MyDishesApp.WebApi.Controllers;
using MyDishesApp.WebApi.Dtos;
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
                new DishController(LoggerMock.Object, MapperMock.Object, DishRepositoryMock.Object, UserInfoServiceMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenLoggerIsNull() => Assert.Throws<ArgumentNullException>("logger",
                () => new DishController(null, MapperMock.Object, DishRepositoryMock.Object, UserInfoServiceMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenMapperIsNull() => Assert.Throws<ArgumentNullException>("mapper",
                () => new DishController(LoggerMock.Object, null, DishRepositoryMock.Object, UserInfoServiceMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenDishRepositoryIsNull() => Assert.Throws<ArgumentNullException>("dishRepository",
                () => new DishController(LoggerMock.Object, MapperMock.Object, null, UserInfoServiceMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenUserInfoServiceIsNull() => Assert.Throws<ArgumentNullException>("userInfoService",
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

        //public class GetDishById : DishControllerTestBase
        //{
        //    private const string Input = "Death Atlas";

        //    [Fact]
        //    public async Task CallsServiceCorrectly()
        //    {
        //        // Arrange
        //        var serviceResponse = new Dish { Name = Input };
        //        var mappedResult = new DishDto { Name = Input };

        //        DishRepositoryMock
        //            .Setup(s => s.GetDishAsync(3))
        //            .ReturnsAsync(serviceResponse)
        //            .Verifiable();

        //        MapperMock
        //            .Setup(m => m.Map<DishDto>(serviceResponse))
        //            .Returns(mappedResult)
        //            .Verifiable();

        //        var sut = CreateController();

        //        // Act
        //        var result = await sut.GetDishById(3);

        //        // Assert
        //        VerifyMocks();
        //        Assert.NotNull(result);
        //        Assert.Equal(mappedResult, result.Value);
        //    }

        //    [Fact]
        //    public void LogsAndRethrowsException()
        //    {
        //        var sut = CreateController();
        //        Assert.ThrowsAsync<Exception>(() => sut.GetDishById(3));
        //    }
        //}

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

        //public class AddDish : DishControllerTestBase
        //{
        //    [Fact]
        //    public async Task CallsServiceCorrectly()
        //    {
        //        // Arrange
        //        var songToAdd = new DishDto { Name = "Bottom", Album = "Undertow", Artist = "Tool" };
        //        var mappedDish = new Dish { Name = "Bottom", Album = "Undertow", Artist = new Artist { Name = "Tool" } };

        //        MapperMock
        //            .Setup(m => m.Map<Dish>(songToAdd))
        //            .Returns(mappedDish)
        //            .Verifiable();

        //        DishRepositoryMock
        //            .Setup(s => s.AddDishAsync(mappedDish))
        //            .Returns(Task.CompletedTask)
        //            .Verifiable();

        //        var sut = CreateController();

        //        // Act
        //        var result = await sut.AddDish(songToAdd);

        //        // Assert
        //        VerifyMocks();
        //        Assert.IsType<NoContentResult>(result);
        //    }

        //    [Fact]
        //    public void ReturnsUnprocessableEntityObjectResultWhenDishAlreadyExists()
        //    {
        //        DishRepositoryMock
        //            .Setup(s => s.DishExists("Rocky Mountain Way", "Joe Walsh"))
        //            .ReturnsAsync(true)
        //            .Verifiable();

        //        var sut = CreateController();
        //        var result = sut.AddDish(new DishDto { Name = "Rocky Mountain Way", Artist = "Joe Walsh" });
        //        Assert.IsType<UnprocessableEntityObjectResult>(result.Result);
        //    }
        //}

        //public class EditDish : DishControllerTestBase
        //{
        //    [Fact]
        //    public async Task CallsServiceCorrectly()
        //    {
        //        // Arrange
        //        var songToEdit = new DishDto { Name = "Bottom", Album = "Undertow", Artist = "Tool", DishId = 5 };
        //        var mappedDish = new Dish { Name = "Bottom", Album = "Undertow", Id = 5, Artist = new Artist { Name = "Tool" } };

        //        MapperMock
        //            .Setup(m => m.Map<Dish>(songToEdit))
        //            .Returns(mappedDish)
        //            .Verifiable();

        //        DishRepositoryMock
        //            .Setup(s => s.EditDishAsync(mappedDish))
        //            .Returns(Task.CompletedTask)
        //            .Verifiable();

        //        var sut = CreateController();

        //        // Act
        //        var result = await sut.EditDish(5, songToEdit);

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
        //        var songToDelete = new Dish { Name = "Bottom", Album = "Undertow", Id = 5, Artist = new Artist { Name = "Tool" } };

        //        DishRepositoryMock
        //            .Setup(s => s.GetDishAsync(3))
        //            .ReturnsAsync(songToDelete)
        //            .Verifiable();

        //        DishRepositoryMock
        //            .Setup(s => s.DeleteDishAsync(songToDelete))
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
            protected readonly Mock<IUserInfoService> UserInfoServiceMock = new Mock<IUserInfoService>();

            protected DishController CreateController()
            {
                return new DishController(
                    LoggerMock.Object,
                    MapperMock.Object,
                    DishRepositoryMock.Object,
                    UserInfoServiceMock.Object);
            }

            protected void VerifyMocks()
            {
                LoggerMock.Verify();
                MapperMock.Verify();
                DishRepositoryMock.Verify();
                UserInfoServiceMock.Verify();
            }
        }
    }
}