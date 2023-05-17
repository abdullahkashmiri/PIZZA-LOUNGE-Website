using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PIZZA_LOUNGE.Admin
{
    public partial class ManageOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        rptOrders.DataSource = reader;
                        rptOrders.DataBind();
                    }

                    reader.Close();
                }
            }
        }


        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32((sender as Button).CommandArgument);
            UpdateOrderStatus(orderId, 1);
            BindOrderData(); // Refresh the order data
        }

        protected void DeclineButton_Click(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32((sender as Button).CommandArgument);
            UpdateOrderStatus(orderId, 0);
            BindOrderData(); // Refresh the order data
        }

        private void UpdateOrderStatus(int orderId, int status)
        {
            // Update the status in the database
            string connectionString1 = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string updateQuery = "UPDATE Orders SET Status = @Status WHERE OrderNo = @OrderNo";

            using (SqlConnection connection1 = new SqlConnection(connectionString1))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection1))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@OrderNo", orderId);
                    if (connection1.State == ConnectionState.Closed)
                    {
                        connection1.Open();
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
