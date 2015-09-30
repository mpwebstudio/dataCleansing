namespace data_validation.net.Web.Controllers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class CreateInvoiceController : BaseController
    {
        

        public void PdfInvoice(int id)
        {
            var infoCompany = this.Data.BillingInformation.All().Single(x => x.UserName == User.Identity.Name);

            var infoInvoice = this.Data.Invoice.All().Single(x => x.Id == id);

            var infoUser = this.Data.Profile.All().Single(x => x.UserName == User.Identity.Name);

            string totalInvoice = infoInvoice.Amount.ToString();

            DataTable table = new DataTable();
            table.Columns.Add("NO", typeof(int));
            table.Columns.Add("CREDITS PURCHASE", typeof(string));
            table.Columns.Add("PAYMENT TYPE", typeof(string));
            table.Columns.Add("AMOUNT", typeof(string));

            table.Rows.Add(1, infoInvoice.CreditsPurchase.ToString(), infoInvoice.PaymentType, infoInvoice.Amount.ToString());

            try
            {

                var titleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                var boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
                var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

                Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();

                //Image Singature
                Image logo = Image.GetInstance(Server.MapPath("~/Content/img/paid.png"));
                logo.ScalePercent(60f);
                pdfDoc.Add(logo);

                PdfPTable headertable = new PdfPTable(3);
                headertable.HorizontalAlignment = 0;
                headertable.WidthPercentage = 100;
                headertable.SetWidths(new float[] { 4, 2, 4 });  // then set the column's __relative__ widths
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;
                //headertable.DefaultCell.Border = Rectangle.BOX; //for testing
                headertable.SpacingAfter = 30;

                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.BOX;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("Data Cleansing", bodyFont));
                nextPostCell1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("9 Manbry Close", bodyFont));
                nextPostCell2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Deeside, CH5 4YL", bodyFont));
                nextPostCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("United Kingdom", bodyFont));
                nextPostCell4.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(nextPostCell4);
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Rowspan = 8;
                nesthousing.Padding = 0f;
                headertable.AddCell(nesthousing);

                headertable.AddCell("");
                PdfPCell invoiceCell = new PdfPCell(new Phrase("INVOICE", titleFont));
                invoiceCell.HorizontalAlignment = 4;
                invoiceCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(invoiceCell);
                PdfPCell noCell = new PdfPCell(new Phrase("No :", bodyFont));
                noCell.HorizontalAlignment = 2;
                noCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(noCell);
                headertable.AddCell(new Phrase(infoInvoice.InvoiceNo.ToString(), bodyFont));
                PdfPCell dateCell = new PdfPCell(new Phrase("Date :", bodyFont));
                dateCell.HorizontalAlignment = 2;
                dateCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(dateCell);
                headertable.AddCell(new Phrase(infoInvoice.Date.Value.ToShortDateString().ToString(), bodyFont));
                PdfPCell billCell = new PdfPCell(new Phrase("Bill To :", bodyFont));
                billCell.HorizontalAlignment = 2;
                billCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(billCell);
                headertable.AddCell(new Phrase(infoUser.FirstName + " " + infoUser.SurName + "\n" + infoCompany.Company + "\n" + infoCompany.Address + "\n" + infoCompany.Address2 + "\n" + infoCompany.City + "\n" + infoCompany.PostCode + "\n" + infoCompany.Country, bodyFont));
                PdfPCell compCell = new PdfPCell(new Phrase("Company No :", bodyFont));
                compCell.HorizontalAlignment = 2;
                compCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(compCell);
                headertable.AddCell(new Phrase(infoCompany.CompanyNumber.ToString(), bodyFont));
                pdfDoc.Add(headertable);

                //Create body table
                PdfPTable itemTable = new PdfPTable(4);
                itemTable.HorizontalAlignment = 0;
                itemTable.WidthPercentage = 100;
                itemTable.SetWidths(new float[] { 10, 40, 20, 30 });  // then set the column's __relative__ widths
                itemTable.SpacingAfter = 40;
                itemTable.DefaultCell.Border = Rectangle.BOX;
                PdfPCell cell1 = new PdfPCell(new Phrase("NO", boldTableFont));
                cell1.HorizontalAlignment = 1;
                itemTable.AddCell(cell1);
                PdfPCell cell2 = new PdfPCell(new Phrase("CREDITS PURCHASE", boldTableFont));
                cell2.HorizontalAlignment = 1;
                itemTable.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("PAYMENT TYPE", boldTableFont));
                cell3.HorizontalAlignment = 1;
                itemTable.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase("AMOUNT", boldTableFont));
                cell4.HorizontalAlignment = 1;
                itemTable.AddCell(cell4);

                foreach (DataRow row in table.Rows)
                {

                    PdfPCell numberCell = new PdfPCell(new Phrase(row["NO"].ToString(), bodyFont));
                    numberCell.HorizontalAlignment = 1;
                    numberCell.PaddingLeft = 10f;
                    numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    itemTable.AddCell(numberCell);

                    PdfPCell descCell = new PdfPCell(new Phrase(row["CREDITS PURCHASE"].ToString(), bodyFont));
                    descCell.HorizontalAlignment = 1;
                    descCell.PaddingLeft = 10f;
                    descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    itemTable.AddCell(descCell);

                    PdfPCell qtyCell = new PdfPCell(new Phrase(row["PAYMENT TYPE"].ToString(), bodyFont));
                    qtyCell.HorizontalAlignment = 1;
                    qtyCell.PaddingLeft = 10f;
                    qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    itemTable.AddCell(qtyCell);

                    PdfPCell amtCell = new PdfPCell(new Phrase(row["AMOUNT"].ToString(), bodyFont));
                    amtCell.HorizontalAlignment = 1;
                    amtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    itemTable.AddCell(amtCell);
                }

                // Table footer
                PdfPCell totalAmtCell1 = new PdfPCell(new Phrase(""));
                totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
                itemTable.AddCell(totalAmtCell1);
                PdfPCell totalAmtCell2 = new PdfPCell(new Phrase(""));
                totalAmtCell2.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
                itemTable.AddCell(totalAmtCell2);
                PdfPCell totalAmtStrCell = new PdfPCell(new Phrase("Total Amount", boldTableFont));
                totalAmtStrCell.Border = Rectangle.TOP_BORDER;   //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
                totalAmtStrCell.HorizontalAlignment = 1;
                itemTable.AddCell(totalAmtStrCell);
                PdfPCell totalAmtCell = new PdfPCell(new Phrase(totalInvoice, boldTableFont));
                totalAmtCell.HorizontalAlignment = 1;
                itemTable.AddCell(totalAmtCell);

                PdfPCell cell = new PdfPCell(new Phrase("*** Please note that all payments are made in British pound sterling ***", bodyFont));
                cell.Colspan = 4;
                cell.HorizontalAlignment = 1;
                itemTable.AddCell(cell);

                pdfDoc.Add(itemTable);

                PdfPTable footer = new PdfPTable(1);
                footer.PaddingTop = 1500f;
                PdfPCell foot = new PdfPCell(new Phrase("Please not that all credits are valid for a period of one calendar year unless new purchase is made!"));
                foot.HorizontalAlignment = Element.ALIGN_CENTER;
                foot.Border = Rectangle.NO_BORDER;
                PdfPCell foot2 = new PdfPCell(new Phrase("If you have any enquiries please don't hasitate to contact us."));
                foot2.HorizontalAlignment = Element.ALIGN_CENTER;
                foot2.Border = Rectangle.NO_BORDER;
                PdfPCell foot3 = new PdfPCell(new Phrase("Thank you for your business."));
                foot3.Border = Rectangle.NO_BORDER;
                foot3.HorizontalAlignment = Element.ALIGN_CENTER;
                footer.AddCell(foot);
                footer.AddCell(foot2);
                footer.AddCell(foot3);
                pdfDoc.Add(footer);

                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + infoInvoice.InvoiceNo + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}