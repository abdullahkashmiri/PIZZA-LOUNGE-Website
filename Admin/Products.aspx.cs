using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace PIZZA_LOUNGE.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;


                // Fetch data from the database
                DataTable productData = FetchProductDataFromDatabase(connString);

                // Bind the data to the ASP.NET controls
                RepeaterProducts.DataSource = productData;
                RepeaterProducts.DataBind();
            }
        }
     
        private DataTable FetchProductDataFromDatabase(string connectionString)
        {
            string query = "SELECT * FROM Products WHERE IsActive <> 2";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        protected void deleteproduct_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)deleteButton.NamingContainer;

            // Retrieve the data for the clicked item
            string productName = ((Label)item.FindControl("NameLabel")).Text;

            if (!string.IsNullOrEmpty(productName))
            {
                Session["ProductName1"] = productName;

                // Show the confirmation panel
                ConfirmationPanel.Visible = true;
            }
        }


        //protected void addToCartButton_Click(object sender, EventArgs e)
        //{
        //    Button addToCartButton = (Button)sender;
        //    RepeaterItem item = (RepeaterItem)addToCartButton.NamingContainer;

        //    // Retrieve the data for the clicked item
        //    string name = ((Label)item.FindControl("NameLabel")).Text;
        //    string description = ((Label)item.FindControl("DescriptionLabel")).Text;
        //    string imageUrl = ((Image)item.FindControl("ImageLabel")).ImageUrl;
        //    string price = ((System.Web.UI.HtmlControls.HtmlGenericControl)item.FindControl("PriceLabel")).InnerHtml;

        //    // Store the data in session variables
        //    Session["CartName"] = name;
        //    Session["CartDescription"] = description;
        //    Session["CartImageUrl"] = imageUrl;
        //    Session["CartPrice"] = price;

        //    // Redirect to the Order page
        //    Response.Redirect("Order.aspx");
        //}

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
        //protected void YesButton_Click(object sender, EventArgs e)
        //{
        //    // Retrieve the product name from ViewState
        //    string productName = ViewState["ProductName"] as string;

        //    if (!string.IsNullOrEmpty(productName))
        //    {
        //        // Perform further processing or delete the product from the database using the product name
        //        string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //        using (SqlConnection connection = new SqlConnection(connString))
        //        {
        //            connection.Open();

        //            // Update the IsActive column to 2 for the specified product name
        //            string updateQuery = "UPDATE Products SET IsActive = @IsActive WHERE Name = @ProductName";

        //            using (SqlCommand command = new SqlCommand(updateQuery, connection))
        //            {
        //                command.Parameters.AddWithValue("@IsActive", 2);
        //                command.Parameters.AddWithValue("@ProductName", productName);
        //                int rowsAffected = command.ExecuteNonQuery();

        //                if (rowsAffected > 0)
        //                {
        //                    // Update successful
        //                    // Additional processing or display success message
        //                }
        //                else
        //                {
        //                    // Update failed
        //                    // Additional processing or display error message
        //                }
        //            }
        //        }
        //    }

        //    // Clear the ViewState after use if needed
        //    ViewState["ProductName"] = null;
        //}

        protected void YesButton_Click(object sender, EventArgs e)
        {
            // Retrieve the product name from Session
            string productName = Session["ProductName1"].ToString();

            if (!string.IsNullOrEmpty(productName))
            {
                // Perform further processing or delete the product from the database using the product name
                string connString1 = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connString1))
                {
                    connection.Open();

                    // Update the IsActive column to 2 for the specified product name
                    string updateQuery = "UPDATE Products SET IsActive = @IsActive WHERE Name = @ProductName";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@IsActive", 2);
                        command.Parameters.AddWithValue("@ProductName", productName);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update successful
                            // Additional processing or display success message
                        }
                        else
                        {
                            // Update failed
                            // Additional processing or display error message
                        }
                    }
                }
            }

            // Clear the Session variable after use if needed
            Session["ProductName1"] = null;

            // Refresh the data and bind it to the Repeater control
            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            DataTable productData = FetchProductDataFromDatabase(connString);
            RepeaterProducts.DataSource = productData;
            RepeaterProducts.DataBind();
        }




        protected void NoButton_Click(object sender, EventArgs e)
        {
            // This code will be executed when the "No" button is clicked in the confirmation dialog
            Response.Redirect("Products.aspx");

        }

    }
}