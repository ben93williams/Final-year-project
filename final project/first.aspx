<%@ Page Language="C#" AutoEventWireup="true" CodeFile="first.aspx.cs" Inherits="first" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Home</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1, user-scalable=yes" />
    <link rel="stylesheet" type="text/css" href="Default.css" />
    <link rel="stylesheet" type="text/css" href="first.css" />
    <link rel="stylesheet" type="text/css" href="RecipeCard.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.0/umd/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>
    <link rel="shortcut icon" href="#">
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrap">
            <div class="header">
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
            </div>

            <%--https://www.aspsnippets.com/Articles/AJAX-AutoCompleteExtender-Example-in-ASPNet.aspx--%>

            <div class="ROTD">
                <center>
                       <label id="title">Welcome to RecipeBook </label>
                </center>
                <div class="web-info">
                    <span>
                        <p>
                            Welcome to RecipeBook,<br />
                            This website will help you create your own personal cookbook full of your favourite recipes,
                                <br />
                            create discover and share your own recipes with everyone else,<br />
                            adding hidden gems you may find to your customized cookbook.
                        </p>
                        <p>
                            This website was developed as part of my final year project for the university of plymouth, for personal enquiries please contact
                            via ben93williams@gmail.com.
                        </p>
                    </span>
                    <div id="bottom-border-line"></div>
                </div>
                <center>
                    <div>
                        <span id="ROTD-title">Random Recipe:</span>
                    </div>
                </center>
                <div class="grid-container" runat="server" id="recipeCard">

                    <%--Random Recipe--%>
                    <div id="recipePicture">
                        <asp:Image ID="imgFood" runat="server" alt="image" />
                    </div>

                    <div id="recipeLabels">
                        <asp:Label ID="lblRecipeName" runat="server"></asp:Label>
                    </div>

                    <div id="recipeIngredients">
                        <span class="spnHeader">Ingredients:</span>
                        <br />
                        <asp:Label ID="lblIngredients" runat="server" />
                    </div>
                    
                    <div id="recipeMethod">
                        <span class="spnHeader">Method:</span>
                        <br />
                        <asp:Label ID="lblMethod" runat="server" />
                    </div>

                    <div id="RecipeRating" runat="server">
                        <asp:Label ID="lblScore" runat="server" Visible="true" Text="0" CssClass="score" />
                        <asp:Image ID="imgStar1" runat="server" alt="star" ImageUrl="~\\Images\\stars.jpg" Visible="false" />
                    </div>
                </div>
                <div class="button-save">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save recipe" Visible="false" />
                    <asp:Label ID="lblSave" runat="server" Visible="true">Log in to save recipe</asp:Label>
                    <asp:Label ID="lblControl" runat="server" Visible="false"></asp:Label>
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
