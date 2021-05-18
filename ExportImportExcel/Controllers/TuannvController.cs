using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;

namespace ExportImportExcel.Controllers
{
    public class TuannvController : Controller
    {
        // GET: Tuannv
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            // file se nhan dc o day
            try
            {
                if (file.ContentLength > 0)
                {

                    // khởi tạo 
                    var excelPackage = new ExcelPackage();
                    // load file vào đối tượng
                    excelPackage.Load(file.InputStream);
                    // lấy ra sheet đầu tiên
                    var workSheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (workSheet != null)
                    {
                        // số hàng
                        var rows = workSheet.Dimension.Rows;
                        // số cột
                        var column = workSheet.Dimension.Columns;

                        // lấy giá trị hàng 1 cột 1, trong excel ko có khái niệm hàng số 0, cột 0
                        // bắt đầu từ 1
                        var cell = workSheet.Cells[1, 1].Value.ToString();
                    }

                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}