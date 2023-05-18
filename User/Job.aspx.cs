using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace PIZZA_LOUNGE.User
{
    public partial class Job : System.Web.UI.Page
    {
        String jobcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public static object ConfigurationManger { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendButton1(object sender, EventArgs e)
        {
            
            // Get the file name and extension
            string fileName = Path.GetFileName(cvUF.PostedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);

            // Save the file to the server's file system
            string fileSavePath = Server.MapPath("~/UploadedFiles/") + fileName;
            cvUF.SaveAs(fileSavePath);

            // Insert the file into the database
            using (SqlConnection conn = new SqlConnection(jobcon))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into job_form ([firstname],[lastname],[email],[city],[zip],[cv_file]) values (@firstname, @lastname, @email, @city, @zip, @cv_file)", conn);
                cmd.Parameters.AddWithValue("@firstname", firstnameUF.Text.Trim());
                cmd.Parameters.AddWithValue("@lastname", lastnameUF.Text.Trim());
                cmd.Parameters.AddWithValue("@email", emailUF.Text.Trim());
                cmd.Parameters.AddWithValue("@city", cityUF.Text.Trim());
                cmd.Parameters.AddWithValue("@zip", zipUF.Text.Trim());
                cmd.Parameters.AddWithValue("@cv_file", File.ReadAllBytes(fileSavePath));
                cmd.ExecuteNonQuery();
            }

          //  SendMail();
            // Delete the file from the server's file system
            //File.Delete(fileSavePath);
        }

        void SendMail()
        {
            string from = "l211770@lhr.nu.edu.pk";
            using (MailMessage mail = new MailMessage(from, "l215373@lhr.nu.edu.pk,l211770@lhr.nu.edu.pk"))
            {
                mail.Subject = "Job Application From Pizza Lounge";
                mail.Body = "Dear Hiring Manager,\n\nA new job applicant has submitted their details and CV through our website.Their details are as follows:\nName: " + firstnameUF.Text.Trim() + " " + lastnameUF.Text.Trim() + "\nEmail: " + emailUF.Text.Trim() + "\nCity: " + cityUF.Text.Trim() + "\nPhone number: " + zipUF.Text + "\nCV is attached below.\n\n";
                if (cvUF.HasFile)
                {
                    string fileName = Path.GetFileName(cvUF.PostedFile.FileName);
                    mail.Attachments.Add(new Attachment(cvUF.PostedFile.InputStream, fileName));
                }
                mail.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(from, "QM123456");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mail);
            }
        }
    }
}