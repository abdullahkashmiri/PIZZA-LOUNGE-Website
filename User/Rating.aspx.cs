using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.User
{
    public partial class Rating : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ProductrateID"] = 1;

                // Load existing ratings on initial page load
                LoadExistingRatings();
            }
        }

        protected void LoadExistingRatings()
        {
            int productId=(int) Session["ProductrateID"];
            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL query to retrieve existing ratings for the specified product ID
                string query = "SELECT Name, Rating, Comment FROM Rating WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable ratingsTable = new DataTable();
                        adapter.Fill(ratingsTable);

                        // Bind the ratings data to the repeater control
                        ratingsRepeater.DataSource = ratingsTable;
                        ratingsRepeater.DataBind();
                    }
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Session["status"] == null) //user not logedin
            {
                Response.Redirect("./Login.aspx");
            }

            int userId = Convert.ToInt32(Session["user_id"]);

            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL query to check if the UserId exists in the Order table
                string query = "SELECT COUNT(*) FROM [Orders] WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    int count = (int)command.ExecuteScalar();

                    if (count <= 0)
                    {
                        return;
                    }
                }
            }

            string name = this.name.Text;
            int rating = Convert.ToInt32(this.rating.SelectedValue);
            string comment = this.comment.Text;
            int productId = (int)Session["ProductrateID"];
            // Connection string
            string connectionString1 = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection1 = new SqlConnection(connectionString1))
            {
                connection1.Open();

                // SQL query to insert a new rating into the table
                string query = "INSERT INTO Rating (Name, ProductID, ProductName, Rating, UserId, Comment) VALUES (@Name, @ProductID, @ProductName, @Rating, @UserId, @Comment)";

                using (SqlCommand command = new SqlCommand(query, connection1))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@ProductName", "Replace with actual product name"); // Replace with the actual product name
                    command.Parameters.AddWithValue("@Rating", rating);
                    command.Parameters.AddWithValue("@UserId", 1); // Replace with actual user ID
                    command.Parameters.AddWithValue("@Comment", comment);

                    command.ExecuteNonQuery();
                }
            }

            // Clear the input fields
            this.name.Text = "";
            this.rating.SelectedIndex = 0;
            this.comment.Text = "";

            // Reload the existing ratings after submitting a new rating
            LoadExistingRatings();

            // Display a success message
            message.InnerText = "Thank you for your rating!";
            Response.Redirect("./Default.aspx");
        }
    }
}
