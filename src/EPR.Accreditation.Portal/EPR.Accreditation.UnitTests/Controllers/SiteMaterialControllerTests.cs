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

            _siteMaterialController = new SiteMaterialController(
                _mockUrlHelper.Object,
                _mockAccreditationSiteMaterialService.Object,
                _mockSaveAndComeBackService.Object,
                _backPageViewModel);
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

        [TestMethod]
        public async Task WasteLastYear_ReturnsView_ForValidIds_()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedViewModel = new ReprocessedWasteLastYearViewModel();

            _mockAccreditationSiteMaterialService.Setup(
                s => s.GetReprocessedWasteLastYearViewModel(id, siteId, materialId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await _siteMaterialController.WasteLastYear(id, siteId, materialId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual(expectedViewModel, viewResult.Model);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _siteMaterialController.WasteLastYear(null, Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsNotFound_WhenAllIdsAreNull()
        {
            // Act
            var result = await _siteMaterialController.WasteLastYear(null, null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsBadRequest_ForAnInvalidModelState()
        {
            // Arrange
            _siteMaterialController.ModelState.AddModelError("key", "error message");

            // Act
            var result = await _siteMaterialController.WasteLastYear(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task WasteLastYear_CallsUrlHelper_WithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            await _siteMaterialController.WasteLastYear(id, siteId, materialId);

            // Assert
            _mockUrlHelper.Verify(u => u.ActionLink("EnterProcessingCapacity", "SiteMaterial", null, null, null, null), Times.Once);
        }

        [TestMethod]
        public async Task WasteLastYear_ValidIds_ReturnsCorrectViewName()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedViewModel = new ReprocessedWasteLastYearViewModel();

            _mockAccreditationSiteMaterialService.Setup(
                s => s.GetReprocessedWasteLastYearViewModel(id, siteId, materialId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await _siteMaterialController.WasteLastYear(id, siteId, materialId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsNull(viewResult.ViewName);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            _mockAccreditationSiteMaterialService.Setup(
                s => s.GetReprocessedWasteLastYearViewModel(id, siteId, materialId)).ReturnsAsync((ReprocessedWasteLastYearViewModel)null);

            // Act
            var result = await _siteMaterialController.WasteLastYear(id, siteId, materialId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
