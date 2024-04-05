using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Portal.Controllers;
using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Services.Accreditation;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [Ignore]
    [TestClass]
    public class SiteMaterialControllerTests
    {
        protected SiteMaterialController _siteMaterialController;

        protected Mock<IHttpContextAccessor> _mockContextAccessor;
        protected Mock<IUrlHelper> _mockUrlHelper;
        protected Mock<IAccreditationSiteMaterialService> _mockAccreditationSiteMaterialService;
        protected Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        protected BackPageViewModel _backPageViewModel;

        [TestInitialize]
        public void Init()
        {
            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _mockAccreditationSiteMaterialService = new Mock<IAccreditationSiteMaterialService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService>();
            _backPageViewModel = new BackPageViewModel();
            
            _siteMaterialController = new SiteMaterialController(
                _mockContextAccessor.Object,
                _mockUrlHelper.Object,
                _mockAccreditationSiteMaterialService.Object,
                _mockSaveAndComeBackService.Object,
                _backPageViewModel);

            var context = new DefaultHttpContext();
            _mockContextAccessor.Setup(context => context.HttpContext).Returns(context);
        }

        [TestMethod]
        public async Task MaterialOutputs_AllParametersNotNull_WithNullWasteLastYear_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            _mockAccreditationSiteMaterialService.Setup(a => a.GetMaterialOutputs(
                id,
                materialId)).ReturnsAsync(new MaterialOutputsViewModel());

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                id,
                materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task MaterialOutputs_AllParametersNotNull_WithWasteLastYearTrue_ReturnsExpectedResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            var materialOutputsViewModel = new MaterialOutputsViewModel
            {
                WasteLastYear = true,
            };

            _mockAccreditationSiteMaterialService.Setup(a => a.GetMaterialOutputs(
                id,
                materialId)).ReturnsAsync(materialOutputsViewModel);

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                id, 
                materialId);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("MaterialOutputsLastYear", viewResult.ViewName);
        }

        [TestMethod]
        public async Task MaterialOutputs_AllParametersNotNull_WithWasteLastYearFalse_ReturnsExpectedResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            var materialOutputsViewModel = new MaterialOutputsViewModel
            {
                WasteLastYear = false,
            };

            _mockAccreditationSiteMaterialService.Setup(a => a.GetMaterialOutputs(
                id,
                materialId)).ReturnsAsync(materialOutputsViewModel);

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                id,
                materialId);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("MaterialOutputsEstimated", viewResult.ViewName);
        }

        [TestMethod]
        public async Task MaterialWasteSource_AnyParameterNull_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = await _siteMaterialController.MaterialOutputs(null, Guid.NewGuid());
            var result2 = await _siteMaterialController.MaterialOutputs(Guid.NewGuid(), null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));
            // You can add more specific assertions here based on the expected behavior
        }

        [TestMethod]
        public async Task MaterialOutputs_AnyParameterNull_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = await _siteMaterialController.MaterialOutputs(null, Guid.NewGuid());
            var result2 = await _siteMaterialController.MaterialOutputs(Guid.NewGuid(), null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsView_ForValidIds()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedViewModel = new ReprocessedWasteLastYearViewModel();

            _mockAccreditationSiteMaterialService.Setup(
                s => s.GetReprocessedWasteLastYearViewModel(id, materialId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await _siteMaterialController.WasteLastYear(id, materialId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual(expectedViewModel, viewResult.Model);

            _mockAccreditationSiteMaterialService.Verify(service => service.GetReprocessedWasteLastYearViewModel(id, materialId), Times.Once);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _siteMaterialController.WasteLastYear(null, Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            _mockAccreditationSiteMaterialService.Verify(
                service => service.GetReprocessedWasteLastYearViewModel(Guid.Empty, Guid.NewGuid()), Times.Never);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsNotFound_WhenAllIdsAreNull()
        {
            // Act
            var result = await _siteMaterialController.WasteLastYear(null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            _mockAccreditationSiteMaterialService.Verify(
                service => service.GetReprocessedWasteLastYearViewModel(Guid.Empty, Guid.Empty), Times.Never);
        }

        [TestMethod]
        [Ignore]
        public async Task WasteLastYear_ReturnsBadRequest_ForAnInvalidModelState()
        {
            // Arrange
            _siteMaterialController.ModelState.AddModelError("key", "error message");

            // Act
            var result = await _siteMaterialController.WasteLastYear(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        [Ignore]
        public async Task WasteLastYear_CallsUrlHelper_WithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            await _siteMaterialController.WasteLastYear(id, materialId);

            // Assert
            _mockUrlHelper.Verify(u => u.ActionLink("EnterProcessingCapacity", "SiteMaterial", null, null, null, null), Times.Once);
            _mockAccreditationSiteMaterialService.Verify(
                service => service.GetReprocessedWasteLastYearViewModel(id, materialId), Times.Once);
        }

        [TestMethod]
        public async Task WasteLastYear_ValidIds_ReturnsCorrectViewName()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedViewModel = new ReprocessedWasteLastYearViewModel();

            _mockAccreditationSiteMaterialService.Setup(
                s => s.GetReprocessedWasteLastYearViewModel(id, materialId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await _siteMaterialController.WasteLastYear(id, materialId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsNull(viewResult.ViewName);

            _mockAccreditationSiteMaterialService.Verify(
                service => service.GetReprocessedWasteLastYearViewModel(id, materialId), Times.Once);
        }

        [TestMethod]
        [Ignore]
        public async Task WasteLastYear_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            _mockAccreditationSiteMaterialService.Setup(
                s => s.GetReprocessedWasteLastYearViewModel(id, materialId)).ReturnsAsync((ReprocessedWasteLastYearViewModel)null);

            // Act
            var result = await _siteMaterialController.WasteLastYear(id, materialId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            _mockAccreditationSiteMaterialService.Verify(
                service => service.GetReprocessedWasteLastYearViewModel(id, materialId), Times.Once);
        }

        [TestMethod]
        [Ignore]
        public async Task WasteLastYear_SavesWithValidData_SaveAndContinue()
        {
            // Arrange
            var viewModel = new ReprocessedWasteLastYearViewModel
            {
                Id = Guid.NewGuid(),
                HasReprocessedWasteLastYear = true
            };

            // Act
            var result = await _siteMaterialController.WasteLastYear(viewModel, SaveButton.SaveAndContinue);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNull(redirectToActionResult.ControllerName);

            _mockAccreditationSiteMaterialService.Verify(service => service.UpdateReprocessedWasteLastYear(viewModel), Times.Once);
        }

        [TestMethod]
        [Ignore]
        public async Task WasteLastYear_SavesWithValidData_SaveAndComeBack()
        {
            // Arrange
            var viewModel = new ReprocessedWasteLastYearViewModel
            {
                Id = Guid.NewGuid(),
                HasReprocessedWasteLastYear = false
            };

            // Act
            var result = await _siteMaterialController.WasteLastYear(viewModel, SaveButton.SaveAndComeBack);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNull(redirectToActionResult.ControllerName);

            _mockAccreditationSiteMaterialService.Verify(service => service.UpdateReprocessedWasteLastYear(viewModel), Times.Once);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsCorrectView_WhenModelIsInvalid()
        {
            // Arrange
            var viewModel = new ReprocessedWasteLastYearViewModel();
            var saveButton = new SaveButton();

            _siteMaterialController.ModelState.AddModelError("Error", "Error");

            // Act
            var result = await _siteMaterialController.WasteLastYear(viewModel, saveButton);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.ViewData.Model);

            // check model is expected type
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ReprocessedWasteLastYearViewModel));

            // check view name
            Assert.IsNull(viewResult.ViewName); // It's going to return the view name of the action by default

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateReprocessedWasteLastYear(viewModel), Times.Never);
        }

        [TestMethod]
        public async Task SaveMaterialWasteSource_ModelStateInvalid_ReturnsViewResult()
        {
            // Arrange
            var viewModel = new WasteSourceViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.AddModelError("PropertyName", "ErrorMessage");

            // Act
            var result = await _siteMaterialController.MaterialWasteSource(
                viewModel, 
                saveButton);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsNull(viewResult.ViewName);
            _mockAccreditationSiteMaterialService.Verify(s => s.GetWasteSource(
                It.Is<SiteType>(p => p == SiteType.Site),
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                It.IsAny<Guid>()));
        }

        [TestMethod]
        public async Task SaveMaterialWasteSource_SaveAndComeBack_ReturnsViewResult()
        {
            // Arrange
            var viewModel = new WasteSourceViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            // Act
            var result = await _siteMaterialController.MaterialWasteSource(
                viewModel, 
                saveButton) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_ApplicationSaved", result.ViewName);
            _mockAccreditationSiteMaterialService.Verify(s => 
                s.UpdateWasteSource(
                    SiteType.Site,
                    viewModel), 
                Times.Once());
            _mockSaveAndComeBackService.Verify(x => 
                x.AddSaveAndComeBack(
                    It.IsAny<Guid>(),
                    It.IsAny<RouteValueDictionary>()),
                Times.Once());
        }

        [TestMethod]
        public async Task SaveMaterialWasteSource_SaveAndComeBack_CallsServicesCorrectly()
        {
            // Arrange
            var viewModel = new WasteSourceViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            // Act
            await _siteMaterialController.MaterialWasteSource(viewModel, saveButton);

            // Assert
            _mockAccreditationSiteMaterialService.Verify(x => x.UpdateWasteSource(It.IsAny<SiteType>(), viewModel), Times.Once);
            _mockSaveAndComeBackService.Verify(x => x.AddSaveAndComeBack(
                It.IsAny<Guid>(), 
                It.IsAny<RouteValueDictionary>()), Times.Once);
        }

        [TestMethod]
        public async Task MaterialOutputs_ModelStateInvalid_ReturnsViewResult()
        {
            // Arrange
            var viewModel = new MaterialOutputsViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.AddModelError("PropertyName", "ErrorMessage");

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                viewModel,
                saveButton);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsNull(viewResult.ViewName);
            _mockAccreditationSiteMaterialService.Verify(s => 
                s.GetMaterialOutputs(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()), 
                Times.Once);
        }

        [TestMethod]
        public async Task MaterialOutputs_SaveAndComeBack_ReturnsViewResult()
        {
            // Arrange
            var viewModel = new MaterialOutputsViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                viewModel,
                saveButton) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_ApplicationSaved", result.ViewName);
            _mockAccreditationSiteMaterialService.Verify(s =>
                s.UpdateMaterialOutputs(
                    viewModel),
                Times.Once());
            _mockSaveAndComeBackService.Verify(x =>
                x.AddSaveAndComeBack(
                    It.IsAny<Guid>(),
                    It.IsAny<RouteValueDictionary>()),
                Times.Once());
        }

        [TestMethod]
        public async Task MaterialOutputs_SaveAndComeBack_CallsServicesCorrectly()
        {
            // Arrange
            var viewModel = new MaterialOutputsViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            _mockAccreditationSiteMaterialService.Setup(x => x.UpdateMaterialOutputs(viewModel)).Returns(Task.CompletedTask);
            _mockSaveAndComeBackService.Setup(x => x.AddSaveAndComeBack(
                It.IsAny<Guid>(),
                It.IsAny<RouteValueDictionary>()))
            .Returns(Task.CompletedTask);

            // Act
            await _siteMaterialController.MaterialOutputs(viewModel, saveButton);

            // Assert
            _mockAccreditationSiteMaterialService.Verify(x => x.UpdateMaterialOutputs(viewModel), Times.Once);
            _mockSaveAndComeBackService.Verify(x => x.AddSaveAndComeBack(
                It.IsAny<Guid>(),
                It.IsAny<RouteValueDictionary>()), Times.Once);
        }
    }
}
