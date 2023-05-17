<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="PIZZA_LOUNGE.Admin.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="../TemplateFiles/css/products.css"><!--linked css file of menupage-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

         <!--//---------------------------------------------//-->
    <!-- MENU SECTION STARTS HERE-->
    
    <style>

.hh31 {
    font-size: 2.5rem;
    color: var(--black);
    align-items: start;
     font-weight: bold;
}

.pp12 {
    font-size: 1.6rem;
    color: var(--lightcolor);
    padding: .5rem 0;
    line-height: 1.5;
}

    </style>


    <section class="pmenuad" id="pmenuad1">


        <h3 class="sub-headingad">our menu</h3>
<h1 class="headingad">today's special</h1>
<div class="box-containerad">
    <div class="rowad1">
        <asp:Repeater ID="RepeaterProducts" runat="server">
            <ItemTemplate>
                <div class="col-md-3ad1">
                    <div class="boxad">
                        <div class="imagead">
                            <asp:Image ID="ImageLabel" runat="server" AlternateText="" ImageUrl='<%# Eval("ImageUrl") %>' />
                            <a href="./Rating.aspx" class="fas fa-heart"></a>
                        </div>

                        <div class="contentad">
                            <div class="starsad">
 
                                <%# GetRatingStars(Convert.ToDouble(Eval("Rating"))) %>
                            </div>
                            <asp:Label ID="NameLabel" CssClass="hh31" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            <br />
                            <asp:Label ID="DescriptionLabel" CssClass="pp12" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            <br />
                       <asp:Button ID="deleteproduct" CssClass="btnad" runat="server" Text="Delete Item" OnClick="deleteproduct_Click" OnClientClick="return ShowConfirmationPanel();" />
       <span id="PriceLabel" runat="server" class="pricead">RS:<%# Eval("Price") %></span>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

           </section>

  
    <!-- Invisible panel for confirmation dialog -->
    <asp:Panel ID="ConfirmationPanel" runat="server" CssClass="confirmation-panel" style="display: none;">
        <div class="confirmation-box">
            <h3>Are you sure you want to delete the item?</h3>
            <div class="confirmation-buttons">
                <asp:Button ID="YesButton" runat="server" Text="Yes" CssClass="btn-confirmation" OnClick="YesButton_Click" />
                <asp:Button ID="NoButton" runat="server" Text="No" CssClass="btn-confirmation" OnClick="NoButton_Click" />
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript">
    function ShowConfirmationPanel() {
        document.getElementById('<%= ConfirmationPanel.ClientID %>').style.display = 'block';
        return false; // Prevent postback
    }
    </script>


</asp:Content>
