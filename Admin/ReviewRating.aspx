<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ReviewRating.aspx.cs" Inherits="PIZZA_LOUNGE.Admin.ReviewRating" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="../TemplateFiles/css/reviewrating.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 

 
    <div class="parentcontainer">
        <div class="container">
            <div class="heading">Reviews and Ratings</div>
            <div class="subheading">Thanks or Apologize</div>

            <div class="rating-container">
                <%-- Display ratings from users with status null --%>
                <asp:Repeater ID="rptRatings" runat="server">
                    <ItemTemplate>
                        <div class="rating-item">
                            <div class="rating-info">
                                <div class="rating-name">Name: <%# Eval("Name") %></div>
                                <div class="rating-product">Product: <%# Eval("ProductName") %></div>
                                <div class="rating-value">Rating: <%# Eval("Rating") %></div>
                                <div class="rating-comment">Comment: <%# Eval("Comment") %></div>
                            </div>
                            <div class="rating-action">
                                <asp:Button ID="btnThanks" runat="server" Text="Thanks" CssClass="thanks-button" CommandName="Thanks" CommandArgument='<%# Eval("ID") %>' />
                                <asp:Button ID="btnApologize" runat="server" Text="Apologize" CssClass="apologize-button" CommandName="Apologize" CommandArgument='<%# Eval("ID") %>' />
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
