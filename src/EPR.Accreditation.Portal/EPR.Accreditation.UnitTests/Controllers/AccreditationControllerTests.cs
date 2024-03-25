
using EPR.Accreditation.Portal.Controllers;
using EPR.Accreditation.Portal.Services.Interfaces;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class AccreditationControllerTests
    {
        private Mock<IAccreditationService> _mockAccreditationService;
        private AccreditationController _accreditationController;

        [TestInitialize]
        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _accreditationController = new AccreditationController(_mockAccreditationService.Object);
        }
    }
}
