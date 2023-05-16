using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace PIZZA_LOUNGE.User
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["status"] != null)
            {
                if (!IsPostBack)
                {
                    Refresh_Screen();
                }
            }
            else
            {
                Response.Redirect("LogIn.aspx");
            }
        }

        void Refresh_Screen()
        {
            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            // Fetch data from the database
            DataTable productData = FetchProductDataFromDatabase(connString);

            // Bind the data to the ASP.NET controls
            Repeatercart.DataSource = productData;
            Repeatercart.DataBind();

            SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            if (connec.State == ConnectionState.Closed)
            {
                connec.Open();
            }

            SqlCommand cmd = new SqlCommand("cart_manage", connec)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Action", "PRICE");
            cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));

            SqlDataReader sdr = cmd.ExecuteReader();
            cmd.Dispose();

            if (sdr.HasRows)
            {
                sdr.Read();

                cmd = new SqlCommand("discount_order_manage", connec)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Action", "DISCOUNT");
                cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
                SqlDataReader dr = cmd.ExecuteReader();
                string price = sdr.GetValue(0).ToString();
                int disc_price = Convert.ToInt32(price);

                if (dr.HasRows && disc_price >= 500) //if Discount can be applied
                {
                    dr.Read();
                    Decimal disc = Convert.ToDecimal(dr.GetValue(0));
                    disc_price = Convert.ToInt32(disc_price - disc_price * disc);

                    dis_notify.Visible = true;
                    dis_notify.Text = "" + (Convert.ToInt32(disc * 100)).ToString() + "% Discount Applied";

                    t_price.Text = "Total Price: " + disc_price.ToString() + " Rs";
                }

                else
                {
                    dis_notify.Visible = false;
                    t_price.Text = "Total Price: " + price + " Rs";
                }
            }

            else //if cart is empty
            {
                dis_notify.Visible = false;
                t_price.Text = "Total Price: 0 Rs";
            }

            connec.Close();
        }

        private DataTable FetchProductDataFromDatabase(string connectionString)
        {
            string query = "SELECT CASE When c.C_Order = 1  Then -1 Else c.ProductId END as ProductId, ";
            query += "CASE WHEN c.C_Order = 1 THEN 'Custom Order' ELSE p.Name END AS Name, ";
            query += "c.Price, ";
            query += "CASE WHEN c.C_Order = 1 THEN '..//TemplateFiles//menuimages//iimg4.jpg' ELSE p.ImageUrl END AS ImageUrl, ";
            query += "c.Quantity, ";
            query += "Case When Size = 1 Then 'Size: Small' ";
            query += "     When Size = 2 Then 'Size: Medium' ";
            query += "     When Size = 3 Then 'Size: Large' ";
            query += "END as Size ";
            query += "FROM Carts c ";
            query += "INNER JOIN dbo.Products p ON (c.ProductId = p.ProductId) ";
            query += "WHERE c.UserId = @UserId";
            //string query = "SELECT CASE WHEN c.C_Order = 1 THEN -1 ELSE c.ProductId END as ProductId, ";
            //query += "CASE WHEN c.C_Order = 1 THEN 'Custom Order' ELSE p.Name END AS Name, ";
            //query += "c.Price, ";
            //query += "CASE WHEN c.C_Order = 1 THEN '..//TemplateFiles//menuimages//iimg4.jpg' ELSE p.ImageUrl END AS ImageUrl, ";
            //query += "c.Quantity, ";
            //query += "CASE WHEN Size = 1 THEN 'Size: Small' ";
            //query += "     WHEN Size = 2 THEN 'Size: Medium' ";
            //query += "     WHEN Size = 3 THEN 'Size: Large' ";
            //query += "END as Size ";
            //query += "FROM Carts c ";
            //query += "INNER JOIN dbo.Products p ON (c.ProductId = p.ProductId) ";
            //query += "WHERE c.UserId = @UserId AND c.CartId = (SELECT MAX(CartId) FROM Carts WHERE UserId = @UserId)";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["user_id"]));

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable productData = new DataTable();
                    adapter.Fill(productData);
                    return productData;
                }
            }
        }
       
        protected void add_Button_Click(object sender, EventArgs e)
        {
            Button add = (Button)sender;
            RepeaterItem item = (RepeaterItem)add.NamingContainer;

            string p_id = ((Label)item.FindControl("product_id")).Text;
            int prod_id = Convert.ToInt32(p_id);

            if (prod_id != -1)
            {
                SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
                if (connec.State == ConnectionState.Closed)
                {
                    connec.Open();
                }

                SqlCommand cmd = new SqlCommand("cart_manage", connec) //executing Procedure
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Action", "INC");
                cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@Prod_id", prod_id);

                cmd.ExecuteNonQuery();

                connec.Close();
                Refresh_Screen();
            }
        }

        protected void sub_Button_Click(object sender, EventArgs e)
        {
            Button sub = (Button)sender;
            RepeaterItem item = (RepeaterItem)sub.NamingContainer;

            string p_id = ((Label)item.FindControl("product_id")).Text;
            int prod_id = Convert.ToInt32(p_id);

            if (prod_id != -1) //-1 product for custom order
            {
                SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
                if (connec.State == ConnectionState.Closed)
                {
                    connec.Open();
                }


                SqlCommand cmd = new SqlCommand("cart_manage", connec)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Action", "DEC");
                cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@Prod_id", prod_id);

                cmd.ExecuteNonQuery();

                connec.Close();
                Refresh_Screen();
            }
        }

        protected void rem_Button_Click(object sender, EventArgs e)
        {
            Button sub = (Button)sender;
            RepeaterItem item = (RepeaterItem)sub.NamingContainer;
            string p_id = ((Label)item.FindControl("product_id")).Text;
            int prod_id = Convert.ToInt32(p_id);

            if (prod_id != -1)
            {
                SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
                if (connec.State == ConnectionState.Closed)
                {
                    connec.Open();
                }

                SqlCommand cmd = new SqlCommand("cart_manage", connec)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@Prod_id", prod_id);

                cmd.ExecuteNonQuery();

                connec.Close();
                Refresh_Screen();
            }
        }

        protected void ord_Button_Click(object sender, EventArgs e)
        {
            SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            if (connec.State == ConnectionState.Closed)
            {
                connec.Open();
            }

            SqlCommand cmd = new SqlCommand("cart_manage", connec)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Action", "PRICE");
            cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                string price = sdr.GetValue(0).ToString();

                cmd = new SqlCommand("discount_order_manage", connec)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Action", "DISCOUNT");
                cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
                SqlDataReader dr = cmd.ExecuteReader();
                string disc = "0";

                if (dr.Read())
                {
                    disc = dr.GetValue(0).ToString();
                    t_price.Visible = false;
                }

                cmd.Dispose();
                cmd = new SqlCommand("discount_order_manage", connec)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Action", "ORDER");
                cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@disc", Convert.ToDecimal(disc));
                cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(price));
                cmd.ExecuteNonQuery();

                Refresh_Screen();
            }
            connec.Close();
        }

        protected void clr_Button_Click(object sender, EventArgs e)
        {
            SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            if (connec.State == ConnectionState.Closed)
            {
                connec.Open();
            }

            SqlCommand cmd = new SqlCommand("cart_manage", connec)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Action", "CLEAR");
            cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session["user_id"]));

            cmd.ExecuteNonQuery();

            connec.Close();
            //Refresh_Screen();
            Response.Redirect("Tracking.aspx");
        }
    }
}