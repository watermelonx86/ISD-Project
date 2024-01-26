using ISD_Project.Server.Services.Interfaces;
using ISD_Project.Server.Controllers;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ISD_Project.Tests.Controllers
{
    public class HealthInformationControllerTests
    {
        [Fact]
        public async Task HealthInformationController_AddHealthInformation_ReturnsExpectedResult_WhenRequestIsValid()
        {
            // Arrange
            var mockService = new Mock<IHealthInformationService>();
            var validRequest = new HealthInformationDto();
            mockService.Setup(service => service.AddHealthInformationAsync(validRequest)).ReturnsAsync(new OkResult());

            var controller = new HealthInformationController(mockService.Object);

            // Act
            var result = await controller.AddHealthInformation(validRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }
        
        [Fact]
        public async Task HealthInformationController_AddHealthInformation_ReturnsBadRequest_WhenRequestIsNull()
        {
            // Arrange
            var mockService = new Mock<IHealthInformationService>();
            mockService.Setup(service => service.AddHealthInformationAsync(null)).ReturnsAsync(new BadRequestResult());

            var controller = new HealthInformationController(mockService.Object);

            // Act
            var result = await controller.AddHealthInformation(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task HealthInformationController_GetHealthInformation_ReturnsExpectedResult_WhenUserIdIsValid()
        {
            // Arrange
            var mockService = new Mock<IHealthInformationService>();
            var validUserId = 1;
            mockService.Setup(service => service.GetHealthInformationAsync(validUserId)).ReturnsAsync(new OkResult());

            var controller = new HealthInformationController(mockService.Object);

            // Act
            var result = await controller.GetHealthInformation(validUserId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task HealthInformationController_GetHealthInformation_ReturnsNotFound_WhenUserIdIsInvalid()
        {
            // Arrange
            var mockService = new Mock<IHealthInformationService>();
            var invalidUserId = -1;
            mockService.Setup(service => service.GetHealthInformationAsync(invalidUserId)).ReturnsAsync(new NotFoundResult());

            var controller = new HealthInformationController(mockService.Object);

            // Act
            var result = await controller.GetHealthInformation(invalidUserId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
