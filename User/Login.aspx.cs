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
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (connec.State == ConnectionState.Closed)
                {
                    connec.Open();
                }

                SqlCommand cmd = new SqlCommand("Select*From Users where User_name = '" + username.Text.Trim() + "' AND u_password = '" + password.Text.Trim() + "'", connec);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    sdr.Read();
                    Session["username"] = sdr.GetValue(1).ToString();
                    Session["user_id"] = sdr.GetValue(0).ToString();
                    Session["status"] = "Logedin";
                    Session["OrderNo"] = Convert.ToInt32(sdr.GetValue(4)) + 1;

                    regular_discount(Convert.ToInt32(Session["user_id"]));

                    if (HaveDiscount(Convert.ToInt32(Session["user_id"])))
                    {
                        cmd.Dispose();

                        //checking validity of discount
                        cmd = new SqlCommand("Select*From user_discount where UserId = " + Convert.ToInt32(Session["user_id"]) + "", connec);
                        SqlDataReader disc_SDR = cmd.ExecuteReader();
                        disc_SDR.Read();

                        DateTime validdate = Convert.ToDateTime(disc_SDR.GetValue(2));
                        Decimal disc = Convert.ToDecimal(disc_SDR.GetValue(1));

                        disc = decimal.Multiply(disc, 100);
                        int result = DateTime.Compare(validdate.Date, DateTime.Today.Date);

                        if (result < 0) //if discount expired...
                        {
                            cmd.Dispose();

                            //deleting the discount from table
                            cmd = new SqlCommand("Delete From user_discount where UserId = " + Convert.ToInt32(Session["user_id"]) + ";", connec);
                            cmd.ExecuteNonQuery();

                            Response.Write("<script>alert('We\\'re sorry, but your " + disc + "% discount offer has expired');</script>");
                        }

                        else if (result == 0)//last day of expiration
                        {
                            Response.Write("<script>alert('Last day to save\\! Use your " + disc + "% discount before it expires today\\. Don\\'t miss out on this opportunity\\.');</script>");
                        }

                        else
                        {
                            Response.Write("<script>alert('You have a " + disc + "% discount valid untill " + validdate.Date.ToString() + "\\!');</script>");
                        }
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "callBackFunction", "callBackFunction();", true); //for client side redirection...
                }

                else
                {
                    Response.Write("<script>alert('Username or Password is Incorrect, Try Again!');</script>");
                }

                connec.Close();
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
            }
        }

        //user defined

        void regular_discount(int u_id)
        {
            SqlCommand cmd = new SqlCommand("Select * From user_discount where UserId = " + u_id + ";", connec);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                int orders = Convert.ToInt32(Session["OrderNo"]);
                if (orders % 10 == 0)
                {
                    try
                    {
                        cmd.Dispose();

                        if (HaveDiscount(u_id))
                        {
                            cmd = new SqlCommand("Update user_discount Set valid_till = @v_date, discount = @disc Where UserId = " + u_id + "", connec);
                            cmd.Parameters.AddWithValue("@disc", 0.07);
                            cmd.Parameters.AddWithValue("@v_date", DateTime.Today.Date.AddDays(7));
                        }

                        else
                        {
                            cmd = new SqlCommand("Insert Into user_discount values(@u_id, @disc, @v_date)", connec);
                            cmd.Parameters.AddWithValue("@u_id", u_id);
                            cmd.Parameters.AddWithValue("@disc", 0.07);
                            cmd.Parameters.AddWithValue("@v_date", DateTime.Today.Date.AddDays(7));
                        }

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                }
            }

        }

        bool HaveDiscount(int user_id)
        {
            SqlCommand cmd = new SqlCommand("Select*From user_discount where UserId = " + user_id + ";", connec);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                return true;
            }

            return false;
        }
    }
}