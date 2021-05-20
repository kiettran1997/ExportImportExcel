using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using ExportImportExcel.Models;
using OfficeOpenXml;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTable = System.Data.DataTable;

namespace ExportImportExcel.Controllers
{
    public class ImageInfoController : Controller
    {
        ImageEntities2 db = new ImageEntities2();
        //public int Department { get; set; }
        //public IEnumerable<ActualLabel> Departments { get; set; }
        // GET: ImageInfo
        public ActionResult Index()
        {

            using (db = new ImageEntities2())
            {
                dynamic model = new List<ExpandoObject>();
                List<ActualLabel> cate = db.ActualLabels.ToList();
                SelectList cateList = new SelectList(cate, "id", "actual_label_name");
                ViewBag.CategoryList = cateList;

                // lay du lieu bang ImageInfoes from image in db.ImageInfoes
                // join voi db.ActualLabels 
                // dieu kien join image.actual_label_id equals actual.id 
                var images = (from image in db.ImageInfoes
                              join actual in db.ActualLabels on image.actual_label_id equals actual.id into gj
                              from subpet in gj.DefaultIfEmpty()
                              select new
                              {
                                  image.id,
                                  image.image_id,
                                  image.image_link,
                                  image.predict_label,
                                  image.actual_label_id,
                                  subpet.actual_label_name
                              }).ToList();

                foreach (var image in images)
                {
                    dynamic img = new ExpandoObject();
                    img.id = image.id;
                    img.image_id = image.image_id;
                    img.image_link = image.image_link;
                    img.predict_label = image.predict_label;
                    img.actual_label_id = image.actual_label_id; ;
                    img.actual_label_name = image.actual_label_name;

                    //model.As = true;
                    model.Add(img);
                }
                //return json data
                //return Json(model, JsonRequestBehavior.AllowGet);

                return View(model);
            }

        }



        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4]
            {
                new DataColumn("id"),
                new DataColumn("image_id"),
                new DataColumn("image_link"),
                new DataColumn("predict_label")
            });
            var imageInfo = from ImageInfo in db.ImageInfoes select ImageInfo;
            foreach (var img in imageInfo)
            {
                dt.Rows.Add(img.image_id, img.image_link, img.predict_label);
            }
            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }

        [HttpPost]
        public ActionResult Save(ImageInfo model)
        {
            return View();

        }

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase postedFile)
        {

            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["postedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First(); if (workSheet != null)
                        {
                            using (var imageContext = new ImageEntities2())
                            {
                                var usersList = new List<ImageInfo>();

                                // số hàng
                                var rows = workSheet.Dimension.Rows;
                                // số cột
                                var column = workSheet.Dimension.Columns;

                                for (int i = 2; i <= rows; i++)
                                {
                                    if (workSheet.Cells[i, 1].Value != null)
                                    {
                                        var imageId = int.Parse(workSheet.Cells[i, 1].Value.ToString());
                                        var imageLink = workSheet.Cells[i, 2].Value.ToString();
                                        var PredictLabel = workSheet.Cells[i, 3].Value.ToString();

                                        var pictureInformation = new ImageInfo
                                        {
                                            image_id = imageId,
                                            image_link = imageLink,
                                            predict_label = PredictLabel
                                        };
                                        usersList.Add(pictureInformation);
                                    }
                                }

                                imageContext.ImageInfoes.AddRange(usersList);
                                imageContext.SaveChanges();
                                var imageInfos = db.ImageInfoes.ToList();
                                return View("imageInfos");
                            }


                        }

                    }
                }
            }
            return Json("no files were selected !");
        }


    }
}