
using EPR.Accreditation.Portal.Controllers;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class AccreditationControllerTests
    {
        protected Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        protected Mock<IHttpAccreditationService> _mockhttpAccreditationService;
        private Mock<IAccreditationService> _mockAccreditationService;
        private Mock<IWastePermitService> _mockWastePermitService;
        private Mock<IUrlHelper> _mockUrlHelper;
        private Mock<BackPageViewModel> _backPageViewModel;
        private AccreditationController _accreditationController;

        [TestInitialize]
        public void Init()
        {
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService> { CallBase = true };
            _mockhttpAccreditationService = new Mock<IHttpAccreditationService>();
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockWastePermitService = new Mock<IWastePermitService>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _backPageViewModel = new Mock<BackPageViewModel>();

            _accreditationController = new AccreditationController(
                _mockWastePermitService.Object,
                _mockSaveAndComeBackService.Object,
                _mockAccreditationService.Object,
                _mockUrlHelper.Object,
                _backPageViewModel.Object);
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
            var expectedUrl = "expectedUrl";

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
            // Arrange
            Guid? id = null;

            // Act
            var result = await _accreditationController.CheckWastePermitExemption(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            _mockWastePermitService.Verify(service => service.GetPermitExemptionViewModel((Guid)id), Times.Never());

        }

        [TestMethod]
        public async Task CheckWastePermitExemption_SetsBackPageViewModelUrl()
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
            var viewResult = (ViewResult)result;
            Assert.AreEqual(expectedUrl, viewResult.ViewData["Url"]);

            _mockWastePermitService.Verify(service => service.GetPermitExemptionViewModel(id), Times.Once());

        }
    }
}
