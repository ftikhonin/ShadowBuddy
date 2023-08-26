using Google.Protobuf.WellKnownTypes;
using ShadowBuddy.Domain.Entities;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers;
using ShadowBuddy.Infrastructure.Repositories;

namespace ShadowBuddy.Tests.Handlers;

public class GetOperationsQueryHandlerTests
{
    public Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public GetOperationsQueryHandlerTests()
    {
        _accountProcessingRepositoryMock = new Mock<IAccountProcessingRepository>();
    }

    [Fact]
    public void Handler_handles_properly()
    {
        //Arrange
        _accountProcessingRepositoryMock
            .Setup(x => x.GetOperations(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Operation[]
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
            });
        
        //Act
        var handler = new GetOperationsQueryHandler(_accountProcessingRepositoryMock.Object);
        var subject = handler.Handle(new GetOperationsQuery(1234, DateTime.UtcNow.ToTimestamp()),
            CancellationToken.None);
        
        //Assert
        Assert.NotEmpty(subject.Result.Operations);
    }
}