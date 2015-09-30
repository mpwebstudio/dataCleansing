using data_validation.net.Web.Models.DataCleansing;
using data_validation.net.Web.Controllers;
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
using data_validation.net.Web.Controllers.Helpers;

namespace data_validation.net.Web.Controllers.Cleansing
{
    public class AddressCleansingController : BaseController
    {
        
        public ActionResult Index()
        {
            return View();
        }

        //AddressCleansing#/addressCleansing
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
            var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder, "address");
            var tmpResults = new DataCleansing.DataCleansingLogich();

            fileLocation.Add(targetFolder);
            result.Add(fileLocation);

            if(csvData.Rows.Count > 10)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(404, JsonRequestBehavior.AllowGet);
            }

            foreach (DataRow row in csvData.Rows)
            {
                List<string> str = new List<string>();

                foreach (var item in row.ItemArray)
                {
                    var tmpResult = tmpResults.Cleansing(item.ToString());

                    foreach(var items in tmpResult)
                    {
                        str.Add(items.BuildingName);
                        str.Add(items.Flat);
                        str.Add(items.BuildingNumber);
                        str.Add(items.Street);
                        str.Add(items.Locality);
                        str.Add(items.TraditionalCounty);
                        str.Add(items.PostCode);
                        break;
                    }
                    result.Add(str);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //Application#/addressCleansing
        public ActionResult BulkCleansing(IEnumerable<HttpPostedFileBase>fileToUpload)
        {
            if(User.Identity.IsAuthenticated)
            {
                return new HttpStatusCodeResult(404, "Item Not Found");
            }

            var outputStream = new MemoryStream();
            int i = 0;
            var dataCleansing = new DataCleansing.DataCleansingLogich();
            using (var zip = new ZipFile())
            {
                string time = "TemporaryUser" + DateTime.Now.Ticks;
                Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
                string folder = Server.MapPath("~/Tuploads/") + time + "/";
                foreach (var csvFile in fileToUpload)
                {
                    var results = new List<AddressModel>();

                    if (csvFile.ContentType.ToString() != "application/vnd.ms-excel")
                    {
                        return new HttpStatusCodeResult(402, "Wrong File format");
                    }
                    string targetFolder = folder + csvFile.FileName;
                    csvFile.SaveAs(targetFolder);
                    var csvToData = new CsvToDataTable();
                    var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder,"address");
                    //Billing 
                    int records = csvData.Rows.Count;

                    if(records > 10)
                    {
                        return new HttpStatusCodeResult(403, "More than 10 records");
                    }
                                        
                    foreach (DataRow res in csvData.Rows)
                    {
                        StringBuilder tempString = new StringBuilder();

                        foreach (var item in res.ItemArray)
                        {
                            if (item.ToString().Length == 0)
                            {
                                continue;
                            }
                            tempString = tempString.Append(item + ",");
                        }
                        
                        var tempeResult = dataCleansing.Cleansing(tempString.ToString().Substring(0, tempString.Length - 1));

                        foreach (var tempResult in tempeResult)
                        {

                            results.Add(new AddressModel
                                {
                                    AdministrativeCounty = tempResult.AdministrativeCounty,
                                    BuildingName = tempResult.BuildingName,
                                    BuildingNumber = tempResult.BuildingNumber,
                                    City = tempResult.City,
                                    Flat = tempResult.Flat,
                                    IsValid = tempResult.IsValid,
                                    Locality = tempResult.Locality,
                                    PostCode = tempResult.PostCode,
                                    Street = tempResult.Street,
                                    TraditionalCounty = tempResult.TraditionalCounty,
                                });
                            break;
                        }
                    }

                    MemoryStream output = new MemoryStream();

                    if (results.Count() > 0)
                    {

                        StreamWriter write = new StreamWriter(output, Encoding.UTF8);
                        StringBuilder sb = new StringBuilder();

                        write.Write("Corrected address");
                        write.Write(Environment.NewLine);

                        foreach (var details in results)
                        {
                            sb.Append(details.Flat + "," + details.BuildingName + "," + details.BuildingNumber + "," + details.Street + "," + details.City + "," + details.Locality + "," + details.TraditionalCounty + "," + details.PostCode + "," + details.IsValid);
                            write.Write(sb);
                            write.Write(Environment.NewLine);
                            sb.Clear();
                        }
                        write.Flush();
                        output.Position = 0;
                    }
                    //If any records write them to csv file and return to user
                    if (fileToUpload.Count() == 1)
                    {
                        return File(output, "text/coma-separated-values", "data-cleansing.co.uk" + "_" + DateTime.Now.Ticks + ".csv");
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
                return File(outputStream, "application/octet-stream", "data-cleansing.co.uk.zip");
            }

        }
    }
}
