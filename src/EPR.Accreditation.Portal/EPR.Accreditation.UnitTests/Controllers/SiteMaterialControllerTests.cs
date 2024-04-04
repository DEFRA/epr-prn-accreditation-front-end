using EPR.Accreditation.Portal.Controllers;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class SiteMaterialControllerTests
    {
        protected SiteMaterialController _siteMaterialController;

        protected Mock<IUrlHelper> _mockUrlHelper;
        protected Mock<IAccreditationSiteMaterialService> _mockAccreditationSiteMaterialService;
        protected Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        protected BackPageViewModel _backPageViewModel;

        [TestInitialize]
        public void Init()
        {
            _mockUrlHelper = new Mock<IUrlHelper>();
            _mockAccreditationSiteMaterialService = new Mock<IAccreditationSiteMaterialService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService>();
            _backPageViewModel = new BackPageViewModel();

            //_siteMaterialController = new SiteMaterialController(
            //    _mockUrlHelper.Object,
            //    _mockAccreditationSiteMaterialService.Object,
            //    _mockSaveAndComeBackService.Object,
            //    _backPageViewModel);
        }

        [TestMethod]
        public async Task MaterialOutputs_AllParametersNotNull_ReturnsExpectedResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            var result = await _siteMaterialController.MaterialOutputs(id, siteId, materialId);

            // Assert
            Assert.IsNotNull(result); // Assuming you expect a non-null result
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
    }
}
