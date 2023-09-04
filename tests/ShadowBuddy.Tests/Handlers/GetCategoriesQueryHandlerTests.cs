using ShadowBuddy.Domain.Entities;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers.Queries;

namespace ShadowBuddy.Tests.Handlers;

public class GetCategoriesQueryHandlerTests
{
    private Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public GetCategoriesQueryHandlerTests()
    {
        _accountProcessingRepositoryMock = new Mock<IAccountProcessingRepository>();
    }

    [Fact]
    public void Handler_handles_properly()
    {
        //Arrange
        var expectedResult = new[]
        {
            new Category
            {
                Id = 1,
                Name = "TestName",
                ShortName = "Test",
                Label = "t"
            }
        };
        _accountProcessingRepositoryMock
            .Setup(x => x.GetCategories())
            .ReturnsAsync(expectedResult);

        //Act
        var handler = new GetCategoriesQueryHandler(_accountProcessingRepositoryMock.Object);
        var subject = handler.Handle(new GetCategoriesQuery(),
            CancellationToken.None);

        //Assert
        Assert.Equal(expectedResult, subject.Result.Categories);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_null()
    {
        //Arrange
        var handler = new GetCategoriesQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetCategoriesQuery(),
            CancellationToken.None));

        //Assert
        Assert.Contains("Categories not found", exception.Result.Message);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_empty_array()
    {
        //Arrange
        _accountProcessingRepositoryMock
            .Setup(x => x.GetCategories()).ReturnsAsync(new Category[] { });

        var handler = new GetCategoriesQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetCategoriesQuery(),
            CancellationToken.None));

        //Assert
        Assert.Contains("Categories not found", exception.Result.Message);
    }
}