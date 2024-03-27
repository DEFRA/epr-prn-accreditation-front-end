using AutoMapper;
using EPR.Accreditation.Portal.Options;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation;
using EPR.Accreditation.Portal.ViewModels;
using EPRN.Accreditation.Profiles;
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

            _accreditationService = new AccreditationService(_mockMapper.Object, _mockConfigSettings.Object, _httpAccreditionService.Object);
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
            Assert.IsInstanceOfType(result, typeof(WasteLicensesAndPermitsViewModel));
        }
    }
}
