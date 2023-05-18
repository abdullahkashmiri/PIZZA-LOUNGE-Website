using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.Admin
{
    public partial class ManageComplaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int bit = 1;// for admin
            if (Session["status"] == null || Session["bit"].ToString() != bit.ToString())  // admin not logged in
            {
                Response.Redirect("../User/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindComplaintData();
            }
        }

        protected void BindComplaintData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT complaint_id, username, email, message, submit_time FROM complaint_form WHERE status IS NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        rptComplaints.DataSource = dt;
                        rptComplaints.DataBind();
                    }
                }
            }
        }

        protected void rptComplaints_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Decline")
            {
                int complaintId = Convert.ToInt32(e.CommandArgument);
                int status = e.CommandName == "Approve" ? 1 : 0; // Set the status based on the command name

                UpdateComplaintStatus(complaintId, status);
                BindComplaintData(); // Refresh the complaint data
            }
        }

        private void UpdateComplaintStatus(int complaintId, int status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string updateQuery = "UPDATE complaint_form SET status = @Status WHERE complaint_id = @ComplaintId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@ComplaintId", complaintId);
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
