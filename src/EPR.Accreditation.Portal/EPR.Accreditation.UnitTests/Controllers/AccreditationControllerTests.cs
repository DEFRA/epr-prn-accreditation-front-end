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
        protected AccreditationController _accreditationController;

        protected Mock<IUrlHelper> _mockUrlHelper;
        protected Mock<IWastePermitService> _mockWastePermitService;
        protected Mock<IAccreditationService> _mockAccreditationService;
        protected Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        protected BackPageViewModel _backPageViewModel;
        
        [TestInitialize]
        public void Init()
        {
            _mockUrlHelper = new Mock<IUrlHelper>();
            _mockWastePermitService = new Mock<IWastePermitService>();
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService>();
            _backPageViewModel = new BackPageViewModel();

            _accreditationController = new AccreditationController(
                _mockWastePermitService.Object,
                _mockSaveAndComeBackService.Object,
                _mockUrlHelper.Object,
                _backPageViewModel,
                _mockAccreditationService.Object);
        }

        [TestMethod]
        public async Task TaskList_AllParameters_NotNulll_ReturnsExpectedResult()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _accreditationController.TaskList(id);

            // Assert
            Assert.IsNotNull(result); // Assuming you expect a non-null result
        }

    }
}
