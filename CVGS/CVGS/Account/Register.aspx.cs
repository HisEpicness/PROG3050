/*
 * Sign Up Use Case
 * Team SiX - Jankiben Joshi
 * 
 * Last Updated: 10/10/18
 * 
 */



using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CVGS.Models;
using System.Data.SqlClient;
using System.Data;

namespace CVGS.Account
{
    public partial class Register : Page
    {
        private static SqlDataReader dr;
        private static DataTable dt;

        protected void CreateUser_Click(object sender, EventArgs e)
        {
          
            String email = "";
            String uName = "";
            String password = "";
            String fName = "";
            String lName = "";
            Int16 age = 10;
            Boolean isEmployee = false;
            Boolean invalidUserName = false;

            SqlConnection con = new SqlConnection("Data Source=2A409-A03;Initial Catalog=CVGS;User ID=sa;Password=Conestoga1");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "SELECT [userName] FROM [user]";
            con.Open();

            try
            {
                dr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                con.Close();
            }
            catch (SqlException err)
            {
                Console.WriteLine(err.Message);
                con.Close();
            }


            if (Email.Text.Trim() != null || Email.Text.Trim() != "")
            {
                email = Email.Text.Trim();
            }
            if (userName.Text.Trim() != null || userName.Text.Trim() != "")
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (userName.Text.Trim() == row["userName"].ToString())
                    {
                        ErrorMessage.Text = "Invalid user name";
                        invalidUserName = true;
                    }
                }
                uName = userName.Text.Trim();
            }
            if (firstName.Text.Trim() != null || firstName.Text.Trim() != "")
            {
                fName = firstName.Text.Trim();
            }
            if (lastName.Text.Trim() != null || lastName.Text.Trim() != "")
            {
                lName = lastName.Text.Trim();
            }
            if (Password.Text.Trim() != null || Password.Text.Trim() != "")
            {
                password = Password.Text.Trim();
            }

            age = Convert.ToInt16(Age.Text.Trim());
            isEmployee = Employee.Checked;

            if (!invalidUserName)
            {
                String insert = "INSERT INTO [user] " +
                                 "([userName], [firstName], [lastName], [email], [mailAddress], [shipAddress], [age], [employee]) VALUES " +
                                 "('" + uName + "', " +
                                 "'" + fName + "', " +
                                 "'" + lName + "', " +
                                 "'" + email + "', " +
                                 "'" + null + "', " +
                                 "'" + null + "', " +
                                 "'" + age + "', " +
                                 "'" + isEmployee + "')";
                cmd.CommandText = insert;
                con.Open();

                try
                {
                    dr = cmd.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dr);

                    insert = "INSERT INTO [login] " +
                                     "([userName], [password]) VALUES " +
                                     "('" + uName + "', " +
                                     "'" + password + "')";
                    cmd.CommandText = insert;

                    try
                    {
                        cmd.CommandText = insert;
                        dr = cmd.ExecuteReader();
                        dt = new DataTable();
                        dt.Load(dr);
                    }
                    catch (SqlException err)
                    {
                        Console.WriteLine(err.Message);
                        con.Close();
                    }
                    con.Close();
                }
                catch (SqlException err)
                {
                    Console.WriteLine(err.Message);
                    con.Close();
                }

                userName.Text = "";
                firstName.Text = "";
                lastName.Text = "";
                Age.Text = "";
                Email.Text = "";

                Response.Write("<script>alert('User Added Successfully')</script>");
            }
            
        }
    }
}