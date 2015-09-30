namespace data_validation.net.Web.Controllers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using data_cleansing.net.Models;
    using data_validation.net.Web.Controllers.Cleansing;

    public class DeduplicateHelper : BaseController
    {
        
        public List<List<string>> ExactMatch(string matchCriteria, string toggleCheck, string time, int columns,string userName)
        {
            int uniqueRecords = 0;
            int dublicatedRecords = 0;
            var results = new List<List<string>>();
            var res = new List<List<string>>();
            var columnCriteria = toggleCheck.Split(',');
            var csvToData = new CsvToDataTable();
            var csvData = csvToData.GetDataTabletFromCSVFile(time, "deduplicate");
            HashSet<string> ScannedRecords = new HashSet<string>();
            HashSet<string> ScannedRecords2 = new HashSet<string>();

            int recordsSubmited = csvData.Rows.Count;
            int rowNumb = recordsSubmited;

            var getCredits = this.Data.Credits.All().Where(x => x.UserName == userName);
            int credits = 0;
            foreach (var item in getCredits)
            {
                credits = Int32.Parse(item.Credits.ToString());
                break;
            }

            //Checking if user have enough credits
            if (credits - rowNumb  < 0)
            {
                return results;
            }

            // 2 = not preview, bill user
            if (columns == 2)
            {
                var helper = new Helpers.CreditsDeduct();

                var creditsLeft = helper.IsValid(userName, rowNumb, "", "bulk", "deduplicate");

                if (!creditsLeft)
                {
                    return results;
                }
            }

            if (matchCriteria == "1")
            {
                foreach (DataRow row in csvData.Rows)
                {
                    List<string> str = new List<string>();
                    StringBuilder sb = new StringBuilder();

                    if (rowNumb > 0)
                    {
                        List<string> numb = new List<string>();
                        numb.Add(rowNumb.ToString());
                        results.Add(numb);
                        rowNumb = 0;
                    }

                    foreach (DataColumn col in csvData.Columns)
                    {
                        //checking for column conditions 
                        if (columnCriteria.Contains(col.ToString()))
                            sb.AppendFormat("[{0}={1}]", col, row[col.ColumnName].ToString());
                    }

                    if (ScannedRecords.Add(sb.ToString()))
                    {
                        uniqueRecords++;
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            str.Add(row.ItemArray[i].ToString());
                        }

                        results.Add(str);
                        //Showing first 100 records to prevent fraud
                        if (results.Count == 100 && columns == 1)
                        {
                            return results;
                        }
                    }
                    else
                    {
                        dublicatedRecords++;
                    }
                }
            }
            else if (matchCriteria == "2")
            {
                List<string> str = new List<string>();

                foreach (DataRow row in csvData.Rows)
                {
                    var sb = new StringBuilder();
                    foreach (DataColumn col in csvData.Columns)
                    {
                        if (columnCriteria.Contains(col.ToString()))
                        {
                            sb.AppendFormat("[{0}={1}]", col, row[col.ColumnName].ToString());
                        }
                    }
                    str.Add(sb.ToString());
                }

                int rowNumber = str.Count();

                foreach (DataRow row in csvData.Rows)
                {
                    List<string> str2 = new List<string>();
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb3 = new StringBuilder();

                    if (rowNumber > 0)
                    {
                        List<string> numb = new List<string>();
                        numb.Add(rowNumber.ToString());
                        results.Add(numb);
                        rowNumber = 0;
                    }

                    foreach (DataColumn col in csvData.Columns)
                    {
                        //checking for column conditions 
                        if (columnCriteria.Contains(col.ToString()))
                        {
                            sb.AppendFormat("[{0}={1}]", col, row[col.ColumnName].ToString());
                            sb3.AppendFormat("[{0}={1}]", col, row[col.ColumnName].ToString());
                        }
                    }

                    if (ScannedRecords.Add(sb.ToString()))
                    {
                        uniqueRecords++;
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            str2.Add(row.ItemArray[i].ToString());
                        }

                        var lenshtine = new Levenshtine();

                        var levenshtineResult = lenshtine.Search(sb3.ToString(), str, 0.9);

                        foreach (var item in levenshtineResult)
                        {
                            dublicatedRecords++;
                            ScannedRecords.Add(item);
                        }

                        results.Add(str2);
                        //Showing first 100 records to prevent fraud
                        if (results.Count == 100 && columns == 1)
                        {
                            return results;
                        }
                    }
                }
            }

            this.Data.DeduplicateCleansingHistory.Add(new DeduplicateCleansingHistory
                {
                    DateSubmited = DateTime.Now,
                    DuplicateRecords = dublicatedRecords,
                    SubmitedRecords = recordsSubmited,
                    UniqueRecords = uniqueRecords,
                    UserName = userName
                });

            this.Data.SaveChanges();

            return results;

        }
    }
}