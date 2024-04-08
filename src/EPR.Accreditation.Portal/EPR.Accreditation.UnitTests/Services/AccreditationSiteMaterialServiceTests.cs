using AutoMapper;
using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Moq;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class AccreditationSiteMaterialServiceTests
    {
        protected AccreditationSiteMaterialService _accreditationSiteMaterialService;
        protected Mock<IMapper> _mockMapper;
        protected Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        protected Mock<IHttpSiteMaterialService> _mockHttpSiteMaterialService;

        [TestInitialize]
        public void Init()
        {
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpSiteMaterialService = new Mock<IHttpSiteMaterialService>();
            _accreditationSiteMaterialService = new AccreditationSiteMaterialService(
                _mockMapper.Object,
                _mockHttpContextAccessor.Object,
                _mockHttpSiteMaterialService.Object);
        }

        [TestMethod]
        public async Task GetWasteName_WithValidParameters_English_ReturnsWasteName()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var cultureFeatureMock = new Mock<IRequestCultureFeature>();
            var culture = "en-GB";
            var requestCulture = new RequestCulture(culture);
            cultureFeatureMock.Setup(f => f.RequestCulture).Returns(requestCulture);

            var featureCollection = new Mock<IFeatureCollection>();
            featureCollection.Setup(f => f.Get<IRequestCultureFeature>()).Returns(cultureFeatureMock.Object);

            var httpContextMock = new DefaultHttpContext(featureCollection.Object);

            _mockHttpContextAccessor.SetupGet(h => h.HttpContext).Returns(httpContextMock);

            var expectedWasteName = "SomeWasteName";
            _mockHttpSiteMaterialService.Setup(x =>
                x.GetMeterialName(
                    id,
                    siteId,
                    materialId,
                    It.IsAny<Language>()))
                .ReturnsAsync(expectedWasteName);

            // Act
            var result = await _accreditationSiteMaterialService.GetWasteName(id, siteId, materialId);

            // Assert
            Assert.AreEqual(expectedWasteName, result);
            _mockHttpSiteMaterialService.Verify(s =>
                s.GetMeterialName(
                    id,
                    siteId,
                    materialId,
                    Language.English),
                Times.Once);
        }

        [TestMethod]
        public async Task GetWasteName_WithValidParameters_Welsh_ReturnsWasteName()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var cultureFeatureMock = new Mock<IRequestCultureFeature>();
            var culture = "cy-GB";
            var requestCulture = new RequestCulture(culture);
            cultureFeatureMock.Setup(f => f.RequestCulture).Returns(requestCulture);

            var featureCollection = new Mock<IFeatureCollection>();
            featureCollection.Setup(f => f.Get<IRequestCultureFeature>()).Returns(cultureFeatureMock.Object);

            var httpContextMock = new DefaultHttpContext(featureCollection.Object);

            _mockHttpContextAccessor.SetupGet(h => h.HttpContext).Returns(httpContextMock);

            var expectedWasteName = "SomeWasteName";
            _mockHttpSiteMaterialService.Setup(x =>
                x.GetMeterialName(
                    id,
                    siteId,
                    materialId,
                    It.IsAny<Language>()))
                .ReturnsAsync(expectedWasteName);

            // Act
            var result = await _accreditationSiteMaterialService.GetWasteName(id, siteId, materialId);

            // Assert
            Assert.AreEqual(expectedWasteName, result);
            _mockHttpSiteMaterialService.Verify(s =>
                s.GetMeterialName(
                    id,
                    siteId,
                    materialId,
                    Language.Welsh),
                Times.Once);
        }

        [TestMethod]
        public async Task GetWasteSource_WithValidParameters_ReturnsWasteSourceViewModel()
        {
            // Arrange
            var siteType = SiteType.Site;
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedWasteSource = "SomeWasteSource";
            _mockHttpSiteMaterialService.Setup(x => x.GetWasteSource(siteType, id, siteId, materialId))
                .ReturnsAsync(expectedWasteSource);

            // Act
            var result = await _accreditationSiteMaterialService.GetWasteSource(siteType, id, siteId, materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedWasteSource, result.WasteSource);
        }

        [TestMethod]
        public async Task UpdateWasteSource_WithValidParameters_CallsHttpServiceAndUpdate()
        {
            // Arrange
            var siteType = SiteType.Site;
            var viewModel = new WasteSourceViewModel
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                MaterialId = Guid.NewGuid(),
                WasteSource = "SomeWasteSource"
            };

            // Act
            await _accreditationSiteMaterialService.UpdateWasteSource(siteType, viewModel);

            // Assert
            _mockHttpSiteMaterialService.Verify(x =>
                x.UpdateWasteSource(
                    siteType,
                    viewModel.Id,
                    viewModel.SiteId,
                    viewModel.MaterialId,
                    viewModel.WasteSource),
                Times.Once);
        }

        [TestMethod]
        public async Task GetMaterialOutputs_WithValidParameters_ReturnsMaterialOutputsViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var materialOutputsDto = new MaterialOutputsDto(); // Assuming MaterialOutputsDto is defined
            var expectedViewModel = new MaterialOutputsViewModel(); // Assuming MaterialOutputsViewModel is defined
            _mockHttpSiteMaterialService.Setup(x => x.GetMaterialOutputs(id, materialId))
                .ReturnsAsync(materialOutputsDto);
            _mockMapper.Setup(x => x.Map<MaterialOutputsViewModel>(materialOutputsDto))
                .Returns(expectedViewModel);

            // Act
            var result = await _accreditationSiteMaterialService.GetMaterialOutputs(id, materialId);

            // Assert
            Assert.AreEqual(expectedViewModel, result);
        }

        [TestMethod]
        public async Task UpdateMaterialOutputs_WithValidParameters_CallsHttpService()
        {
            // Arrange
            var viewModel = new MaterialOutputsViewModel(); // Assuming MaterialOutputsViewModel is defined
            var expectedDto = new MaterialOutputsDto(); // Assuming MaterialOutputsDto is defined
            _mockMapper.Setup(x => x.Map<MaterialOutputsDto>(viewModel))
                .Returns(expectedDto);

            // Act
            await _accreditationSiteMaterialService.UpdateMaterialOutputs(viewModel);

            // Assert
            _mockHttpSiteMaterialService.Verify(x =>
                x.UpdateMaterialOutputs(
                    viewModel.Id,
                    viewModel.MaterialId,
                    expectedDto),
                Times.Once);
        }

        [TestMethod]
        public async Task GetReprocessedWasteLastYearViewModel_WithValidParameters_ReturnsCorrectViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedWasteLastYear = true;

            _mockHttpSiteMaterialService.Setup(x => x.GetReprocessedWasteLastYear(id, materialId))
                .ReturnsAsync(expectedWasteLastYear);

            // Act
            var result = await _accreditationSiteMaterialService.GetReprocessedWasteLastYearViewModel(
                id,
                materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedWasteLastYear, result.HasReprocessedWasteLastYear);
        }
    }
}
