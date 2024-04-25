namespace EPR.Accreditation.UnitTests.Controllers
{
    using EPR.Accreditation.Portal.Controllers;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Moq;

    [TestClass]
    public class AccreditationControllerTests
    {
        private Mock<IHttpContextAccessor> _mockContextAccessor;
        private Mock<IHttpAccreditationService> _mockhttpAccreditationService;
        private Mock<IAccreditationService> _mockAccreditationService;
        private Mock<IWastePermitService> _mockWastePermitService;
        private Mock<IUrlHelperWrapper> _mockUrlHelper;
        private AccreditationController _accreditationController;
        private BackPageViewModel _backPageViewModel;

        [TestInitialize]
        public void Init()
        {
            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _mockhttpAccreditationService = new Mock<IHttpAccreditationService>();
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockWastePermitService = new Mock<IWastePermitService>();
            _mockUrlHelper = new Mock<IUrlHelperWrapper>();
            _backPageViewModel = new BackPageViewModel();

            _accreditationController = new AccreditationController(
                _mockContextAccessor.Object,
                _mockWastePermitService.Object,
                _mockAccreditationService.Object,
                _mockUrlHelper.Object,
                _backPageViewModel);

            var context = new DefaultHttpContext();
            _mockContextAccessor.Setup(context => context.HttpContext).Returns(context);
        }

        [TestMethod]
        [Ignore]
        public void WasteLicensesAndPermits_ReturnsCorrectly_WithValidId()
        {
            // Arrange
            Guid id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var viewModel = new WasteLicensesAndPermitsViewModel();

            _mockAccreditationService.Setup(service => service.GetWastePermitViewModel(id)).ReturnsAsync(viewModel);

            // Act
            var result = _accreditationController.WasteLicensesAndPermits(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(ViewResult));

            var viewResult = result.Result as ViewResult;
            Assert.IsNotNull(viewResult.ViewData.Model);

            // check model is expected type
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(WasteLicensesAndPermitsViewModel));

            // check view name
            Assert.IsNull(viewResult.ViewName); // It's going to return the view name of the action by default

            _mockAccreditationService.Verify(service => service.GetWastePermitViewModel(id), Times.Once);
        }

        [TestMethod]
        public async Task CheckWastePermitExemption_ReturnsViewWithViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedViewModel = new PermitExemptionViewModel();
            var expectedUrl = "Home/ApplyForAccreditation";

            _mockUrlHelper.Setup(helper => helper.ActionLink(
                "ApplyForAccreditation", "Home", null, null, null, null)).Returns(expectedUrl);

            _mockWastePermitService.Setup(service => service.GetPermitExemptionViewModel(id)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await _accreditationController.CheckWastePermitExemption(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.ViewData.Model);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(PermitExemptionViewModel));
            Assert.IsNull(viewResult.ViewName);

            _mockWastePermitService.Verify(service => service.GetPermitExemptionViewModel(id), Times.Once());
        }

        [TestMethod]
        public async Task CheckWastePermitExemption_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _accreditationController.CheckWastePermitExemption(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            _mockWastePermitService.Verify(service => service.GetPermitExemptionViewModel(Guid.Empty), Times.Never());
        }

        [TestMethod]
        public async Task CheckWastePermitExemption_CallsUrlHelper()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedUrl = "expectedUrl";

            _mockUrlHelper.Setup(helper => helper.ActionLink(
                "ApplyForAccreditation", "Home", null, null, null, null)).Returns(expectedUrl);

            // Act
            var result = await _accreditationController.CheckWastePermitExemption(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            _mockUrlHelper.Verify(helper => helper.ActionLink("ApplyForAccreditation", "Home", null, null, null, null), Times.Once);
            _mockWastePermitService.Verify(service => service.GetPermitExemptionViewModel(id), Times.Once());
        }

        [TestMethod]
        public async Task CheckWastePermitExemption_SavesWithValidData_SaveAndContinue()
        {
            // Arrange
            var viewModel = new PermitExemptionViewModel
            {
                Id = Guid.NewGuid(),
                HasPermitExemption = true
            };

            // Act
            var result = await _accreditationController.CheckWastePermitExemption(viewModel, SaveButton.SaveAndContinue);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("ExemptionReferences", redirectToActionResult.ActionName);

            _mockWastePermitService.Verify(service => service.UpdatePermitExemption(viewModel), Times.Once);
        }

        [TestMethod]
        public async Task CheckWastePermitExemption_ReturnsCorrectView_WhenModelIsInvalid()
        {
            // Arrange
            var viewModel = new PermitExemptionViewModel();
            var saveButton = SaveButton.Undefined;

            _accreditationController.ModelState.AddModelError("Error", "Error");

            // Act
            var result = await _accreditationController.CheckWastePermitExemption(viewModel, saveButton);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.ViewData.Model);

            // check model is expected type
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(PermitExemptionViewModel));

            // check view name
            Assert.IsNull(viewResult.ViewName); // It's going to return the view name of the action by default

            _mockWastePermitService.Verify(s => s.UpdatePermitExemption(viewModel), Times.Never);
        }
    }
}