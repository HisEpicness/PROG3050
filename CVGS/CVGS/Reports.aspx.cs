using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CVGS
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           
        }

        protected void DownloadReport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable QueryList = MakeDataTable();
                String reportName = DdlReports.SelectedItem.Value;
                //ErrorMessage.Text = reportName;
                //String url = @"D:\report.pdf";
                ExportDataTableToPdf(QueryList, @"D:\" + reportName + ".pdf", reportName, this);
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

        private void ExportDataTableToPdf(DataTable queryList, String url, String v2, Control control)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=" + v2 + ".pdf");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

            //Table Data
            PdfPTable table = new PdfPTable(queryList.Columns.Count);
            for (int i = 0; i < queryList.Rows.Count; i++)
            {
                for (int j = 0; j < queryList.Columns.Count; j++)
                {
                    table.AddCell(queryList.Rows[i][j].ToString());
                }
            }

            document.Add(table);

            document.Close();

            Response.Write(document);

            Response.End();

            ////MemoryStream ms = new MemoryStream();
            //FileStream fs = new System.IO.FileStream(url, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
            //Document document = new Document();
            //document.SetPageSize(iTextSharp.text.PageSize.A4);
            //PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //document.Open();

            ////Report Header

            ////Table Data
            //PdfPTable table = new PdfPTable(queryList.Columns.Count);
            //for(int i=0; i<queryList.Rows.Count; i++)
            //{
            //    for (int j = 0; j< queryList.Columns.Count; j++)
            //    {
            //        table.AddCell(queryList.Rows[i][j].ToString());
            //    }
            //}          
        }

        private DataTable MakeDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            return table;
        }
    }
}