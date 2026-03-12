using Microsoft.AspNetCore.Mvc;
using NGCP.BaseClass;
using NGCP.BaseModel;

namespace NGCP.LIS_NT.Controllers
{
    public class HelperController : Controller
    {

        private readonly ILogger<HelperController> _logger;
        private readonly IConfiguration _configuration;
        private readonly clsSessionUser _sessionUser;

        public HelperController(ILogger<HelperController> logger, IConfiguration configuration, clsSessionUser sessionUser)
        {
            _logger = logger;
            _configuration = configuration;
            _sessionUser = sessionUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ErrorPage(string id = "")
        {
            ViewData["id"] = id;

            //_sessionUser.session_unset();
            return View();
        }

        public mResponse JSONError(string id = "")
        {
            mResponse __ResponseModel = new mResponse();
            __ResponseModel.ResponseCode = Convert.ToInt32(id);
            __ResponseModel.ResponseMessage = "Something went wrong...";




            return __ResponseModel;
        }


        public IActionResult OrgAuthentication()
        {
           ViewData["responseMessage"] = "This account has not been assigned to an organization. Please contact your administrator";
            ViewData["userName"] = _sessionUser.session_get_username();
            return View();
        }
    }
}
