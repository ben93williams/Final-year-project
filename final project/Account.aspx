<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Account" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Account</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="Default.css" />
    <link rel="stylesheet" href="Account.css" />
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
                            <li class="nav-item active"><a class="nav-link" href="First.aspx">Home</a></li>
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
                                    class="glyphicon glyphicon-user"> Sign Up &nbsp</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnLogin"
                                    runat="server"
                                    OnClick="btnLogin_Click"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    class="glyphicon glyphicon-log-in"> Login</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnAccount"
                                    runat="server"
                                    OnClick="btnAccount_Click"
                                    Visible="false"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    class="glyphicon glyphicon-user"> Account &nbsp</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnSignOut"
                                    runat="server"
                                    OnClick="btnLogOut_Click"
                                    Visible="false"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    class="glyphicon glyphicon-remove"> Log Out</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </nav>
            </header>
                <div class="account">
                    <div class="title">
                        <label id="lblTitle">Account</label>
                    </div>
                    <div class="info-form" runat="server" id="info" Visible="true">
                        <div class="info-form-name">
                            <label id="lblName">Account name: </label>
                            <asp:Label ID="lblGetName" runat="server"></asp:Label>
                        </div>
                        <div class="info-form-email">
                            <label id="lblEmail">Account Email: </label>
                            <asp:Label ID="lblGetEmail" runat="server"></asp:Label>
                        </div>
                        <div class="info-form-count">
                            <label id="lblCount"> # of recipes<br />
                                in account: </label>
                            <asp:Label ID="lblGetAccount" runat="server"></asp:Label>
                        </div>
                        <div class="info-form-change">
                            <asp:Button ID="btnChange" runat="server" Text="Change password" OnClick="btnChange_Click" />
                        </div>
                    </div>
                    <div class="reset-form" runat="server" id="reset" Visible="false">
                        <div class="reset-form-old">
                            <asp:TextBox ID="txtOldPass" runat="server" placeholder="Enter current password"></asp:TextBox>
                        </div>
                        <div class="reset-form-new">
                            <asp:TextBox ID="txtNewPass" runat="server" placeholder="Enter new password"></asp:TextBox>
                        </div>
                        <div class="reset-form-confirm">
                            <asp:TextBox ID="txtConfirmPass" runat="server" placeholder="Confirm new password"></asp:TextBox>
                        </div>
                        <div class="reset-form-save">
                            <asp:Button ID="btnSave" runat="server" Text="Save new password" OnClick="btnSave_Click"/>
                        </div>
                        <div class="reset-form-feedback">
                            <asp:Label ID="lblFeedback" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            <div class="foot">
                <span class="span-foot">© 2018 Copyright: Benjamin Williams </span>
            </div>
        </div>
    </form>
 </body>
</html>