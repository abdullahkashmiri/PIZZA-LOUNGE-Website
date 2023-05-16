<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="PIZZA_LOUNGE.User.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="../TemplateFiles/css/menupage.css"><!--linked css file of menupage-->

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
                            <asp:Button ID="addToCartButton" CssClass="btnad" runat="server" Text="Add to Cart" OnClick="addToCartButton_Click" />
                              <span id="PriceLabel" runat="server" class="pricead">RS:<%# Eval("Price") %></span>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

           </section>


</asp:Content>
