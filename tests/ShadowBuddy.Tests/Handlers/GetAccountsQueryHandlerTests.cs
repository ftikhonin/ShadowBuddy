using ShadowBuddy.Domain.Entities;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers.Queries;

namespace ShadowBuddy.Tests.Handlers;

public class GetAccountsQueryHandlerTests
{
    private Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public GetAccountsQueryHandlerTests()
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
                Name = "name",
                Moment = DateTime.UtcNow,
                Balance = 1L,
            }
        };
        _accountProcessingRepositoryMock
            .Setup(x => x.GetAccounts(It.IsAny<long>()))
            .ReturnsAsync(expectedResult);

        //Act
        var handler = new GetAccountsQueryHandler(_accountProcessingRepositoryMock.Object);
        var subject = handler.Handle(new GetAccountsQuery(1234),
            CancellationToken.None);

        //Assert
        Assert.Equal(expectedResult, subject.Result.Accounts);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_null()
    {
        //Arrange
        var handler = new GetAccountsQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetAccountsQuery(1234),
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

        var handler = new GetAccountsQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetAccountsQuery(1234),
            CancellationToken.None));

        //Assert
        Assert.Contains("Accounts not found", exception.Result.Message);
    }

    [Fact]
    public void Should_throws_argument_null_exception_when_repository_is_null()
    {
        Assert.Throws<ArgumentNullException>(() => new GetAccountsQueryHandler(null!));
    }
}