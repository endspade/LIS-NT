using CentralData.Class;
using CentralData.Models;
using Microsoft.AspNetCore.Mvc;
using NGCP.BaseClass;
using NGCP.BaseModel;
using System.Data;

namespace NGCP.LIS_NT.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SecurityController : Controller
    {
        private readonly ILogger<SecurityController> _logger;
        private readonly IConfiguration _configuration;
        private readonly clsSessionUser _sessionUser;

        public SecurityController(ILogger<SecurityController> logger, IConfiguration configuration, clsSessionUser sessionUser)
        {
            _logger = logger;
            _configuration = configuration;
            _sessionUser = sessionUser;
        }

        public IActionResult Index()
        {
            return View();
        }



        public ActionResult UserMaster()
        {
            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "700009";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");
        }
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_UserMaster(mGenericParameter param)
        {
            clsUserMaster user = new clsUserMaster(_configuration);

            return Json(clsGlobal.ConvertDataTable<mUserMaster>(user.GET_DATA(param)));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_UserMaster(mUserMaster model)
        {
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            clsUserMaster user = new clsUserMaster(_configuration);
            mGenericParameter param = new mGenericParameter();

            param._action = "INC";
            param.strParam = "";
            model.userNumber = auto.GET_Number(param);

            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);
            string msg = user.CUD(model);

            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);
        }


        public ActionResult RBACGroup()
        {
            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "700010";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");
        }

        [AutoValidateAntiforgeryToken]
        public JsonResult GET_RBACGroup(mGenericParameter param)
        {
            clsRBACGroup rbac = new clsRBACGroup(_configuration);
            param.strParam = mAppInformation.applicationCode;

            return Json(clsGlobal.ConvertDataTable<mRBAC>(rbac.GET_DATA(param)));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_RBACGroup(mRBAC model)
        {
            clsRBACGroup rbac = new clsRBACGroup(_configuration);
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            mGenericParameter param = new mGenericParameter();

            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);

            param._action = "INC";
            param.strParam = "700010";
            model.RBACCode = auto.GET_Number(param);

            string msg = rbac.CUD(model);
            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public ActionResult RBACGrouping()
        {

            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "700011";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);


                clsRBACGroup RBAC = new clsRBACGroup(_configuration);
                clsUserMaster user = new clsUserMaster(_configuration);
                mGenericParameter model = new mGenericParameter();

                model._action = "GH";
                ViewData["USERList"] = user.GET_DATA(model);

                model._action = "AP";
                model.strParam = mAppInformation.applicationCode;
                model.intParam = 1;
                ViewData["RBACGroup"] = RBAC.GET_DATA(model);

                return View();
            }

            return RedirectToAction("unauthorized", "Error");
        }

        [AutoValidateAntiforgeryToken]
        public JsonResult GET_RBACGrouping(mGenericParameter param)
        {
            clsRBACGrouping RBAC = new clsRBACGrouping(_configuration);
            return Json(clsGlobal.ConvertDataTable<mRBAC>(RBAC.GET_DATA(param)));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_RBACGrouping(mRBAC model)
        {
            clsRBACGrouping RBAC = new clsRBACGrouping(_configuration);
            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);
            string msg = RBAC.CUD(model);
            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);
        }
        


        public ActionResult SFInfo()
        {
         
            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "700008";

            //if(true)
            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);

                clsRBACGroup RBAC = new clsRBACGroup(_configuration);
                //mGenericParameter param = new mGenericParameter();

                param._action = "AP";
                param.strParam = mAppInformation.applicationCode;
                param.intParam = 1;
                ViewData["RBACGroup"] = RBAC.GET_DATA(param);

                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }

        [AutoValidateAntiforgeryToken]
        public JsonResult GET_SFInfo(mGenericParameter param)
        {
            clsSFInfo SFInfo = new clsSFInfo(_configuration);
            return Json(clsGlobal.ConvertDataTable<mSFInfo>(SFInfo.GET_DATA(param)));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_SFInfo(mSFInfo model)
        {
            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            model.id = clsGlobal.Decrypt(model.id);
            string msg = sFInfo.CUD(model);
            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }
         
    }
}
