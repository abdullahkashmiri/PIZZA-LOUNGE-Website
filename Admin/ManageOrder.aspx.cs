
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.Admin
{
    public partial class ManageOrder : System.Web.UI.Page
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
                BindOrderData();
            }
        }

        protected void BindOrderData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT OrderNo, UserId, OrderDate, Price FROM Orders WHERE Status IS NULL";

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

                        rptOrders.DataSource = dt;
                        rptOrders.DataBind();
                    }
                }
            }
        }

        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Decline")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                int status = e.CommandName == "Approve" ? 1 : 0; // Set the status based on the command name

                UpdateOrderStatus(orderId, status);
                BindOrderData(); // Refresh the order data
            }
        }

        private void UpdateOrderStatus(int orderId, int status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string updateQuery = "UPDATE Orders SET Status = @Status WHERE OrderNo = @OrderNo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@OrderNo", orderId);
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
