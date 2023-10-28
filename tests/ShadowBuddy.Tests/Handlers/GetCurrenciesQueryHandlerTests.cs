using ShadowBuddy.Domain.Entities;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers.Queries;

namespace ShadowBuddy.Tests.Handlers;

public class GetCurrenciesQueryHandlerTests
{
    private Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public GetCurrenciesQueryHandlerTests()
    {
        _accountProcessingRepositoryMock = new Mock<IAccountProcessingRepository>();
    }

    [Fact]
    public void Handler_handles_properly()
    {
        //Arrange
        var expectedResult = new[]
        {
            new Currency()
            {
                Id = 1,
                Name = "TestName",
                ShortName = "Test",
                Label = "t"
            }
        };
        _accountProcessingRepositoryMock
            .Setup(x => x.GetCurrencies())
            .ReturnsAsync(expectedResult);

        //Act
        var handler = new GetCurrenciesQueryHandler(_accountProcessingRepositoryMock.Object);
        var subject = handler.Handle(new GetCurrenciesQuery(),
            CancellationToken.None);

        //Assert
        Assert.Equal(expectedResult, subject.Result.Currencies);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_null()
    {
        //Arrange
        var handler = new GetCurrenciesQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetCurrenciesQuery(),
            CancellationToken.None));

        //Assert
        Assert.Contains("Currencies not found", exception.Result.Message);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_empty_array()
    {
        //Arrange
        _accountProcessingRepositoryMock
            .Setup(x => x.GetCurrencies()).ReturnsAsync(new Currency[] { });

        var handler = new GetCurrenciesQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetCurrenciesQuery(),
            CancellationToken.None));

        //Assert
        Assert.Contains("Currencies not found", exception.Result.Message);
    }

    [Fact]
    public void Should_throws_argument_null_exception_when_repository_is_null()
    {
        Assert.Throws<ArgumentNullException>(() => new GetCurrenciesQueryHandler(null!));
    }
}