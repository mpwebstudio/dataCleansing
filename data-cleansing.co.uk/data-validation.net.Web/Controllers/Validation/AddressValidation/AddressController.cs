namespace data_validation.net.Web.Controllers.Validation.AddressValidation
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using data_validation.net.Web.Models;
    using data_validation.net.Web.Models.DataCleansing;
    using Ionic.Zip;
    using data_cleansing.net.Models;
    using data_validation.net.Web.Controllers.Helpers;

    [Authorize]
    public class AddressController : BaseController
    {

        public JsonResult SingleAddress(string id)
        {
            var helper = new Helpers.CreditsDeduct();
            var res = helper.IsValid(User.Identity.Name, 6, id, "single", "address");

            if (!res)
            {
                var error = new List<ResultForPostCode>();

                error.Add(new ResultForPostCode { PostCode = "You do not have enough credits remain " + res });

                return Json(error, JsonRequestBehavior.AllowGet);
            }

            var dataCleansing = new DataCleansing.DataCleansingLogich();
            var temp = dataCleansing.Cleansing(id);

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BulkAddress(IEnumerable<HttpPostedFileBase> fileToUpload)
        {
            var outputStream = new MemoryStream();
            int foundAddress = 0;
            int notFoundAddress = 0;
            int totalRecords = 0;
            using (var zip = new ZipFile())
            {
                string time = User.Identity.Name + DateTime.Now.Ticks;
                Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
                string folder = Server.MapPath("~/Tuploads/") + time + "/";
                foreach (var csvFile in fileToUpload)
                {
                    var results = new List<AddressModel>();

                    //if (csvFile.ContentType.ToString() != "application/vnd.ms-excel")
                    //{
                    //    //TO CHANGE!!!!!!!!!!!!!!!!
                    //    ViewBag.WrongFileType = "Wrong file type!";
                    //    return View();
                    //}
                    string targetFolder = folder + csvFile.FileName;
                    csvFile.SaveAs(targetFolder);
                    var csvToData = new CsvToDataTable();
                    var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder, "address");
                    //Billing 
                    int records = csvData.Rows.Count;

                    var helper = new Helpers.CreditsDeduct();
                    //if more than 1 file submited
                    totalRecords += records;

                    var resul = helper.IsValid(User.Identity.Name, records, "", "bulk", "address");

                    if (resul == false)
                    {
                        //TO CHANGE!!!!!!!
                        ViewBag.Error = "You don't have enough credits!";
                        return View();
                    }

                    //If user has enough credits start cleansing
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
                        var dataCleansing = new DataCleansing.DataCleansingLogich();
                        var tempResult = dataCleansing.Cleansing(tempString.ToString().Substring(0, tempString.Length - 1)).FirstOrDefault();

                        if (tempResult.IsValid == "Corrected")
                        {
                            foundAddress++;
                        }
                        else
                        {
                            notFoundAddress++;
                        }

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
                                TraditionalCounty = tempResult.TraditionalCounty
                            });
                    }

                    this.Data.AddressCleansingHistory.Add(new AddressCleansingHistory
                        {
                            AddressCorrected = foundAddress,
                            AddressNotFound = notFoundAddress,
                            RecordsUploaded = totalRecords,
                            UserName = User.Identity.Name,
                            DateSubmited = DateTime.Now
                        });

                    this.Data.SaveChanges();

                    MemoryStream output = new MemoryStream();
                    int i = 0;

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

        public JsonResult Chart()
        {
            var res = new List<AddressCleansingHistory>();

            var result = this.Data.AddressCleansingHistory.All().Where(x => x.UserName == User.Identity.Name).OrderByDescending(x=> x.Id).FirstOrDefault();

            res.Add(result);

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}