namespace EPR.Accreditation.UnitTests.Services.Accreditation
{
    using AutoMapper;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Moq;

    [TestClass]
    public class WastePermitServiceTests
    {
        private WastePermitService _wastePermitService;
        private Mock<IHttpContextAccessor> _mockContextAccessor;
        private Mock<IHttpWastePermitService> _mockHttpWastePermitService;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void Init()
        {
            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpWastePermitService = new Mock<IHttpWastePermitService>();

            _wastePermitService = new WastePermitService(
                _mockContextAccessor.Object,
                _mockHttpWastePermitService.Object,
                _mockMapper.Object);

            var context = new DefaultHttpContext();
            _mockContextAccessor.Setup(context => context.HttpContext).Returns(context);
        }

        [TestMethod]
        public async Task GetPermitExemptionViewModel_ReturnsTrue()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _mockHttpWastePermitService.Setup(x => x.GetHasPermitExemption(id)).ReturnsAsync(true);

            // Act
            var result = await _wastePermitService.GetPermitExemptionViewModel(id);

            // Assert
            Assert.IsTrue(result.HasPermitExemption);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task GetPermitExemptionViewModel_ReturnsFalse()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _mockHttpWastePermitService.Setup(x => x.GetHasPermitExemption(id)).ReturnsAsync(false);

            // Act
            var result = await _wastePermitService.GetPermitExemptionViewModel(id);

            // Assert
            Assert.IsFalse(result.HasPermitExemption);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task GetPermitExemptionViewModel_HandlesException()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _mockHttpWastePermitService.Setup(x =>
            x.GetHasPermitExemption(id))
                .ThrowsAsync(new Exception("Simulated exception"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
                await _wastePermitService
                .GetPermitExemptionViewModel(id));
        }

        [TestMethod]
        public async Task UpdatePermitExemption_Calls_Mapper_With_Correct_Argument()
        {
            // Arrange
            var permitExemptionViewModel = new PermitExemptionViewModel();
            var permitExemptionDto = new PermitExemption();

            _mockMapper.Setup(m => m.Map<PermitExemption>(permitExemptionViewModel)).Returns(permitExemptionDto);

            // Act
            await _wastePermitService.UpdatePermitExemption(permitExemptionViewModel);

            // Assert
            _mockMapper.Verify(m => m.Map<PermitExemption>(permitExemptionViewModel), Times.Once);
        }

        [TestMethod]
        public async Task UpdatePermitExemption_Calls_HttpWastePermitService_With_Correct_Arguments()
        {
            // Arrange
            var permitExemptionViewModel = new PermitExemptionViewModel();
            var permitExemptionDto = new PermitExemption();

            _mockMapper.Setup(m => m.Map<PermitExemption>(permitExemptionViewModel)).Returns(permitExemptionDto);

            // Act
            await _wastePermitService.UpdatePermitExemption(permitExemptionViewModel);

            // Assert
            _mockHttpWastePermitService.Verify(
                m =>
                    m.UpdatePermitExemption(
                        permitExemptionViewModel.Id,
                        permitExemptionDto),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdatePermitExemption_Calls_HttpWastePermitService_Once()
        {
            // Arrange
            var permitExemptionViewModel = new PermitExemptionViewModel();
            var permitExemptionDto = new PermitExemption();

            _mockMapper.Setup(m => m.Map<PermitExemption>(permitExemptionViewModel)).Returns(permitExemptionDto);

            // Act
            await _wastePermitService.UpdatePermitExemption(permitExemptionViewModel);

            // Assert
            _mockHttpWastePermitService.Verify(
                m =>
                    m.UpdatePermitExemption(
                        It.IsAny<Guid>(),
                        It.IsAny<PermitExemption>()),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdatePermitExemption_Passes_Correct_Parameters_To_HttpWastePermitService()
        {
            // Arrange
            var permitExemptionViewModel = new PermitExemptionViewModel { Id = Guid.NewGuid() };
            var permitExemptionDto = new PermitExemption();

            _mockMapper.Setup(m => m.Map<PermitExemption>(permitExemptionViewModel)).Returns(permitExemptionDto);

            // Act
            await _wastePermitService.UpdatePermitExemption(permitExemptionViewModel);

            // Assert
            _mockHttpWastePermitService.Verify(
                m =>
                    m.UpdatePermitExemption(
                        permitExemptionViewModel.Id,
                        permitExemptionDto),
                Times.Once);
        }
    }
}
