<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Rating.aspx.cs" Inherits="PIZZA_LOUNGE.User.Rating" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../TemplateFiles/css/rating.css" />
    <title>Pizza Lounge Rating</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" >
        <div class="container">
            <div class="rating-form">
                <h1>Pizza Rating</h1>
                <div>
                    <label for="name">Name:</label>
                    <asp:TextBox ID="name" runat="server" placeholder="Your name" Required="true"></asp:TextBox>

                    <label for="rating">Rating:</label>
                    <asp:DropDownList ID="rating" runat="server" Required="true">
                        <asp:ListItem Value="" Disabled="true" Selected="true">Select rating</asp:ListItem>
                        <asp:ListItem Value="5">5 stars</asp:ListItem>
                        <asp:ListItem Value="4">4 stars</asp:ListItem>
                        <asp:ListItem Value="3">3 stars</asp:ListItem>
                        <asp:ListItem Value="2">2 stars</asp:ListItem>
                        <asp:ListItem Value="1">1 star</asp:ListItem>
                    </asp:DropDownList>

                    <label for="comment">Comment:</label>
                    <asp:TextBox ID="comment" runat="server" placeholder="Your comment"></asp:TextBox>

                    <asp:Button ID="SubmitButton" CssClass="btnad" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
                </div>

                <div class="message" runat="server" id="message"></div>

                <div class="existing-ratings">
                    <h2>Existing Ratings</h2>
                    <table>
                        <tr>
                            <th>Name</th>
                            <th>Rating</th>
                            <th>Comment</th>
                        </tr>
                        <asp:Repeater runat="server" ID="ratingsRepeater">
                            <ItemTemplate>
                                <tr>
                                    <td><asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                                    <td><asp:Label runat="server" Text='<%# Eval("Rating") %>'></asp:Label> stars</td>
                                    <td><asp:Label runat="server" Text='<%# Eval("Comment") %>'></asp:Label></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
