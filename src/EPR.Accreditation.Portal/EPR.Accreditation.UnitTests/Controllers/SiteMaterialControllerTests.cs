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

            var httpContext = new DefaultHttpContext();
            _mockContextAccessor.Setup(c => c.HttpContext).Returns(httpContext);
        }

        [TestMethod]
        public async Task MaterialWasteSource_AllParametersNotNull_ReturnsExpectedResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            var result = await _siteMaterialController.MaterialWasteSource(
                id,
                siteId,
                materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task MaterialOutputs_AllParametersNotNull_ReturnsExpectedResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                id, 
                siteId, 
                materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task MaterialWasteSource_AnyParameterNull_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = await _siteMaterialController.MaterialOutputs(null, Guid.NewGuid(), Guid.NewGuid());
            var result2 = await _siteMaterialController.MaterialOutputs(Guid.NewGuid(), null, Guid.NewGuid());
            var result3 = await _siteMaterialController.MaterialOutputs(Guid.NewGuid(), Guid.NewGuid(), null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result3, typeof(NotFoundResult));
            // You can add more specific assertions here based on the expected behavior
        }

        [TestMethod]
        public async Task MaterialOutputs_AnyParameterNull_ReturnsNotFound()
        {
            // Arrange
            
            // Act
            var result = await _siteMaterialController.MaterialOutputs(null, Guid.NewGuid(), Guid.NewGuid());
            var result2 = await _siteMaterialController.MaterialOutputs(Guid.NewGuid(), null, Guid.NewGuid());
            var result3 = await _siteMaterialController.MaterialOutputs(Guid.NewGuid(), Guid.NewGuid(), null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result3, typeof(NotFoundResult));
            // You can add more specific assertions here based on the expected behavior
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
