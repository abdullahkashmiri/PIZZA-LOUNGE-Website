<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PIZZA_LOUNGE.User.Login" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Login </title>
    <link rel="stylesheet" href="../TemplateFiles/css/login-style.css"/>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.3.0/css/all.min.css"/>
</head>
<body>
   
<style>
    .textbox{
        width: 92%;
        outline: none;
        border: 1px solid #fff;
        padding:  12px 20px;
        margin-bottom: 10px;
        border-radius: 20px;
        background: #e4e4e4;
    }
    .textbox:focus{
        border: 1.5px solid rgb(200, 200, 200);
    }

    .error{
        font-family: "Nunito", sans-serif;
        font-size: 0.76rem;
    }

    .button{
        width: 90%;
        font-size: 1rem;
        margin-top: 1.8rem;
        padding: 10px 0px;
        outline: none;
        border-radius: 20px;
        border: none;
        color: #fff;
        cursor: pointer;
        background: rgb(237, 23, 23);
    }
    .button:hover{
        background: rgba(237, 23, 23, 0.856);
    }

</style>

<script type="text/javascript">
    function callBackFunction() {
        window.location.href = "./Default.aspx";
    }
</script>

    <form id="form1" runat="server">
        <div class="login-box-QM">
            <div class="close">
                <a href="./Default.aspx" class="fas fa-arrow-alt-circle-left" style="font-size: 20px"></a>
            </div>
        
            
            <h1>Login</h1>
            <div>
                <asp:RequiredFieldValidator ID="RFV_username" CssClass="error" runat="server" ErrorMessage="Username Required"
                    ControlToValidate="username" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:TextBox ID="username" CssClass="textbox" runat="server" placeholder="Username"></asp:TextBox>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="RFV_pass" CssClass="error" runat="server" ErrorMessage="Password Required" 
                    ControlToValidate="password" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:TextBox ID="password" type="password" CssClass="textbox" runat="server" placeholder="password"></asp:TextBox>
            </div>

            <div class="recover-QM">
                <a href="#"> Forgot Password?</a>
            </div>

            <asp:Button ID="Login_btn" CssClass="button" runat="server" Text="Login" OnClick="Login_btn_Click" />

            <div class="member-QM">
                <label >Not a member? <a href="./SignUp.aspx">SignUp Now</a></label>
            </div>
        </div>
    </form>
</body>
</html>
