<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Tracking.aspx.cs" Inherits="PIZZA_LOUNGE.User.Tracking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
      <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.3.0/css/all.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" >
    <link href="https://fonts.googleapis.com/css2?family=Source+Sans+Pro:wght@300;600&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../TemplateFiles/css/tracking.css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
 <section class="step-wizard">
        <ul class="step-wizard-list">
            <li class="step-wizard-item">
                <span class="progress-count">1</span>
                <span class="progress-label">Cart</span>
            </li>
            <li class="step-wizard-item">
                <span class="progress-count">2</span>
                <span class="progress-label">Checkout</span>
            </li>
            <li class="step-wizard-item current-item">
                <span class="progress-count">3</span>
                <span class="progress-label">Preparing Order</span>
            </li>
            <li class="step-wizard-item">
                <span class="progress-count">4</span>
                <span class="progress-label">On the Way</span>
            </li>
            <li class="step-wizard-item">
                <span class="progress-count">5</span>
                <span class="progress-label">Expected Arrival</span>
            </li>
        </ul>
    </section>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script>
        var seconds = 0; // 30 minutes
        $(document).ready(function () {
            setInterval(updateTimer, 1000);
        });
        function updateTimer() {
            seconds++;
            if (seconds == 5) {
                $('.step-wizard-item:eq(2)').removeClass('current-item');
                $('.step-wizard-item:eq(2)').addClass('completed-item');
                $('.step-wizard-item:eq(3)').addClass('current-item');
            } else if (seconds == 10) {
                $('.step-wizard-item:eq(3)').removeClass('current-item');
                $('.step-wizard-item:eq(3)').addClass('completed-item');
                $('.step-wizard-item:eq(4)').addClass('current-item');
            } else if (seconds == 15) {
                $('.step-wizard-item:eq(4)').removeClass('current-item');
                $('.step-wizard-item:eq(4)').addClass('completed-item');
                $('.step-wizard-item:eq(5)').addClass('current-item');
                // Redirect to PageB.aspx
                window.location.href = './Rating.aspx';
                clearInterval();
            }
        }
    </script>

</asp:Content>
