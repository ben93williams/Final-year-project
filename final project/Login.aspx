<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1" />
    <link rel="stylesheet" type="text/css" href="Default.css" />
    <link rel="stylesheet" type="text/css" href="Login.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.0/umd/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="wrap">
            <header class="header">
                <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
                    <a class="navbar-brand" href="First.aspx">RecipeBook</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarCollapse">
                        <ul class="navbar-nav mr-auto">
                            <li class="nav-item"><a class="nav-link" href="First.aspx">Home</a></li>
                            <li class="nav-item"><a class="nav-link" href="Creator.aspx">Recipe creator</a></li>
                            <li class="nav-item"><a class="nav-link" href="Search.aspx">Recipe search</a></li>
                            <li class="nav-item"><a class="nav-link" href="Recipes.aspx">My recipes</a></li>
                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <asp:LinkButton ID="btnSignUp"
                                    runat="server"
                                    OnClick="btnSignUp_Click"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    causesvalidation="false"
                                    class="glyphicon glyphicon-user"> Sign Up &nbsp</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnLogin"
                                    runat="server"
                                    OnClick="btnLogin_Click"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    causesvalidation="false"
                                    class="glyphicon glyphicon-log-in"> Login</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnAccount"
                                    runat="server"
                                    OnClick="btnAccount_Click"
                                    Visible="false"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    causesvalidation="false"
                                    class="glyphicon glyphicon-user"> Account &nbsp</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnSignOut"
                                    runat="server"
                                    OnClick="btnLogOut_Click"
                                    Visible="false"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    causesvalidation="false"
                                    class="glyphicon glyphicon-remove"> LogOut</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </nav>
            </header>

            <div class="login-form">
                <label id="lblTitle">LOG IN</label>
                <div class="login-form-email">
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" TextMode="Email" AutoComplete="off"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="emailValidator"
                        runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Please enter your Email"
                        Enabled="false"
                        validationgroup="login-details"
                        Style="color: red"></asp:RequiredFieldValidator>
                </div>
                <div class="login-form-password">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" AutoComplete="off"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="passwordValidator"
                        runat="server"
                        ControlToValidate="txtPassword"
                        ErrorMessage="Please enter your password"
                        Enabled="false"
                        validationgroup="login-details"
                        Style="color: red"></asp:RequiredFieldValidator>
                </div>
                <div class="login-form-submit">
                    <asp:Button ID="btnSubmit" 
                        runat="server" 
                        validationgroup="login-details" 
                        OnClick="btnLogin_Click" 
                        Text="SIGN IN" />
                    <br />
                    <asp:Label ID="lblFeedback" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="login-form-remember">
                    <asp:CheckBox ID="cbxRemeber" runat="server" Name="remember" />
                    <span id="span-remember">Remember me</span>
                </div>
                <div class="login-form-lost">
                    <asp:LinkButton ID="btnLost"
                        runat="server"
                        OnClick="btnLost_Click">Forgot password?</asp:LinkButton>
                </div>
                <div class="login-form-register">
                    <span>Haven't created an account yet?</span>
                    <br />
                    <asp:LinkButton ID="btnRegister"
                        runat="server"
                        OnClick="btnRegister_Click">Register</asp:LinkButton>
                </div>
            </div>
            <div class="foot">
                <span class="span-foot">© 2018 Copyright: Benjamin Williams </span>
            </div>
        </div>
        <div runat="server" id="AlertBox" class="alertBox" visible="false">
            <div runat="server" id="AlertBoxMessage"></div>
            <button onclick="closeAlert.call(this, event)">Ok</button>
        </div>
    </form>
</body>
</html>
