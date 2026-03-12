using CentralData.Class;
using CentralData.Models;
using Microsoft.AspNetCore.Mvc;
using NGCP.BaseClass;
using NGCP.BaseModel;
using NGCP.LIS_NT.Class;
using NGCP.LIS_NT.Models;

namespace NGCP.LIS_NT.Controllers
{
    public class PhilDemographicController : Controller
    {

        private readonly ILogger<PhilDemographicController> _logger;
        private readonly IConfiguration _configuration;

        public PhilDemographicController(ILogger<PhilDemographicController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AutoValidateAntiforgeryToken]
        public JsonResult GET_PSG(mGenericParameter param)
        {
            clsPhilDemographic philDemograpic = new clsPhilDemographic(_configuration);
            return Json(clsGlobal.ConvertDataTable<mPhilDemographic>(philDemograpic.GET_DATA(param)));
        }
    }
}
