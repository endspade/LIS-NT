using CentralData.Class;
using CentralData.Models;
using Microsoft.AspNetCore.Mvc;
using NGCP.BaseClass;
using NGCP.BaseModel;
using NGCP.LIS_NT.Class;
using NGCP.LIS_NT.Models;
using System.Data;

namespace NGCP.LIS_NT.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class LibraryController : Controller
    {
        private readonly ILogger<LibraryController> _logger;
        private readonly IConfiguration _configuration;
        private readonly clsSessionUser _sessionUser;

        public LibraryController(ILogger<LibraryController> logger, IConfiguration configuration, clsSessionUser sessionUser)
        {
            _logger = logger;
            _configuration = configuration;
            _sessionUser = sessionUser;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CaseType()
        {

            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360010";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_CaseType(mGenericParameter param)
        {

            clsCaseType caseType = new clsCaseType(_configuration);
            return Json(clsGlobal.ConvertDataTable<mCaseType>(caseType.GET_DATA(param)));

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_CaseType(mCaseType model)
        {

            clsCaseType caseType = new clsCaseType(_configuration);
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            mGenericParameter param = new mGenericParameter();

            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);

            param._action = "INC";
            param.strParam = "360010";
            model.caseTypeNumber = model._action == "C" ? auto.GET_Number(param) : model.caseTypeNumber;
            model.userCode = _sessionUser.session_get_username();
            string msg = caseType.CUD(model);


            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }

        public IActionResult CaseLevel()
        {

            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360011";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_CaseLevel(mGenericParameter param)
        {

            clsCaseLevel caseLevel = new clsCaseLevel(_configuration);
            return Json(clsGlobal.ConvertDataTable<mCaseLevel>(caseLevel.GET_DATA(param)));

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]  
        public JsonResult POST_CaseLevel(mCaseLevel model)
        {

            clsCaseLevel caseLevel = new clsCaseLevel(_configuration);
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            mGenericParameter param = new mGenericParameter();

            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);

            param._action = "INC";
            param.strParam = "";
            model.caseLevelNumber = model._action == "C" ? auto.GET_Number(param) : model.caseLevelNumber;
            model.userCode = _sessionUser.session_get_username();
            string msg = caseLevel.CUD(model);


            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public IActionResult CaseStatus()
        {
            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360012";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_CaseStatus(mGenericParameter param)
        {
            clsCaseStatus caseStatus = new clsCaseStatus(_configuration);
            return Json(clsGlobal.ConvertDataTable<mCaseStatus>(caseStatus.GET_DATA(param)));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_CaseStatus(mCaseStatus model)
        {
            clsCaseStatus caseStatus = new clsCaseStatus(_configuration);
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            mGenericParameter param = new mGenericParameter();

            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);

            param._action = "INC";
            param.strParam = "";
            model.caseStatusNumber = model._action == "C" ? auto.GET_Number(param) : model.caseStatusNumber;
            model.userCode = _sessionUser.session_get_username();
            string msg = caseStatus.CUD(model);

            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public IActionResult DocumentCategory()
        {
            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360013";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_DocumentCategory(mGenericParameter param)
        {

            clsDocumentCategory documentCategory = new clsDocumentCategory(_configuration);
            return Json(clsGlobal.ConvertDataTable<mDocumentCategory>(documentCategory.GET_DATA(param)));

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_DocumentCategory(mDocumentCategory model)
        {

            clsDocumentCategory documentCategory = new clsDocumentCategory(_configuration);
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            mGenericParameter param = new mGenericParameter();

            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);

            param._action = "INC";
            param.strParam = "";
            model.docCatNumber = model._action == "C" ? auto.GET_Number(param) : model.docCatNumber;
            model.userCode = _sessionUser.session_get_username();
            string msg = documentCategory.CUD(model);

            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public IActionResult LawyerMaster()
        {
            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360014";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_LawyerMaster(mGenericParameter param)
        {

            clsLawyerMaster lawyerMaster = new clsLawyerMaster(_configuration);
            return Json(clsGlobal.ConvertDataTable<mLawyer>(lawyerMaster.GET_DATA(param)));

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_LawyerMaster(mLawyer model)
        {
            clsLawyerMaster lawyerMaster = new clsLawyerMaster(_configuration);
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            mGenericParameter param = new mGenericParameter();

            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);

            param._action = "INC";
            param.strParam = "";
            model.lawyerNumber = model._action == "C" ? auto.GET_Number(param) : model.lawyerNumber;
            model.userCode = _sessionUser.session_get_username();
            string msg = lawyerMaster.CUD(model);


            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public ActionResult OrganizationalGroup()
        {
            ViewData["sfInfo"] = "1111111";
            return View();
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_OrgGroup(mGenericParameter param)
        {
            clsOrganization org = new clsOrganization(_configuration);
            param.strParam = mAppInformation.applicationCode;

            return Json(clsGlobal.ConvertDataTable<mOrganization>(org.GET_DATA(param)));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_OrgGroup(mOrganization model)
        {
            clsOrganization Organizational = new clsOrganization(_configuration);
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            mGenericParameter param = new mGenericParameter();

            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);

            param._action = "INC";
            param.strParam = "";
            model.orgCode = auto.GET_Number(param);

            string msg = Organizational.CUD(model);
            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public ActionResult OrganizationalGrouping()
        {
            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }


            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360016";

            if (sFInfo.ModuleAccess(param))
            {

                clsUserMaster user = new clsUserMaster(_configuration);
                mGenericParameter model = new mGenericParameter();

                //model._action = "AP";
                //model.strParam = mAppInformation.applicationCode;
                //model.intParam = 1;
                //ViewData["group"] = Organizational.GET_DATA(model);

                model._action = "GH";
                ViewData["USERList"] = user.GET_DATA(model);

                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_OrgGrouping(mGenericParameter param)
        {
            clsOrganization org = new clsOrganization(_configuration);
            return Json(clsGlobal.ConvertDataTable<mOrganization>(org.GET_Grouping(param)));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_OrgGrouping(mOrganization model)
        {
            clsOrganization org = new clsOrganization(_configuration);
            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);
            model.userCode = _sessionUser.session_get_username();
            string msg = org.POST_Grouping(model);
            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);
        }


    }
}
