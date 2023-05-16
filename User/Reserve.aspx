<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Reserve.aspx.cs" Inherits="PIZZA_LOUNGE.User.Reserve" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../TemplateFiles/css/reserve.css"><!--linked css file of reserve-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <style>
        .bannerad {
             background-image: url("/TemplateFiles/menuimages/banner/img1.jpg");
            background-repeat: no-repeat;
            background-size: cover;
        }

    </style>
       <!--//---------------------------------------------//-->
    <div class="background-image"></div>

    <section class="bannerad">
        <h1>BOOK YOUR TABLE NOW</h1>
                <h1><asp:Label ID="SuccessLabel" runat="server" Text=""></asp:Label></h1>

        <div class="card-containerad">
            <div class="card-imgad">
                <!-- image here -->
                <img src="../TemplateFiles/menuimages/banner/img2.jpg" runat="server" />
            </div>
            <div class="card-contentad">
                <h3>Reservation</h3>
                <form action="#">
                    <div class="form-rowad">
                        <asp:DropDownList ID="daysDropdown" runat="server">
                            <asp:ListItem Value="day-select">Select Day</asp:ListItem>
                            <asp:ListItem Value="sunday">Sunday</asp:ListItem>
                            <asp:ListItem Value="monday">Monday</asp:ListItem>
                            <asp:ListItem Value="tuesday">Tuesday</asp:ListItem>
                            <asp:ListItem Value="wednesday">Wednesday</asp:ListItem>
                            <asp:ListItem Value="thursday">Thursday</asp:ListItem>
                            <asp:ListItem Value="friday">Friday</asp:ListItem>
                            <asp:ListItem Value="saturday">Saturday</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="hoursDropdown" runat="server">
                            <asp:ListItem Value="hour-select">Select Hour</asp:ListItem>
                            <asp:ListItem Value="10">10: 00</asp:ListItem>
                            <asp:ListItem Value="12">12: 00</asp:ListItem>
                            <asp:ListItem Value="14">14: 00</asp:ListItem>
                            <asp:ListItem Value="16">16: 00</asp:ListItem>
                            <asp:ListItem Value="18">18: 00</asp:ListItem>
                            <asp:ListItem Value="20">20: 00</asp:ListItem>
                            <asp:ListItem Value="22">22: 00</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-rowad">
                    <asp:TextBox ID="fullNameTextBox" runat="server" placeholder="Full Name"></asp:TextBox>
                    <asp:TextBox ID="phoneNumberTextBox" runat="server" placeholder="Phone Number"></asp:TextBox>
                    </div>
                    <div class="form-rowad">
                    <asp:TextBox ID="personsTextBox" runat="server" placeholder="How Many Persons?" type="number" min="1"></asp:TextBox>
                    <asp:Button ID="bookTableButton" runat="server" Text="BOOK TABLE" OnClick="SubmitButton_Click" />
                      </div>

                </form>

            </div>
        </div>
    </section>
</asp:Content>
