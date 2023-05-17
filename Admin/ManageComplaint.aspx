<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageComplaint.aspx.cs" Inherits="PIZZA_LOUNGE.Admin.ManageComplaint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../TemplateFiles/css/managecomplaint.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="parentcontainer"> 
        <div class="container">
            <div class="heading">Manage Complaints</div>
            <div class="subheading">Approve or Decline</div>
        
            <asp:Repeater ID="rptComplaints" runat="server" OnItemCommand="rptComplaints_ItemCommand">
                <ItemTemplate>
                    <div class="complaint-container">
                        <div class="complaint-details">
                            <div class="complaint-name">Complaint ID: <%# Eval("complaint_id") %></div>
                            <div class="complaint-name">Username: <%# Eval("username") %></div>
                            <div class="complaint-email">Email: <%# Eval("email") %></div>
                            <div class="complaint-message">Message: <%# Eval("message") %></div>
                            <div class="complaint-time">Submit Time: <%# Eval("submit_time") %></div>
                        </div>
                        <div class="complaint-actions">
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="approve-button" CommandName="Approve" CommandArgument='<%# Eval("complaint_id") %>' />
                            <asp:Button ID="btnDecline" runat="server" Text="Decline" CssClass="decline-button" CommandName="Decline" CommandArgument='<%# Eval("complaint_id") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
