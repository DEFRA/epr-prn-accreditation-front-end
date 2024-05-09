namespace EPR.Accreditation.UnitTests.Services.AccreditationSite
{
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation;
    using EPR.Accreditation.Portal.ViewModels;
    using Moq;

    [TestClass]
    public class AccreditationSiteServiceTests
    {
        private AccreditationSiteService _accreditationSiteService;
        private Mock<IHttpAccreditationSiteService> _mockHttpAccreditationSiteService;

        [TestInitialize]
        public void Init()
        {
            _mockHttpAccreditationSiteService = new Mock<IHttpAccreditationSiteService>();
            _accreditationSiteService = new AccreditationSiteService(_mockHttpAccreditationSiteService.Object);
        }

        [TestMethod]
        public async Task GetExemptionReferencesViewModel_ReturnsViewModelWithCorrectReferences()
        {
            // Arrange
            var id = Guid.NewGuid();
            var exemptionReferences = new List<string>
            {
                "REF001",
                "REF002",
                "REF003"
            };

            _mockHttpAccreditationSiteService.Setup(s => s.GetExemptionReferences(id)).ReturnsAsync(exemptionReferences);

            // Act
            var result = await _accreditationSiteService.GetExemptionReferencesViewModel(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(exemptionReferences.ElementAtOrDefault(0), result.Reference1);
            Assert.AreEqual(exemptionReferences.ElementAtOrDefault(1), result.Reference2);
            Assert.AreEqual(exemptionReferences.ElementAtOrDefault(2), result.Reference3);

            _mockHttpAccreditationSiteService.Verify(s => s.GetExemptionReferences(id), Times.Once());
        }

        [TestMethod]
        public async Task UpdateExemptionReferences_CallsHttpAccreditationSiteServiceWithCorrectParameters()
        {
            // Arrange
            var viewModel = new ExemptionReferencesViewModel
            {
                Id = Guid.NewGuid(),
                Reference1 = "Reference 1",
                Reference2 = "Reference 2",
                Reference3 = "Reference 3",
                Reference4 = null,
                Reference5 = null
            };

            var expectedReferences = new List<string>
            {
                "Reference 1",
                "Reference 2",
                "Reference 3"
            };

            // Act
            await _accreditationSiteService.UpdateExemptionReferences(viewModel);

            // Assert
            _mockHttpAccreditationSiteService.Verify(s => s.UpdateExemptionReferences(viewModel.Id, expectedReferences), Times.Once);
        }
    }
}
