using System;
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
            menu.CssClass = "a";
            order.CssClass = "a";
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
                case "Menu.aspx":
                    menu.CssClass = "a active";
                    break;
                case "Custom.aspx":
                    order.CssClass = "a active";
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

        protected void menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Menu.aspx");
        }

        protected void order_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Custom.aspx");
        }

        protected void reserve_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Reserve.aspx");

        }

        protected void comp_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Complaint.aspx");
        }
        protected void job_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Job.aspx");
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Login.aspx");
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["username"] = null;
            Session["user_id"] = null;
            Session["status"] = null;
            Session["bit"] = 2;
            Response.Redirect("./Default.aspx");
        }
    }
}