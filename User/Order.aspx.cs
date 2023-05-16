using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.User
{
    public partial class Order : System.Web.UI.Page
    {
        string n = "Spaghetti Bolognese";
        SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (Session["CartName"] != null)
                {
                    string name = Session["CartName"].ToString();
                    string description = Session["CartDescription"].ToString();
                    string imageUrl = Session["CartImageUrl"].ToString();
                    string price = Session["CartPrice"].ToString();

                    nameLabel.Text = name;
                    descriptionLabel.Text = description;
                    priceLabel.Text = price;
                    Img1.Attributes["src"] = imageUrl;
                    n = name;

                }
                else
                {
                    string name = null;
                    string description = "Please Select Any Product";
                    string imageUrl = "../TemplateFiles/menuimages/banner/img111.png";
                    string price = null;

                    nameLabel.Text = name;
                    descriptionLabel.Text = description;
                    priceLabel.Text = price;
                    Img1.Attributes["src"] = imageUrl;
                }
            }
        }
        protected void placeOrderButton_Click(object sender, EventArgs e)
        {
            if (Session["status"] == null) //user not logedin
            {
                Response.Redirect("./Login.aspx");
            }

            else //user logedin
            {
                try
                {
                    if (connec.State == ConnectionState.Closed)
                    {
                        connec.Open();
                    }

                    SqlCommand cmd = new SqlCommand("Select ProductId From Products Where Name = '" + Session["CartName"] + "'", connec);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    sdr.Read();
                    int prod_id = Convert.ToInt32(sdr.GetValue(0));

                    if (!AlredyAdded(prod_id)) //else inc quantity by one
                    {
                        //add new cart item
                        cmd.Dispose();

                        cmd = new SqlCommand("cart_manage", connec)
                        {
                            CommandType = System.Data.CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@Action", "INSERT");
                        cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
                        cmd.Parameters.AddWithValue("@Prod_id", prod_id);
                        cmd.Parameters.AddWithValue("@Quantity", 1);
                        cmd.ExecuteNonQuery();
                    }

                    connec.Close();
                    Response.Redirect("Cart.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("" + ex.Message + "');</script>");
                }

            }
            // Check if a size is selected
            if (!small.Checked && !medium.Checked && !large.Checked)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please fill in all categories.";
                return;
            }
            // Calculate the price based on the selected radio button and toppings
            int basePrice = GetPriceFromProductsTable(n);
            int size = 0;
            if (small.Checked)
            {
                basePrice += 100; // Update with the actual price for small size
                size = 1;
            }
            else if (medium.Checked)
            {
                basePrice += 200; // Update with the actual price for medium size
                size = 2;
            }
            else if (large.Checked)
            {
                basePrice += 300; // Update with the actual price for large size
                size = 3;
            }
            // Calculate the price based on the selected toppings
            if (pepperoni.Checked)
            {
                basePrice += 50; // Update with the actual price for pepperoni topping
            }

            if (onions.Checked)
            {
                basePrice += 30; // Update with the actual price for onions topping
            }

            if (mushrooms.Checked)
            {
                basePrice += 70; // Update with the actual price for mushrooms topping
            }

            if (cheese.Checked)
            {
                basePrice += 100; // Update with the actual price for cheese topping
            }
            // Calculate the price based on the selected drink
            string drink = drinks.SelectedItem.Value;
            int drinkPrice = 0;

            switch (drink)
            {
                case "coke":
                    drinkPrice = 80; // Update with the actual price for Coke
                    break;
                case "pepsi":
                    drinkPrice = 70; // Update with the actual price for Pepsi
                    break;
                case "sprite":
                    drinkPrice = 80; // Update with the actual price for Sprite
                    break;
            }

            // Calculate the price based on the selected fries
            string fries = Fries.SelectedItem.Value;
            int friesPrice = 0;

            switch (fries)
            {
                case "small":
                    friesPrice = 180; // Update with the actual price for small fries
                    break;
                case "medium":
                    friesPrice = 260; // Update with the actual price for medium fries
                    break;
                case "large":
                    friesPrice = 340; // Update with the actual price for large fries
                    break;
            }
            int s = (int)Session["OrderNo"];
            Session["OrderNo"] = s + 1;
            int orderNo = s + 1; // Get the order number from the session

            int totalPrice = basePrice + drinkPrice + friesPrice;

            int userId = (int)Session["user_id"];
            int quantity = 1;
            int productId = GetProductIdFromProductsTable(Session["CartName"].ToString());
            int paymentId = totalPrice;
            int cOrder = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Carts (ProductId, Quantity, UserId, Price, OrderNo, Size, C_Order) " +
                               "VALUES (@ProductId, @Quantity, @UserId, @Price, @OrderNo, @Size, @C_Order)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Price", paymentId);
                    command.Parameters.AddWithValue("@OrderNo", orderNo);
                    command.Parameters.AddWithValue("@Size", size);
                    command.Parameters.AddWithValue("@C_Order", cOrder);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }


            // Redirect to a confirmation page or display a success message
            Response.Redirect("Default.aspx");
        }
        public int GetProductIdFromProductsTable(string productName)
        {
            int productId = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductId FROM Products WHERE Name = @ProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);

                    connection.Open();

                    // Execute the query and retrieve the product ID
                    object result = command.ExecuteScalar();

                    // Check if the result is not null and can be converted to an integer
                    if (result != null && int.TryParse(result.ToString(), out int parsedProductId))
                    {
                        productId = parsedProductId;
                    }
                }
            }

            return productId;
        }
        // Method to retrieve the price from the Products table based on the name
        private int GetPriceFromProductsTable(string productName)
        {
            int price = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Price FROM Products WHERE Name = @ProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);

                    connection.Open();

                    // Execute the query and retrieve the price
                    object result = command.ExecuteScalar();

                    // Check if the result is not null and can be converted to an integer
                    if (result != null && int.TryParse(result.ToString(), out int parsedPrice))
                    {
                        price = parsedPrice;
                    }
                }
            }
            return price;
        }
        bool AlredyAdded(int prod_id)
        {
            SqlCommand cmd = new SqlCommand("cart_manage", connec)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
            cmd.Parameters.AddWithValue("@Prod_id", prod_id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                return false;
            }

            cmd.Dispose();

            cmd = new SqlCommand("cart_manage", connec)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Action", "INC");
            cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
            cmd.Parameters.AddWithValue("@Prod_id", prod_id);

            cmd.ExecuteNonQuery();

            return true;
        }
    }
}