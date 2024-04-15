namespace EPR.Accreditation.UnitTests.Controllers
{

    using EPR.Accreditation.Portal.Controllers;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Moq;

    [TestClass]
    public class AccreditationSiteControllerTests
    {
        private AccreditationSiteController _accreditationSiteController;
        private Mock<IAccreditationSiteService> _mockAccreditationSiteService;
        private Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private BackPageViewModel _backPageViewModel;

        [TestInitialize]
        public void Init()
        {
            _mockAccreditationSiteService = new Mock<IAccreditationSiteService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _backPageViewModel = new BackPageViewModel();

            _accreditationSiteController = new AccreditationSiteController(
                _mockAccreditationSiteService.Object,
                _mockSaveAndComeBackService.Object,
                _mockHttpContextAccessor.Object,
                _backPageViewModel);

            var context = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(context => context.HttpContext).Returns(context);
        }

        [TestMethod]
        public async Task ExemptionReferences_WithValidId_ReturnsViewWithViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedViewModel = new ExemptionReferencesViewModel();

            _mockAccreditationSiteService.Setup(s => s.GetExemptionReferencesViewModel(id)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await _accreditationSiteController.ExemptionReferences(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewModel, result.Model);
            Assert.IsNull(result.ViewName);
            Assert.AreEqual($"/Accreditation/{id}/PermitExemption", _backPageViewModel.Url);

            _mockAccreditationSiteService.Verify(s => s.GetExemptionReferencesViewModel(id), Times.Once);
        }

        [TestMethod]
        public async Task ExemptionReferences_WithNullId_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = await _accreditationSiteController.ExemptionReferences(null) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            _mockAccreditationSiteService.Verify(s => s.GetExemptionReferencesViewModel(Guid.Empty), Times.Never);
        }

        [TestMethod]
        public async Task ExemptionReferences_WithInvalidModelState_ReturnsViewWithViewModel()
        {
            // Arrange
            var viewModel = new ExemptionReferencesViewModel();
            _accreditationSiteController.ModelState.AddModelError("dummy property name", "ErrorMessage");

            // Act
            var result = await _accreditationSiteController.ExemptionReferences(viewModel, SaveButton.SaveAndContinue) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(viewModel, result.Model);
            Assert.AreEqual($"/Accreditation/{viewModel.Id}/PermitExemption", _backPageViewModel.Url);

            _mockAccreditationSiteService.Verify(s => s.GetExemptionReferencesViewModel(Guid.Empty), Times.Never);
        }

        [TestMethod]
        public async Task ExemptionReferences_WithValidModelStateAndSaveButtonSaveAndContinue_RedirectsToHowManyTonnes()
        {
            // Arrange
            var viewModel = new ExemptionReferencesViewModel();
            var saveButton = SaveButton.SaveAndContinue;

            // Act
            var result = await _accreditationSiteController.ExemptionReferences(viewModel, saveButton) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("HowManyTonnes", result.ActionName);
            Assert.AreEqual("Accreditation", result.ControllerName);

            _mockAccreditationSiteService.Verify(s => s.UpdateExemptionReferences(viewModel), Times.Once);
        }

        [TestMethod]
        public async Task ExemptionReferences_WithValidModelStateAndSaveButtonNotSaveAndContinue_SavesDataAndReturnsApplicationSavedView()
        {
            // Arrange
            var viewModel = new ExemptionReferencesViewModel();
            var saveButton = SaveButton.SaveAndComeBack;

            // Act
            var result = await _accreditationSiteController.ExemptionReferences(viewModel, saveButton) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_ApplicationSaved", result.ViewName);

            _mockSaveAndComeBackService.Verify(s => s.AddSaveAndComeBack(viewModel.Id, It.IsAny<RouteValueDictionary>()), Times.Once);
            _mockAccreditationSiteService.Verify(s => s.UpdateExemptionReferences(viewModel), Times.Once);
        }
    }
}
