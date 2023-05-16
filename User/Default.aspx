<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PIZZA_LOUNGE.User.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

          <!--//---------------------------------------------//-->
    <!-- MAIN SLIDER PAGE START HERE-->
    <!--//---------------------------------------------//-->
    <section class="homead" id="homead">
        <div class="swiper homesliderad">
            <div class="swiper-wrapper wrapperad">
                <div class="swiper-slide slidead">
                    <div class="contentad">
                        <span>our special dishes</span>
                        <h3>delicious pizza</h3>
                        <p>
                            I love the taste of the fresh, crunchy lettuce leaves, the tangy
                            tomatoes and the cheese that melts in the mouth.
                        </p>
                      
                    <asp:Button ID="Btn2ad1" runat="server" CssClass="btnad" Text="Order Now"  PostBackUrl="Menu.aspx" />
                    </div>
                    <div class="imagead">
                        <img src="../TemplateFiles/menuimages/slider/img1.jpg" alt="">
                    </div>

                </div>

                <div class="swiper-slide slidead">
                    <div class="contentad">
                        <span>our special dishes</span>
                        <h3>juicy burgers</h3>
                        <p>
                            I love the taste of the fresh, crunchy lettuce leaves, the tangy
                            tomatoes and the cheese that melts in the mouth.
                        </p>
                        <%-- <a href="#" class="btnad">order now</a>--%>

                    <asp:Button ID="Btn2ad2" runat="server" CssClass="btnad" Text="Order Now"  PostBackUrl="Menu.aspx" />
                    </div>
                    <div class="imagead">
                        <img src="../TemplateFiles/menuimages/slider/img2.jpg" alt="">
                    </div>

                </div>
                <div class="swiper-slide slidead">
                    <div class="contentad">
                        <span>our special dishes</span>
                        <h3>fried chicken</h3>
                        <p>
                            I love the taste of the fresh, crunchy lettuce leaves, the tangy
                            tomatoes and the cheese that melts in the mouth.
                        </p>
 
                    <asp:Button ID="Btn2ad3" runat="server" CssClass="btnad" Text="Order Now"  PostBackUrl="Menu.aspx"  />
                    </div>
                    <div class="imagead">
                        <img src="../TemplateFiles/menuimages/slider/img3.jpg" alt="">
                    </div>
                </div>
            </div>
            <div class="swiper-pagination"></div>
        </div>
    </section>

    <!--//---------------------------------------------//-->
    <!-- MAIN SLIDER PAGE END HERE-->
    <!--//---------------------------------------------//-->

       
          <!--Most ordered section starts here-->
    <section class="menuad" id="menuad">

        <style>
 
.btnad:hover {
    background: var(--red);
    letter-spacing: 0rem;
}
.sr-only {
    position: absolute;
    width: 1px;
    height: 1px;
    padding: 0;
    margin: -1px;
    overflow: hidden;
    clip: rect(0, 0, 0, 0);
    border: 0;
}

        </style>

              <h3 class="sub-headingad">SPECIALITIES</h3>
<h1 class="headingad">most ordered</h1>
        <div class="box-containerad">
    <asp:Repeater ID="RepeaterSpecialities" runat="server">
        <ItemTemplate>
            <div class="boxad">
                <a href="./Rating.aspx" class="fas fa-heart"></a>
                <a href="./Menu.aspx" class="fas fa-eye"></a>
                 <asp:Image ID="ImageLabel" runat="server" AlternateText="" ImageUrl='<%# Eval("ImageUrl") %>' />
                 <asp:Label ID="NameLabel" CssClass="hh31" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                <div class="starsad">
                    <%#GetRatingStars(Convert.ToDouble(Eval("Rating"))) %>
                </div>
                 <span id="PriceLabel" runat="server" class="pricead">RS:<%# Eval("Price") %></span>
                <asp:Label ID="DescriptionLabel" CssClass="descriptionad sr-only" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                <asp:Button ID="OrderButton" runat="server" CssClass="btnad" Text="Order Now" OnClick="OrderButton_Click" CommandArgument='<%# Container.ItemIndex %>' />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

    </section>

    <!--Menu section ends here-->
    <!--//---------------------------------------------//-->


</asp:Content>
