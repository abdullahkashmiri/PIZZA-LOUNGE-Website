<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Job.aspx.cs" Inherits="PIZZA_LOUNGE.User.Job" %>
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

            <div class="job-box-UF">
                
                <div class="title_uf">
                    <h2>APPLY FOR JOB</h2>
                </div>

                <div class="half_uf">
                    <div class="item_uf">
                        <label for="firstnameUF1">FIRST NAME</label>
                        <asp:TextBox ID="firstnameUF" runat="server" required="required"></asp:TextBox>
                    </div>
                    <div class="item_uf">
                        <label for="lastnameUF1">LAST NAME</label>
                        <asp:TextBox ID="lastnameUF" runat="server" required="required"></asp:TextBox>
                    </div>
                </div>

                <div class="half_uf">
                    <div class="item_uf">
                        <label for="cityUF1">CITY</label>
                        <asp:TextBox ID="cityUF" runat="server" required="required"></asp:TextBox>
                    </div>
                    <div class="item_uf">
                        <label for="zipUF1">PHONE NUMBER</label>
                        <asp:TextBox ID="zipUF" type="number" runat="server" required="required"></asp:TextBox>
                    </div>
                </div>

                <div class="half_uf">
                    <div class="item_uf">
                        <label for="emailUF1">EMAIL</label>
                        <asp:TextBox ID="emailUF" runat="server" required="required"></asp:TextBox>
                    </div>
                    <div class="item_uf">
                        <label for="cvUF1">UPLOAD YOUR CV</label>
                        <asp:FileUpload ID="cvUF" runat="server" accept=".pdf" />
                    </div>
                </div>

                <div class="action_uf">
                    <asp:Button ID="sendbuttonUF1" runat="server" Text="SEND" Onclick ="SendButton1"/>
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
