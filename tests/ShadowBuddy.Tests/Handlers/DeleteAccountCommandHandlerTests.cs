using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers.Commands;

namespace ShadowBuddy.Tests.Handlers;

public class DeleteAccountCommandHandlerTests
{
    private Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public DeleteAccountCommandHandlerTests()
    {
        _accountProcessingRepositoryMock = new Mock<IAccountProcessingRepository>();
    }

    [Fact]
    public void Handler_handles_properly()
    {
        //Arrange
        var handler = new DeleteAccountCommandHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new DeleteAccountCommand(1234L),
            CancellationToken.None));

        //Assert
        Assert.Null(exception.Result);
    }

    [Fact]
    public void Should_throws_argument_null_exception_when_repository_is_null()
    {
        Assert.Throws<ArgumentNullException>(() => new DeleteAccountCommandHandler(null!));
    }
}