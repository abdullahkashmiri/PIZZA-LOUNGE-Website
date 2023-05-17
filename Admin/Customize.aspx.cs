using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Collections.Generic;


namespace PIZZA_LOUNGE.Admin
{
    public partial class Customize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize radio button event handlers
                insertRadioButton.CheckedChanged += new EventHandler(RadioBtnPage_CheckedChanged);
                updateRadioButton.CheckedChanged += new EventHandler(RadioBtnPage_CheckedChanged);

                // Set initial visibility
                insertSection.Visible = false;
                updateSection.Visible = false;

                // Hide the update textboxes initially
                HideUpdateTextBoxes();
            }
        }

        protected void RadioBtnPage_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            // Uncheck the other radio button
            if (radioButton == insertRadioButton)
            {
                updateRadioButton.Checked = false;
            }
            else if (radioButton == updateRadioButton)
            {
                insertRadioButton.Checked = false;
            }

            // Hide both sections initially
            insertSection.Visible = false;
            updateSection.Visible = false;

            if (insertRadioButton.Checked)
            {
                // Show the insert section
                insertSection.Visible = true;

                // Hide the update textboxes
                HideUpdateTextBoxes();
            }
            else if (updateRadioButton.Checked)
            {
                // Show the update section
                updateSection.Visible = true;
            }
        }

        private void HideUpdateTextBoxes()
        {
            // Hide the update textboxes
            updateNameTextBox.Visible = true;
            updateDescriptionTextBox.Visible = false;
            updatePriceTextBox.Visible = false;
            updateImage.Visible = false;
            updateIsActiveTextBox.Visible = false;
        }
      
        protected void UpdateButtonshow_Click(object sender, EventArgs e)
        {
            // Check if a product name is selected
            string productName = GetSelectedProductName();
            if (string.IsNullOrEmpty(productName))
            {
                // Redirect to PageB
                Response.Redirect("./Customize.aspx");
                return;
            }

            // Get the product data from the database and store it in session variables
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products WHERE Name = @ProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Store the product data in session variables
                        Session["ProductName"] = reader["Name"].ToString();
                        Session["ProductDescription"] = reader["Description"].ToString();
                        Session["ProductPrice"] = reader["Price"].ToString();
                        Session["ProductImageUrl"] = reader["ImageUrl"].ToString();
                        Session["ProductIsActive"] = reader["IsActive"].ToString();
                    }

                    reader.Close();
                }
            }

            // Show the update section
            updateSection.Visible = true;
            productimage.Visible = true;

            // Show the update textboxes if the respective checkboxes are checked
            updateDescriptionTextBox.Visible = updateDescription.Checked;
            updatePriceTextBox.Visible = updatePrice.Checked;
            updateImage.Visible = updateImageCheckBox.Checked;
            updateIsActiveTextBox.Visible = updateIsActive.Checked;

            // Set the product name in the label if available
            productNamehere.Text = !string.IsNullOrEmpty(productName) ? productName : "";

            // Set the product data in their respective textboxes from session variables
            updateDescriptionTextBox.Text = Session["ProductDescription"]?.ToString();
            updatePriceTextBox.Text = Session["ProductPrice"]?.ToString();
            // You can similarly set other textbox values here

            // Set the image URL in the productimage img tag
            string imageUrl = Session["ProductImageUrl"]?.ToString();
            if (!string.IsNullOrEmpty(imageUrl))
            {
                productimage.Src = imageUrl;
            }

            // Hide the update button and show the save button
            updateButton.Visible = true;
            UpdateButtonshow.Visible = false;
        }



        private string GetSelectedProductName()
        {
            // Get the selected product name from the updateNameTextBox
            string selectedProductName = updateNameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(selectedProductName))
            {
                return selectedProductName;
            }

            return null; // No product name entered
        }
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            // Check if the product name is present in the session
            if (Session["ProductName"] == null)
            {
                // Redirect to another page since the product name is not available in the session
                Response.Redirect("./Customize.aspx");
                return;
            }

            string productName = Session["ProductName"].ToString();

            // Retrieve the data from the respective checkboxes and update only the non-empty fields
            string description = string.Empty;
            int price = 0;
            string imageUrl = string.Empty;
            int isActive = 0;

            if (updateDescription.Checked && !string.IsNullOrEmpty(updateDescriptionTextBox.Text))
            {
                description = updateDescriptionTextBox.Text;
            }

            if (updatePrice.Checked && !string.IsNullOrEmpty(updatePriceTextBox.Text))
            {
                price = Convert.ToInt32(updatePriceTextBox.Text);
            }

            if (updateImageCheckBox.Checked && updateImage.HasFile)
            {
                imageUrl = SaveImage(updateImage);
            }

            if (updateIsActive.Checked && !string.IsNullOrEmpty(updateIsActiveTextBox.Text))
            {
                isActive = Convert.ToInt32(updateIsActiveTextBox.Text);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Products SET";

                // Update only the non-empty fields
                List<string> updateFields = new List<string>();

                if (!string.IsNullOrEmpty(description))
                {
                    query += " Description = @Description,";
                    updateFields.Add("@Description");
                }

                if (price != 0)
                {
                    query += " Price = @Price,";
                    updateFields.Add("@Price");
                }

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    query += " ImageUrl = @ImageUrl,";
                    updateFields.Add("@ImageUrl");
                }

                if (isActive != 0)
                {
                    query += " IsActive = @IsActive,";
                    updateFields.Add("@IsActive");
                }

                // Remove the last comma from the query
                query = query.TrimEnd(',');

                query += " WHERE Name = @ProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);

                    // Set the parameter values for the non-empty fields
                    foreach (string field in updateFields)
                    {
                        switch (field)
                        {
                            case "@Description":
                                command.Parameters.AddWithValue("@Description", description);
                                break;
                            case "@Price":
                                command.Parameters.AddWithValue("@Price", price);
                                break;
                            case "@ImageUrl":
                                command.Parameters.AddWithValue("@ImageUrl", imageUrl);
                                break;
                            case "@IsActive":
                                command.Parameters.AddWithValue("@IsActive", isActive);
                                break;
                        }
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(productName.Text) || string.IsNullOrEmpty(description.Text) ||
                string.IsNullOrEmpty(price.Text) || string.IsNullOrEmpty(categoryId.Text) ||
                string.IsNullOrEmpty(rating.Text))
            {
                // Display an error message on the screen
                errorMessage.Text = "Please fill in all the required fields.";
                errorMessage.Visible = true;
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Products (Name, Description, Price, ImageUrl, CategoryId, IsActive, Rating) VALUES (@Name, @Description, @Price, @ImageUrl, @CategoryId, @IsActive, @Rating)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", productName.Text);
                        command.Parameters.AddWithValue("@Description", description.Text);
                        command.Parameters.AddWithValue("@Price", Convert.ToInt32(price.Text));
                        command.Parameters.AddWithValue("@ImageUrl", SaveImage(image));
                        command.Parameters.AddWithValue("@CategoryId", Convert.ToInt32(categoryId.Text));
                        command.Parameters.AddWithValue("@IsActive", isActive.Checked ? 0 : 2);
                        command.Parameters.AddWithValue("@Rating", Convert.ToDouble(rating.Text));

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                // Clear the form fields after successful insertion
                productName.Text = "";
                description.Text = "";
                price.Text = "";
                categoryId.Text = "";
                rating.Text = "";
                isActive.Checked = false;

                // Display a success message on the screen
                successMessage.Text = "Product added successfully.";
                successMessage.Visible = true;
            }
            catch (Exception ex)
            {
                // Display an error message on the screen
                errorMessage.Text = "An error occurred while adding the product: " + ex.Message;
                errorMessage.Visible = true;
            }
        }
        private string SaveImage(FileUpload fileUpload)
        {
            // Save the file to a folder and return the image URL
            if (fileUpload.HasFile)
            {
                string fileName = fileUpload.FileName;
                string folderPath = Server.MapPath("~/TemplateFiles/menuimages/");
                string filePath = folderPath + fileName;
                fileUpload.SaveAs(filePath);
                return "/TemplateFiles/menuimages/" + fileName;
            }
            else
            {
                // Return a default image URL or handle the case when no file is uploaded
                return string.Empty;
            }
        }

        private string GetExistingImageUrl(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
            string imageUrl = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ImageUrl FROM Products WHERE ProductId = @ProductId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            imageUrl = reader["ImageUrl"].ToString();
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(imageUrl))
            {
                return imageUrl;
            }
            else
            {
                // Return a default image URL or handle the case when no image is found
                return string.Empty;
            }
        }
    }
}
