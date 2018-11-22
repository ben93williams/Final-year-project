<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recipes.aspx.cs" Inherits="Recipe" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>My recipes</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="Default.css" />
    <link rel="stylesheet" href="RecipeCard.css" />
    <link rel="stylesheet" href="Recipes.css" />
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
                            <li class="nav-item active"><a class="nav-link" href="Recipes.aspx">My recipes</a></li>
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
            <div class="myRecipe">
                <label id="title">Your recipes</label>

                <asp:Label ID="lblFeedback" runat="server" Visible="false" />
                <div class="recipe-list">
                    <asp:GridView ID="GridView1"
                        runat="server"
                        AutoGenerateSelectButton="true"
                        CssClass="table table-hover table-striped"
                        GridLines="None"
                        SelectedIndex="1"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    </asp:GridView>

                </div>
                <div class="grid-container" 
                    runat="server"
                    id="theGrid">

                    <%--showing your stored recipes--%>
                    <div id="recipeLabels">
                        <asp:Label ID="lblRecipeName" runat="server" Visible="false" />
                    </div>

                    <div id="recipePicture">
                        <asp:Image ID="imgFood" Visible="false" runat="server" alt="image" />
                    </div>

                    <div id="recipeIngredients">
                        <span class="spnHeader">Ingredients:</span>
                        <br />
                        <asp:Label ID="lblIngredients" runat="server" Visible="false" />
                    </div>

                    <div id="recipeMethod">
                        <span class="spnHeader">Method:</span>
                        <br />
                        <asp:Label ID="lblMethod" runat="server" Visible="false" />
                    </div>

                    <div id="RecipeRating" runat="server">
                        <asp:Label ID="lblScore" runat="server" Visible="false" CssClass="score" />
                        <asp:Image ID="imgStar1" runat="server" Visible="false" />
                    </div>
                </div>
                <div style="clear: both"></div>

                <center>
                    <div class="buttons">
                        <asp:Button id="btnDelete" 
                            runat="server" 
                            Visible="false" 
                            Text="Delete recipe" 
                            OnClick="btnDelete_Click" />
                        <asp:Button ID="btnYes"
                            runat="server"
                            Visible="false"
                            text="Yes"
                            OnCLick="btnYes_Click" />
                        <asp:Button ID="btnNo"
                            runat="server"
                            Visible="false"
                            Text="No"
                            OnClick="btnNo_Click" />
                    </div>
                </center>
            </div>
            <div class="foot">
                <span class="span-foot">© 2018 Copyright: Benjamin Williams</span>
            </div>
        </div>

        <div runat="server" id="AlertBox" class="alertBox" visible="false">
            <div runat="server" id="AlertBoxMessage"></div>
            <button onclick="closeAlert.call(this, event)">Ok</button>
        </div>

    </form>

</body>

</html>
