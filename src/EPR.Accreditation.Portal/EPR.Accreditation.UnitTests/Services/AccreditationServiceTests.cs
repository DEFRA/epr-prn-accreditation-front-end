using AutoMapper;
using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.Options;
using EPR.Accreditation.Portal.RESTservices;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class AccreditationServiceTests
    {
        private Mock<IMapper> _mockMapper = null;
        private Mock<IHttpAccreditationService> _httpAccreditionService;
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
            _httpAccreditionService = new Mock<IHttpAccreditationService>();
            _mockConfigSettings.Setup(o => o.Value).Returns(mockConfig);
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _accreditationService = new AccreditationService(_mockMapper.Object, _httpAccreditionService.Object, _mockHttpContextAccessor.Object);
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

            _httpAccreditionService.Setup(service => service.GetWastePermit(id))
                .ReturnsAsync(expectedDto);

            // Act
            var result = _accreditationService?.GetWastePermitViewModel(id);

            // Asset
            Assert.IsNotNull(result);
            _httpAccreditionService.Verify(s =>
                s.GetWastePermit(
                    It.Is<Guid>(p => p == id)),
                Times.Once);
            _httpAccreditionService.Verify(s =>
                s.GetWastePermit(id), Times.Once);
        }

        [TestMethod]
        public async Task GetHasOverseasAgent_ReturnsCorrectViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedDto = new HasOverseasAgentDto
            {
                HasOverseasAgent = true,
            };

            _httpAccreditionService.Setup(service => service.GetHasOverseasAgent(id))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _accreditationService?.GetHasOverseasAgent(id);

            // Asset
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HasOverseasAgentViewModel));
        }

        [TestMethod]
        public void GetHasOverseasAgent_CheckService_Call()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedViewModel = new HasOverseasAgentViewModel();

            var expectedDto = new HasOverseasAgentDto()
            {
                HasOverseasAgent = true,
            };

            _httpAccreditionService.Setup(service => service.GetHasOverseasAgent(id))
                .ReturnsAsync(expectedDto);

            // Act
            var result = _accreditationService?.GetHasOverseasAgent(id);

            // Asset
            Assert.IsNotNull(result);
            _httpAccreditionService.Verify(s =>
                s.GetHasOverseasAgent(
                    It.Is<Guid>(p => p == id)),
                Times.Once);
            _httpAccreditionService.Verify(s =>
                s.GetHasOverseasAgent(id), Times.Once);
        }

        [TestMethod]
        public async Task SetOverseasAgentFlag_WithValidParameters_CallsHttpService()
        {
            // Arrange
            var viewModel = new HasOverseasAgentViewModel(); // Assuming MaterialOutputsViewModel is defined
            var expectedDto = new HasOverseasAgentDto(); // Assuming MaterialOutputsDto is defined
            _mockMapper.Setup(x => x.Map<HasOverseasAgentDto>(viewModel))
                .Returns(expectedDto);

            // Act
            await _accreditationService.SetOverseasAgentFlag(viewModel);

            // Assert
            _httpAccreditionService.Verify(x =>
                x.SetHasOverseasAgent(
                    viewModel.ExternalId,
                    viewModel.UseOverseasAgent),
                Times.Once);
        }
    }
}
