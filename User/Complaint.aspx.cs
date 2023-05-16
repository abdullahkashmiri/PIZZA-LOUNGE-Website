using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace PIZZA_LOUNGE.User
{
    public partial class Complaint : System.Web.UI.Page
    {
        String complaintcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        public static object ConfigurationManger { get; private set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SendButton(object sender, EventArgs e)
        {
           
            SqlConnection conn = new SqlConnection(complaintcon);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("insert into complaint_form ([username], [email], [message], [submit_time]) values (@username, @email, @message, @submit_time)", conn);
            cmd.Parameters.AddWithValue("@username", usernameUF.Text.Trim());
            cmd.Parameters.AddWithValue("@email", emailUF.Text.Trim());
            cmd.Parameters.Add("@message", SqlDbType.VarChar).Value = messageUF.Value.Trim();
            DateTime submitTime = DateTime.Now;
            cmd.Parameters.Add("@submit_time", SqlDbType.DateTime).Value = submitTime;
            cmd.ExecuteNonQuery();
        }
    }
}