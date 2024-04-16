namespace EPR.Accreditation.UnitTests.Controllers
{
    using EPR.Accreditation.Portal.Controllers;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.SiteMaterial;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
    using Moq;

    [TestClass]
    public class SiteMaterialControllerTests
    {
        private SiteMaterialController _siteMaterialController;
        private Mock<IHttpContextAccessor> _mockContextAccessor;
        private Mock<IUrlHelperWrapper> _mockUrlHelper;
        private Mock<IAccreditationSiteMaterialService> _mockAccreditationSiteMaterialService;
        private Mock<ISaveAndComeBackService> _mockSaveAndComeBackService;
        private Mock<IOptions<AppSettingsConfigOptions>> _mockAppSettingsConfiguration;
        private BackPageViewModel _backPageViewModel;

        [TestInitialize]
        public void Init()
        {
            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUrlHelper = new Mock<IUrlHelperWrapper>();
            _mockAccreditationSiteMaterialService = new Mock<IAccreditationSiteMaterialService>();
            _mockSaveAndComeBackService = new Mock<ISaveAndComeBackService>();
            _mockAppSettingsConfiguration = new Mock<IOptions<AppSettingsConfigOptions>>();
            _backPageViewModel = new BackPageViewModel();

            var config = new AppSettingsConfigOptions
            {
                MaximumMultiLineRecordNumber = 10
            };

            _mockAppSettingsConfiguration
                .Setup(o => o.Value)
                .Returns(config);

            _siteMaterialController = new SiteMaterialController(
                _mockContextAccessor.Object,
                _mockUrlHelper.Object,
                _mockAccreditationSiteMaterialService.Object,
                _mockSaveAndComeBackService.Object,
                _mockAppSettingsConfiguration.Object,
                _backPageViewModel);

            var context = new DefaultHttpContext();
            _mockContextAccessor.Setup(context => context.HttpContext).Returns(context);
        }

        [TestMethod]
        public async Task MaterialOutputs_AllParametersNotNull_WithNullWasteLastYear_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            _mockAccreditationSiteMaterialService.Setup(a => a.GetMaterialOutputs(
                id,
                materialId)).ReturnsAsync(new MaterialOutputsViewModel());

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                id,
                materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task MaterialOutputs_AllParametersNotNull_WithWasteLastYearTrue_ReturnsExpectedResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            var materialOutputsViewModel = new MaterialOutputsViewModel
            {
                WasteLastYear = true,
            };

            _mockAccreditationSiteMaterialService.Setup(a => a.GetMaterialOutputs(
                id,
                materialId)).ReturnsAsync(materialOutputsViewModel);

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                id,
                materialId);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("MaterialOutputsLastYear", viewResult.ViewName);
        }

        [TestMethod]
        public async Task MaterialOutputs_AllParametersNotNull_WithWasteLastYearFalse_ReturnsExpectedResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            var materialOutputsViewModel = new MaterialOutputsViewModel
            {
                WasteLastYear = false,
            };

            _mockAccreditationSiteMaterialService.Setup(a => a.GetMaterialOutputs(
                id,
                materialId)).ReturnsAsync(materialOutputsViewModel);

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                id,
                materialId);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("MaterialOutputsEstimated", viewResult.ViewName);
        }

        [TestMethod]
        public async Task MaterialWasteSource_AnyParameterNull_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = await _siteMaterialController.MaterialOutputs(null, Guid.NewGuid());
            var result2 = await _siteMaterialController.MaterialOutputs(Guid.NewGuid(), null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task MaterialOutputs_AnyParameterNull_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                null,
                Guid.NewGuid());
            var result2 = await _siteMaterialController.MaterialOutputs(
                Guid.NewGuid(),
                null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsView_ForValidIds()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedViewModel = new ReprocessedWasteLastYearViewModel();

            _mockAccreditationSiteMaterialService.Setup(
                s =>
                    s.GetReprocessedWasteLastYearViewModel(
                        id,
                        materialId))
                .ReturnsAsync(expectedViewModel);

            // Act
            var result = await _siteMaterialController.WasteLastYear(id, materialId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual(expectedViewModel, viewResult.Model);

            _mockAccreditationSiteMaterialService.Verify(service => service.GetReprocessedWasteLastYearViewModel(id, materialId), Times.Once);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _siteMaterialController.WasteLastYear(null, Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            _mockAccreditationSiteMaterialService.Verify(
                service =>
                    service.GetReprocessedWasteLastYearViewModel(
                        Guid.Empty,
                        Guid.NewGuid()),
                Times.Never);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsNotFound_WhenAllIdsAreNull()
        {
            // Act
            var result = await _siteMaterialController.WasteLastYear(null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            _mockAccreditationSiteMaterialService.Verify(
                service =>
                    service.GetReprocessedWasteLastYearViewModel(
                        Guid.Empty,
                        Guid.Empty),
                Times.Never);
        }

        [TestMethod]
        public async Task WasteLastYear_CallsService_WithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            await _siteMaterialController.WasteLastYear(id, materialId);

            // Assert
            _mockAccreditationSiteMaterialService.Verify(
                service =>
                    service.GetReprocessedWasteLastYearViewModel(
                        id,
                        materialId),
                Times.Once);
        }

        [TestMethod]
        public async Task WasteLastYear_ValidIds_ReturnsCorrectViewName()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedViewModel = new ReprocessedWasteLastYearViewModel();

            _mockAccreditationSiteMaterialService.Setup(
                s =>
                    s.GetReprocessedWasteLastYearViewModel(
                        id,
                        materialId))
                .ReturnsAsync(expectedViewModel);

            // Act
            var result = await _siteMaterialController.WasteLastYear(
                id,
                materialId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsNull(viewResult.ViewName);

            _mockAccreditationSiteMaterialService.Verify(
                service =>
                    service.GetReprocessedWasteLastYearViewModel(
                        id,
                        materialId),
                Times.Once);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsNotFound_WhenAllThreeIdsAreNull()
        {
            // Act
            var result = await _siteMaterialController.WasteLastYear(null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            _mockAccreditationSiteMaterialService.Verify(
                service =>
                    service.GetReprocessedWasteLastYearViewModel(
                        Guid.Empty,
                        Guid.Empty),
                Times.Never);
        }

        [TestMethod]
        public async Task WasteLastYear_SavesWithValidData_SaveAndContinue()
        {
            // Arrange
            var viewModel = new ReprocessedWasteLastYearViewModel
            {
                Id = Guid.NewGuid(),
                HasReprocessedWasteLastYear = true
            };

            // Act
            var result = await _siteMaterialController.WasteLastYear(viewModel, SaveButton.SaveAndContinue);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var redirectToRouteResult = result as RedirectToRouteResult;
            Assert.AreEqual("NonWasteInputs", redirectToRouteResult.RouteName);

            _mockAccreditationSiteMaterialService.Verify(
                service =>
                    service.UpdateReprocessedWasteLastYear(viewModel),
                Times.Once);
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsViewResult_ForSaveAndComeBack()
        {
            // Arrange
            var saveButton = SaveButton.SaveAndComeBack;
            var viewModel = new ReprocessedWasteLastYearViewModel
            {
                Id = Guid.NewGuid(),
                HasReprocessedWasteLastYear = false
            };

            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            // Act
            var result = await _siteMaterialController.WasteLastYear(viewModel, saveButton) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_ApplicationSaved", result.ViewName);

            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateReprocessedWasteLastYear(
                        viewModel),
                Times.Once);

            _mockSaveAndComeBackService.Verify(
                x =>
                    x.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Once());
        }

        [TestMethod]
        public async Task WasteLastYear_ReturnsCorrectView_WhenModelIsInvalid()
        {
            // Arrange
            var viewModel = new ReprocessedWasteLastYearViewModel();
            var saveButton = SaveButton.Undefined;

            _siteMaterialController.ModelState.AddModelError("Error", "Error");

            // Act
            var result = await _siteMaterialController.WasteLastYear(viewModel, saveButton);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.ViewData.Model);

            // check model is expected type
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ReprocessedWasteLastYearViewModel));

            // check view name
            Assert.IsNull(viewResult.ViewName); // It's going to return the view name of the action by default

            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateReprocessedWasteLastYear(viewModel),
                Times.Never);
        }

        [TestMethod]
        public async Task SaveMaterialWasteSource_ModelStateInvalid_ReturnsViewResult()
        {
            // Arrange
            var viewModel = new WasteSourceViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.AddModelError("PropertyName", "ErrorMessage");

            // Act
            var result = await _siteMaterialController.MaterialWasteSource(
                viewModel,
                saveButton);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsNull(viewResult.ViewName);
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.GetWasteSource(
                        It.Is<SiteType>(p => p == SiteType.Site),
                        It.IsAny<Guid>(),
                        It.IsAny<Guid>(),
                        It.IsAny<Guid>()),
                Times.Once);
        }

        [TestMethod]
        public async Task SaveMaterialWasteSource_SaveAndComeBack_ReturnsViewResult()
        {
            // Arrange
            var viewModel = new WasteSourceViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            // Act
            var result = await _siteMaterialController.MaterialWasteSource(
                viewModel,
                saveButton) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_ApplicationSaved", result.ViewName);
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateWasteSource(
                        SiteType.Site,
                        viewModel),
                Times.Once());
            _mockSaveAndComeBackService.Verify(
                x =>
                    x.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Once());
        }

        [TestMethod]
        public async Task SaveMaterialWasteSource_SaveAndComeBack_CallsServicesCorrectly()
        {
            // Arrange
            var viewModel = new WasteSourceViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            // Act
            await _siteMaterialController.MaterialWasteSource(viewModel, saveButton);

            // Assert
            _mockAccreditationSiteMaterialService.Verify(
                x =>
                    x.UpdateWasteSource(
                        It.IsAny<SiteType>(),
                        viewModel),
                Times.Once);
            _mockSaveAndComeBackService.Verify(
                x =>
                    x.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Once);
        }

        [TestMethod]
        public async Task MaterialOutputs_ModelStateInvalid_ReturnsViewResult()
        {
            // Arrange
            var viewModel = new MaterialOutputsViewModel
            {
                WasteLastYear = false
            };

            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.AddModelError("PropertyName", "ErrorMessage");
            _mockAccreditationSiteMaterialService.Setup(s =>
                s.GetMaterialOutputs(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
                .ReturnsAsync(viewModel);

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                viewModel,
                saveButton);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual("MaterialOutputsEstimated", viewResult.ViewName);

            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.GetMaterialOutputs(
                        It.IsAny<Guid>(),
                        It.IsAny<Guid>()),
                Times.Once);
        }

        [TestMethod]
        public async Task MaterialOutputs_SaveAndComeBack_ReturnsViewResult()
        {
            // Arrange
            var viewModel = new MaterialOutputsViewModel
            {
                WasteLastYear = true
            };

            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            // Act
            var result = await _siteMaterialController.MaterialOutputs(
                viewModel,
                saveButton) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_ApplicationSaved", result.ViewName);
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateMaterialOutputs(
                        viewModel),
                Times.Once());
            _mockSaveAndComeBackService.Verify(
                x =>
                    x.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Once());
        }

        [TestMethod]
        public async Task MaterialOutputs_SaveAndComeBack_CallsServicesCorrectly()
        {
            // Arrange
            var viewModel = new MaterialOutputsViewModel();
            var saveButton = SaveButton.SaveAndComeBack;
            _siteMaterialController.ModelState.Clear(); // Ensuring ModelState is valid

            _mockAccreditationSiteMaterialService.Setup(x => x.UpdateMaterialOutputs(viewModel)).Returns(Task.CompletedTask);

            // Act
            await _siteMaterialController.MaterialOutputs(viewModel, saveButton);

            // Assert
            _mockAccreditationSiteMaterialService.Verify(
                x =>
                    x.UpdateMaterialOutputs(viewModel),
                Times.Once);
            _mockSaveAndComeBackService.Verify(
                x =>
                    x.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Once);
        }

        [TestMethod]
        public async Task NonWasteInputs_Returns_NotFound_When_Id_Or_MaterialId_Not_Provided()
        {
            // Arrange
            var id = (Guid?)null;
            var materialId = (Guid?)null;

            // Act
            var result = await _siteMaterialController.NonWasteInputs(id, materialId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task NonWasteInputs_Returns_View_When_Valid_Id_And_MaterialId_Provided()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            var nonWasteInputsViewModel = new NonWasteInputsViewModel { WasteLastYear = true };
            _mockAccreditationSiteMaterialService.Setup(
                x =>
                    x.GetNonWasteInputs(
                        id,
                        materialId))
                .ReturnsAsync(nonWasteInputsViewModel);

            // Act
            var result = await _siteMaterialController.NonWasteInputs(id, materialId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("NonWasteInputsLastYear", result.ViewName);
            Assert.AreEqual(nonWasteInputsViewModel, result.Model);
        }

        [TestMethod]
        public async Task NonWasteInputs_Returns_NotFound_When_WasteLastYear_Null()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            var nonWasteInputsViewModel = new NonWasteInputsViewModel { WasteLastYear = null };
            _mockAccreditationSiteMaterialService.Setup(
                x =>
                    x.GetNonWasteInputs(
                        id,
                        materialId))
                .ReturnsAsync(nonWasteInputsViewModel);

            // Act
            var result = await _siteMaterialController.NonWasteInputs(id, materialId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task NonWasteInputs_Returns_View_When_WasteLastYear_False()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            var nonWasteInputsViewModel = new NonWasteInputsViewModel { WasteLastYear = false };
            _mockAccreditationSiteMaterialService.Setup(
                x =>
                    x.GetNonWasteInputs(
                        id,
                        materialId))
                .ReturnsAsync(nonWasteInputsViewModel);

            // Act
            var result = await _siteMaterialController.NonWasteInputs(id, materialId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("NonWasteInputsEstimated", result.ViewName);
            Assert.AreEqual(nonWasteInputsViewModel, result.Model);
        }

        [TestMethod]
        public async Task NonWasteInputs_Post_Returns_View_With_New_Row_On_SaveButton_AddRow()
        {
            // Arrange
            var viewModel = new NonWasteInputsViewModel
            {
                Rows = new List<TypeTonnesRowViewModel>(),
                WasteLastYear = true
            };
            var saveButton = SaveButton.AddRow;

            // Act
            var result = await _siteMaterialController.NonWasteInputs(viewModel, saveButton) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((NonWasteInputsViewModel)result.Model).RowsToAdd);
        }

        [TestMethod]
        public async Task ProductsProduced_ReturnsNotFound_WhenNullIdsPassed()
        {
            // Arrange

            // Act
            var result = await _siteMaterialController.ProductsProduced(null, null) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Never);
        }

        [TestMethod]
        public async Task ProductsProduced_ReturnsLastYearView_WhenWasteLastYearProduced()
        {
            // Arrange
            _mockAccreditationSiteMaterialService.Setup(s =>
                s.GetProductsProduced(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>())).ReturnsAsync(new ProductsProducedViewModel
                    {
                        WasteLastYear = true
                    });

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                Guid.NewGuid(),
                Guid.NewGuid()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewName == "ProductsProducedLastYear");

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Never);
        }

        [TestMethod]
        public async Task ProductsProduced_ReturnsEstimatedView_WhenWasteLastYearNotProduced()
        {
            // Arrange
            _mockAccreditationSiteMaterialService.Setup(s =>
                s.GetProductsProduced(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>())).ReturnsAsync(new ProductsProducedViewModel
                    {
                        WasteLastYear = false
                    });

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                Guid.NewGuid(),
                Guid.NewGuid()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewName == "ProductsProducedEstimated");

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Never);
        }

        [TestMethod]
        public async Task ProductsProduced_ReturnsNotFound_WhenWasteLastYearNotPresent()
        {
            // Arrange
            _mockAccreditationSiteMaterialService.Setup(s =>
                s.GetProductsProduced(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>())).ReturnsAsync(new ProductsProducedViewModel
                    {
                        WasteLastYear = null
                    });

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                Guid.NewGuid(),
                Guid.NewGuid()) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Never);
        }

        [TestMethod]
        public async Task ProductProduced_AddsNewRow_WhenAddRowButtonAndRowsToDisplayIsLessThanMax()
        {
            // Arrange
            var viewModel = new ProductsProducedViewModel
            {
                WasteLastYear = false,
                RowsToAdd = 0,
                Rows = new List<TypeTonnesRowViewModel>
                {
                    new TypeTonnesRowViewModel(),
                    new TypeTonnesRowViewModel(),
                    new TypeTonnesRowViewModel(),
                }
            };

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                viewModel,
                SaveButton.AddRow) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as ProductsProducedViewModel;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.RowsToAdd == 1);

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Never);
        }

        [TestMethod]
        public async Task ProductProduced_DoesNotAddNewRow_WhenAddRowButtonAndRowsToDisplayIsAtMax()
        {
            // Arrange
            var config = new AppSettingsConfigOptions
            {
                MaximumMultiLineRecordNumber = 5,
            };

            _mockAppSettingsConfiguration
                .Setup(o => o.Value)
                .Returns(config);

            _siteMaterialController = new SiteMaterialController(
                _mockContextAccessor.Object,
                _mockUrlHelper.Object,
                _mockAccreditationSiteMaterialService.Object,
                _mockSaveAndComeBackService.Object,
                _mockAppSettingsConfiguration.Object,
                _backPageViewModel);

            var viewModel = new ProductsProducedViewModel
            {
                WasteLastYear = true,
                RowsToAdd = 0,
                Rows = new List<TypeTonnesRowViewModel>
                {
                    new TypeTonnesRowViewModel
                    {
                        Type = "A",
                        Tonnes = 1
                    },
                    new TypeTonnesRowViewModel
                    {
                        Type = "A",
                        Tonnes = 1
                    },
                    new TypeTonnesRowViewModel
                    {
                        Type = "A",
                        Tonnes = 1
                    },
                    new TypeTonnesRowViewModel
                    {
                        Type = "A",
                        Tonnes = 1
                    },
                    new TypeTonnesRowViewModel
                    {
                        Type = "A",
                        Tonnes = 1
                    }
                }
            };

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                viewModel,
                SaveButton.AddRow) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as ProductsProducedViewModel;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.RowsToAdd == 0);

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Never);
        }

        [TestMethod]
        public async Task ProductProduced_ReturnsLastYearView_WhenValidationErrorsOccur()
        {
            // Arrange
            var viewModel = new ProductsProducedViewModel
            {
                WasteLastYear = true
            };

            _siteMaterialController.ModelState.AddModelError("Error", "Error");

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                viewModel,
                SaveButton.SaveAndContinue) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewName == "ProductsProducedLastYear");

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Never);
        }

        [TestMethod]
        public async Task ProductProduced_ReturnsEstimatedView_WhenValidationErrorsOccur()
        {
            // Arrange
            var viewModel = new ProductsProducedViewModel
            {
                WasteLastYear = false
            };

            _siteMaterialController.ModelState.AddModelError("Error", "Error");

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                viewModel,
                SaveButton.SaveAndContinue) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewName == "ProductsProducedEstimated");

            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Never);
        }

        [TestMethod]
        public async Task ProductsProduced_ReturnsApplicationSavedView_WhenSaveAndComeBackLater()
        {
            // Arrange
            var accreditationId = Guid.NewGuid();
            var viewModel = new ProductsProducedViewModel
            {
                Id = accreditationId,
                WasteLastYear = false
            };

            var defaultContext = new DefaultHttpContext();
            _mockContextAccessor.Setup(c => c.HttpContext).Returns(defaultContext);

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                viewModel,
                SaveButton.SaveAndComeBack) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewName == "_ApplicationSaved");
            _mockAccreditationSiteMaterialService.Verify(
                s =>
                    s.UpdateProductsProduced(
                        It.IsAny<ProductsProducedViewModel>()),
                Times.Once);
            _mockSaveAndComeBackService.Verify(
                s =>
                    s.AddSaveAndComeBack(
                        It.Is<Guid>(p => p == accreditationId),
                        It.IsAny<RouteValueDictionary>()),
                Times.Once);
        }

        [TestMethod]
        public async Task ProductsProduced_ReturnsRedirectResult_WhenSaveAndContinue()
        {
            // Arrange
            var accreditationId = Guid.NewGuid();
            var viewModel = new ProductsProducedViewModel
            {
                Id = accreditationId,
                WasteLastYear = false
            };

            var defaultContext = new DefaultHttpContext();
            _mockContextAccessor.Setup(c => c.HttpContext).Returns(defaultContext);

            // Act
            var result = await _siteMaterialController.ProductsProduced(
                viewModel,
                SaveButton.SaveAndContinue) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.RouteName == "Authority");
            _mockAccreditationSiteMaterialService.Verify(s => s.UpdateProductsProduced(It.IsAny<ProductsProducedViewModel>()), Times.Once);
            _mockSaveAndComeBackService.Verify(
                s =>
                    s.AddSaveAndComeBack(
                        It.IsAny<Guid>(),
                        It.IsAny<RouteValueDictionary>()),
                Times.Never);
        }
    }
}