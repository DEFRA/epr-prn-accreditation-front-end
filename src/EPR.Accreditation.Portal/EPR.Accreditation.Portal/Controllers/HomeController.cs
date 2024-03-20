using EPR.Accreditation.App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(int? id)
        {
            return View(
                "WasteLicensesAndPermits");
        }

        [HttpPost]
        [ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel)
        {
            //if (!ModelState.IsValid)
            //    return View(wasteLicensesAndPermitsViewModel);

            //var accreditation = _httpAccreditationServicel.GetAccreditation("6E04132D-9E52-4853-BE1E-D48C2DCA82BA");
            //_httpAccreditationServicel.CreateWastePermit(1, 1);

            return View(
                "WasteLicensesAndPermits");
        }
    }
}
