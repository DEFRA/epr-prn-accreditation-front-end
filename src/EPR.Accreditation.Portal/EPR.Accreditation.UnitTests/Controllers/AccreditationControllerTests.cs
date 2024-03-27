using EPR.Accreditation.Portal.Controllers;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class AccreditationControllerTests
    {
        private Mock<IWastePermitService> _mockWastePermitService;
        private Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        private Mock<BackPageViewModel> _mockBackPageViewModel;
        private Mock<IUrlHelper> _mockUrlHelper;
        private Mock<IAccreditationService> _mockAccreditationService;
        private AccreditationController _accreditationController;

        [TestInitialize]
        public void Init()
        {
            _mockWastePermitService = new Mock<IWastePermitService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService>();
            _mockBackPageViewModel = new Mock<BackPageViewModel>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _mockAccreditationService = new Mock<IAccreditationService>();

            _accreditationController = new AccreditationController(
                _mockWastePermitService.Object,
                _mockSaveAndComeBackService.Object,
                _mockUrlHelper.Object,
                _mockBackPageViewModel.Object,
                _mockAccreditationService.Object);
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
