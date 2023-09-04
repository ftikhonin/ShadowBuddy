using ShadowBuddy.Domain.Entities;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers.Queries;

namespace ShadowBuddy.Tests.Handlers;

public class GetBalanceQueryHandlerTests
{
    private Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public GetBalanceQueryHandlerTests()
    {
        _accountProcessingRepositoryMock = new Mock<IAccountProcessingRepository>();
    }

    [Fact]
    public void Handler_handles_properly()
    {
        //Arrange
        var expectedResult = new[]
        {
            new Account
            {
                Id = 1,
                CurrencyId = 2,
                Name = "name1",
                Moment = DateTime.UtcNow
            },
            new Account
            {
                Id = 2,
                CurrencyId = 3,
                Name = "name2",
                Moment = DateTime.UtcNow
            }
        };
        _accountProcessingRepositoryMock
            .Setup(x => x.GetAccounts(It.IsAny<long>()))
            .ReturnsAsync(expectedResult);

        _accountProcessingRepositoryMock
            .Setup(x => x.GetAccountBalance(It.IsAny<long>()))
            .ReturnsAsync(1);

        //Act
        var handler = new GetBalanceQueryHandler(_accountProcessingRepositoryMock.Object);
        var subject = handler.Handle(new GetBalanceQuery(1234),
            CancellationToken.None);

        //Assert
        Assert.Equal(2L, subject.Result.Balance);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_null()
    {
        //Arrange
        var handler = new GetBalanceQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetBalanceQuery(1234),
            CancellationToken.None));

        //Assert
        Assert.Contains("Accounts not found", exception.Result.Message);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_empty_array()
    {
        //Arrange
        _accountProcessingRepositoryMock
            .Setup(x => x.GetAccounts(It.IsAny<long>())).ReturnsAsync(new Account[] { });

        var handler = new GetBalanceQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetBalanceQuery(1234),
            CancellationToken.None));

        //Assert
        Assert.Contains("Accounts not found", exception.Result.Message);
    }
}