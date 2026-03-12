using Microsoft.AspNetCore.Mvc;

namespace NGCP.LIS_NT.Controllers
{
    public class RPTController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RPT()
        {
            return View();
        }

    }
}
