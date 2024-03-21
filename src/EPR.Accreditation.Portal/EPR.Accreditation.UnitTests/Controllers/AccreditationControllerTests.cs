﻿
using EPR.Accreditation.Portal.Controllers;
using EPR.Accreditation.Portal.Services.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class AccreditationControllerTests
    {
        private Mock<IAccreditationService> _mockAccreditationService;
        private AccreditationController _accreditationController;

        [TestInitialize]
        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _accreditationController = new AccreditationController(_mockAccreditationService.Object);
        }

        [TestMethod]
        public void ApplicationSaved_ReturnsNotFound_WithNullId()
        {
            // Arrange
            Guid? id = null;

            // Act
            var result = _accreditationController.ApplicationSaved(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            _mockAccreditationService.Verify(service => service.GetApplicationSavedViewModel(It.IsAny<Guid>()), Times.Never);
        }

        [TestMethod]
        public void ApplicationSaved_ReturnsCorrectly_WithValidId()
        {
            // Arrange
            Guid id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var viewModel = new ApplicationSavedViewModel();

            _mockAccreditationService.Setup(service => service.GetApplicationSavedViewModel(id)).Returns(viewModel);

            // Act
            var result = _accreditationController.ApplicationSaved(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.ViewData.Model);

            // check model is expected type
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ApplicationSavedViewModel));

            // check view name
            Assert.IsNull(viewResult.ViewName); // It's going to return the view name of the action by default

            _mockAccreditationService.Verify(service => service.GetApplicationSavedViewModel(id), Times.Once);
        }
    }
}
