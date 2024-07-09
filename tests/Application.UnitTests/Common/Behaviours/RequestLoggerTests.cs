using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProductCRUD.Application.Common.Behaviours;
using ProductCRUD.Application.Common.Interfaces;
using ProductCRUD.Application.Products.Commands.CreateProduct;

namespace ProductCRUD.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateProductCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateProductCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateProductCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateProductCommand { 
                Data = {
                Name = "Test ProductA",
                Description = "A Test Product for Seeding",
                SupplierName = "Test Supplier",
                Price = 25,
                Tax = 1.5
        }}, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateProductCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateProductCommand { 
            Data = {
                Name = "Test ProductB",
                Description = "A Test Product for Seeding",
                SupplierName = "Test Supplier",
                Price = 25,
                Tax = 1.5
        }}, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
