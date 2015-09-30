namespace data_validation.net.Web.Controllers.CardValidation
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using Ionic.Zip;

    using data_validation.net.Web.Models.ViewModels;
    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.Models;
    using data_validation.net.Web.Models.DataCleansing;
    using data_cleansing.net.Models;


    public class CardValidationController : BaseController
    {
        [HttpGet]
        public JsonResult CardValidation(string id, string id2)
        {
            var res = new List<CardValidationViewModel>();

            try
            {
                string number = id.Replace(" ", "");
                long intNumber = Int64.Parse(number);
            }
            catch
            {
                res.Add(new CardValidationViewModel
                    {
                        CardNumber = id,
                        CardIssue = "N/A",
                        IsValid = "Entered number contains non-digit character!",
                        Message = "Number contains non-digit character!"
                    });
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            if (User.Identity.IsAuthenticated)
            {
                var helper = new Helpers.CreditsDeduct();
                var isValid = helper.IsValid(User.Identity.Name, 6, id, "single", "card");

                if (isValid == false)
                {

                    if (User.Identity.IsAuthenticated)
                    {
                        res.Add(new CardValidationViewModel
                        {
                            Message = "You don't have enought credits left. Please Top Up!"
                        });
                    }
                    else
                    {
                        res.Add(new CardValidationViewModel
                        {
                            Message = "You don't have more free credits left. Please Sign in and Top Up!"
                        });
                    }

                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string apiNumber = id2;
                var isValid = new IsValid();
                bool tUser = isValid.ValidUser(apiNumber, id, "address");

                if (tUser == false)
                {
                    res.Add(new CardValidationViewModel { Message = "You have reach your free daily limits!" });
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

            }
            var cardType = new CardType();
            var result = cardType.CardLogic(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult BulkCardValidation(IEnumerable<HttpPostedFileBase> fileToUpload)
        {
            var outputStream = new MemoryStream();
            int i = 0;
            int recordsSubmited = 0;
            int validCards = 0;
            int invalidCards = 0;
            using (var zip = new ZipFile())
            {
                string time = User.Identity.Name + DateTime.Now.Ticks;
                Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
                string folder = Server.MapPath("~/Tuploads/") + time + "/";
                foreach (var csvFile in fileToUpload)
                {
                    var results = new List<CardValidationViewModel>();

                    if (csvFile.ContentType.ToString() != "application/vnd.ms-excel")
                    {
                        results.Add(new CardValidationViewModel
                        {
                            Message = "Wrong file type! File must be CSV"
                        });

                        return Json(results, JsonRequestBehavior.AllowGet);
                    }
                    string targetFolder = folder + csvFile.FileName;
                    csvFile.SaveAs(targetFolder);
                    var csvToData = new CsvToDataTable();
                    var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder, "card");
                    //Billing 
                    int records = csvData.Rows.Count;

                    recordsSubmited += records;

                    if (User.Identity.IsAuthenticated)
                    {
                        var helper = new Helpers.CreditsDeduct();
                        var res = helper.IsValid(User.Identity.Name, records, "", "bulk", "card");

                        if (res == false)
                        {
                            results.Add(new CardValidationViewModel
                            {
                                Message = "You don't have enought credits left. Please Top Up!"
                            });

                            return Json(results, JsonRequestBehavior.AllowGet);
                        }
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
                        var cardType = new CardType();

                        var temp = cardType.CardLogic(tempString.ToString().Substring(0, tempString.Length - 1));

                        foreach (var resul in temp)
                        {
                            results.Add(new CardValidationViewModel
                            {
                                CardIssue = resul.CardIssue,
                                IsValid = resul.IsValid,
                                Message = resul.Message,
                                CardNumber = resul.CardNumber
                            });

                            if (resul.IsValid == "Card is valid")
                                validCards++;
                            else
                                invalidCards++;
                        }
                    }

                    MemoryStream output = new MemoryStream();

                    this.Data.CardCleansingHistory.Add(new CardCleansingHistory
                        {
                            DateSubmited = DateTime.Now,
                            InvalidCards = invalidCards,
                            RecordsUploaded = recordsSubmited,
                            UserName = User.Identity.Name,
                            ValidCards = validCards
                        });

                    this.Data.SaveChanges();

                    if (results.Count() > 0)
                    {
                        StreamWriter writer = new StreamWriter(output, Encoding.UTF8);
                        StringBuilder sb = new StringBuilder();

                        writer.Write("Card Number, Card Type, Is valid, Message ");
                        writer.Write(Environment.NewLine);

                        foreach (var details in results)
                        {
                            sb.Append(details.CardNumber + "," + details.CardIssue + "," + details.IsValid + "," + details.Message);
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

        public JsonResult Chart()
        {
            var result = new List<CardCleansingHistory>();

            var res = this.Data.CardCleansingHistory.All().Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.Id).FirstOrDefault();

            result.Add(res);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
