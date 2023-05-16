using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace PIZZA_LOUNGE.User
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Signup_btn_Click(object sender, EventArgs e)
        {
            try
            {
                connec.Open();
                if (name_exist(username.Text.Trim()) == false)
                {
                    SqlCommand cmd = new SqlCommand("insert into Users(User_name, Email, u_password)" + "values(@username, @email, @password)", connec);

                    cmd.Parameters.AddWithValue("@username", username.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", Email.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", password.Text.Trim());

                    cmd.ExecuteNonQuery(); //INSERT NEW USER

                    cmd = new SqlCommand("Select * From Users where User_name = '" + username.Text.Trim() + "'", connec);

                    SqlDataReader sdr = cmd.ExecuteReader(); //read the detail of new user to make session...
                    sdr.Read();

                    Session["username"] = username.Text.Trim();
                    Session["user_id"] = sdr.GetValue(0).ToString();
                    Session["status"] = "Logedin";
                    Session["user_order"] = "1";
                    Give_discount(Convert.ToInt32(Session["user_id"]));

                    //Response.Write("<script>alert('Welcome, " + Session["username"].ToString() + "\\nAs a welcome gift you have been given a 3% discount valid till " + DateTime.Today.AddDays(7).ToString() + "');</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "callBackFunction", "callBackFunction();", true); //for client side redirection...

                    Response.Write("<script>alert('Welcome, " + Session["username"].ToString() + "\\nWe are delighted to offer you a welcome gift in the form of a 3% discount on your next purchase\\. This exclusive discount is valid until " + DateTime.Today.Date.AddDays(7).ToString() + ", so don\\'t miss out on this opportunity');</script>");

                }

                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Name already exists');", true);
                }

                connec.Close();
            }

            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
            }
        }

        // user defined functions
        bool name_exist(string name)
        {
            SqlCommand cmd = new SqlCommand("Select*From Users where User_name = '" + name + "'", connec);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                sdr.Close();
                return true;
            }
            sdr.Close();
            return false;
        }

        void Give_discount(int u_id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert Into user_discount values(@u_id, @disc, @v_date)", connec);

                cmd.Parameters.AddWithValue("@u_id", u_id);
                cmd.Parameters.AddWithValue("@disc", 0.03);
                cmd.Parameters.AddWithValue("@v_date", DateTime.Today.Date.AddDays(7));

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}