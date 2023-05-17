﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PIZZA_LOUNGE.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    // Set the active button based on the current page
                    SetActiveButton(currentPage);
                }

                if (Session["status"] == null) //user logged out
                {
                    Logout.Visible = false;
                    Login.Visible = true;
                    Login.CssClass = "a active";
                }
                else if (Session["status"].Equals("Logedin")) //user logged in
                {
                    Login.Visible = false;
                    Logout.Visible = true;
                    Logout.CssClass = "a active";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        private void SetActiveButton(string currentPage)
        {
            // Reset the CSS class for all buttons
            products.CssClass = "a";
            customize.CssClass = "a";
            manageorder.CssClass = "a";
            reserve.CssClass = "a";
            comp.CssClass = "a";
            job.CssClass = "a";
            Login.CssClass = "a";
            Logout.CssClass = "a";

            // Set the active CSS class for the current page button
            switch (currentPage)
            {
                case "Products.aspx":
                    products.CssClass = "a active";
                    break;
                case "Customize.aspx":
                    customize.CssClass = "a active";
                    break;
                case "ManageOrder.aspx":
                    manageorder.CssClass = "a active";
                    break;
                case "Reserve.aspx":
                    reserve.CssClass = "a active";
                    break;
                case "Complaint.aspx":
                    comp.CssClass = "a active";
                    break;
                case "Job.aspx":
                    job.CssClass = "a active";
                    break;
                case "Login.aspx":
                    Login.CssClass = "a active";
                    break;
            }
        }

        protected void products_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Products.aspx");
        }

        protected void customize_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Customize.aspx");
        }

        protected void manageorder_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ManageOrder.aspx");
        }

        protected void reserve_Click(object sender, EventArgs e)
        {
            Response.Redirect("../User/Reserve.aspx");

        }

        protected void comp_Click(object sender, EventArgs e)
        {
            Response.Redirect("../User/Complaint.aspx");
        }
        protected void job_Click(object sender, EventArgs e)
        {
            Response.Redirect("../User/Job.aspx");
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("../User/Login.aspx");
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["ProductName"] = null;
            Session["username"] = null;
            Session["user_id"] = null;
            Session["status"] = null;
            Session["bit"] = 2;
            Response.Redirect("../User/Default.aspx");
        }
    }
}