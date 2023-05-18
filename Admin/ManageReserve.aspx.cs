using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.Admin
{
    public partial class ManageReserve : System.Web.UI.Page
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
                BindReservationData();
            }
        }

        protected void BindReservationData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT ID, Day, Hour, Persons FROM Reservation WHERE status IS NULL";

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

                        rptReservations.DataSource = dt;
                        rptReservations.DataBind();
                    }
                }
            }
        }

        protected void rptReservations_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Decline")
            {
                int reservationId = Convert.ToInt32(e.CommandArgument);
                int status = e.CommandName == "Approve" ? 1 : 0; // Set the status based on the command name

                UpdateReservationStatus(reservationId, status);
                BindReservationData(); // Refresh the reservation data
            }
        }

        private void UpdateReservationStatus(int reservationId, int status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string updateQuery = "UPDATE Reservation SET status = @Status WHERE ID = @ReservationID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@ReservationID", reservationId);
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
