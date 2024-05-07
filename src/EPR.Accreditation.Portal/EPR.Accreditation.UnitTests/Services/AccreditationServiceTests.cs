namespace EPR.Accreditation.UnitTests.Services
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Moq;

    [TestClass]
    public class AccreditationServiceTests
    {
        private Mock<IMapper> _mockMapper = null;
        private Mock<IHttpAccreditationService> _mockHttpAccreditionService;
        private AccreditationService _accreditationService;
        private Mock<IOptions<AppSettingsConfigOptions>> _mockConfigSettings;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        [TestInitialize]
        public void Init()
        {
            var mockConfig = new AppSettingsConfigOptions
            {
                DaysUntilExpiration = 30
            };

            _mockMapper = new Mock<IMapper>();
            _mockConfigSettings = new Mock<IOptions<AppSettingsConfigOptions>>();
            _mockHttpAccreditionService = new Mock<IHttpAccreditationService>();
            _mockConfigSettings.Setup(o => o.Value).Returns(mockConfig);

            _accreditationService = new AccreditationService(
                _mockMapper.Object,
                _mockHttpAccreditionService.Object);
        }

        [TestMethod]
        public void GetWasteLicensesAndPermitsViewModel_ReturnsCorrectViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = _accreditationService?.GetWastePermitViewModel(id);

            // Asset
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(WasteLicensesAndPermitsViewModel));
        }

        [TestMethod]
        public void GetWasteLicensesAndPermitsViewModel_CheckService_Call()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedDto = new Portal.DTOs.WastePermit.LicensesAndPermitsReferences();
            var expectedViewModel = new WasteLicensesAndPermitsViewModel();

            expectedDto = new Portal.DTOs.WastePermit.LicensesAndPermitsReferences()
            {
                AccreditationId = 1,
                EnvironmentalPermitNumber = "1",
                DealerRegistrationNumber = "1",
                PartAActivityReferenceNumber = "1",
                PartBActivityReferenceNumber = "1",
            };

            _mockHttpAccreditionService.Setup(service => service.GetWastePermit(id))
                .ReturnsAsync(expectedDto);

            // Act
            var result = _accreditationService?.GetWastePermitViewModel(id);

            // Assert
            Assert.IsNotNull(result);
            _mockHttpAccreditionService.Verify(
                s =>
                    s.GetWastePermit(
                        It.Is<Guid>(p => p == id)),
                Times.Once);

            _mockHttpAccreditionService.Verify(
                s =>
                    s.GetWastePermit(id),
                Times.Once);
        }

        [TestMethod]
        public async Task IsExporter_ReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockHttpAccreditionService.Setup(s => s.GetOperatorType(id)).ReturnsAsync(Portal.Common.Enums.OperatorType.Exporter);

            // Act
            var result = await _accreditationService.IsExporter(id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task IsExporter_ReturnsFalse()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockHttpAccreditionService.Setup(s => s.GetOperatorType(id)).ReturnsAsync(Portal.Common.Enums.OperatorType.Reprocessor);

            // Act
            var result = await _accreditationService.IsExporter(id);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetLegalDocumentsAddressViewModel_CallsServiceAndMaps()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _accreditationService.GetLegalDocumentsAddressViewModel(id);

            // Assert
            _mockHttpAccreditionService.Verify(
                s =>
                    s.GetLegalDocumentsAddress(id),
                Times.Once);
            _mockMapper.Verify(
                s =>
                    s.Map<LegalDocumentsAddressViewModel>(It.IsAny<AddressDto>()),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdateLegalDocumentsAddress_MapsAndCallsService()
        {
            // Arrange
            var id = Guid.NewGuid();
            var viewModel = new LegalDocumentsAddressViewModel
            {
                Id = id
            };

            // Act
            await _accreditationService.UpdateLegalDocumentsAddress(viewModel);

            // Assert
            _mockMapper.Verify(
                m =>
                    m.Map<AddressDto>(viewModel),
                Times.Once);
            _mockHttpAccreditionService.Verify(
                s =>
                    s.UpdateLegalDocumentsAddress(
                        id,
                        It.IsAny<AddressDto>()),
                Times.Once);
        }
    }
}
