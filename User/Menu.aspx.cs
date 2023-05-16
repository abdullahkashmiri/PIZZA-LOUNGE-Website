using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;

namespace PIZZA_LOUNGE.User
{
    public partial class Menu : System.Web.UI.Page
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
            string query = "SELECT * FROM Products";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable productData = new DataTable();
                    adapter.Fill(productData);
                    return productData;
                }
            }
        }
        protected void addToCartButton_Click(object sender, EventArgs e)
        {
            Button addToCartButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)addToCartButton.NamingContainer;

            // Retrieve the data for the clicked item
            string name = ((Label)item.FindControl("NameLabel")).Text;
            string description = ((Label)item.FindControl("DescriptionLabel")).Text;
            string imageUrl = ((Image)item.FindControl("ImageLabel")).ImageUrl;
            string price = ((System.Web.UI.HtmlControls.HtmlGenericControl)item.FindControl("PriceLabel")).InnerHtml;

            // Store the data in session variables
            Session["CartName"] = name;
            Session["CartDescription"] = description;
            Session["CartImageUrl"] = imageUrl;
            Session["CartPrice"] = price;

            // Redirect to the Order page
            Response.Redirect("Order.aspx");
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