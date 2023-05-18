using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.Admin
{
    public partial class ManageJob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (!IsPostBack)
            {
                int bit = 1;// for admin
                if (Session["status"] == null || Session["bit"].ToString() != bit.ToString())  // admin not logged in
                {
                    Response.Redirect("../User/Login.aspx");
                }
                BindJobData();
            }
        }

        protected void BindJobData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT job_id, firstname, lastname, email, city, zip FROM job_form WHERE status IS NULL";

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

                        rptJobs.DataSource = dt;
                        rptJobs.DataBind();
                    }
                }
            }
        }

        protected void rptJobs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Decline")
            {
                int jobId = Convert.ToInt32(e.CommandArgument);
                int status = e.CommandName == "Approve" ? 1 : 0; // Set the status based on the command name

                UpdateJobStatus(jobId, status);
                BindJobData(); // Refresh the job data
            }
        }

        private void UpdateJobStatus(int jobId, int status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string updateQuery = "UPDATE job_form SET status = @Status WHERE job_id = @JobId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@JobId", jobId);
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
