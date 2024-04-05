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
    public class OverseasSiteMaterialControllerTests
    {
        protected OverseasSiteMaterialController _siteMaterialController;

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

            _siteMaterialController = new OverseasSiteMaterialController(
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
                It.Is<SiteType>(p => p == SiteType.OverseasSite),
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
                    SiteType.OverseasSite,
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
    }
}
