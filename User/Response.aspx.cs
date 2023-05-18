using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.User
{
    public partial class Response : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                if (Session["status"] == null) //user not logedin
                {
                    Response.Redirect("./Login.aspx");
                }
                // Get the user ID from the session variable
                int userId = Convert.ToInt32(Session["user_id"]);

                BindOrders(userId);
                BindComplaints(userId);
                BindJobs(userId);
                BindReservations(userId);
            }
        }

        protected void BindOrders(int userId)
        {
            // Logic to bind orders specific to the user with the given user ID to rptOrders repeater
            // Replace this with your actual data retrieval logic based on the user ID

            // Assuming you have a connection string named "con" defined in your web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Query to retrieve orders for the specified user ID
            string query = "SELECT * FROM Orders WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Clear previous items in the repeater
                        rptOrders.DataSource = null;
                        rptOrders.DataBind();

                        // Create a list to hold the orders
                        List<Order> orders = new List<Order>();

                        while (reader.Read())
                        {
                            // Read order details from the reader
                            int orderNo = Convert.ToInt32(reader["OrderNo"]);
                            DateTime orderDate = Convert.ToDateTime(reader["OrderDate"]);
                            int price = Convert.ToInt32(reader["Price"]);
                            int? status = reader["Status"] as int?;

                            // Create a new Order object
                            Order order = new Order
                            {
                                OrderNo = orderNo,
                                OrderDate = orderDate,
                                Price = price,
                                Status = status
                            };

                            // Add the order to the list
                            orders.Add(order);
                        }

                        // Bind the orders list to the repeater
                        rptOrders.DataSource = orders;
                        rptOrders.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the database operation
                    // You can display an error message or log the exception
                }
            }
        }

        // Method to get the status label text based on the value
        protected string GetStatusLabel(object statusObj)
        {
            int? status = statusObj as int?;

            if (status.HasValue)
            {
                if (status.Value == 0)
                {
                    return "Rejected";
                }
                else if (status.Value == 1)
                {
                    return "Accepted";
                }
            }

            return "Pending";
        }


        public class Order
        {
            public int OrderNo { get; set; }
            public int UserId { get; set; } // Add the UserId property
            public DateTime OrderDate { get; set; }
            public int Price { get; set; }
            public int? Status { get; set; }
        }


        protected void BindComplaints(int userId)
        {
            // Logic to bind complaints specific to the user with the given user ID to rptComplaints repeater
            // Replace this with your actual data retrieval logic based on the user ID

            // Assuming you have a connection string named "con" defined in your web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Query to retrieve complaints for the specified user ID
            string query = "SELECT * FROM complaint_form WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Clear previous items in the repeater
                        rptComplaints.DataSource = null;
                        rptComplaints.DataBind();

                        // Create a list to hold the complaints
                        List<Complaint> complaints = new List<Complaint>();

                        while (reader.Read())
                        {
                            // Read complaint details from the reader
                            int complaintId = Convert.ToInt32(reader["complaint_id"]);
                            string username = reader["username"].ToString();
                            string email = reader["email"].ToString();
                            string message = reader["message"].ToString();
                            DateTime submitTime = Convert.ToDateTime(reader["submit_time"]);
                            int? status = reader["status"] as int?;

                            // Create a new Complaint object
                            Complaint complaint = new Complaint
                            {
                                ComplaintId = complaintId,
                                Username = username,
                                Email = email,
                                Message = message,
                                SubmitTime = submitTime,
                                Status = status
                            };

                            // Add the complaint to the list
                            complaints.Add(complaint);
                        }

                        // Bind the complaints list to the repeater
                        rptComplaints.DataSource = complaints;
                        rptComplaints.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the database operation
                    // You can display an error message or log the exception
                }
            }
        }


        // Complaint class to hold complaint details
        public class Complaint
        {
            public int ComplaintId { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
            public DateTime SubmitTime { get; set; }
            public int? Status { get; set; }
        }
        protected void BindJobs(int userId)
        {
            // Logic to bind jobs specific to the user with the given user ID to rptJobs repeater
            // Replace this with your actual data retrieval logic based on the user ID

            // Assuming you have a connection string named "con" defined in your web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Query to retrieve jobs for the specified user ID
            string query = "SELECT * FROM job_form WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Clear previous items in the repeater
                        rptJobs.DataSource = null;
                        rptJobs.DataBind();

                        // Create a list to hold the jobs
                        List<Job> jobs = new List<Job>();

                        while (reader.Read())
                        {
                            // Read job details from the reader
                            int jobId = Convert.ToInt32(reader["job_id"]);
                            string firstName = reader["firstname"].ToString();
                            string lastName = reader["lastname"].ToString();
                            string email = reader["email"].ToString();
                            string city = reader["city"].ToString();
                            string zip = reader["zip"].ToString();
                            byte[] cvFile = (byte[])reader["cv_file"];
                            int? status = reader["status"] as int?;

                            // Create a new Job object
                            Job job = new Job
                            {
                                JobId = jobId,
                                FirstName = firstName,
                                LastName = lastName,
                                Email = email,
                                City = city,
                                Zip = zip,
                                CVFile = cvFile,
                                Status = status
                            };

                            // Add the job to the list
                            jobs.Add(job);
                        }

                        // Bind the jobs list to the repeater
                        rptJobs.DataSource = jobs;
                        rptJobs.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the database operation
                    // You can display an error message or log the exception
                }
            }
        }

        
        // Job class to hold job details
        public class Job
        {
            public int JobId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string City { get; set; }
            public string Zip { get; set; }
            public byte[] CVFile { get; set; }
            public int? Status { get; set; }
        }

        protected void BindReservations(int userId)
        {
            // Logic to bind reservations specific to the user with the given user ID to rptReservations repeater
            // Replace this with your actual data retrieval logic based on the user ID

            // Assuming you have a connection string named "con" defined in your web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Query to retrieve reservations for the specified user ID
            string query = "SELECT * FROM Reservation WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Clear previous items in the repeater
                        rptReservations.DataSource = null;
                        rptReservations.DataBind();

                        // Create a list to hold the reservations
                        List<Reservation> reservations = new List<Reservation>();

                        while (reader.Read())
                        {
                            // Read reservation details from the reader
                            int reservationId = Convert.ToInt32(reader["ID"]);
                            string day = reader["Day"].ToString();
                            string hour = reader["Hour"].ToString();
                            string name = reader["Name"].ToString();
                            string phone = reader["Phone"].ToString();
                            int persons = Convert.ToInt32(reader["Persons"]);
                            int? status = reader["status"] as int?;

                            // Create a new Reservation object
                            Reservation reservation = new Reservation
                            {
                                Day = day,
                                Hour = hour,
                                Persons = persons,
                                Status = status
                            };

                            // Add the reservation to the list
                            reservations.Add(reservation);
                        }

                        // Bind the reservations list to the repeater
                        rptReservations.DataSource = reservations;
                        rptReservations.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the database operation
                    // You can display an error message or log the exception
                }
            }
        }

        // Reservation class to hold reservation details
        public class Reservation
        {
            public int ID { get; set; } // Add the ID property
            public string Day { get; set; }
            public string Hour { get; set; }
            public int Persons { get; set; }
            public int? Status { get; set; }
        }


        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Logic for handling item commands in rptOrders repeater
            // Replace this with your actual logic for handling item commands
        }

        protected void rptComplaints_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Logic for handling item commands in rptComplaints repeater
            // Replace this with your actual logic for handling item commands
        }

        protected void rptJobs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Logic for handling item commands in rptJobs repeater
            // Replace this with your actual logic for handling item commands
        }

        protected void rptReservations_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Logic for handling item commands in rptReservations repeater
            // Replace this with your actual logic for handling item commands
        }
    }
}
