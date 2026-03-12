using Microsoft.AspNetCore.Mvc;

namespace NGCP.LIS_NT.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult unauthorized()
        {
            return View();
        }



    }
}
