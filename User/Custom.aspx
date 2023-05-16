<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Custom.aspx.cs" Inherits="PIZZA_LOUNGE.User.Custom" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link rel="stylesheet" type="text/css" href="../TemplateFiles/css/custom.css">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <div class="parent_container">
        <div class="container">
            <div class="header-container">
                <h1>Make Your Own Pizza</h1>
            </div>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false" Text=""></asp:Label>

<form id="pizzaForm">
    <h2>Pizza Size:</h2>
    <asp:RadioButton ID="size_personal" runat="server" Text="Personal" GroupName="size" value="personal" required />
    <asp:RadioButton ID="size_medium" runat="server" Text="Medium" GroupName="size" value="medium" required />
    <asp:RadioButton ID="size_large" runat="server" Text="Large" GroupName="size" value="large" required />

    <h2>Pizza Crust:</h2>
    <asp:RadioButton ID="crust_thin" runat="server" Text="Thin Crust" GroupName="crust" value="thin" required />
    <asp:RadioButton ID="crust_thick" runat="server" Text="Thick Crust" GroupName="crust" value="thick" required />
    <asp:RadioButton ID="crust_hand_tossed" runat="server" Text="Hand-tossed" GroupName="crust" value="hand-tossed" required />
    <asp:RadioButton ID="crust_pan" runat="server" Text="Pan Crust" GroupName="crust" value="pan" required />
    <asp:RadioButton ID="crust_gluten_free" runat="server" Text="Gluten-free" GroupName="crust" value="gluten-free" required />

    <h2>Pizza Sauce:</h2>
    <asp:RadioButton ID="sauce_tomato" runat="server" Text="Tomato Sauce" GroupName="sauce" value="tomato" required />
    <asp:RadioButton ID="sauce_garlic" runat="server" Text="Garlic Sauce" GroupName="sauce" value="garlic" required />
    <asp:RadioButton ID="sauce_barbecue" runat="server" Text="Barbecue Sauce" GroupName="sauce" value="barbecue" required />
    <asp:RadioButton ID="sauce_pesto" runat="server" Text="Pesto Sauce" GroupName="sauce" value="pesto" required />
    <asp:RadioButton ID="sauce_olive_oil" runat="server" Text="Olive Oil" GroupName="sauce" value="olive-oil" required />

    <h2>Cheese Quantity:</h2>
    <asp:RadioButton ID="cheese_light" runat="server" Text="Light" GroupName="cheese" value="light" required />
    <asp:RadioButton ID="cheese_regular" runat="server" Text="Regular" GroupName="cheese" value="regular" required />
    <asp:RadioButton ID="cheese_extra" runat="server" Text="Extra" GroupName="cheese" value="extra" required />

    <h2>Toppings:</h2>
    <asp:CheckBox ID="topping_pepperoni" runat="server" Text="Pepperoni" value="pepperoni" />
    <asp:CheckBox ID="topping_sausage" runat="server" Text="Sausage" value="sausage" />
    <asp:CheckBox ID="topping_mushrooms" runat="server" Text="Mushrooms" value="mushrooms" />
    <asp:CheckBox ID="topping_onions" runat="server" Text="Onions" value="onions" />
    <asp:CheckBox ID="topping_bell_peppers" runat="server" Text="Bell Peppers" value="bell-peppers" />
    <asp:CheckBox ID="topping_olives" runat="server" Text="Olives" value="olives" />
    <asp:CheckBox ID="topping_tomatoes" runat="server" Text="Tomatoes" value="tomatoes" />
    <asp:CheckBox ID="topping_spinach" runat="server" Text="Spinach" value="spinach" />
    <asp:CheckBox ID="topping_jalapenos" runat="server" Text="Jalapenos" value="jalapenos" />
    <asp:CheckBox ID="topping_extra_cheese" runat="server" Text="Extra Cheese" value="extra-cheese" />

    <asp:Button ID="btnProceed" runat="server" Text="Proceed To Next" OnClick="btnProceed_Click" />
</form>
        </div>

      <asp:Panel ID="invContainerPanel" runat="server" CssClass="inv_container" Visible="false">
    <!-- Content of the inv_container -->
      <div class="inv_container">
    <div class="selection-container"  id="selectionContainer" runat="server">
    <h2>Selected Options</h2>
   <div class="selection-item">
    <div class="selection-label">
        <label for="size">Pizza Size:</label>
    </div>
    <div class="selection-value">
        <asp:Label ID="lblSelectedSize" runat="server" Text=""></asp:Label>
    </div>
</div>

<div class="selection-item">
    <div class="selection-label">
        <label for="crust">Pizza Crust:</label>
    </div>
    <div class="selection-value">
        <asp:Label ID="lblSelectedCrust" runat="server" Text=""></asp:Label>
    </div>
</div>

<div class="selection-item">
    <div class="selection-label">
        <label for="sauce">Pizza Sauce:</label>
    </div>
    <div class="selection-value">
        <asp:Label ID="lblSelectedSauce" runat="server" Text=""></asp:Label>
    </div>
</div>

<div class="selection-item">
    <div class="selection-label">
        <label for="cheese">Cheese Quantity:</label>
    </div>
    <div class="selection-value">
        <asp:Label ID="lblSelectedCheese" runat="server" Text=""></asp:Label>
    </div>
</div>

<div class="selection-item">
    <div class="selection-label">
        <label for="toppings">Toppings:</label>
    </div>
    <div class="selection-value">
        <asp:Label ID="lblSelectedToppings" runat="server" Text=""></asp:Label>
    </div>
</div>
    
    </div>

        <div class="container"  id="container" runat="server">
             <div class="amount-container">
    <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
</div>

            <div class="container_1">

             <asp:Button ID="btnPlaceOrder" runat="server" CssClass="place-order-button" Text="Place Order" OnClick="btnPlaceOrder_Click" />

            </div>
        </div>
    </div>
</asp:Panel>

  </div>

</asp:Content>
