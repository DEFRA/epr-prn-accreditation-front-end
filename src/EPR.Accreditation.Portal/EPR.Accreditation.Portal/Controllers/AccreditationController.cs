using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EPR.Accreditation.Portal.Controllers
{
    public class AccreditationController : Controller
    {
        private readonly IAccreditationService _accreditationService;

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public AccreditationController(IAccreditationService accreditationService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
        }

        [HttpGet]
        public IActionResult ApplicationSaved(Guid? id)
        {
            if (id == null)
                return NotFound();

            var viewModel = _accreditationService.GetApplicationSavedViewModel(id.Value);

            return View(viewModel);
        }

        [HttpGet]
        [ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(Guid? id)
        {
            if (id == null)
                id = Guid.NewGuid();
                //return NotFound();

            var viewModel = _accreditationService.GetWasteLicensesAndPermitsViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel)
        {
            if (!ModelState.IsValid)
                return View(wasteLicensesAndPermitsViewModel);

            await _accreditationService.SaveWasteLicensesAndPermitsViewMode(wasteLicensesAndPermitsViewModel);

            var accreditation = new DTOs.Accreditation();

            accreditation.ReferenceNumber = RandomString(12);
            accreditation.OperatorTypeId = Enums.OperatorType.Reprocessor;
            accreditation.OrganisationId = Guid.NewGuid();
            accreditation.Large = true;
            accreditation.AccreditationStatusId = Enums.AccreditationStatus.Accepted;
            accreditation.SiteId = 1;
            
            var site = new DTOs.Site();
            site.Address1 = "203 Pooley Green Rd";
            site.Address2 = "Egham";
            site.Id = 2;
            site.ExternalId = Guid.NewGuid();

            var wastePermit = new DTOs.WastePermit();
            wastePermit.EnvironmentalPermitNumber = wasteLicensesAndPermitsViewModel.PermitNumber;
            wastePermit.DischargeConsentNumber = wasteLicensesAndPermitsViewModel.DischargeConstentNumber;
            wastePermit.DealerRegistrationNumber = wasteLicensesAndPermitsViewModel.RegistrationNumber;
            wastePermit.PartAActivityReferenceNumber = wasteLicensesAndPermitsViewModel.ActivityReferenceNumber;
            wastePermit.PartBActivityReferenceNumber = wasteLicensesAndPermitsViewModel.ActivityNumber;
            wastePermit.WastePermitExemption = true;
            accreditation.WastePermit = wastePermit;

            var overseaSites = new List<DTOs.OverseasReprocessingSite>();
            overseaSites.Add(new DTOs.OverseasReprocessingSite());
            accreditation.Site = site;
            accreditation.OverseasReprocessingSites = overseaSites;

            wasteLicensesAndPermitsViewModel.SiteId = Guid.Parse("60FB09B6-07B4-4A33-9F8D-461FB02D64D7");
            await _accreditationService.SaveAccreditation(wasteLicensesAndPermitsViewModel.Id, accreditation);

            return View(wasteLicensesAndPermitsViewModel);
        }
    }
}
