using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CVGS.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace CVGS
{
    public partial class Reports : System.Web.UI.Page
    {
        private static SqlDataReader dr;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DownloadReport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable QueryList = getTable(DdlReports.SelectedIndex);
                String reportName = DdlReports.SelectedItem.Value;
                ExportDataTableToPdf(QueryList, @"D:\" + reportName + ".pdf", reportName, this);
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.InnerException.ToString();
            }
        }

        private DataTable getTable(int selectedIndex)
        {
            DataTable dt = new DataTable();

            if (selectedIndex == 1)
            {
                IList<game> games = null;
                using (var context = new CVGSEntities())
                {
                    games = context.games.Select(s => new game()
                    {
                        name = s.name
                    }).ToList<game>();
                }
                dt.Columns.Add("Name", typeof(string));
                foreach (game row in games)
                {
                    dt.Rows.Add(row.name.ToString());
                }
            }
            else if (selectedIndex == 2)
            {
                IList<game> games = null;
                using (var context = new CVGSEntities())
                {
                    games = context.games.Select(s => new game()
                    {
                        name = s.name,
                        description = s.description,
                        publisher = s.publisher,
                        publishDate = s.publishDate,
                        genre = s.genre,
                        rating = s.rating,
                        price = s.price
                    }).ToList<game>();
                }
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Publisher", typeof(string));
                dt.Columns.Add("PublishDate", typeof(DateTime));
                dt.Columns.Add("Genre", typeof(string));
                dt.Columns.Add("Rating", typeof(string));
                dt.Columns.Add("Price", typeof(Decimal));

                foreach (game row in games)
                {
                    dt.Rows.Add(row.name.ToString(), row.description.ToString(), row.publisher.ToString(),
                        row.publishDate.ToString(), row.genre.ToString(), row.rating.ToString(), row.price.ToString());
                }
            }
            // Member List
            else if (selectedIndex == 3)
            {
                IList<user> users = null;
                using (var context = new CVGSEntities())
                {
                    users = context.users.Select(s => new user()
                    {
                        username = s.firstName + ' ' + s.lastname
                    }).ToList<user>();
                }
                dt.Columns.Add("Name", typeof(string));
                foreach (user user in users)
                {
                    dt.Rows.Add(user.username.ToString());
                }
            }
            // Member Detail
            else if (selectedIndex == 4)
            {
                IList<user> users = null;
                using (var context = new CVGSEntities())
                {
                    users = context.users.Select(s => new user()
                    {
                        username = s.username,
                        firstName = s.firstName,
                        lastname = s.lastname,
                        email = s.email,
                        age = s.age
                    }).ToList<user>();
                }

                dt.Columns.Add("username", typeof(string));
                dt.Columns.Add("firstName", typeof(string));
                dt.Columns.Add("lastname", typeof(string));
                dt.Columns.Add("email", typeof(DateTime));
                dt.Columns.Add("age", typeof(int));

                foreach (user row in users)
                {
                    dt.Rows.Add(row.username.ToString(), row.firstName.ToString(), row.lastname.ToString(),
                        row.email.ToString(), row.age.ToString());
                }
            }
            // wishList
            //else if (selectedIndex == 5)
            //{
            //    IList<wishList> wish = null;
            //    using (var context = new CVGSEntities())
            //    {
            //        wish = context.wishLists.Select(s => new wishList()
            //        {
            //            name = s.name,
            //            // need to fecth genre name
            //        }).ToList<wishList>();
            //    }

            //    dt.Columns.Add("name", typeof(string));

            //    foreach (wishList row in wish)
            //    {
            //        dt.Rows.Add(row.name.ToString(), row.description.ToString(), row.publisher.ToString(),
            //            row.publishDate.ToString(), row.genre.ToString(), row.rating.ToString(), row.price.ToString());
            //    }
            //}
            // Sales Report
            else if (selectedIndex == 6)
            {
                IList<order> sales = null;
                using (var context = new CVGSEntities())
                {
                    sales = context.orders.Select(s => new order()
                    {
                        username = s.username,
                        orderDate = s.orderDate,
                        shipDate = s.shipDate,
                        gameId = s.gameId, // game name
                    }).ToList<order>();
                }

                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Order Date", typeof(DateTime));
                dt.Columns.Add("Ship Date", typeof(DateTime));

                foreach (order row in sales)
                {
                    dt.Rows.Add(row.username.ToString(), row.orderDate.ToString(), row.shipDate.ToString());
                }
            }
            return dt;
        }

        private void ExportDataTableToPdf(DataTable queryList, String url, String v2, Control control)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=" + v2 + ".pdf");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            iTextSharp.text.Document document = new iTextSharp.text.Document();
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
        }

        //dummy data Delete this if everything works fine
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
