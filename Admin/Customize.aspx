<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Customize.aspx.cs" Inherits="PIZZA_LOUNGE.Admin.Customize" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../TemplateFiles/css/customize.css" />
    <title>Customize Your Menu</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <form class="customize-form">
        <div class="container">
            <h2>Customize Your Menu</h2>
          <div class="radiocontainer">
    <asp:RadioButton ID="insertRadioButton" runat="server" Text="Add New Item" AutoPostBack="True" OnCheckedChanged="RadioBtnPage_CheckedChanged" />
    <asp:RadioButton ID="updateRadioButton" runat="server" Text="Update Existing Item" AutoPostBack="True" OnCheckedChanged="RadioBtnPage_CheckedChanged" />
</div>

            <div class="displaycontainer">

        <div id="insertSection" runat="server" class="form-section" visible="false">
    <asp:Label ID="errorMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
    <asp:Label ID="successMessage" runat="server" CssClass="success-message" Visible="false"></asp:Label>

    <label for="productName">Product Name:</label>
    <asp:TextBox ID="productName" runat="server"></asp:TextBox><br /><br />

    <label for="description">Description:</label>
    <asp:TextBox ID="description" runat="server" TextMode="MultiLine"></asp:TextBox><br /><br />

    <label for="price">Price:</label>
    <asp:TextBox ID="price" runat="server"></asp:TextBox><br /><br />

    <label for="image">Image:</label>
    <asp:FileUpload ID="image" runat="server"></asp:FileUpload><br /><br />

    <label for="categoryId">Category ID:</label>
    <asp:TextBox ID="categoryId" runat="server"></asp:TextBox><br /><br />

    <label for="isActive">Is Active:</label>
    <asp:CheckBox ID="isActive" runat="server" /><br /><br />

    <label for="rating">Rating:</label>
    <asp:TextBox ID="rating" runat="server"></asp:TextBox><br /><br />

    <asp:Button ID="insertButton" CssClass="btnad" runat="server" Text="Add to Menu" OnClick="InsertButton_Click" />
</div>

                <div id="updateSection" runat="server" class="form-section">
   <div class="container1">
  <asp:Label ID="productLabel" runat="server" Visible="true">Update Product</asp:Label><br />
        <asp:Label ID="productNamehere" runat="server" Visible="true" Text=""> </asp:Label>

       <img id="productimage" src="../TemplateFiles/menuimages/banner/img1.jpg" alt="Image" style="width: 200px; height: 150px; " runat="server" visible="false"  >
</div>

                    
                    <br /><br />
    <label id="selectname" runat="server" visible="true" >Enter an Existing Item Name</label>
  
                    
                    <asp:TextBox ID="updateNameTextBox" runat="server"></asp:TextBox>

    <asp:CheckBox ID="updateDescription" runat="server" Text="Update Description" Visible="true" onchange="toggleTextBox('descriptionTextBox', this.checked)" />
    <asp:TextBox ID="updateDescriptionTextBox" runat="server"></asp:TextBox>

    <asp:CheckBox ID="updatePrice" runat="server" Text="Update Price" Visible="true" onchange="toggleTextBox('priceTextBox', this.checked)" />
    <asp:TextBox ID="updatePriceTextBox" runat="server"></asp:TextBox>

   <asp:CheckBox ID="updateImageCheckBox" runat="server" Text="Update Image" Visible="true" onchange="toggleFileUpload(this.checked)" />
<asp:FileUpload ID="updateImage" runat="server" Visible="false" /><br /><br />

    <asp:CheckBox ID="updateIsActive" runat="server" Text="Update IsActive" Visible="true" onchange="toggleTextBox('isActiveTextBox', this.checked)" />
    <asp:TextBox ID="updateIsActiveTextBox" runat="server" Visible="false"></asp:TextBox><br /><br />
   <asp:Button ID="UpdateButtonshow" runat="server" CssClass="btnad" Text="Update Selected Items" OnClick="UpdateButtonshow_Click" Visible="true" />


    <asp:Button ID="updateButton" runat="server" CssClass="btnad" Text="Update Item" OnClick="UpdateButton_Click" Visible="false" />
</div>

<script>
    function toggleTextBox(textBoxId, isChecked) {
        var textBox = document.getElementById(textBoxId);
        if (textBox) {
            textBox.style.display = isChecked ? 'block' : 'none';
            textBox.disabled = !isChecked;
            if (isChecked) {
                textBox.style.visibility = 'visible';
                textBox.style.position = 'relative';
            } else {
                textBox.style.visibility = 'hidden';
                textBox.style.position = 'absolute';
            }
        }
    }
    function toggleFileUpload(checked) {
        var fileUpload = document.getElementById('<%= updateImage.ClientID %>');
        fileUpload.style.display = checked ? 'block' : 'none';
    }

</script>
        </div>
        </div>
    </form>
</asp:Content>