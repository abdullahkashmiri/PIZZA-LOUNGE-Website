
<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageOrder.aspx.cs" Inherits="PIZZA_LOUNGE.Admin.ManageOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../TemplateFiles/css/manageorder.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="parentcontainer"> 
   
    <div class="container">
        <div class="heading">Manage Orders</div>
        <div class="subheading">Approve or Decline</div>
        
        <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
            <ItemTemplate>
                <div class="order-container">
                    <div class="order-details">
                        <div class="order-name">Order No: <%# Eval("OrderNo") %></div>
                        <div class="order-name">User ID: <%# Eval("UserId") %></div>
                        <div class="order-price">Order Date: <%# Eval("OrderDate", "{0:yyyy-MM-dd}") %></div>
                        <div class="order-price">Price: <%# Eval("Price", "{0:C}") %></div>
                    </div>
                    <div class="order-actions">
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="approve-button" CommandName="Approve" CommandArgument='<%# Eval("OrderNo") %>' />
                        <asp:Button ID="btnDecline" runat="server" Text="Decline" CssClass="decline-button" CommandName="Decline" CommandArgument='<%# Eval("OrderNo") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </div>
</asp:Content>
