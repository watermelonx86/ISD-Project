using FakeItEasy;
using ISD_Project.Server.Controllers;
using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Models;
using ISD_Project.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace ISD_Project.Tests.Controllers
{
    public class ApprovalStatusControllerTests
    {
        [Fact]
        public async Task ApprovalStatusController_GetApprovalStatus_ReturnsExpectedResult()
        {
            // Arrange
            var mockService = new Mock<IApprovalStatusService>();
            mockService.Setup(service => service.GetApprovalStatusAsync())
                .ReturnsAsync(new OkObjectResult(new ApprovalStatusDto()));
            var controller = new ApprovalStatusController(mockService.Object);

            // Act
            var result = await controller.GetApprovalStatus();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ApprovalStatusDto>(okResult.Value);
        }

        [Fact]
        public async Task ApprovalStatusController_GetApprovalStatus_ThrowsException()
        {
    
            // Arrange
            var mockService = new Mock<IApprovalStatusService>();
            mockService.Setup(service => service.GetApprovalStatusAsync())
                .ThrowsAsync(new Exception());
            var controller = new ApprovalStatusController(mockService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => controller.GetApprovalStatus());
        }
        
        [Fact]
        public async Task ApprovalStatusController_GetApprovalStatusWithProfileStatus_ReturnsExpectedResult()
        {
            var mockService = new Mock<IApprovalStatusService>();
            var expectedProfileStatus = ProfileStatus.Approved;
            mockService.Setup(service => service.GetApprovalStatusAsync(expectedProfileStatus))
                .ReturnsAsync(new OkObjectResult(new ApprovalStatusDto()));
            var controller = new ApprovalStatusController(mockService.Object);

            var result = await controller.GetApprovalStatus(expectedProfileStatus);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ApprovalStatusDto>(okResult.Value);
        }
        
        [Fact]
        public async Task ApprovalStatusController_GetApprovalStatusWithProfileStatus_ThrowsException_WhenServiceThrowsException()
        {
            var mockService = new Mock<IApprovalStatusService>();
            var expectedProfileStatus = ProfileStatus.Approved;
            mockService.Setup(service => service.GetApprovalStatusAsync(expectedProfileStatus))
                .ThrowsAsync(new Exception());
            var controller = new ApprovalStatusController(mockService.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.GetApprovalStatus(expectedProfileStatus));
        }
        
        [Fact]
        public async Task ApprovalStatusController_AddApprovalStatus_ReturnsExpectedResult()
        {
            var mockService = new Mock<IApprovalStatusService>();
            var approvalStatusDto = new ApprovalStatusDto();
            mockService.Setup(service => service.AddApprovalStatusAsync(approvalStatusDto))
                .ReturnsAsync(new OkObjectResult(approvalStatusDto));
            var controller = new ApprovalStatusController(mockService.Object);

            var result = await controller.AddApprovalStatus(approvalStatusDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ApprovalStatusDto>(okResult.Value);
        }

        [Fact]
        public async Task ApprovalStatusController_AddApprovalStatus_ThrowsException_WhenServiceThrowsException()
        {
            var mockService = new Mock<IApprovalStatusService>();
            var approvalStatusDto = new ApprovalStatusDto();
            mockService.Setup(service => service.AddApprovalStatusAsync(approvalStatusDto))
                .ThrowsAsync(new Exception());
            var controller = new ApprovalStatusController(mockService.Object);

            await Assert.ThrowsAsync<Exception>(() => controller.AddApprovalStatus(approvalStatusDto));
        }

        [Fact]
        public async Task GetApprovalStatus_ReturnsApprovalStatus_WhenProfileStatusExists()
        {
            // Arrange
            var mockService = new Mock<IApprovalStatusService>();
            var profileStatus = new ProfileStatus();
            mockService.Setup(service => service.GetApprovalStatusAsync(profileStatus))
                .ReturnsAsync(new OkObjectResult(new ApprovalStatusDto()));
            var controller = new ApprovalStatusController(mockService.Object);

            // Act
            var result = await controller.GetApprovalStatus(profileStatus);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ApprovalStatusDto>(okResult.Value);
        }

        [Fact]
        public async Task GetApprovalStatus_ReturnsNotFound_WhenProfileStatusDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IApprovalStatusService>();
            var profileStatus = new ProfileStatus();
            mockService.Setup(service => service.GetApprovalStatusAsync(profileStatus))
                .ReturnsAsync(new NotFoundResult());
            var controller = new ApprovalStatusController(mockService.Object);

            // Act
            var result = await controller.GetApprovalStatus(profileStatus);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddApprovalStatus_ReturnsCreated_WhenApprovalStatusDtoIsValid()
        {
            // Arrange
            var mockService = new Mock<IApprovalStatusService>();
            var approvalStatusDto = new ApprovalStatusDto();
            mockService.Setup(service => service.AddApprovalStatusAsync(approvalStatusDto))
                .ReturnsAsync(new CreatedResult("", approvalStatusDto));
            var controller = new ApprovalStatusController(mockService.Object);

            // Act
            var result = await controller.AddApprovalStatus(approvalStatusDto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(approvalStatusDto, createdResult.Value);
        }

        [Fact]
        public async Task AddApprovalStatus_ReturnsBadRequest_WhenApprovalStatusDtoIsInvalid()
        {
            // Arrange
            var mockService = new Mock<IApprovalStatusService>();
            var approvalStatusDto = new ApprovalStatusDto();
            mockService.Setup(service => service.AddApprovalStatusAsync(approvalStatusDto))
                .ReturnsAsync(new BadRequestResult());
            var controller = new ApprovalStatusController(mockService.Object);

            // Act
            var result = await controller.AddApprovalStatus(approvalStatusDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        

    }
}
