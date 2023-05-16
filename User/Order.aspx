<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="PIZZA_LOUNGE.User.Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link rel="stylesheet" href="../TemplateFiles/css/order.css"><!--linked css file of menupage-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container">
  <h1>Order Now</h1>
<div class="container_ad2">
    <div class="left_ad2">
         <img src= '<%#Eval("ImageUrl") %>' alt="Pizza Image" runat="server" id="Img1" />
      
        <h2><asp:Label ID="nameLabel"  runat="server" Text=" "></asp:Label></h2>
        <p><asp:Label ID="descriptionLabel" runat="server" Text=" "></asp:Label></p>
        <asp:Label ID="priceLabel" CssClass="pricead1" runat="server" Text=""></asp:Label>
        <asp:Label ID="ratingLabel"   runat="server" Text=""></asp:Label>
    </div>

    <div class="right_ad2">
        <h1>Build Your Pizza</h1>
        <div class="con_ad2">
             <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false" Text=""></asp:Label>

            <div class="size-container">
                <h2>Size:</h2>
                <div class="size-options_ad2">
                    <asp:RadioButton ID="small" runat="server" Text="Small" GroupName="size" /><br />
                    <asp:RadioButton ID="medium" runat="server" Text="Medium" GroupName="size" />
                    <asp:RadioButton ID="large" runat="server" Text="Large" GroupName="size" />
                </div>
            </div>
            <div class="toppings-container">
                <h2>Toppings:</h2>
                <div class="toppings_ad2">
                    <asp:CheckBox ID="pepperoni" runat="server" Text="Pepperoni" />
                    <asp:CheckBox ID="onions" runat="server" Text="Onions" />
                    <asp:CheckBox ID="mushrooms" runat="server" Text="Mushrooms" />
                    <asp:CheckBox ID="cheese" runat="server" Text="Cheese" />
                </div>
            </div>
        </div>

        <div class="con_ad2">
            <h2>Drinks:</h2>
            <asp:DropDownList ID="drinks" runat="server" CssClass="ad2">
                <asp:ListItem Text="None" Value="none" />
                <asp:ListItem Text="Coke Rs 80/-" Value="coke" />
                <asp:ListItem Text="Pepsi Rs 70/-" Value="pepsi" />
                <asp:ListItem Text="Sprite Rs 80/-" Value="sprite" />
            </asp:DropDownList>
        </div>

          <div class="con_ad2">
            <h2>Fries:_ </h2>
            <asp:DropDownList ID="Fries" runat="server" CssClass="ad2">
                <asp:ListItem Text="None" Value="none" />
                <asp:ListItem Text="Small Rs 180/-" Value="small" />
                <asp:ListItem Text="Medium Rs 260/-" Value="medium" />
                <asp:ListItem Text="Large Rs 340/-" Value="large" />
            </asp:DropDownList>
        </div>

            

<asp:Button ID="placeOrderButton" CssClass="btnad" runat="server" Text="Place Order" OnClick="placeOrderButton_Click" />
</div>
</div>
        </div>
</asp:Content>
