namespace EPR.Accreditation.UnitTests.Controllers
{
    using EPR.Accreditation.Portal.Controllers;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
    using Moq;

    [TestClass]
    public class OverseasSiteMaterialControllerTests
    {
        private OverseasSiteMaterialController _siteMaterialController;
        private Mock<IHttpContextAccessor> _mockContextAccessor;
        private Mock<IOptions<AppSettingsConfigOptions>> _mockOptions;
        private Mock<IUrlHelperWrapper> _mockUrlHelper;
        private Mock<IAccreditationSiteMaterialService> _mockAccreditationSiteMaterialService;
        private Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        private BackPageViewModel _backPageViewModel;
        private AppSettingsConfigOptions _appSettingsConfig;

        [TestInitialize]
        public void Init()
        {
            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _mockOptions = new Mock<IOptions<AppSettingsConfigOptions>>();
            _mockUrlHelper = new Mock<IUrlHelperWrapper>();
            _mockAccreditationSiteMaterialService = new Mock<IAccreditationSiteMaterialService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService>();
            _backPageViewModel = new BackPageViewModel();

            _appSettingsConfig = new AppSettingsConfigOptions
            {
                MaximumMultiLineRecordNumber = 10,
                InitialTypeTonnesRows = 1
            };

            _mockOptions.Setup(o => o.Value).Returns(_appSettingsConfig);

            _siteMaterialController = new OverseasSiteMaterialController(
                _mockContextAccessor.Object,
                _mockOptions.Object,
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
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateWasteSource(
                        SiteType.OverseasSite,
                        viewModel),
                Times.Once());
            _mockSaveAndComeBackService.Verify(
                x =>
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
            _mockSaveAndComeBackService.Verify(
                x =>
                    x.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Once);
        }

        [TestMethod]
        public async Task WasteDescription_NullIdsSupplied_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result1 = await _siteMaterialController.WasteDescription(
                Guid.NewGuid(),
                null,
                null) as NotFoundResult;
            var result2 = await _siteMaterialController.WasteDescription(
                null,
                Guid.NewGuid(),
                null) as NotFoundResult;
            var result3 = await _siteMaterialController.WasteDescription(
                null,
                null,
                Guid.NewGuid()) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);
        }

        [TestMethod]
        public async Task WasteDescription_ValidIdsSupplied_CallsService()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            var result = await _siteMaterialController.WasteDescription(
                id,
                siteId,
                materialId) as ViewResult;

            // Assert
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.GetWasteDescriptionCodeViewModel(
                        id,
                        siteId,
                        materialId),
                Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task WasteDescription_AddRowSaveButtonRowsLessThanMax_AddRowAndReturnView()
        {
            // Arrange
            var viewModel = new WasteDescriptionCodeViewModel
            {
                Rows = new List<WasteDescriptionCodeRowViewModel>()
            };

            // Act
            var result = await _siteMaterialController.WasteDescription(
                viewModel,
                SaveButton.AddRow) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as WasteDescriptionCodeViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.RowsToAdd);
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateWasteDescriptionCodeViewModel(
                        It.IsAny<WasteDescriptionCodeViewModel>()),
                Times.Never);
        }

        [TestMethod]
        public async Task WasteDescription_WithMaxRows_DoesNotAddRowAndReturnView()
        {
            // Arrange
            var viewModel = new WasteDescriptionCodeViewModel
            {
                Rows = new List<WasteDescriptionCodeRowViewModel>()
            };

            for (int i = 0; i < 10; i++)
            {
                viewModel.Rows.Add(new WasteDescriptionCodeRowViewModel
                {
                    WasteDescriptionCode = "AAA"
                });
            }

            // Act
            var result = await _siteMaterialController.WasteDescription(
                viewModel,
                SaveButton.AddRow) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as WasteDescriptionCodeViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.RowsToAdd);
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateWasteDescriptionCodeViewModel(
                        It.IsAny<WasteDescriptionCodeViewModel>()),
                Times.Never);
        }

        [TestMethod]
        public async Task WasteDescription_WithModelErrors_ReturnsView()
        {
            // Arrange
            var viewModel = new WasteDescriptionCodeViewModel();
            _siteMaterialController.ModelState.AddModelError("A", "A");

            // Act
            var result = await _siteMaterialController.WasteDescription(
                viewModel,
                SaveButton.SaveAndContinue) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(viewModel, result.Model);
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateWasteDescriptionCodeViewModel(
                        It.IsAny<WasteDescriptionCodeViewModel>()),
                Times.Never);
        }

        [TestMethod]
        public async Task WasteDescription_SaveAndComeBackLaterButton_CallsServices()
        {
            // Arrange
            var viewModel = new WasteDescriptionCodeViewModel();

            // Act
            var result = await _siteMaterialController.WasteDescription(
                viewModel,
                SaveButton.SaveAndComeBack) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_ApplicationSaved", result.ViewName);
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateWasteDescriptionCodeViewModel(
                        viewModel),
                Times.Once);
            _mockSaveAndComeBackService.Verify(
                s =>
                    s.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Once);
        }

        [TestMethod]
        public async Task WasteDescription_SaveAndContinueButton_CallsServices()
        {
            // Arrange
            var viewModel = new WasteDescriptionCodeViewModel();

            // Act
            var result = await _siteMaterialController.WasteDescription(
                viewModel,
                SaveButton.SaveAndContinue) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateWasteDescriptionCodeViewModel(
                        viewModel),
                Times.Once);
            _mockSaveAndComeBackService.Verify(
                s =>
                    s.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Never);
        }
    }
}
