using CentralData.Class;
using CentralData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NGCP.BaseClass;
using NGCP.BaseModel;
using NGCP.LIS_NT.Class;
using NGCP.LIS_NT.Models;
using System.Diagnostics;

namespace NGCP.LIS_NT.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        private readonly clsSessionUser _sessionUser;


        public HomeController(ILogger<HomeController> logger, clsSessionUser sessionUser, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _sessionUser = sessionUser;

        }

        public IActionResult Index()
        {
            //HomePageModel homePageModel = new HomePageModel();
            //homePageModel.userName = HttpContext?.User.Identity?.Name;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Main()
        {
            var _userName = _sessionUser.session_get_username();
            var _userNumber = _sessionUser.session_get_userNumber();
            var _userDiv = _sessionUser.session_get("_userDiv");
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(mLogin model)
        {

            clsLogin login = new clsLogin(_configuration);
            clsUserMaster userMaster = new clsUserMaster(_configuration);
            clsOrganization userOrg = new clsOrganization(_configuration);
            mGenericParameter param = new mGenericParameter();

            string rvalue = login.IsValidate(model);
            if (rvalue.Contains("Authorized"))
            {
                param._action = "GETDETAIL";
                param.strParam = model.userName;
                _sessionUser.session_set_userNumber(userMaster.GET_USERNUMBER(param));
                _sessionUser.session_set_userName(model.userName);

                param._action = "ORG";
                param.strParam = _sessionUser.session_get_userNumber();
                string userDiv = userOrg.GET_USERDIVISION(param);

                if (userDiv == null)
                {
                    return RedirectToAction("OrgAuthentication", "Helper");
                }

                _sessionUser.session_set("_userDiv",userDiv ?? "" );

                return RedirectToAction("Main", "Home");
            }

            return View();
        }


        public IActionResult Logout()
        {

            _sessionUser.session_unset();

            return RedirectToAction("Login");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult MainPage()
        {
            return View();
        }


    }
}
