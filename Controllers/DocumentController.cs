using CentralData.Class;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Utils.Design;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using NGCP.BaseClass;
using NGCP.BaseModel;
using NGCP.LIS_NT.Class;
using NGCP.LIS_NT.Models;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;

namespace NGCP.LIS_NT.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DocumentController : Controller
    {


        private readonly ILogger<DocumentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly clsSessionUser _sessionUser;

        public DocumentController(ILogger<DocumentController> logger, IConfiguration configuration, clsSessionUser sessionUser)
        {
            _logger = logger;
            _configuration = configuration;
            _sessionUser = sessionUser;
        }


        public IActionResult Index()
        {
            return View();
        }



        public ActionResult CaseMaster()
        {

            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            if (_sessionUser.session_get("_userDiv") == string.Empty || _sessionUser.session_get("_userDiv") == null)
            {
                if (_sessionUser.session_get_username() == string.Empty || _sessionUser.session_get_username() == null)
                {
                    _sessionUser.session_unset();
                    return RedirectToAction("Login", "Home");
                }

                return RedirectToAction("OrgAuthentication", "Helper");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360050";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);

                clsCaseType type = new clsCaseType(_configuration);
                clsCaseLevel level = new clsCaseLevel(_configuration);
                clsCaseStatus status = new clsCaseStatus(_configuration);
                param.strParam = "";
                param.intParam = 1;
                ViewData["caseType"] = type.GET_DATA(param);
                ViewData["caseLevel"] = level.GET_DATA(param);
                ViewData["caseStatus"] = status.GET_DATA(param);

                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }

        [AutoValidateAntiforgeryToken]
        public JsonResult GET_CaseMaster(mGenericParameter param)
        {
            param._action = _sessionUser.session_get("_userDiv");
            clsCaseHeader caseHeader = new clsCaseHeader(_configuration);
            return Json(clsGlobal.ConvertDataTable<mCase>(caseHeader.GET_DATA(param)));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_CaseMaster(mCase model)
        {
            mGenericParameter param = new mGenericParameter();
            clsAutoNumber auto = new clsAutoNumber(_configuration);
            clsCaseHeader caseHeader = new clsCaseHeader(_configuration);


            param._action = "INC";
            param.strParam = "";
            model.docNumber = model._action == "C" ? auto.GET_Number(param) : model.docNumber;
            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);
            model.orgType = _sessionUser.session_get("_userDiv");
            model.userCode = _sessionUser.session_get_username();
            string msg = caseHeader.CUD(model);

            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public ActionResult CaseDocument()
        {

            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            if (_sessionUser.session_get("_userDiv") == string.Empty || _sessionUser.session_get("_userDiv") == null)
            {
                if (_sessionUser.session_get_username() == string.Empty || _sessionUser.session_get_username() == null)
                {
                    _sessionUser.session_unset();
                    return RedirectToAction("Login", "Home");
                }

                return RedirectToAction("OrgAuthentication", "Helper");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360051";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);

                clsCaseHeader caseHeader = new clsCaseHeader(_configuration);
                clsLawyerMaster lawyerMaster = new clsLawyerMaster(_configuration);
                param.strParam = "";
                param.intParam = 1;
                ViewData["caseMaster"] = caseHeader.GET_DATA(param);
                ViewData["lawyerMaster"] = lawyerMaster.GET_DATA(param);

                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }

        [AutoValidateAntiforgeryToken]
        public JsonResult GET_CaseDocument(mGenericParameter param)
        {
            clsCaseDetail caseDetail = new clsCaseDetail(_configuration);
            param.strParam = _sessionUser.session_get("_userDiv");
            return Json(clsGlobal.ConvertDataTable<mCase>(caseDetail.GET_DATA(param)));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_CaseDocument(mCase model, List<IFormFile> file)
        {

            mFileDocument fileDocument = new mFileDocument();
            clsCaseDetail caseDetail = new clsCaseDetail(_configuration);
            clsFileDocument upload = new clsFileDocument(_configuration);

            model.refNumber = "";
            model.recordStatus = true;
            model.docGuid = model._action == "C" ? Guid.NewGuid().ToString() : model.docGuid;
            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);
            model.userCode = _sessionUser.session_get_username();
            string msg = caseDetail.CUD(model);

            if (file.Count > 0)
            {
                fileDocument._action = "C";
                fileDocument.docGuid = model.docGuid;
                fileDocument.fileName = file[0].FileName;
                fileDocument.fileType = file[0].ContentType;
                fileDocument.fileSize = file[0].Length.ToString();
                fileDocument.userCode = _sessionUser.session_get_username();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file[0].CopyTo(memoryStream);
                    fileDocument.file = memoryStream.ToArray();
                }
                upload.CUD(fileDocument);
            }

            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public ActionResult DocumentIssuance()
        {


            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            if (_sessionUser.session_get("_userDiv") == string.Empty || _sessionUser.session_get("_userDiv") == null)
            {
                if (_sessionUser.session_get_username() == string.Empty || _sessionUser.session_get_username() == null)
                {
                    _sessionUser.session_unset();
                    return RedirectToAction("Login", "Home");
                }
                return RedirectToAction("OrgAuthentication", "Helper");
            }


            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360052";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);

                clsCaseHeader caseHeader = new clsCaseHeader(_configuration);
                clsLawyerMaster lawyerMaster = new clsLawyerMaster(_configuration);

                param.strParam = "";
                param.intParam = 1;
                ViewData["caseMaster"] = caseHeader.GET_DATA(param);
                ViewData["lawyerMaster"] = lawyerMaster.GET_DATA(param);

                return View();
            }

            return RedirectToAction("unauthorized", "Error");
        }

        [AutoValidateAntiforgeryToken]
        public JsonResult GET_DocumentIssuance(mGenericParameter param)
        {
            clsDocumentIssuance docIssuance = new clsDocumentIssuance(_configuration);
            return Json(clsGlobal.ConvertDataTable<mIssuance>(docIssuance.GET_DATA(param)));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_DocumentIssuance(mIssuance model, List<IFormFile> file)
        {

            mFileDocument fileDocument = new mFileDocument();
            clsDocumentIssuance docIssuance = new clsDocumentIssuance(_configuration);
            clsFileDocument upload = new clsFileDocument(_configuration);

            string msg;
            List<string> data = new List<string>();


            string? fileExt = file[0].FileName;

            string[] strArray = { ".pdf", ".xlsx", ".docx" };

            if (file.Count > 0) 
            {
                foreach (string x in strArray)
                {
                    if (fileExt.Contains(x))
                    {

                        model.docGuid = model._action == "C" ? Guid.NewGuid().ToString() : model.docGuid;
                        model.recordStatus = true;
                        model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);
                        model.userCode = _sessionUser.session_get_username();
                        msg = docIssuance.CUD(model);



                        fileDocument._action = "C";
                        fileDocument.docGuid = model.docGuid;
                        fileDocument.fileName = file[0].FileName;
                        fileDocument.fileType = file[0].ContentType;
                        fileDocument.fileSize = file[0].Length.ToString();
                        fileDocument.userCode = _sessionUser.session_get_username();
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            file[0].CopyTo(memoryStream);
                            fileDocument.file = memoryStream.ToArray();
                        }
                        upload.CUD(fileDocument);

                        data.Add(msg);
                        return Json(data);

                    }
                }
            }
            msg = "Please upload a PDF file.";
            data.Add(msg);
            return Json(data);


            //if (file.Count > 0)
            //{
            //    fileDocument._action = "C";
            //    fileDocument.docGuid = model.docGuid;
            //    fileDocument.fileName = file[0].FileName;
            //    fileDocument.fileType = file[0].ContentType;
            //    fileDocument.fileSize = file[0].Length.ToString();
            //    fileDocument.userCode = _sessionUser.session_get_username();
            //    using (MemoryStream memoryStream = new MemoryStream())
            //    {
            //        file[0].CopyTo(memoryStream);
            //        fileDocument.file = memoryStream.ToArray();
            //    }
            //    upload.CUD(fileDocument);
            //}




        }


        public ActionResult TaxMatter()
        {

            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            if (_sessionUser.session_get("_userDiv") == string.Empty || _sessionUser.session_get("_userDiv") == null)
            {
                if (_sessionUser.session_get_username() == string.Empty || _sessionUser.session_get_username() == null)
                {
                    _sessionUser.session_unset();
                    return RedirectToAction("Login", "Home");
                }

                return RedirectToAction("OrgAuthentication", "Helper");
            }

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360053";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }

        [AutoValidateAntiforgeryToken]
        public JsonResult GET_TaxMatter(mGenericParameter param)
        {

            if (param.provCode.Length == 10)
            {
                param.munCode = param.munCode ?? "00";
                param.bgyCode = param.bgyCode ?? "000";
                param.strParam = param.provCode.Substring(0, 5) + param.munCode + param.bgyCode;
            }

            clsRPTDocument rpt = new clsRPTDocument(_configuration);
            return Json(clsGlobal.ConvertDataTable<mRPTDocument>(rpt.GET_DATA(param)));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_TaxMatter(mRPTDocument model, List<IFormFile> file)
        {

            mFileDocument fileDocument = new mFileDocument();
            clsRPTDocument rpt = new clsRPTDocument(_configuration);
            clsFileDocument upload = new clsFileDocument(_configuration);

            model.docGuid = model._action == "C" ? Guid.NewGuid().ToString() : model.docGuid;
            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);
            model.munCode = model.munCode ?? "00";
            model.bgyCode = model.bgyCode ?? "000";

            if (model.bgyCode != "000")
            {
                model.pdCode = model.bgyCode;
            }
            else
            {
                model.pdCode = model.provCode?.Substring(0, 5) + model.munCode + model.bgyCode;
            }

            model.userCode = _sessionUser.session_get_username();
            string msg = rpt.CUD(model);

            if (file.Count > 0)
            {
                fileDocument._action = "C";
                fileDocument.docGuid = model.docGuid;
                fileDocument.refType = "RPT";
                fileDocument.fileName = file[0].FileName;
                fileDocument.fileType = file[0].ContentType;
                fileDocument.fileSize = file[0].Length.ToString();
                fileDocument.userCode = _sessionUser.session_get_username();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file[0].CopyTo(memoryStream);
                    fileDocument.file = memoryStream.ToArray();
                }
                upload.CUD(fileDocument);
            }

            List<string> data = new List<string>();
            data.Add(msg);

            return Json(data);

        }


        public ActionResult ProceduralManual()
        {

            if (_sessionUser.session_active())
            {
                return RedirectToAction("Login", "Home");
            }

            if (_sessionUser.session_get("_userDiv") == string.Empty || _sessionUser.session_get("_userDiv") == null)
            {
                if (_sessionUser.session_get_username() == string.Empty || _sessionUser.session_get_username() == null)
                {
                    _sessionUser.session_unset();
                    return RedirectToAction("Login", "Home");
                }
                return RedirectToAction("OrgAuthentication", "Helper");

            }

            ViewData["_username"] = _sessionUser.session_get_username();

            clsSFInfo sFInfo = new clsSFInfo(_configuration);
            mGenericParameter param = new mGenericParameter();
            param.strParam = _sessionUser.session_get_userNumber();
            param.moduleCode = "360054";

            if (sFInfo.ModuleAccess(param))
            {
                ViewData["sfInfo"] = sFInfo.AccessInfo(param);
                return View();
            }

            return RedirectToAction("unauthorized", "Error");

        }
        [AutoValidateAntiforgeryToken]
        public JsonResult GET_ProcedureManual(mGenericParameter param)
        {
            clsProcedureManual procedureManual = new clsProcedureManual(_configuration);
            return Json(clsGlobal.ConvertDataTable<mProcedureManual>(procedureManual.GET_DATA(param)));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult POST_ProcedureManual(mProcedureManual model, List<IFormFile> file)
        {

            mFileDocument fileDocument = new mFileDocument();
            clsProcedureManual procedureManual = new clsProcedureManual(_configuration);
            clsFileDocument upload = new clsFileDocument(_configuration);
            string msg = string.Empty;
            List<string> data = new List<string>();

            if (model._action != "D")
            {
                if (file[0].ContentType.ToString() != "application/pdf")
                {
                    msg = "Invalid file!";
                    data.Add(msg);
                    return Json(data);
                }

            }

            model.docGuid = Guid.NewGuid().ToString();
            model.id = model._action == "C" ? "0" : clsGlobal.Decrypt(model.id);
            model.userCode = _sessionUser.session_get_username();
            msg = procedureManual.CUD(model);

            if (file.Count > 0)
            {
                fileDocument._action = "C";
                fileDocument.docGuid = model.docGuid;
                fileDocument.fileName = file[0].FileName;
                fileDocument.fileType = file[0].ContentType;
                fileDocument.fileSize = file[0].Length.ToString();
                fileDocument.userCode = _sessionUser.session_get_username();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file[0].CopyTo(memoryStream);
                    fileDocument.file = memoryStream.ToArray();
                }
                upload.CUD(fileDocument);
            }


            data.Add(msg);

            return Json(data);

        }


        [AutoValidateAntiforgeryToken]
        public FileContentResult Preview(mGenericParameter param)
        {

            clsFileDocument fileDocument = new clsFileDocument(_configuration);
            mFileDocument mFile = new mFileDocument();
            param._action = "R";
            DataTable dtFile = fileDocument.GET_DATA(param);

            if (dtFile.Rows.Count > 0)
            {
                foreach (DataRow item in dtFile.Rows)
                {
                    mFile.file = (byte[])(item["File"]);
                    mFile.fileType = item["FileType"].ToString();
                    mFile.fileName = item["FileName"].ToString();
                    string? fileExt = Path.GetExtension(mFile.fileName);
                    return new FileContentResult(mFile.file, fileExt);
                }

            }

            //return new FileContentResult(new byte[8], "image/png");
            return null;

        }


        [AutoValidateAntiforgeryToken]
        public IActionResult ZipDownload(mGenericParameter param)
        {

            clsFileDocument fileDocument = new clsFileDocument(_configuration);
            mFileDocument mFile = new mFileDocument();
            param._action = "ZIP";

            string zipFileName = param.strParam + ".zip";

            DataTable dtFile = fileDocument.GET_DATA(param);

            if (dtFile.Rows.Count == 0)
            {
                return new FileContentResult(new byte[8], "image/png") { FileDownloadName = "No File" };
            }

            try
            {
                using (var compressedFileStream = new MemoryStream())
                {

                    using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                    {
                        foreach (DataRow dr in dtFile.Rows)
                        {

                            var zipEntry = zipArchive.CreateEntry(dr["FileName"].ToString());

                            using (var originalFileStream = new MemoryStream((byte[])(dr["File"])))
                            using (var zipEntryStream = zipEntry.Open())
                            {
                                originalFileStream.CopyTo(zipEntryStream);
                            }
                        }
                    }

                    return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = zipFileName };
                }
            }
            catch (Exception e)
            {

                return new FileContentResult(new byte[8], "image/png") { FileDownloadName = "No File" };
            }



        }

    }
}
