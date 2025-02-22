using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace assignment
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadgrid();
                LoadDropDown();
            }


        }
        public void loadgrid()
        {

            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Employee";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            GridView1.DataSource = data;

            GridView1.DataBind();


        }


        protected void LoadDropDown()
        {
            if (!IsPostBack)
            {
                string CS = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("Select role_name from rolemaster", con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    DropDownList1.DataTextField = "role_name";
                    DropDownList1.DataValueField = "role_name";
                    DropDownList1.DataSource = rdr;
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("Select", ""));
                }
            }
        }



        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;
            if (command == "EditFile")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("WebForm1.aspx?id=" + id + "&command=" + e.CommandName);
            }

            if (command == "DeleteFile")
            {
                string id = e.CommandArgument.ToString();
                // Response.Redirect("WebForm1.aspx?id=" + id + "&command=" + e.CommandName);

                SqlConnection con = new SqlConnection(cs);
                string query = "Delete FROM Employee WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loadgrid();


            }
        }
        protected void Button1_Click2(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "SELECT * FROM Employee WHERE 1=1";
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (!string.IsNullOrEmpty(TextBox1.Text))
                    {
                        query += " AND EmpName LIKE @name";
                        cmd.Parameters.AddWithValue("@name", "%" + TextBox1.Text + "%");
                    }

                    if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
                    {
                        query += " AND roles LIKE @role";
                        cmd.Parameters.AddWithValue("@role", "%" + DropDownList1.SelectedValue + "%");
                    }

                    cmd.CommandText = query;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable data = new DataTable();
                    sda.Fill(data);

                    GridView1.DataSource = data;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx");

        }
    }
}