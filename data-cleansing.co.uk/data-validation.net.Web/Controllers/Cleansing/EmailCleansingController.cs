namespace data_validation.net.Web.Controllers.Cleansing
{
    using data_cleansing.net.Models;
    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.Models.ViewModels;
    using Ionic.Zip;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    public class EmailCleansingController : BaseController
    {
        //
        // GET: /EmailCleansing/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData(HttpPostedFileBase fileToUpload)
        {
            var result = new List<List<string>>();
            string time = RandomGeneratorController.GetUserId();
            Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
            string folder = Server.MapPath("~/Tuploads/" + time + "/");
            var fileLocation = new List<string>();
            string targetFolder = folder + fileToUpload.FileName;
            fileToUpload.SaveAs(targetFolder);
            var csvToData = new CsvToDataTable();
            var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder,"email");
            var tmpResilts = new Controllers.Helpers.Email();

            //add file location to List
            fileLocation.Add(targetFolder);
            result.Add(fileLocation);

            if (csvData.Rows.Count > 10)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(404, JsonRequestBehavior.AllowGet);
            }

            foreach (DataRow row in csvData.Rows)
            {
                List<string> str = new List<string>();

                foreach (var item in row.ItemArray)
                {
                    var tmpResult = tmpResilts.Validate(item.ToString());

                    foreach (var items in tmpResult)
                    {
                        str.Add(items.Email);
                        str.Add(items.IsValid);
                        str.Add(items.Message);
                        str.Add(items.MxRecord);
                        break;
                    }
                    result.Add(str);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult BulkEmailValidation(IEnumerable<HttpPostedFileBase> fileToUpload)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return new HttpStatusCodeResult(404, "User is authenticated!");
        //    }

        //    var outputStream = new MemoryStream();
        //    int i = 0;
        //    using (var zip = new ZipFile())
        //    {
        //        string time = "TemporaryUser" + DateTime.Now.Ticks;
        //        Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
        //        string folder = Server.MapPath("~/Tuploads/") + time + "/";
        //        var results = new List<EmailValidationViewModel>();
        //        foreach (var csvFile in fileToUpload)
        //        {
        //            if (csvFile.ContentType.ToString() != "application/vnd.ms-excel")
        //            {
        //                return new HttpStatusCodeResult(402, "Wrong File format");
        //            }
        //            string targetFolder = folder + csvFile.FileName;
        //            csvFile.SaveAs(targetFolder);
        //            var csvToData = new CsvToDataTable();
        //            var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder, "email");
        //            //Billing 
        //            int records = csvData.Rows.Count;

        //            if (records > 10)
        //            {
        //                return new HttpStatusCodeResult(403, "More than 10 records");
        //            }

        //            var tmpResilts = new Helpers.Email();

        //            foreach (DataRow res in csvData.Rows)
        //            {
        //                foreach (var item in res.ItemArray)
        //                {
        //                    var tmpResult = tmpResilts.Validate(item.ToString());

        //                    foreach (var tt in tmpResult)
        //                    {
        //                        results.Add(new EmailValidationViewModel
        //                        {
        //                            Email = tt.Email,
        //                            IsValid = tt.IsValid,
        //                            Message = tt.Message,
        //                            MxRecord = tt.MxRecord
        //                        });
        //                    }
        //                }
        //            }

        //            MemoryStream output = new MemoryStream();

        //            if (results.Count() > 0)
        //            {
        //                StreamWriter writer = new StreamWriter(output, Encoding.UTF8);
        //                StringBuilder sb = new StringBuilder();
        //                writer.Write("Email address, Mx Record, Is valid, Message ");
        //                writer.Write(Environment.NewLine);
        //                foreach (var details in results)
        //                {
        //                    sb.Append(details.Email + "," + details.MxRecord + "," + details.IsValid + "," + details.Message);
        //                    writer.Write(sb);
        //                    writer.Write(Environment.NewLine);
        //                    sb.Clear();
        //                }
        //                writer.Flush();
        //                output.Position = 0;
        //            }
        //            //If any records write them to csv file and return to user
        //            if (fileToUpload.Count() == 1)
        //            {
        //                return File(output, "text/coma-separated-values", User.Identity.Name + "_" + DateTime.Now.Ticks + ".csv");
        //            }
        //            else
        //            {
        //                using (FileStream file = new FileStream(folder + "test" + i + ".csv", FileMode.Create, System.IO.FileAccess.Write))
        //                {
        //                    byte[] bytes = new byte[output.Length];
        //                    output.Read(bytes, 0, (int)output.Length);
        //                    file.Write(bytes, 0, bytes.Length);
        //                    output.Close();
        //                }
        //                zip.AddFile(folder + "test" + i + ".csv", @"\");
        //                i++;
        //                if (i == fileToUpload.Count())
        //                {
        //                    zip.Save(outputStream);
        //                }
        //            }
        //        }
        //        outputStream.Position = 0;
        //        outputStream.Seek(0, SeekOrigin.Begin);
        //        return File(outputStream, "application/octet-stream", "data-cleansing.co.uk.zip");
        //    }
        //}

        [Authorize]
        public ActionResult BulkEmailValidation(IEnumerable<HttpPostedFileBase> fileToUpload)
        {
            var outputStream = new MemoryStream();
            int i = 0;
            int validEmails = 0;
            int invalidEmails = 0;
            int sintacsisValid = 0;
            int recordsSubmited = 0;
            using (var zip = new ZipFile())
            {
                string time = User.Identity.Name + DateTime.Now.Ticks;
                Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
                string folder = Server.MapPath("~/Tuploads/") + time + "/";
                foreach (var csvFile in fileToUpload)
                {
                    var results = new List<EmailValidationViewModel>();

                    if (csvFile.ContentType.ToString() != "application/vnd.ms-excel")
                    {
                        results.Add(new EmailValidationViewModel
                        {
                            Message = "Wrong file type! File must be CSV"
                        });
                        return Json(results, JsonRequestBehavior.AllowGet);
                    }
                    string targetFolder = folder + csvFile.FileName;
                    csvFile.SaveAs(targetFolder);
                    var csvToData = new CsvToDataTable();
                    var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder, "email");
                    //Billing 
                    int records = csvData.Rows.Count;

                    recordsSubmited += records;

                    if (User.Identity.IsAuthenticated)
                    {
                        var helper = new Helpers.CreditsDeduct();
                        var res = helper.IsValid(User.Identity.Name, records, "", "bulk", "email");

                        if (res == false)
                        {
                            results.Add(new EmailValidationViewModel
                            {
                                Message = "You don't have enought credits left. Please Top Up!"
                            });

                            return Json(results, JsonRequestBehavior.AllowGet);
                        }
                    }

                    foreach (DataRow res in csvData.Rows)
                    {
                        foreach (var item in res.ItemArray)
                        {
                            var tmpResilts = new Helpers.Email();

                            var tmpResult = tmpResilts.Validate(item.ToString());

                            foreach (var tt in tmpResult)
                            {
                                results.Add(new EmailValidationViewModel
                                {
                                    Email = tt.Email,
                                    IsValid = tt.IsValid,
                                    Message = tt.Message,
                                    MxRecord = tt.MxRecord
                                });

                                if (tt.IsValid == "Yes")
                                    validEmails++;
                                else if (tt.IsValid == "")
                                    invalidEmails++;
                                else
                                    sintacsisValid++;
                            }
                        }
                    }

                    this.Data.EmailCleansingHistory.Add(new EmailCleansingHistory
                    {
                        DateSubmited = DateTime.Now,
                        InvalidEmail = invalidEmails,
                        RecordsUploaded = recordsSubmited,
                        SyntaxValid = sintacsisValid,
                        UserName = User.Identity.Name,
                        ValidEmail = validEmails
                    });

                    this.Data.SaveChanges();

                    MemoryStream output = new MemoryStream();

                    if (results.Count() > 0)
                    {
                        StreamWriter writer = new StreamWriter(output, Encoding.UTF8);
                        StringBuilder sb = new StringBuilder();
                        writer.Write("Email address, Mx Record, Is valid, Message ");
                        writer.Write(Environment.NewLine);
                        foreach (var details in results)
                        {
                            sb.Append(details.Email + "," + details.MxRecord + "," + details.IsValid + "," + details.Message);
                            writer.Write(sb);
                            writer.Write(Environment.NewLine);
                            sb.Clear();
                        }
                        writer.Flush();
                        output.Position = 0;
                    }
                    //If any records write them to csv file and return to user
                    if (fileToUpload.Count() == 1)
                    {
                        return File(output, "text/coma-separated-values", User.Identity.Name + "_" + DateTime.Now.Ticks + ".csv");
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

        [Authorize]
        public JsonResult Chart()
        {
            var result = new List<EmailCleansingHistory>();
            var res = this.Data.EmailCleansingHistory.All().Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.Id).FirstOrDefault();
            result.Add(res);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
