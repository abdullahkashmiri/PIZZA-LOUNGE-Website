using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Diagnostics;

using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.Panel ConfirmationPanel;

        protected void Page_Load(object sender, EventArgs e)
        {
            int bit = 1;// for admin
            if (Session["status"] == null || Session["bit"].ToString() != bit.ToString())  // admin not logged in
            {
                Response.Redirect("../User/Login.aspx");
            }

            if (!IsPostBack)
            {
                // Fetch data from the database
                DataTable productData = FetchProductDataFromDatabase();

                // Bind the data to the ASP.NET controls
                RepeaterProducts.DataSource = productData;
                RepeaterProducts.DataBind();
            }
        }

        private DataTable FetchProductDataFromDatabase()
        {
            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT * FROM Products WHERE IsActive <> 3";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        protected void deleteproduct_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)deleteButton.NamingContainer;
            Label nameLabel = (Label)item.FindControl("NameLabel");
            string productName = nameLabel.Text;

            // Store the product name in a session variable
            Session["ProductName"] = productName;

            Debug.WriteLine("Product Name1: " + Session["ProductName"]);
            // Show the confirmation panel
            ConfirmationPanel.Visible = true;
        }

        protected void YesButton_Click(object sender, EventArgs e)
        {
            // Retrieve the product name from the session variable
            string productName = Session["ProductName"] as string;
            Debug.WriteLine("Product Name2: " + Session["ProductName"]);

            Debug.WriteLine("Product Name3: " + productName);
            if (!string.IsNullOrEmpty(productName))
            {
                string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "UPDATE Products SET IsActive = 3 WHERE Name = @ProductName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productName);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }

            // Hide the confirmation panel
            ConfirmationPanel.Visible = false;

            // Refresh the page to reflect the changes
            Response.Redirect(Request.Url.ToString());
        }

        protected void NoButton_Click(object sender, EventArgs e)
        {
            // Hide the confirmation panel
            ConfirmationPanel.Visible = false;
        }

        protected string GetRatingStars(double rating)
        {
            int fullStars = (int)rating;
            int halfStar = rating - fullStars >= 0.5 ? 1 : 0;
            int emptyStars = 5 - fullStars - halfStar;

            string starHtml = "";

            for (int i = 0; i < fullStars; i++)
            {
                starHtml += "<i class='fas fa-star'></i>";
            }

            if (halfStar == 1)
            {
                starHtml += "<i class='fas fa-star-half-alt'></i>";
            }

            for (int i = 0; i < emptyStars; i++)
            {
                starHtml += "<i class='far fa-star'></i>";
            }

            return starHtml;
        }
    }
}
