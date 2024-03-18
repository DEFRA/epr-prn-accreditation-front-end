using EPR.Accreditation.Portal.ViewModels;
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
            if (!ModelState.IsValid)
                return View(wasteLicensesAndPermitsViewModel);


            return View(
                "WasteLicensesAndPermits");
        }
    }
}
