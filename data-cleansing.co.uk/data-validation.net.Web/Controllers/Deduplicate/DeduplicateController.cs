namespace data_validation.net.Web.Controllers.Deduplicate
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using data_cleansing.net.Models;
    using data_validation.net.Web.Controllers.Helpers;


    public class DeduplicateController : BaseController
    {
        //Deduplicate/
        public ActionResult Index()
        {
            return View();
        }

        //Application#/deduplicate
        //Deduplicate#/deduplicate
        public JsonResult GetColumn(IEnumerable<HttpPostedFileBase> fileToUpload)
        {
            var columnName = new List<string>();
            string time = User.Identity.Name + DateTime.Now.Ticks;
            Directory.CreateDirectory(Server.MapPath("~/Tuploads/") + time);
            string folder = Server.MapPath("~/Tuploads/") + time + "/";

            foreach (var csvFile in fileToUpload)
            {
                string targetFolder = folder + csvFile.FileName;
                csvFile.SaveAs(targetFolder);
                columnName.Add(targetFolder);
                var csvToData = new CsvToDataTable();
                var csvData = csvToData.GetDataTableColumns(targetFolder);

                foreach (DataColumn col in csvData.Columns)
                {
                    columnName.Add(col.ColumnName);
                }
            }

            return Json(columnName, JsonRequestBehavior.AllowGet);
        }

        //Application#/deduplicate
        [Authorize]
        public JsonResult GetData(string matchCriteria, string toggleCheck, string time)
        {
            var match = new Helpers.DeduplicateHelper();
            string userName = User.Identity.Name;
            var results = match.ExactMatch(matchCriteria, toggleCheck, time, 1, userName);

            if (results.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(404, JsonRequestBehavior.AllowGet);
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        //Deduplicate#/deduplicate
        public JsonResult GetDataTemp(string matchCriteria, string toggleCheck, string time)
        {
            var match = new Helpers.DeduplicateHelper();
            string userName = "";

            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            else
            {
                userName = "temp";
            }

            var results = match.ExactMatch(matchCriteria, toggleCheck, time, 2, userName);

            if (results.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(404, JsonRequestBehavior.AllowGet);
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        //Application/#deduplicate
        [Authorize]
        [HttpPost]
        public ActionResult DownloadFile(string matchCriteria, string toggleCheck, string time, string[] columns)
        {
            var outputStream = new MemoryStream();
            var match = new Helpers.DeduplicateHelper();
            string userName = User.Identity.Name;
            var results = match.ExactMatch(matchCriteria, toggleCheck, time, 2, userName);
            MemoryStream output = new MemoryStream();

            if (results.Count() > 0)
            {
                results.RemoveAt(0);
                StreamWriter write = new StreamWriter(output, Encoding.UTF8);
                StringBuilder sb = new StringBuilder();

                foreach (var item in columns)
                {
                    sb.Append(item + ",");
                }

                write.Write(sb);
                write.Write(Environment.NewLine);
                sb.Clear();

                foreach (var list in results)
                {
                    foreach (var item in list)
                    {
                        sb.Append(item.ToString() + ",");
                    }
                    write.Write(sb);
                    write.Write(Environment.NewLine);
                    sb.Clear();
                }
                write.Flush();
                output.Position = 0;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(404, JsonRequestBehavior.AllowGet);
            }

            return File(output, "text/coma-separated-values", User.Identity.Name + "_" + DateTime.Now.Ticks + ".csv");
        }

        //Application#/deduplicate
        [Authorize]
        public JsonResult Chart()
        {
            var result = new List<DeduplicateCleansingHistory>();
            var resultHistory = this.Data.DeduplicateCleansingHistory.All().Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.Id).FirstOrDefault();

            result.Add(resultHistory);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}