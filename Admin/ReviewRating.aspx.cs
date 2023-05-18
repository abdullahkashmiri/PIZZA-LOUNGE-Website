using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.Admin
{
    public partial class ReviewRating : System.Web.UI.Page
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
                BindRatings();
            }
        }

        protected void BindRatings()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, Name, ProductName, Rating, Comment FROM Rating WHERE Status IS NULL";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                rptRatings.DataSource = dt;
                rptRatings.DataBind();
            }
        }

        protected void btnThanks_Click(object sender, EventArgs e)
        {
            Button btnThanks = (Button)sender;
            int ratingID = Convert.ToInt32(btnThanks.CommandArgument);

            // Update the status to 1
            UpdateRatingStatus(ratingID, 1);

            // Refresh the ratings
            BindRatings();
        }

        protected void btnApologize_Click(object sender, EventArgs e)
        {
            Button btnApologize = (Button)sender;
            int ratingID = Convert.ToInt32(btnApologize.CommandArgument);

            // Update the status to 0
            UpdateRatingStatus(ratingID, 0);

            // Refresh the ratings
            BindRatings();
        }

        protected void UpdateRatingStatus(int ratingID, int status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Rating SET Status = @Status WHERE ID = @RatingID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@RatingID", ratingID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
