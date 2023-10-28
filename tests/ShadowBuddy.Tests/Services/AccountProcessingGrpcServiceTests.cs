using MediatR;
using ShadowBuddy.Handlers.Commands;

namespace ShadowBuddy.Tests.Services;

public class AccountProcessingGrpcServiceTests
{
    private readonly Mock<IMediator> _mediator;

    public AccountProcessingGrpcServiceTests()
    {
        _mediator = new Mock<IMediator>();
    }

    [Fact]
    public void CreateAccount_handles_without_exception()
    {
        //Arrange
        var command = new CreateAccountCommand(1, "Test", 12.34F, DateTime.UtcNow, 1, "test");
        //Act
        var exception = Record.ExceptionAsync(() => _mediator.Object.Send(command, CancellationToken.None));

        //Assert
        Assert.Null(exception.Result);
    }
}