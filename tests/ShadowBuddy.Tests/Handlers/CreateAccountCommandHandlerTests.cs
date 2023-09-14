using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Handlers.Commands;

namespace ShadowBuddy.Tests.Handlers;

public class CreateAccountCommandHandlerTests
{
    private Mock<IAccountProcessingRepository> _accountProcessingRepositoryMock { get; set; }

    public CreateAccountCommandHandlerTests()
    {
        _accountProcessingRepositoryMock = new Mock<IAccountProcessingRepository>();
    }

    [Fact]
    public void Handler_handles_properly()
    {
        //Arrange
        _accountProcessingRepositoryMock
            .Setup(x => x.CreateAccount(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<DateTime>(),
                It.IsAny<long>())).ReturnsAsync(1);
        var handler = new CreateAccountCommandHandler(_accountProcessingRepositoryMock.Object);

        //Act
        var exception = Record.ExceptionAsync(() => handler.Handle(
            new CreateAccountCommand(1234, "test", 1234.1, DateTime.Now, 1L),
            CancellationToken.None));

        //Assert
        Assert.Null(exception.Result);
    }
}