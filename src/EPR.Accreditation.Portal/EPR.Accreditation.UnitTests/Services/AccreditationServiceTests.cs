﻿
using EPR.Accreditation.Portal.Options;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation;
using Microsoft.Extensions.Options;
using Moq;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class AccreditationServiceTests
    {
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

            _mockConfigSettings = new Mock<IOptions<AppSettingsConfigOptions>>();
            _httpAccreditionService = new Mock<IHttpAccreditationService>();
            _mockConfigSettings.Setup(o => o.Value).Returns(mockConfig);

            _accreditationService = new AccreditationService(_mockConfigSettings.Object, _httpAccreditionService.Object);

        }

        [TestMethod]
        public void GetApplicationSavedViewModel_ReturnsCorrectViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = _accreditationService?.GetApplicationSavedViewModel(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(30, result.ApplicationExpiry);

        }
    }
}
