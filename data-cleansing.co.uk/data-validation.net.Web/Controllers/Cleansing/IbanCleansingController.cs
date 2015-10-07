namespace data_validation.net.Web.Controllers.Cleansing
{
    using data_cleansing.net.Models;
    using data_validation.net.Web.Controllers.Helpers;
    using data_validation.net.Web.ViewModels;
    using Ionic.Zip;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class IbanCleansingController : BaseController
    {
        public ActionResult BulkIban(IEnumerable<HttpPostedFileBase> fileToUpload)
        {
            var outputStream = new MemoryStream();
            int recordsSubmited = 0;
            int validRecords = 0;
            int invalidRecords = 0;

            int i = 0;
            using (var zip = new ZipFile())
            {
                string time = User.Identity.Name + DateTime.Now.Ticks;
                Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
                string folder = Server.MapPath("~/Tuploads/") + time + "/";

                foreach (var csvFile in fileToUpload)
                {
                    var returData = new List<BulkIbanModel>();
                    var results = new List<BulkIbanModel>();
                    if (csvFile.ContentType.ToString() != "application/vnd.ms-excel")
                    {
                        //TO CHANGE!!!!!!!!!!!!!!!!
                        ViewBag.WrongFileType = "Wrong file type!";
                        return View();
                    }
                    string targetFolder = folder + csvFile.FileName;
                    csvFile.SaveAs(targetFolder);
                    var csvToData = new CsvToDataTable();
                    var csvData = csvToData.GetDataTabletFromCSVFile(targetFolder, "iban");

                    //Billing 
                    int records = csvData.Rows.Count;

                    recordsSubmited += records;
                    
                        var helper = new Helpers.CreditsDeduct();
                        var res = helper.IsValid(User.Identity.Name, records, "", "bulk", "iban");
                        if (res == false)
                        {
                            //TO CHANGE!!!!!!!
                            ViewBag.Error = "You don't have enough credits!";
                            return View();
                        }
                    
                    foreach (DataRow item in csvData.Rows)
                    {
                        var iban = BankValidationController.Iban.CheckIban(item.Field<string>(0).ToString().Replace(" ", "").TrimEnd().TrimStart(), true);
                        //Show why iban fail if so
                        if (iban.IsValid == false)
                        {
                            returData.Add(new BulkIbanModel { IsValid = iban.Message });
                            invalidRecords++;
                        }

                        var bankValidation = new BankValidationController();

                        //If iban is correct take details from DB
                        var result = bankValidation.BicCountry(item.Field<string>(0).TrimStart().TrimEnd().ToUpper());
                        foreach (var rr in result)
                        {
                            returData.Add(new BulkIbanModel
                            {
                                BankCode = rr.BankCode,
                                BankName = rr.BankName,
                                BranchAddress = rr.BranchAddress,
                                BranchCode = rr.BranchCode,
                                BranchName = rr.BranchName,
                                City = rr.City,
                                ClientIban = item.Field<string>(0),
                                Country = rr.Country,
                                Fax = rr.Fax,
                                IsoCode = rr.IsoCode,
                                IsValid = iban.IsValid.ToString(),
                                Postcode = rr.Postcode,
                                Swift = rr.Swift,
                                Telephone = rr.Telephone
                            });
                            validRecords++;
                        }
                    }

                    this.Data.IbanCleansingHistory.Add(new IbanCleansingHistory
                    {
                        DateSubmited = DateTime.Now,
                        InvalidRecords = invalidRecords,
                        SubmitedRecords = recordsSubmited,
                        UserName = User.Identity.Name,
                        ValidRecords = validRecords
                    });

                    this.Data.SaveChanges();

                    MemoryStream output = new MemoryStream();
                    //Save data to CSV file
                    if (returData.Count > 0)
                    {
                        StreamWriter writer = new StreamWriter(output, Encoding.UTF8);
                        StringBuilder sb = new StringBuilder();

                        writer.Write("Iban,Is Valid,Bank Name,Branch Name,Branch Address,City,Post Code,Telephone,Fax,Swift ");
                        writer.Write(Environment.NewLine);

                        foreach (var bankName in returData)
                        {
                            sb.Append(bankName.ClientIban + "," + bankName.IsValid + "," + bankName.BankName + "," + bankName.BranchName + "," + bankName.BranchAddress + "," + bankName.City + "," + bankName.Postcode + "," + bankName.Telephone + "," + bankName.Fax + "," + bankName.Swift);
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

        public JsonResult Chart()
        {
            var result = new List<IbanCleansingHistory>();

            var res = this.Data.IbanCleansingHistory.All().Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.Id).FirstOrDefault();

            result.Add(res);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}