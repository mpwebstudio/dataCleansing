namespace data_validation.net.Web.Controllers.BicValidation
{
    using data_cleansing.net.Data;
    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.Models;
    using Ionic.Zip;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    public class BicController : Controller
    {
        public JsonResult SingleBic(string id)
        {
            var helper = new Helpers.CreditsDeduct();
            var res = helper.IsValid(User.Identity.Name, 6,id,"single", "bic");
            if (res == false)
            {
                var error = new List<BicInfoModel>();
                error.Add(new BicInfoModel { ErrorCode = "You do not have enough credits remain" });
                return Json(error, JsonRequestBehavior.AllowGet);
            }

            using (var context = new ApplicationDbContext())
            {
                var result = context.Database.SqlQuery<BicInfoModel>("GetFullDetailsSwift @SwiftCode", new SqlParameter("SwiftCode", id)).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BulkBic(IEnumerable<HttpPostedFileBase> fileToUpload)
        {
            var outputStream = new MemoryStream();

            int i = 0;
            using (var zip = new ZipFile())
            {
                string time = User.Identity.Name + DateTime.Now.Ticks;
                Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
                string folder = Server.MapPath("~/Tuploads/") + time + "/";

                foreach (var csvFile in fileToUpload)
                {
                    var returData = new HashSet<BicInfoModel>();
                    if (csvFile.ContentType.ToString() != "application/vnd.ms-excel")
                    {
                        //TO CHANGE!!!!!!!!!!!!!!!!
                        ViewBag.WrongFileType = "Wrong file type!";
                        return View();
                    }

                    string targetFolder = folder + csvFile.FileName;
                    csvFile.SaveAs(targetFolder);
                    var csvToData = new CsvToDataTable();
                    var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder,"bic");
                    //Billing 
                    int records = csvData.Rows.Count;

                    if (User.Identity.IsAuthenticated)
                    {
                        var helper = new Helpers.CreditsDeduct();
                        var res = helper.IsValid(User.Identity.Name, records,"","bulk","bic");
                        if (res == false)
                        {
                            //TO CHANGE!!!!!!!
                            ViewBag.Error = "You don't have enough credits!";
                            return View();
                        }
                    }

                    foreach (DataRow item in csvData.Rows)
                    {
                        using (var context = new ApplicationDbContext())
                        {
                            var result = context.Database.SqlQuery<BicInfoModel>("GetFullDetailsSwift @SwiftCode", new SqlParameter("SwiftCode", item.Field<string>(0)));

                            foreach (var rr in result)
                            {
                                returData.Add(new BicInfoModel()
                                {
                                    BankOrInstitution = rr.BankOrInstitution,
                                    Branch = rr.Branch,
                                    City = rr.City,
                                    Country = rr.Country,
                                    SwiftCode = rr.SwiftCode
                                });
                            }
                        }
                    }

                    MemoryStream output = new MemoryStream();
                    //Save data to CSV file
                    if (returData.Count > 0)
                    {
                        StreamWriter writer = new StreamWriter(output, Encoding.UTF8);
                        StringBuilder sb = new StringBuilder();

                        writer.Write("Bank Name,Branch Name,City,Country,Swift ");
                        writer.Write(Environment.NewLine);

                        foreach (var bicDetails in returData)
                        {
                            sb.Append(bicDetails.BankOrInstitution + "," + bicDetails.Branch + "," + bicDetails.City + "," + bicDetails.Country + "," + bicDetails.SwiftCode);
                            writer.Write(sb);
                            writer.Write(Environment.NewLine);
                            sb.Clear();
                        }
                        writer.Flush();
                        output.Position = 0;
                    }

                    if (fileToUpload.Count() == 1)
                    {
                        return File(output, "text/comma-separated-values", User.Identity.Name + "_" + DateTime.Now.Ticks + ".csv");
                    }
                    else
                    {
                        using (FileStream file = new FileStream(folder + "test" + i + ".csv", FileMode.Create, System.IO.FileAccess.Write))
                        {
                            byte[] bytes = new byte[output.Length];
                            output.Read(bytes, 0, (int)output.Length);
                            file.Write(bytes, 0, bytes.Length);
                            output.Close();
                        }
                        zip.AddFile(folder + "test" + i + ".csv", @"\");
                        i++;
                        if (i == fileToUpload.Count())
                        {
                            zip.Save(outputStream);
                        }
                    }
                }
                outputStream.Position = 0;
                outputStream.Seek(0, SeekOrigin.Begin);
                return File(outputStream, "application/octet-stream", "filename.zip");
            }
        }
    }
}