namespace EPR.Accreditation.UnitTests.Services
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.DTOs.SiteMaterial;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.SiteMaterial;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Localization;
    using Moq;
    using static EPR.Accreditation.Portal.Constants.Strings;

    [TestClass]
    public class AccreditationSiteMaterialServiceTests
    {
        private AccreditationSiteMaterialService _accreditationSiteMaterialService;
        private Mock<IMapper> _mockMapper;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IHttpSiteMaterialService> _mockHttpSiteMaterialService;

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
            _mockHttpSiteMaterialService.Verify(
                s =>
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
            _mockHttpSiteMaterialService.Verify(
                s =>
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
            _mockHttpSiteMaterialService.Setup(
                x =>
                    x.GetWasteSource(
                        siteType,
                        id,
                        siteId,
                        materialId))
                .ReturnsAsync(expectedWasteSource);

            // Act
            var result = await _accreditationSiteMaterialService.GetWasteSource(
                siteType,
                id,
                siteId,
                materialId);

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
            _mockHttpSiteMaterialService.Verify(
                x =>
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
            _mockHttpSiteMaterialService.Verify(
                x =>
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

        [TestMethod]
        public async Task GetNonWasteInputs_Returns_NonWasteInputsViewModel_With_Minimum_Rows_If_Less_Than_Minimum()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            _mockMapper
                .Setup(m => m.Map<NonWasteInputsViewModel>(It.IsAny<ReprocessingSupportingInformationDto>()))
                .Returns(
                    new NonWasteInputsViewModel
                    {
                        Rows = new List<TypeTonnesRowViewModel>
                        {
                            new TypeTonnesRowViewModel
                            {
                                Type = "ABC",
                                Tonnes = 123
                            }
                        }
                    });

            // Act
            var result = await _accreditationSiteMaterialService.GetNonWasteInputs(id, materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(GenericConstants.MinimumMultiLineRecordNumber, result.Rows.Count);
            Assert.AreEqual("ABC", result.Rows[0].Type);
            Assert.AreEqual(123, result.Rows[0].Tonnes);
        }

        [TestMethod]
        public async Task GetNonWasteInputs_Returns_NonWasteInputsViewModel_With_CorrectNumberOfRows()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            _mockMapper
                .Setup(m => m.Map<NonWasteInputsViewModel>(It.IsAny<ReprocessingSupportingInformationDto>()))
                .Returns(
                    new NonWasteInputsViewModel
                    {
                        Rows = new List<TypeTonnesRowViewModel>
                        {
                            new TypeTonnesRowViewModel
                            {
                                Type = "ABC",
                                Tonnes = 123
                            },
                            new TypeTonnesRowViewModel
                            {
                                Type = "ABC",
                                Tonnes = 123
                            },
                            new TypeTonnesRowViewModel
                            {
                                Type = "ABC",
                                Tonnes = 123
                            },
                            new TypeTonnesRowViewModel
                            {
                                Type = "ABC",
                                Tonnes = 123
                            }
                        }
                    });

            // Act
            var result = await _accreditationSiteMaterialService.GetNonWasteInputs(id, materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Rows.Count);
        }

        [TestMethod]
        public async Task UpdateNonWasteInputs_Removes_Blank_Rows_Before_Mapping()
        {
            // Arrange
            _mockMapper.Setup(m => m.Map<ReprocessingSupportingInformationDto>(It.IsAny<NonWasteInputsViewModel>()))
                .Returns(new ReprocessingSupportingInformationDto());
            var viewModel = new NonWasteInputsViewModel
            {
                Rows = new List<TypeTonnesRowViewModel>
                {
                    new TypeTonnesRowViewModel { Type = "Type1", Tonnes = 10 },
                    new TypeTonnesRowViewModel { Type = string.Empty, Tonnes = null },
                    new TypeTonnesRowViewModel { Type = "Type3", Tonnes = 20 },
                    new TypeTonnesRowViewModel { Type = "Type4", Tonnes = 30 },
                    new TypeTonnesRowViewModel { Type = string.Empty, Tonnes = null },
                }
            };

            // Act
            await _accreditationSiteMaterialService.UpdateNonWasteInputs(viewModel);

            // Assert
            _mockMapper.Verify(
                m =>
                    m.Map<ReprocessingSupportingInformationDto>(
                        It.Is<NonWasteInputsViewModel>(
                            p =>
                                p.Rows.Count == 3 &&
                                p.Rows[0].Type == "Type1" &&
                                p.Rows[0].Tonnes == 10 &&
                                p.Rows[1].Type == "Type3" &&
                                p.Rows[1].Tonnes == 20 &&
                                p.Rows[2].Type == "Type4" &&
                                p.Rows[2].Tonnes == 30)),
                Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task UpdateProductsProduced_WithNullViewModel_ThrowsException()
        {
            // Arrange

            // Act
            await _accreditationSiteMaterialService.UpdateProductsProduced(null);

            // Assert
        }

        [TestMethod]
        public async Task UpdateProductsProduced_WithBlankRowsPresent_RemovesBlankRowsAndUpdates()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var productsProducedViewModel = new ProductsProducedViewModel
            {
                Id = id,
                MaterialId = materialId,
                Rows = new List<TypeTonnesRowViewModel>
                {
                    new TypeTonnesRowViewModel
                    {
                        Type = "A",
                        Tonnes = 10,
                    },
                    new TypeTonnesRowViewModel()
                }
            };
            _mockMapper.Setup(m => m.Map<ReprocessingSupportingInformationDto>(It.IsAny<ProductsProducedViewModel>())).Returns(new ReprocessingSupportingInformationDto
            {
                Id = id,
                MaterialId = materialId,
            });

            // Act
            await _accreditationSiteMaterialService.UpdateProductsProduced(productsProducedViewModel);

            // Assert
            _mockMapper.Verify(
                m =>
                    m.Map<ReprocessingSupportingInformationDto>(
                        It.Is<ProductsProducedViewModel>(p =>
                            p.Rows.Count == 1 &&
                            p.Rows[0].Tonnes == 10)),
                Times.Once);
            _mockHttpSiteMaterialService.Verify(
                s =>
                    s.UpdateProductsProduced(
                        It.IsAny<Guid>(),
                        It.IsAny<Guid>(),
                        It.IsAny<ReprocessingSupportingInformationDto>()),
                Times.Once);
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodeViewModel_CallsServiceWithParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            await _accreditationSiteMaterialService.GetWasteDescriptionCodeViewModel(
                id,
                siteId,
                materialId);

            // Assert
            _mockHttpSiteMaterialService.Verify(
                s =>
                    s.GetWasteDescriptionCodes(
                        id,
                        siteId,
                        materialId),
                Times.Once);
            _mockMapper.Verify(
                m =>
                    m.Map<List<WasteDescriptionCodeRowViewModel>>(It.IsAny<List<string>>()),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdateWasteDescriptionCodeViewModel_CallsServiceWithParameters()
        {
            // Arrange
            var viewModel = new WasteDescriptionCodeViewModel
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                MaterialId = Guid.NewGuid(),
                Rows = new List<WasteDescriptionCodeRowViewModel>
                {
                    new()
                    {
                        WasteDescriptionCode = "ABC"
                    }
                }
            };

            // act
            await _accreditationSiteMaterialService.UpdateWasteDescriptionCodeViewModel(viewModel);

            // Assert
            _mockHttpSiteMaterialService.Verify(
                s =>
                    s.SaveWasteDescriptionCodes(
                        viewModel.Id,
                        viewModel.SiteId,
                        viewModel.MaterialId,
                        It.Is<IEnumerable<string>>(
                            p =>
                                p.ToList().Count == 1 &&
                                p.ToList()[0] == "ABC")),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdateWasteDescriptionCodeViewModel_WithBlankEntry_CallsServiceWithParametersWithoutBlankEntries()
        {
            // Arrange
            var viewModel = new WasteDescriptionCodeViewModel
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                MaterialId = Guid.NewGuid(),
                Rows = new List<WasteDescriptionCodeRowViewModel>
                {
                    new()
                    {
                        WasteDescriptionCode = "ABC"
                    },
                    new(),
                    new()
                    {
                        WasteDescriptionCode = "DEF"
                    }
                }
            };

            // act
            await _accreditationSiteMaterialService.UpdateWasteDescriptionCodeViewModel(viewModel);

            // Assert
            _mockHttpSiteMaterialService.Verify(
                s =>
                    s.SaveWasteDescriptionCodes(
                        viewModel.Id,
                        viewModel.SiteId,
                        viewModel.MaterialId,
                        It.Is<IEnumerable<string>>(
                            p =>
                                p.ToList().Count == 2 &&
                                p.ToList()[0] == "ABC" &&
                                p.ToList()[1] == "DEF")),
                Times.Once);
        }

        [TestMethod]
        public async Task GetHasAccreditationNumViewModel_ReturnsViewModelWithCorrectProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedValue = true;

            _mockHttpSiteMaterialService.Setup(s =>
                s.GetHasNpwdAccreditationNumber(
                    id,
                    materialId))
                .ReturnsAsync(expectedValue);

            // Act
            var viewModel = await _accreditationSiteMaterialService.GetHasAccreditationNumViewModel(id, materialId);

            // Assert
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(id, viewModel.Id);
            Assert.AreEqual(materialId, viewModel.MaterialId);
            Assert.AreEqual(expectedValue, viewModel.Has2024NPWDAccreditation);

            _mockHttpSiteMaterialService.Verify(s => s.GetHasNpwdAccreditationNumber(id, materialId), Times.Once);
        }

        [TestMethod]
        public async Task UpdateHasNpwdAccreditationNumber_CallsHttpServiceWithCorrectParameters()
        {
            // Arrange
            var viewModel = new HasNpwdAccreditationNumViewModel
            {
                Id = Guid.NewGuid(),
                MaterialId = Guid.NewGuid(),
                Has2024NPWDAccreditation = true
            };
            var dto = new NpwdAccreditationNumber();

            _mockMapper.Setup(m =>
                m.Map<NpwdAccreditationNumber>(viewModel))
            .Returns(dto);

            // Act
            await _accreditationSiteMaterialService.UpdateHasNpwdAccreditationNumber(viewModel);

            // Assert
            _mockHttpSiteMaterialService.Verify(
                s =>
                s.UpdateHasNpwdAccreditationNumber(
                    viewModel.Id,
                    viewModel.MaterialId,
                    dto),
                Times.Once);
        }
    }
}
