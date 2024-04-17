namespace EPR.Accreditation.UnitTests.Services.Accreditation
{
    using AutoMapper;
    using EPR.Accreditation.Portal.DTOs.Country;
    using EPR.Accreditation.Portal.DTOs.OverseasSite;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation;
    using EPR.Accreditation.Portal.ViewModels;
    using Moq;

    [TestClass]
    public class OverseasSiteServiceTests
    {
        private OverseasSiteService _overseasSiteService;
        private Mock<IHttpOverseasSiteService> _mockHttpOverseasSiteService;
        private Mock<IHttpCountryService> _mockHttpCountryService;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void Init()
        {
            _mockHttpOverseasSiteService = new Mock<IHttpOverseasSiteService>();
            _mockHttpCountryService = new Mock<IHttpCountryService>();
            _mockMapper = new Mock<IMapper>();

            _overseasSiteService = new OverseasSiteService(
                _mockHttpOverseasSiteService.Object,
                _mockHttpCountryService.Object,
                _mockMapper.Object);
        }

        [TestMethod]
        public async Task GetReprocessorDetailsViewModel_ReturnsValidViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var overseasSiteId = Guid.NewGuid();
            var reprocessorDetailsDto = new ReprocessorDetailsDto
            {
                Name = "Reprocessor Name",
                Address = "Reprocessor Address",
                CountryId = 1
            };
            var listOfCountries = new List<Country>
            {
                new Country { CountryId = 1, Name = "Country 1" },
                new Country { CountryId = 2, Name = "Country 2" }
            };

            _mockHttpOverseasSiteService.Setup(s => s.GetReprocessorDetails(id, overseasSiteId)).ReturnsAsync(reprocessorDetailsDto);
            _mockHttpCountryService.Setup(s => s.GetCountryList()).ReturnsAsync(listOfCountries);

            // Act
            var viewModel = await _overseasSiteService.GetReprocessorDetailsViewModel(id, overseasSiteId);

            // Assert
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(id, viewModel.Id);
            Assert.AreEqual(reprocessorDetailsDto.Name, viewModel.Name);
            Assert.AreEqual(reprocessorDetailsDto.Address, viewModel.Address);
            Assert.IsNotNull(viewModel.Countries);
            Assert.AreEqual(3, viewModel.Countries.Count());

            _mockHttpOverseasSiteService.Verify(s => s.GetReprocessorDetails(id, overseasSiteId), Times.Once());
            _mockHttpCountryService.Verify(s => s.GetCountryList(), Times.Once());
        }

        [TestMethod]
        public async Task UpdateReprocessorDetails_CallsHttpServiceWithCorrectParameters()
        {
            // Arrange
            var viewModel = new ReprocessorDetailsViewModel
            {
                Id = Guid.NewGuid(),
                OverseasSiteId = Guid.NewGuid(),
                Name = "Test",
                CountryId = "2",
                Address = "123 High Street"
            };

            // Act
            await _overseasSiteService.UpdateReprocessorDetails(viewModel);

            // Assert
            _mockHttpOverseasSiteService.Verify(
                s =>
                s.UpdateReprocessorDetails(
                    viewModel.Id,
                    viewModel.OverseasSiteId,
                    It.IsAny<ReprocessorDetailsDto>()),
                Times.Once);
        }
    }
}
