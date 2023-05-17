<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageReserve.aspx.cs" Inherits="PIZZA_LOUNGE.Admin.ManageReserve" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link rel="stylesheet" href="../TemplateFiles/css/managereserve.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <h1 class="heading">Manage Reservations</h1>
        <h2 class="subheading">Approve Or Decline</h2>
        <table class="reservation-table">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Day</th>
                    <th>Hour</th>
                    <th>Persons</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptReservations" runat="server" OnItemCommand="rptReservations_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("ID") %></td>
                            <td><%# Eval("Day") %></td>
                            <td><%# Eval("Hour") %></td>
                            <td><%# Eval("Persons") %></td>
                            <td>
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="approve-button" CommandName="Approve" CommandArgument='<%# Eval("ID") %>' />
                                <asp:Button ID="btnDecline" runat="server" Text="Decline" CssClass="decline-button" CommandName="Decline" CommandArgument='<%# Eval("ID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
