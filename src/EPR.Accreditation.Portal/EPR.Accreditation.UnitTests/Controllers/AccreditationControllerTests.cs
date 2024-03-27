
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
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService> { CallBase = true };
            _accreditationController = new AccreditationController(_mockWastePermitService.Object, _mockSaveAndComeBackService.Object, _mockAccreditationService.Object, _mockUrlHelper.Object, _backPageViewModel.Object);
        }

        [TestMethod]
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
    }
}
