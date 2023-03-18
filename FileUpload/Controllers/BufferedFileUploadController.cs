using ExcelDataReader;
using FileUpload.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FileUpload.Controllers
{
    public class BufferedFileUploadController : Controller
    {
        readonly IBufferedFileUploadService _bufferedFileUploadService;

        public BufferedFileUploadController(IBufferedFileUploadService bufferedFileUploadService)
        {
            _bufferedFileUploadService = bufferedFileUploadService;
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
                return View();
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
                        await using (stream = file.OpenReadStream())
                        {
                            using ( reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                // Choose one of either 1 or 2:

                                // 1. Use the reader methods
                                do
                                {
                                    while (reader.Read())
                                    {
                                        // reader.GetDouble(0);
                                    }
                                } while (reader.NextResult());

                                // 2. Use the AsDataSet extension method
                                var result = reader.AsDataSet();

                                // The result of each spreadsheet is in result.Tables
                            }
                        }
                        ViewBag.Message = "This file format is xls supported";
                        return View();
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
