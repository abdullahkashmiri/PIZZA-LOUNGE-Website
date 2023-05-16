using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace PIZZA_LOUNGE.User
{
    public partial class Reserve : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Session["status"] == null) //user not logedin
            {
                Response.Redirect("./Login.aspx");
            }
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            string day = daysDropdown.SelectedValue;
            TimeSpan hour = TimeSpan.Parse(hoursDropdown.SelectedValue);
            string formattedHour = hour.ToString(@"hh\:mm"); // Format the TimeSpan object as "hh:mm"
            string name = fullNameTextBox.Text;
            string phone = phoneNumberTextBox.Text;
            int persons = Convert.ToInt32(personsTextBox.Text);
            int userId = Convert.ToInt32(Session["user_id"]);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Reservation (Day, Hour, Name, Phone, Persons, UserId) 
                                VALUES (@Day, @Hour, @Name, @Phone, @Persons, @UserId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Day", day);
                    command.Parameters.AddWithValue("@Hour", formattedHour); // Use the formatted time string
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Persons", persons);
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            SuccessLabel.Text = "Table Reserved successfully";

            // Register client-side script to hide the label after one second and redirect
            string script = @"<script type='text/javascript'>
                        setTimeout(function() {
                            document.getElementById('" + SuccessLabel.ClientID + @"').style.display = 'none';
                            window.location.href = 'Reserve.aspx';
                        }, 1000);
                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabelScript", script);

        }
    }
}