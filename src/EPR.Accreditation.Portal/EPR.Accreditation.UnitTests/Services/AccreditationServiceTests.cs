using EPR.Accreditation.App.Services;
using Microsoft.Extensions.Configuration;
using Moq;


namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class AccreditationServiceTests
    {
        private Mock<IConfiguration>? _mockConfigSettings;
        private AccreditationService? _accreditationService;

        [TestInitialize]
        public void Init()
        {
            _mockConfigSettings = new Mock<IConfiguration>();
            _accreditationService = new AccreditationService(_mockConfigSettings.Object);
        }

        [TestMethod]
        public void GetApplicationSavedViewModel_ReturnsCorrectViewModel()
        {
            _mockConfigSettings.Setup(config => config["Days_Until_Expiration"]).Returns("30");



            // Act



            // Assert

        }


    }
}
