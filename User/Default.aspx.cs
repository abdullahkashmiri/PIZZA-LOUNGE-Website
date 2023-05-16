using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.User
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                // Fetch data from the database
                DataTable productData = FetchProductDataFromDatabase(connString);

                // Filter the data where IsActive bit is 1
                DataView filteredDataView = new DataView(productData);
                filteredDataView.RowFilter = "IsActive = 1";

                // Bind the filtered data to the Repeater control
                RepeaterSpecialities.DataSource = filteredDataView;
                RepeaterSpecialities.DataBind();
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

        public string GetRatingStars(double rating)
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
        protected void OrderButton_Click(object sender, EventArgs e)
        {
            Button orderButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)orderButton.NamingContainer;

            // Retrieve the data for the clicked item
            string name = ((Label)item.FindControl("NameLabel")).Text;
            string price = ((System.Web.UI.HtmlControls.HtmlGenericControl)item.FindControl("PriceLabel")).InnerHtml;
            string imageUrl = ((Image)item.FindControl("ImageLabel")).ImageUrl;
            string description = ((Label)item.FindControl("DescriptionLabel")).Text;
            // Store the data in session variables
            Session["CartName"] = name;
            Session["CartPrice"] = price;
            Session["CartImageUrl"] = imageUrl;
            Session["CartDescription"] = description;

            // Redirect to the Order page (Page B)
            Response.Redirect("Order.aspx");
        }
    }
}