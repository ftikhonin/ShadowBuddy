using Google.Protobuf.WellKnownTypes;
using ShadowBuddy.Domain.Entities;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers.Queries;

namespace ShadowBuddy.Tests.Handlers;

public class GetOperationsQueryHandlerTests
{
    private Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public GetOperationsQueryHandlerTests()
    {
        _accountProcessingRepositoryMock = new Mock<IAccountProcessingRepository>();
    }

    [Fact]
    public void Handler_handles_properly()
    {
        //Arrange
        var expectedResult = new[]
        {
            new Operation
            {
                Id = 1,
                AccountId = 2,
                OperationTypeId = 3,
                Amount = 5L,
                CategoryId = 6,
                Moment = DateTime.UtcNow,
                Comment = "test"
            }
        };
        _accountProcessingRepositoryMock
            .Setup(x => x.GetOperations(It.IsAny<long>(), It.IsAny<DateTime>()))
            .ReturnsAsync(expectedResult);

        //Act
        var handler = new GetOperationsQueryHandler(_accountProcessingRepositoryMock.Object);
        var subject = handler.Handle(new GetOperationsQuery(1234, DateTime.UtcNow.ToTimestamp()),
            CancellationToken.None);

        //Assert
        Assert.Equal(expectedResult, subject.Result.Operations);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_null()
    {
        //Arrange
        var handler = new GetOperationsQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetOperationsQuery(1234, DateTime.UtcNow.ToTimestamp()),
            CancellationToken.None));

        //Assert
        Assert.Contains("Operations not found", exception.Result.Message);
    }

    [Fact]
    public void Handler_throws_not_found_when_repository_returns_empty_array()
    {
        //Arrange
        _accountProcessingRepositoryMock
            .Setup(x => x.GetOperations(It.IsAny<long>(), It.IsAny<DateTime>())).ReturnsAsync(new Operation[] { });

        var handler = new GetOperationsQueryHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new GetOperationsQuery(1234, DateTime.UtcNow.ToTimestamp()),
            CancellationToken.None));

        //Assert
        Assert.Contains("Operations not found", exception.Result.Message);
    }
}