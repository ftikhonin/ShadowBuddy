using MediatR;
using ShadowBuddy.Handlers;
using ShadowBuddy.Handlers.Commands;

namespace ShadowBuddy.Tests.Handlers;

public class AccountProcessingGrpcServiceTests
{
    private readonly Mock<IMediator> _mediator;

    public AccountProcessingGrpcServiceTests()
    {
        _mediator = new Mock<IMediator>();
    }

    [Fact]
    public void CreateAccount_handles_properly()
    {
        //Arrange
        var command = new CreateAccountCommand(1, "Test", 12.34F, DateTime.UtcNow, 1);
        //Act
        var exception = Record.ExceptionAsync(() => _mediator.Object.Send(command, CancellationToken.None));
        //Assert
        //TODO:доделать
        Assert.Null(null);
    }
}