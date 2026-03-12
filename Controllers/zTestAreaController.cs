using DevExpress.Office.Utils;
using Microsoft.AspNetCore.Mvc;
using NGCP.BaseModel;
using NGCP.LIS_NT.Class;
using NGCP.LIS_NT.Models;
using System.Data;
using System.IO.Compression;



namespace NGCP.LIS_NT.Controllers
{
    public class zTestAreaController : Controller
    {

        private readonly ILogger<zTestAreaController> _logger;
        private readonly IConfiguration _configuration;


        public zTestAreaController(ILogger<zTestAreaController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }
        public IActionResult Index()
        {
            string? vstatus = "";

            string folderName = Guid.NewGuid().ToString("N");
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
            string filePath = Path.Combine(folderPath, folderName);



            mGenericParameter param = new mGenericParameter();
            clsFileDocument fileDocument = new clsFileDocument(_configuration);
            mFileDocument mFile = new mFileDocument();
            param._action = "R";
            param.strParam = "0de40f8d-8c0e-4114-89e2-f045cb461fad";


            //if (!Directory.Exists(filePath))
            //{
            //    Directory.CreateDirectory(filePath);

            //    vstatus = "success";
            //}


            DataTable dtFile = fileDocument.GET_DATA(param);

            if (dtFile.Rows.Count > 0)
            {

                MemoryStream ms = new MemoryStream();
                using (var zipArchive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (DataRow item in dtFile.Rows)
                    {
                     
                        var entry = zipArchive.CreateEntry(item["FileName"].ToString());
                        using (var entryStream = entry.Open())
                        {
                            mFile.file = (byte[])(item["File"]);
                            entryStream.Write(mFile.file,0, mFile.file.Length);
                        }
                        //mFile.fileType = item["FileType"].ToString();

                        //System.IO.File.SetAttributes(filePath, FileAttributes.Normal);
                        //System.IO.File.WriteAllBytes(filePath, mFile.file);
                        //return new FileContentResult(mFile.file, mFile.fileType);
                    }
                }
                

            }
            //return File(MsoAnchor., "application/zip", "MyFile.zip");

            // mFile.file


            //TempData["vstatus"] = vstatus;
            return View();
        }



        public IActionResult ZipDownload()
        {


            mGenericParameter param = new mGenericParameter();
            clsFileDocument fileDocument = new clsFileDocument(_configuration);
            mFileDocument mFile = new mFileDocument();
            param._action = "ZIP";
            param.strParam = "TEST00082";


            string zipFileName = param.strParam + ".zip";

            DataTable dtFile = fileDocument.GET_DATA(param);

            using (var compressedFileStream = new MemoryStream())
            {
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    foreach (DataRow dr in dtFile.Rows)
                    {
                        //Create a zip entry for each attachment
                        var zipEntry = zipArchive.CreateEntry(dr["FileName"].ToString());


                        //Get the stream of the attachment
                        using (var originalFileStream = new MemoryStream((byte[])(dr["File"])))
                        using (var zipEntryStream = zipEntry.Open())
                        {
                            //Copy the attachment stream to the zip entry stream
                            originalFileStream.CopyTo(zipEntryStream);
                        }
                    }
                }

                return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = zipFileName };
            }


        }


    }
}
