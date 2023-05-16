<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="PIZZA_LOUNGE.User.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="../TemplateFiles/css/cart.css"><!--linked css file of menupage-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <style>

        .hh31 {
            font-size: 2.5rem;
            color: var(--black);
            align-items: start;
            font-weight: bold;
        }

        .content-crt {
            font-size: 2rem;
            color: var(--lightcolor);
            padding: .5rem 0;
            line-height: 1.5;
        }

        .btnrem {
            margin: 1rem;
            display: inline-block;
            font-size: 1.7rem;
            color: #fff;
            background: var(--red);
            border-radius: .5rem;
            cursor: pointer;
            padding: .8rem 3rem;
        }
        .btnrem:hover {
            background-color: rgba(237, 23, 23, 0.856);
            font-size: 1.8rem;
        }

        .disc{
            margin: 1rem;
            display: inline-block;
            font-size: 1.3rem;
            color: #000/*#fff*/;
            background-color: rgb(60 235 52);
            border-radius: 1.8rem;
            margin-right:auto;
            cursor: pointer;
            padding: .55rem 1.86rem;
        }

        .total_bar {
            display:flex;
            width: auto;
            margin-top: 2.5rem;
            height: auto;
            box-shadow: 0px 23px 35px rgba(0, 0, 0, 0.2);
            border: .1rem solid rgba(0,0,0,.2);
            background-color: #fff;
            border-radius: 1.5rem;
            justify-content: space-between;
            justify-content: center;
        }

        .total_bar .hh31{
            font-size: 2rem;
            margin: 1rem;
            text-align:center;
        }

        .total_bar div{
            text-align: center;
            align-content: center;
        }
    </style>


    <section class="cart" id="sect1">

        
<div class="box-container-crt">
    <div class="rowad1">
        <asp:Repeater ID="Repeatercart" runat="server">
            <ItemTemplate>
                    <div class="box-crt">
                        <div>
                            <asp:Image ID="ImageLabel" CssClass="image-crt" runat="server" AlternateText="" ImageUrl='<%# Eval("ImageUrl") %>' />
                            
                            <asp:Label ID="product_id" visible="false" runat="server" Text='<%# Eval("ProductId") %>'></asp:Label>
                        </div>

                        <div>
                            <br/><br/>
                            <asp:Label ID="NameLabel" CssClass="name hh31" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            <%--<asp:Label ID="Label1" CssClass="name hh31" runat="server" Text='<%# Eval("Name") %>'></asp:Label>--%>
                            <asp:Label ID="Label1" CssClass="hh31 name" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                        </div>

                        <div>
                            <br />
                            <asp:Button ID="sub" CssClass="btn-crt" runat="server" Text="-" OnClick="sub_Button_Click" />
                            <span id="quantity" runat="server" Class="text-crt"> <%# Eval("Quantity") %> </span>
                            <asp:Button ID="add" CssClass="btn-crt" runat="server" Text="+" OnClick="add_Button_Click" />
                            <br/><br/>
                            <span id="PriceLabel" runat="server" class="text-crt"><%# Eval("Price") %> Rs</span>
                            <br/>
                            <asp:Button ID="rem" CssClass="btnrem" runat="server" Text="Remove" OnClick="rem_Button_Click" />
                        </div>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

        <div class="total_bar">
            <div>
                <asp:Button ID="dis_notify" CssClass="disc" Visible="false" runat="server" Text="Discount Applied"/>
                <asp:Label ID="t_price" CssClass="hh31" runat="server" Text='Total Price: 0 Rs'></asp:Label>
                <asp:Button ID="clr" CssClass="btnrem" runat="server" Text="Clear" OnClick="clr_Button_Click"/>
                <asp:Button ID="ord" CssClass="btnrem" runat="server" Text="Order"  OnClick="ord_Button_Click"/>
            </div>
        </div>

</section>
</asp:Content>
