<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageJob.aspx.cs" Inherits="PIZZA_LOUNGE.Admin.ManageJob" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link rel="stylesheet" href="../TemplateFiles/css/managejob.css" />
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class="parentcontainer">
        <div class="container">
            <div class="heading">Manage Jobs</div>
            <div class="subheading">Approve or Decline</div>
            <asp:Repeater ID="rptJobs" runat="server" OnItemCommand="rptJobs_ItemCommand">
                <ItemTemplate>
                    <div class="job-container">
                        <div class="job-details">
                            <div class="job-name">Job ID: <%# Eval("job_id") %></div>
                            <div class="job-name">Name: <%# Eval("firstname") %> <%# Eval("lastname") %></div>
                            <div class="job-email">Email: <%# Eval("email") %></div>
                            <div class="job-location">Location: <%# Eval("city") %>, <%# Eval("zip") %></div>
                        </div>
                        <div class="job-actions">
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="approve-button" CommandName="Approve" CommandArgument='<%# Eval("job_id") %>' />
                            <asp:Button ID="btnDecline" runat="server" Text="Decline" CssClass="decline-button" CommandName="Decline" CommandArgument='<%# Eval("job_id") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div></asp:Content>
