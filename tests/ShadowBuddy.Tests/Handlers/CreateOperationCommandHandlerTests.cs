using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers.Commands;

namespace ShadowBuddy.Tests.Handlers;

public class CreateOperationCommandHandlerTests
{
    private Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public CreateOperationCommandHandlerTests()
    {
        _accountProcessingRepositoryMock = new Mock<IAccountProcessingRepository>();
    }

    [Fact]
    public void Handler_handles_properly()
    {
        //Arrange
        var handler = new CreateOperationCommandHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new CreateOperationCommand(1234, 1, 1234.1, 123, "test", DateTime.Now),
            CancellationToken.None));

        //Assert
        Assert.Null(exception.Result);
    }

    [Fact]
    public void Should_throws_argument_null_exception_when_repository_is_null()
    {
        Assert.Throws<ArgumentNullException>(() => new CreateOperationCommandHandler(null!));
    }
}