using System.Net;
using ISD_Project.Server.Controllers;
using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ISD_Project.Tests.Controllers;

public class CustomerControllerTests
{
    [Fact]
    public async Task CustomerController_GetCustomer_ReturnsExpectedResult_WhenCalledByAdmin()
    {
        // Arrange
        var mockCustomerService = new Mock<ICustomerService>();
        var expectedCustomer = new CustomerDto { Id = 1, Name = "Test Customer" };
        mockCustomerService.Setup(service => service.GetCustomerAsync())
            .ReturnsAsync(new OkObjectResult(expectedCustomer));
        var controller = new CustomerController(mockCustomerService.Object);

        // Act
        var result = await controller.GetCustomer();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCustomer = Assert.IsType<CustomerDto>(okResult.Value);
        Assert.Equal(expectedCustomer, returnedCustomer);
    }

    [Fact]
    public async Task CustomerController_GetCustomer_ReturnsNotFound_WhenNoCustomerExists()
    {
        // Arrange
        var mockCustomerService = new Mock<ICustomerService>();
        mockCustomerService.Setup(service => service.GetCustomerAsync())
            .ReturnsAsync(new NotFoundResult());
        var controller = new CustomerController(mockCustomerService.Object);

        // Act
        var result = await controller.GetCustomer();

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


}