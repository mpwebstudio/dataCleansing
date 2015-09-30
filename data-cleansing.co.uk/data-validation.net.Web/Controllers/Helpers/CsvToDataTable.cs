using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class CsvToDataTable
    {
        public DataTable GetDataTableColumns(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();

                    for (int i = 0; i < colFields.Length; i++)
                    {
                        colFields[i] = this.RemoveSpecialCharacters(colFields[i]);
                    }

                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                }
            }
            catch (Exception ex)
            {
                //TO DO
            }
            return csvData;
        }
        public DataTable GetDataTabletFromCSVFile(string csv_file_path, string service)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();

                    for (int i = 0; i < colFields.Length; i++)
                    {
                        colFields[i] = this.RemoveSpecialCharacters(colFields[i]);
                    }

                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();

                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                            else
                            {
                                switch (service)
                                {
                                    case "email":
                                        fieldData[i] = this.RemoveSpecialCharactersEmail(fieldData[i]);
                                        break;
                                    case "deduplicate":
                                        fieldData[i] = this.RemoveSpecialCharactersDeduplicate(fieldData[i]);
                                        break;
                                    default:
                                        fieldData[i] = this.RemoveSpecialCharacters(fieldData[i]);
                                        break;
                                }
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                //TO DO
            }
            return csvData;
        }

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == ',' || c == ';')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string RemoveSpecialCharactersEmail(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == '_' || c == '-' || c == '@' || c == '.')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string RemoveSpecialCharactersDeduplicate(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == '_' || c == '-' || c == '@' || c == '.' || c == ',' || c == ';' || c == '\'' || c == '-' || c == ';' || c == '`' || c == '(' || c == ')' || c == '&')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}