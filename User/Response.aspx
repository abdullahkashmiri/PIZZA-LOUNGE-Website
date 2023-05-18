<%--<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Response.aspx.cs" Inherits="PIZZA_LOUNGE.User.Response" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="../TemplateFiles/css/response.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="parentcontainer"> 
        <div class="container">
            <div class="heading">Account Details</div>
        <ItemTemplate>
                    <div class="user-container">
                        <div class="user-details">
                            <div class="user-id">Complaint ID: <%# Eval("complaint_id") %></div>
                            <div class="user-name">Username: <%# Eval("username") %></div>
                            <div class="user-email">Email: <%# Eval("email") %></div>
                        </div>
                    </div>
                </ItemTemplate>

            <div class="heading">Orders Details and Status</div>
        
        <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
            <ItemTemplate>
                <div class="order-container">
                    <div class="order-details">
                        <div class="order-name">Order No: <%# Eval("OrderNo") %></div>
                        <div class="order-name">User ID: <%# Eval("UserId") %></div>
                        <div class="order-price">Order Date: <%# Eval("OrderDate", "{0:yyyy-MM-dd}") %></div>
                        <div class="order-price">Price: <%# Eval("Price", "{0:C}") %></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

            <div class="heading">Complaints and Status</div>
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
                         
                    </div>
                </ItemTemplate>
            </asp:Repeater>


            <div class="heading">Jobs Application and Status</div>
            <asp:Repeater ID="rptJobs" runat="server" OnItemCommand="rptJobs_ItemCommand">
                <ItemTemplate>
                    <div class="job-container">
                        <div class="job-details">
                            <div class="job-name">Job ID: <%# Eval("job_id") %></div>
                            <div class="job-name">Name: <%# Eval("firstname") %> <%# Eval("lastname") %></div>
                            <div class="job-email">Email: <%# Eval("email") %></div>
                            <div class="job-location">Location: <%# Eval("city") %>, <%# Eval("zip") %></div>
                        </div>
                        
                    </div>
                </ItemTemplate>
            </asp:Repeater>


              



              <h1 class="heading">Reservations and Status </h1>
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
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>


        </div>
    </div>


</asp:Content>--%>












<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Response.aspx.cs" Inherits="PIZZA_LOUNGE.User.Response" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="../TemplateFiles/css/response.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="parentcontainer"> 
        <div class="container">
            <div class="heading">Account Details</div>
         <asp:Repeater ID="rptUsers" runat="server">
    <ItemTemplate>
        <div class="user-container">
            <div class="user-details">
                <div class="user-id">User ID: <%# Eval("UserId") %></div>
                <div class="user-name">Username: <%# Eval("User_name") %></div>
                <div class="user-email">Email: <%# Eval("Email") %></div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

            <div class="heading">Orders Details and Status</div>

            <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
                <ItemTemplate>
                    <div class="order-container">
                        <div class="order-details">
                            <div class="order-name">Order No: <%# Eval("OrderNo") %></div>
                            <div class="order-name">User ID: <%# Eval("UserId") %></div>
                            <div class="order-price">Order Date: <%# Eval("OrderDate", "{0:yyyy-MM-dd}") %></div>
                            <div class="order-price">Price: <%# Eval("Price", "{0:C}") %></div>
                            <div class="status-label">
                                <%-- Add the status label for approved/rejected here --%>
                                <%# GetStatusLabel(Eval("status")) %>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div class="heading">Complaints and Status</div>
            <asp:Repeater ID="rptComplaints" runat="server" OnItemCommand="rptComplaints_ItemCommand">
                <ItemTemplate>
                    <div class="complaint-container">
                        <div class="complaint-details">
                            <div class="complaint-name">Complaint ID: <%# Eval("complaint_id") %></div>
                            <div class="complaint-name">Username: <%# Eval("username") %></div>
                            <div class="complaint-email">Email: <%# Eval("email") %></div>
                            <div class="complaint-message">Message: <%# Eval("message") %></div>
                            <div class="complaint-time">Submit Time: <%# Eval("submit_time") %></div>
                            <div class="status-label">
                                <%-- Add the status label for approved/rejected here --%>
                                <%# GetStatusLabel(Eval("status")) %>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>


                       <div class="heading">Jobs Application and Status</div>
            <asp:Repeater ID="rptJobs" runat="server" OnItemCommand="rptJobs_ItemCommand">
                <ItemTemplate>
                    <div class="job-container">
                        <div class="job-details">
                            <div class="job-name">Job ID: <%# Eval("job_id") %></div>
                            <div class="job-name">Name: <%# Eval("firstname") %> <%# Eval("lastname") %></div>
                            <div class="job-email">Email: <%# Eval("email") %></div>
                            <div class="job-location">Location: <%# Eval("city") %>, <%# Eval("zip") %></div>
                            <div class="status-label">
                                <%-- Add the status label for approved/rejected here --%>
                                <%# GetStatusLabel(Eval("status")) %>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <h1 class="heading">Reservations and Status </h1>
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
                                    <%-- Add the status label for approved/rejected here --%>
                                    <div class="status-label">
                                        <%# GetStatusLabel(Eval("status")) %>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
