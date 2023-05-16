<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Complaint.aspx.cs" Inherits="PIZZA_LOUNGE.User.Complaint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.3.0/css/all.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" >
    <link href="https://fonts.googleapis.com/css2?family=Source+Sans+Pro:wght@300;600&display=swap" rel="stylesheet">

        <link rel="stylesheet" href="../TemplateFiles/css/complaint.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div class="ufd">

            <div class="complaint-box-UF">
                
                <div class="title_uf">
                    <h2>COMPLAINTS OR SUGGESTIONS</h2>
                </div>

                <div class="half_uf">
                    <div class="item_uf">
                        <label for="usernameUF1">USERNAME</label>
                        <asp:TextBox ID="usernameUF" runat="server" required="required"></asp:TextBox>
                    </div>
                    <div class="item_uf">
                        <label for="emailUF1">EMAIL</label>
                        <asp:TextBox ID="emailUF" runat="server" required="required"></asp:TextBox>
                    </div>
                </div>

                <div class="full_uf">
                    <label for="messageUF1">ENTER COMPLAINTS OR SUGGESTIONS</label>
                    <textarea id="messageUF" runat="server" required="required"></textarea>
                </div>

                <div class="action_uf">
                    <asp:Button ID="sendbuttonUF" runat="server" Text="SEND" Onclick ="SendButton"/>
                    <input type="reset" value="RESET">
                </div>
                
                <div class="icons_uf">
                    <a href="https://www.facebook.com/people/The-Pizza-Lounge/100076377883236/" class="fa fa-facebook"></a>
                    <a href="https://www.instagram.com/thepizzalounge.pk/?hl=en" class="fa fa-instagram"></a>
                    <a href="https://goo.gl/maps/ofWZYKJMSKo2VFVr9" class="fa fa-map-marker"></a>
                </div>

            </div>
        </div>

</asp:Content>
