using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Custom : System.Web.UI.Page
    {
        SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the prices for pizza sizes
            Session["personalPrice"] = 200;
            Session["mediumPrice"] = 400;
            Session["largePrice"] = 600;

            // Set the prices for pizza crusts
            Session["thinCrustPrice"] = 50;
            Session["thickCrustPrice"] = 75;
            Session["handTossedCrustPrice"] = 100;
            Session["panCrustPrice"] = 125;
            Session["glutenFreeCrustPrice"] = 150;

            // Set the prices for pizza sauces
            Session["tomatoSaucePrice"] = 25;
            Session["garlicSaucePrice"] = 30;
            Session["barbecueSaucePrice"] = 35;
            Session["pestoSaucePrice"] = 40;
            Session["oliveOilPrice"] = 45;

            // Set the prices for cheese quantities
            Session["lightCheesePrice"] = 100;
            Session["regularCheesePrice"] = 150;
            Session["extraCheesePrice"] = 200;

            // Set the prices for toppings
            Session["pepperoniPrice"] = 25;
            Session["sausagePrice"] = 25;
            Session["mushroomsPrice"] = 25;
            Session["onionsPrice"] = 25;
            Session["bellPeppersPrice"] = 25;
            Session["olivesPrice"] = 25;
            Session["tomatoesPrice"] = 25;
            Session["spinachPrice"] = 25;
            Session["jalapenosPrice"] = 25;
            Session["extraCheeseToppingPrice"] = 25;

        }
        protected void btnProceed_Click(object sender, EventArgs e)
        {
            // Validate the first container
            if (!IsCategorySelected(size_personal, size_medium, size_large) ||
                !IsCategorySelected(crust_thin, crust_thick, crust_hand_tossed, crust_pan, crust_gluten_free) ||
                !IsCategorySelected(sauce_tomato, sauce_garlic, sauce_barbecue, sauce_pesto, sauce_olive_oil) ||
                !IsCategorySelected(cheese_light, cheese_regular, cheese_extra))
            {
                // At least one category in the first container is not selected
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please fill in all categories.";
                return;
            }
            lblErrorMessage.Visible = false;
            // Get the selected values from the first container
            string size = GetSelectedRadioButtonText(size_personal, size_medium, size_large);
            string crust = GetSelectedRadioButtonText(crust_thin, crust_thick, crust_hand_tossed, crust_pan, crust_gluten_free);
            string sauce = GetSelectedRadioButtonText(sauce_tomato, sauce_garlic, sauce_barbecue, sauce_pesto, sauce_olive_oil);
            string cheeseQuantity = GetSelectedRadioButtonText(cheese_light, cheese_regular, cheese_extra);
            string toppings = GetSelectedCheckBoxValues(
                topping_pepperoni, topping_sausage, topping_mushrooms, topping_onions,
                topping_bell_peppers, topping_olives, topping_tomatoes, topping_spinach,
                topping_jalapenos, topping_extra_cheese
            );
            // Calculate the price based on the selected options (using session prices)
            int price = 0;

            // Calculate the price for the selected size
            if (size_personal.Checked)

            {
                price += (int)Session["personalPrice"];
                Session["PizzaSize"] = 1;
            }
            else if (size_medium.Checked)
            {
                price += (int)Session["mediumPrice"];
                Session["PizzaSize"] = 2;
            }
            else if (size_large.Checked)
            {
                price += (int)Session["largePrice"];
                Session["PizzaSize"] = 3;
            }
            // Calculate the price for the selected crust
            if (crust_thin.Checked)
                price += (int)Session["thinCrustPrice"];
            else if (crust_thick.Checked)
                price += (int)Session["thickCrustPrice"];
            else if (crust_hand_tossed.Checked)
                price += (int)Session["handTossedCrustPrice"];
            else if (crust_pan.Checked)
                price += (int)Session["panCrustPrice"];
            else if (crust_gluten_free.Checked)
                price += (int)Session["glutenFreeCrustPrice"];

            // Calculate the price for the selected sauce
            if (sauce_tomato.Checked)
                price += (int)Session["tomatoSaucePrice"];
            else if (sauce_garlic.Checked)
                price += (int)Session["garlicSaucePrice"];
            else if (sauce_barbecue.Checked)
                price += (int)Session["barbecueSaucePrice"];
            else if (sauce_pesto.Checked)
                price += (int)Session["pestoSaucePrice"];
            else if (sauce_olive_oil.Checked)
                price += (int)Session["oliveOilSaucePrice"];

            // Calculate the price for the selected toppings
            string[] selectedToppings = toppings.Split(',');
            int toppingPrice = (int)Session["pepperoniPrice"]; // Default topping price
            foreach (string topping in selectedToppings)
            {
                if (topping == "sausage")
                    toppingPrice = (int)Session["sausagePrice"];
                else if (topping == "mushrooms")
                    toppingPrice = (int)Session["mushroomsPrice"];
                else if (topping == "onions")
                    toppingPrice = (int)Session["onionsPrice"];
                else if (topping == "bell-peppers")
                    toppingPrice = (int)Session["bellPeppersPrice"];
                else if (topping == "olives")
                    toppingPrice = (int)Session["olivesPrice"];
                else if (topping == "tomatoes")
                    toppingPrice = (int)Session["tomatoesPrice"];
                else if (topping == "spinach")
                    toppingPrice = (int)Session["spinachPrice"];
                else if (topping == "jalapenos")
                    toppingPrice = (int)Session["jalapenosPrice"];
                else if (topping == "extra-cheese")
                    toppingPrice = (int)Session["extraCheeseToppingPrice"];

                price += toppingPrice;
            }

            // Save the price in a session variable
            Session["PizzaPrice"] = price;
            btnCalculate();

            // Save the selected options to the database
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Custom_Order (Size, Crust, Sauce, CheeseQuantity, Toppings, Amount) VALUES (@Size, @Crust, @Sauce, @CheeseQuantity, @Toppings, @Amount)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Size", size);
                    command.Parameters.AddWithValue("@Crust", crust);
                    command.Parameters.AddWithValue("@Sauce", sauce);
                    command.Parameters.AddWithValue("@CheeseQuantity", cheeseQuantity);
                    command.Parameters.AddWithValue("@Toppings", toppings);
                    command.Parameters.AddWithValue("@Amount", price);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    command.ExecuteNonQuery();
                }
            }


            // Proceed with the selection

            // Display the selected options in the labels on the page
            lblSelectedSize.Text = size;
            lblSelectedCrust.Text = crust;
            lblSelectedSauce.Text = sauce;
            lblSelectedCheese.Text = cheeseQuantity;
            lblSelectedToppings.Text = toppings;

            // Show the selection container and other relevant elements

            // Show the inv_container panel
            invContainerPanel.Visible = true;

        }

        private bool IsCategorySelected(params RadioButton[] radioButtons)
        {
            return radioButtons.Any(rb => rb.Checked);
        }

        private bool IsAtLeastOneToppingSelected()
        {
            return GetSelectedCheckBoxValues(
                topping_pepperoni, topping_sausage, topping_mushrooms, topping_onions,
                topping_bell_peppers, topping_olives, topping_tomatoes, topping_spinach,
                topping_jalapenos, topping_extra_cheese
            ).Split(',').Any(topping => !string.IsNullOrWhiteSpace(topping.Trim()));
        }
        private string GetSelectedRadioButtonText(params RadioButton[] radioButtons)
        {
            foreach (RadioButton radioButton in radioButtons)
            {
                if (radioButton.Checked)
                {
                    return radioButton.Text;
                }
            }
            return string.Empty;
        }

        private string GetSelectedCheckBoxValues(params CheckBox[] checkBoxes)
        {
            List<string> selectedValues = new List<string>();
            foreach (CheckBox checkBox in checkBoxes)
            {
                if (checkBox.Checked)
                {
                    selectedValues.Add(checkBox.Text);
                }
            }
            return string.Join(", ", selectedValues);
        }
        protected void btnPlaceOrder_Click(object sender, EventArgs e)
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

            // Access the logged-in user ID from the session
            int loggedinUserId = (int)Session["user_id"];
            int s = (int)Session["CustomOrderNo"];
            Session["CustomOrderNo"] = s + 1;
            int userId = loggedinUserId; // User ID
            int quantity = 1;
            int productId = GetProductIdFromCustomOrderTable(); // Replace this with the actual logic to get the product ID from the custom_order table
            int price = (int)Session["PizzaPrice"];
            int orderNo = (int)Session["CustomOrderNo"];
            int size = (int)Session["PizzaSize"];
            int cOrder = 1; // Set cOrder to 1 (custom order)

            // Save the cart details to the database
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Carts (ProductId, Quantity, UserId, Price, OrderNo, Size, C_Order) " +
                               "VALUES (@ProductId, @Quantity, @UserId, @Price, @OrderNo, @Size, @C_Order)";
                productId = 1;
                price = (int)Session["PizzaPrice"];
                size = (int)Session["PizzaSize"];
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@OrderNo", orderNo);
                    command.Parameters.AddWithValue("@Size", size);
                    command.Parameters.AddWithValue("@C_Order", cOrder);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    command.ExecuteNonQuery();
                }
            }

            // Redirect to Default.aspx
            Response.Redirect("Default.aspx");
        }
        private int GetProductIdFromCustomOrderTable()
        {
            int productId = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 OrderID FROM Custom_Order ORDER BY OrderID DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        productId = Convert.ToInt32(result);
                    }
                }
            }
            return productId;
        }
        private int GetAmountFromCustomOrderTable()
        {
            int amount = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 Amount FROM Custom_Order ORDER BY OrderID DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        amount = Convert.ToInt32(result);
                    }
                }
            }
            return amount;
        }
        private void btnCalculate()
        {
            // Set the text of the label to display the price
            lblAmount.Text = "Total Amount: RS " + Session["PizzaPrice"].ToString();
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