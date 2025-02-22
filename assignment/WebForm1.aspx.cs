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
    public partial class WebForm1 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCheckbox();
                loadStatus();
                //loadGrid();
                if (Request.QueryString["id"] != null && Request.QueryString["command"] == "EditFile")
                {
                    string id = Request.QueryString["id"];
                    LoadEmployeeData(id);
                }

                if (Request.QueryString["id"] != null && Request.QueryString["command"] == "DeleteFile")
                {
                    string id = Request.QueryString["id"];
                    DeleteEmployeeData(id);
                }
            }
        }


     


        public void loadGrid()
        {

            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Employee";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            genderDropDownList1.DataSource = data;

            genderDropDownList1.DataBind();


        }


        private void LoadEmployeeData(string id)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT EmpName, LoginId, Contact, Email, Gender,Status Roles, Date_of_resignation FROM Employee WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) // Move to the first record
            {
                Nametextbox.Text = dr["EmpName"].ToString();
                logintextbox.Text = dr["LoginId"].ToString();
                contacttextbox.Text = dr["Contact"].ToString();
                genderDropDownList1.SelectedValue = dr["Gender"].ToString();
                emailtextbox.Text = dr["Email"].ToString();

          


                DorTextbox.Text = dr["Date_of_resignation"] != DBNull.Value
                    ? Convert.ToDateTime(dr["Date_of_resignation"]).ToString("yyyy-MM-dd")
                    : "";

                // Populate CheckBoxList with roles
                string[] selectedRoles = dr["Roles"].ToString().Split(',');
                foreach (ListItem item in roleCheckBoxList1.Items)
                {
                    if (selectedRoles.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }


        }
        private void DeleteEmployeeData(string id)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Delete FROM Employee WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();




        }


public void loadCheckbox()
{
    using (SqlConnection con = new SqlConnection(cs))
    {
        string query = "SELECT role_name FROM rolemaster";
        SqlDataAdapter sda = new SqlDataAdapter(query, con);
        DataTable datas = new DataTable();
        sda.Fill(datas);

        if (datas.Rows.Count > 0)
        {
                    roleCheckBoxList1.DataSource = datas;
                    roleCheckBoxList1.DataTextField = "role_name";  // Display text
                    roleCheckBoxList1.DataValueField = "role_name"; // Value stored
                    roleCheckBoxList1.DataBind();
        }
    }
    
    // Add a default "Select" option
    genderDropDownList1.Items.Insert(0, new ListItem("-- Select Gender --", ""));
}


        public void loadStatus()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT status FROM rolemaster";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable datas = new DataTable();
                sda.Fill(datas);

                if (datas.Rows.Count > 0)
                {
                    DropDownList2.DataSource = datas;
                    DropDownList2.DataTextField = "status";  // Display text
                    DropDownList2.DataValueField = "status"; // Value stored
                    DropDownList2.DataBind();
                }
            }

            // Add a default "Select" option
            DropDownList2.Items.Insert(0, new ListItem("-- Select--", ""));
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string command = Request.QueryString["command"];

            // Ensure ID is valid when updating
            int employeeId = 0;
            bool isEditMode = !string.IsNullOrEmpty(id) && command == "EditFile" && int.TryParse(id, out employeeId);

            // Get selected roles as a comma-separated string
            string roles = string.Join(",", roleCheckBoxList1.Items.Cast<ListItem>()
                                  .Where(i => i.Selected)
                                  .Select(i => i.Value).ToArray());

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query;
                if (isEditMode)
                {
                    query = "UPDATE Employee SET EmpName = @EmpName, LoginId = @LoginId, Contact = @Contact, " +
                            "Email = @Email,Status=@Status, Gender = @Gender, Roles = @Roles, Date_of_resignation = @Date_of_resignation " +
                            "WHERE Id = @Id";
                }
                else
                {
                    query = "INSERT INTO Employee (EmpName, LoginId, Contact, Email,Status, Gender, Roles, Date_of_resignation) " +
                            "VALUES (@EmpName, @LoginId, @Contact, @Email,@Status, @Gender, @Roles, @Date_of_resignation)";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add common parameters
                    cmd.Parameters.AddWithValue("@EmpName", Nametextbox.Text);
                    cmd.Parameters.AddWithValue("@LoginId", logintextbox.Text);
                    cmd.Parameters.AddWithValue("@Contact", contacttextbox.Text);
                    cmd.Parameters.AddWithValue("@Email", emailtextbox.Text);
                    cmd.Parameters.AddWithValue("@Gender", genderDropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@Roles", roles);
                    cmd.Parameters.AddWithValue("@Status", DropDownList2.SelectedValue);

                    // Attempt to parse the date from the textbox
                    if (DateTime.TryParse(DorTextbox.Text, out DateTime resignationDate))
                    {
                        // Valid date entered; add it as a parameter
                        cmd.Parameters.AddWithValue("@Date_of_resignation", resignationDate);
                    }
                    else
                    {
                        // No valid date entered; set the parameter to NULL
                        cmd.Parameters.AddWithValue("@Date_of_resignation", DBNull.Value);
                    }



                    // Add @Id parameter only if updating
                    if (isEditMode)
                    {
                        cmd.Parameters.AddWithValue("@Id", employeeId);
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Clear form fields
            Nametextbox.Text = "";
            logintextbox.Text = "";
            contacttextbox.Text = "";
            emailtextbox.Text = "";
            DorTextbox.Text = "";
            roleCheckBoxList1.ClearSelection();

            // Redirect
            Response.Redirect("WebForm2.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }

     

    }
}