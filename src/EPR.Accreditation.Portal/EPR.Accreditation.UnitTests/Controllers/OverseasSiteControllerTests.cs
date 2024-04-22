namespace EPR.Accreditation.UnitTests.Controllers
{
    using EPR.Accreditation.Portal.Controllers;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Moq;

    [TestClass]
    public class OverseasSiteControllerTests
    {
        private OverseasSiteController _overseasSiteController;
        private Mock<IOverseasSiteService> _mockOverseasSiteService;
        private Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IUrlHelperWrapper> _mockUrlHelper;
        private BackPageViewModel _backPageViewModel;

        [TestInitialize]
        public void Init()
        {
            _mockOverseasSiteService = new Mock<IOverseasSiteService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUrlHelper = new Mock<IUrlHelperWrapper>();
            _backPageViewModel = new BackPageViewModel();

            _overseasSiteController = new OverseasSiteController(
                _mockOverseasSiteService.Object,
                _mockSaveAndComeBackService.Object,
                _mockHttpContextAccessor.Object,
                _mockUrlHelper.Object,
                _backPageViewModel);

            var context = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(context => context.HttpContext).Returns(context);
        }

        [TestMethod]
        public async Task ReprocessorDetails_IdIsNull_ReturnsNotFound()
        {
            // Arrange
            Guid? id = null;
            Guid? overseasSiteId = Guid.NewGuid();

            // Act
            var result = await _overseasSiteController.ReprocessorDetails(id, overseasSiteId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);

            _mockOverseasSiteService.Verify(s => s.GetReprocessorDetailsViewModel(Guid.Empty, overseasSiteId.Value), Times.Never());
        }

        [TestMethod]
        public async Task ReprocessorDetails_WithValidId_ReturnsViewResult()
        {
            // Arrange
            Guid? id = Guid.NewGuid();
            Guid? overseasSiteId = Guid.NewGuid();
            var viewModel = new ReprocessorDetailsViewModel();

            _mockOverseasSiteService.Setup(s =>
                s.GetReprocessorDetailsViewModel(
                    id.Value,
                    overseasSiteId.Value))
                .ReturnsAsync(viewModel);

            // Act
            var result = await _overseasSiteController.ReprocessorDetails(id, overseasSiteId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(viewModel, result.Model);
            Assert.IsNull(result.ViewName);

            _mockOverseasSiteService.Verify(s => s.GetReprocessorDetailsViewModel(id.Value, overseasSiteId.Value), Times.Once());
        }

        [TestMethod]
        public async Task ReprocessorDetails_WithNullIds_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = await _overseasSiteController.ReprocessorDetails(null, null) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            _mockOverseasSiteService.Verify(s => s.GetReprocessorDetailsViewModel(Guid.Empty, Guid.Empty), Times.Never);
        }

        [TestMethod]
        public async Task ReprocessorDetails_InvalidModelState_ReturnsViewResult()
        {
            // Arrange
            Guid? id = Guid.NewGuid();
            Guid? overseasSiteId = Guid.NewGuid();
            var viewModel = new ReprocessorDetailsViewModel();
            var saveButton = SaveButton.SaveAndContinue;

            _overseasSiteController.ModelState.AddModelError("Dummy error", "Error message");

            _mockOverseasSiteService.Setup(s =>
               s.GetReprocessorDetailsViewModel(
                   id.Value,
                   overseasSiteId.Value))
               .ReturnsAsync(viewModel);

            // Act
            var result = await _overseasSiteController.ReprocessorDetails(viewModel, saveButton);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsNull(viewResult.ViewName);

            _mockOverseasSiteService.Verify(s => s.GetReprocessorDetailsViewModel(id.Value, overseasSiteId.Value), Times.Never);
        }

        [TestMethod]
        public async Task ReprocessorDetails_WithValidModelStateAndSaveButtonSaveAndContinue_RedirectsToCorrectPage()
        {
            // Arrange
            var viewModel = new ReprocessorDetailsViewModel();
            var saveButton = SaveButton.SaveAndContinue;

            // Act
            var result = await _overseasSiteController.ReprocessorDetails(viewModel, saveButton) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("PersonWeCanContact", result.ActionName);
            Assert.AreEqual("Accreditation", result.ControllerName);

            _mockOverseasSiteService.Verify(s => s.UpdateReprocessorDetails(viewModel), Times.Once);
        }

        [TestMethod]
        public async Task ReprocessorDetails_WithValidModelStateAndSaveButtonNotSaveAndContinue_SavesDataAndReturnsApplicationSavedView()
        {
            // Arrange
            var viewModel = new ReprocessorDetailsViewModel();
            var saveButton = SaveButton.SaveAndComeBack;

            // Act
            var result = await _overseasSiteController.ReprocessorDetails(viewModel, saveButton) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_ApplicationSaved", result.ViewName);

            _mockSaveAndComeBackService.Verify(s => s.AddSaveAndComeBack(viewModel.Id, It.IsAny<RouteValueDictionary>()), Times.Once);
            _mockOverseasSiteService.Verify(s => s.UpdateReprocessorDetails(viewModel), Times.Once);
        }
    }
}