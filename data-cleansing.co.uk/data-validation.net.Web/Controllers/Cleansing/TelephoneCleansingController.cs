﻿using data_cleansing.net.Data;
using data_cleansing.net.Models;
using data_validation.net.Web.Controllers.Helpers;
using data_validation.net.Web.Models.ViewModels;
using data_validation.net.Web.ViewModels;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers.Cleansing
{
    public class TelephoneCleansingController : BaseController
    {
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
            var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder, "phone");


            if (csvData.Rows.Count > 10)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(404, JsonRequestBehavior.AllowGet);
            }

            var resul = Validate(csvData);

            //remove last element in witch has valid and invalid data
            resul.RemoveAt(resul.Count - 1);

            foreach (var item in resul)
            {
                List<string> str = new List<string>();
                str.Add(item.Number);
                str.Add(item.IsValid);
                str.Add(item.Area);
                str.Add(item.Message);
                result.Add(str);
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        public ActionResult BulkPhoneValidation(IEnumerable<HttpPostedFileBase> fileToUpload)
        {
            int recordsSubmited = 0;
            var outputStream = new MemoryStream();
            int i = 0;
            using (var zip = new ZipFile())
            {
                string time = User.Identity.Name + DateTime.Now.Ticks;
                Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
                string folder = Server.MapPath("~/Tuploads/") + time + "/";
                foreach (var csvFile in fileToUpload)
                {   
                    if (csvFile.ContentType.ToString() != "application/vnd.ms-excel")
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(404, JsonRequestBehavior.AllowGet);
                    }
                    string targetFolder = folder + csvFile.FileName;
                    csvFile.SaveAs(targetFolder);
                    var csvToData = new CsvToDataTable();
                    var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder, "phone");
                    //Billing 
                    int records = csvData.Rows.Count;

                    recordsSubmited += records;

                    var helper = new Helpers.CreditsDeduct();
                    var res = helper.IsValid(User.Identity.Name, records, "", "bulk", "phone");

                    if (res == false)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(404, JsonRequestBehavior.AllowGet);
                    }

                    var results = Validate(csvData);
                    
                    var counts = results[results.Count - 1];

                    this.Data.PhoneCleansingHistory.Add(new PhoneCleansingHistory
                    {
                        DateSubmited = DateTime.Now,
                        Invalid = Int32.Parse(counts.Number),
                        SubmitedRecords = recordsSubmited,
                        UserName = User.Identity.Name,
                        Valid = Int32.Parse(counts.Message)
                    });

                    this.Data.SaveChanges();

                    MemoryStream output = new MemoryStream();

                    if (results.Count() > 0)
                    {
                        StreamWriter writer = new StreamWriter(output, Encoding.UTF8);
                        StringBuilder sb = new StringBuilder();
                        writer.Write("Phone Number, Area, Is valid, Message ");
                        writer.Write(Environment.NewLine);
                        foreach (var details in results)
                        {
                            sb.Append(details.Number + "," + details.Area + "," + details.IsValid + "," + details.Message);
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
            var result = new List<PhoneCleansingHistory>();

            var res = this.Data.PhoneCleansingHistory.All().Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.Id).FirstOrDefault();

            result.Add(res);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<PhoneValidationViewModel> Validate(DataTable csvData)
        {
            var results = new List<PhoneValidationViewModel>();

            int invalidRecords = 0;
            int validRecords = 0;

            var removeSpecialCharacters = new RemoveSpecialCharacters();

            foreach (DataRow res in csvData.Rows)
            {
                foreach (var number in res.ItemArray)
                {

                    var cleanNumber = removeSpecialCharacters.RemoveCharactersTelephone(number.ToString());

                    if (cleanNumber.Length != 11 && cleanNumber.Length != 10)
                    {
                        results.Add(new PhoneValidationViewModel
                        {
                            IsValid = "Phone is invalid",
                            Number = "=\"" + number.ToString() + "\"",
                        });

                        invalidRecords++;
                    }
                    else
                    {
                        using (var context = new ApplicationDbContext())
                        {
                            var tmpData = context.Database.SqlQuery<GetPhoneArea_Result>("GetPhoneArea2 @code", new SqlParameter("code", cleanNumber.Substring(0, 5))).First();

                            if (tmpData.IsValid == "Valid")
                            {
                                results.Add(new PhoneValidationViewModel
                                {
                                    Area = tmpData.AreaCovered,
                                    Number = "=\"" + number.ToString() + "\"",
                                    IsValid = tmpData.IsValid
                                });
                                validRecords++;
                                break;
                            }
                            else
                            {
                                results.Add(new PhoneValidationViewModel
                                {
                                    Area = tmpData.AreaCovered,
                                    Number = "=\"" + number.ToString() + "\"",
                                    IsValid = tmpData.IsValid
                                });
                                invalidRecords++;
                                break;
                            }
                        }
                    }
                }
            }

            results.Add(new PhoneValidationViewModel
            {
                Number = invalidRecords.ToString(),
                Message = validRecords.ToString()
            });

            return results;
        }
    }
}