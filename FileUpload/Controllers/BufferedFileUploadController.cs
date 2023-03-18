using ExcelDataReader;
using FileUpload.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FileUpload.Models;

namespace FileUpload.Controllers
{
    public class BufferedFileUploadController : Controller
    {
        readonly IBufferedFileUploadService _bufferedFileUploadService;
        private readonly IReadExcel _readExcel;

        public BufferedFileUploadController(IBufferedFileUploadService bufferedFileUploadService, IReadExcel readExcel)
        {
            _bufferedFileUploadService = bufferedFileUploadService;
            _readExcel = readExcel;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file)
        {
            if (file == null)
            {
                ViewBag.Message = "File Upload Failed, selected no file!!";
                List<ReportModelGeneral> list = new List<ReportModelGeneral>();
                ViewData["cnt"] = 0;

                return View(list);
            }
            

           
            try
            {
                if (await _bufferedFileUploadService.UploadFile(file))
                {
                    string str = "";

                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    Stream stream = file.OpenReadStream();
                    // We return the interface, so that
                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        DataSet ds = new DataSet();
                        ds = reader.AsDataSet();
                        reader.Close();
                        var cc =await _readExcel.GetDataFromExcel(ds);

                        ViewData["cnt"] = cc.Count();
                        /*if (ds != null && ds.Tables.Count > 0)
                        {
                            // Read the the Table
                            DataTable serviceDetails = ds.Tables[0];
                            int i = 1;
                            ViewBag.test = serviceDetails.Rows[i][2].ToString();

                        }*/
                        ViewBag.Message = "This file format is xls supported";
                        return View(cc);
                    }
                    if (file.FileName.EndsWith(".xlsx"))
                    {
                        await using (stream = file.OpenReadStream())
                        {
                            using (reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                     reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                     DataSet result = reader.AsDataSet();
                                     reader.Close();

                                     if (result != null && result.Tables.Count > 0)
                                     {
                                         // Read the the Table
                                         DataTable serviceDetails = result.Tables[0];
                                         int i = 1;
                                        ViewBag.test = serviceDetails.Rows[i][2].ToString();

                                     }
                            }
                        }


                       
                        ViewBag.Message = "This file format is xlsx supported";
                        // return View();
                       
                        return View();
                    }
                    else
                    {
                        //ModelState.AddModelError("File", "This file format is not supported");
                        //return View("Index =
                        ViewBag.Message = "This file format is not supported";
                        return View();
                    }


                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }
    }
}
