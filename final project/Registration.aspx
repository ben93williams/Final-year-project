<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Registration</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="Default.css" />
    <link rel="stylesheet" href="Registration.css" />
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
                                    causesValidation ="false"
                                 
                                    class="glyphicon glyphicon-user"> Sign Up &nbsp  </asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnLogin"
                                    runat="server"
                                    OnClick="btnLogin_Click"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    causesValidation ="false"
                                    class="glyphicon glyphicon-log-in"> Login</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnAccount"
                                    runat="server"
                                    OnClick="btnAccount_Click"
                                    Visible="false"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    causesValidation ="false"
                                    class="glyphicon glyphicon-user"> Account &nbsp</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="btnSignOut"
                                    runat="server"
                                    OnClick="btnLogOut_Click"
                                    Visible="false"
                                    aria-hidden="true"
                                    Font-Underline="false"
                                    causesValidation ="false"
                                    class="glyphicon glyphicon-remove"> Log Out</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </nav>
            </header>
                <div class="registration-form">
                    <label id="lblTitle">REGISTER</label>
                    <div class="registration-form-name">
                        <asp:TextBox ID="txtFirstName" runat="server" placeholder="Enter First Name"></asp:TextBox>
                        <asp:TextBox ID="txtLastName" runat="server" placeholder="Enter Last Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="firstNameValidator"
                            runat="server" 
                            ControlToValidate="txtFirstName"
                            ErrorMessage="Please enter your first name"
                            Enabled="false"
                           
                                    validationGroup ="personal-info"

                            style="color:red"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RequiredFieldValidator ID="lastNameValidator"
                            runat="server" 
                            ControlToValidate="txtLastName"
                            ErrorMessage="Please enter your last name"
                            Enabled="false"
                           
                                    validationGroup ="personal-info"

                            style="color:red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="registration-form-email">
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Enter Email"></asp:TextBox>
                    <br />
                        <asp:RequiredFieldValidator ID="emailValidator"
                            runat="server" 
                            ControlToValidate="txtEmail"
                            ErrorMessage="Please enter your Email"
                            Enabled="false"
                           
                                    validationGroup ="personal-info"

                            style="color:red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="registration-form-password">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="passwordValidator"
                            runat="server" 
                            ControlToValidate="txtPassword"
                            ErrorMessage="Please enter your password"
                            Enabled="false"
                           
                                    validationGroup ="personal-info"

                            style="color:red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="registration-form-validation">
                        <asp:TextBox ID="txtPasswordValidator" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="confirmValidator"
                            runat="server" 
                            ControlToValidate="txtPasswordValidator"
                            ErrorMessage="Please Confirm your password"
                            Enabled="false"
                           
                                    validationGroup ="personal-info"

                            style="color:red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="registration-form-register">
                        <asp:Button ID="btnRegister"
                            runat="server"
                            Text="Register"
                            OnClick="btnRegister_Click"
                            validationgroup="personal-info"
                            /><br />
                            <asp:Label ID="lblFeedback" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="registration-form-login">
                        <span>Already have an account?</span>
                        <br />
                        <asp:LinkButton ID="btnLogger"
                            runat="server"
                            OnClick="btnLogger_Click">Log in</asp:LinkButton>
                    </div>
                </div>
           
            <div class="foot">
            <span class="span-foot">© 2018 Copyright: Benjamin Williams </span>
        </div>
        </div>

        
    </form>

</body>
</html>
