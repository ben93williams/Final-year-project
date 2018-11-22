<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Creator.aspx.cs" Inherits="Creator" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Recipe Creator</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="Default.css" />
    <link rel="stylesheet" href="Creator.css" />
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
                            <li class="nav-item active"><a class="nav-link" href="Creator.aspx">Recipe creator</a></li>
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

            <div class="create">
                <center>
                        <label id="title">Create your recipe</label>
                    </center>
                <div class="create-form">
                    <div class="right-form">
                        <div class="create-form-name">
                            <center>
                            <asp:label id="lblRecipeName" runat="server">Enter Recipe name:</asp:label><br />
                            
                                <asp:textbox id="txtRecipeName" runat="server" placeholder="Recipe name"></asp:textbox>
                                </center>
                        </div>
                        <div class="create-form-method">
                            <center>
                            <asp:label id="lblMethod" runat="server">Enter Method</asp:label><br />
                                <asp:textbox id="txtMethod" runat="server" TextMode="MultiLine" placeholder="Method"></asp:textbox>
                                </center>
                        </div>
                        <div class="create-form-time">
                            <center>
                            <asp:label id="lblTime" runat="server">Enter overall time (minutes)</asp:label><br />
                                <asp:textbox id="txtTime" runat="server" placeholder="Time"></asp:textbox>
                                </center>
                        </div>
                    </div>
                    <div class="left-form">
                        <div class="create-form-add">
                            <asp:Button ID="btnAdd" runat="server" Text="Add new ingredient" OnClick="btnAdd_Click" />
                        </div>
                        <div class="create-form-ingredient">
                            <asp:Label ID="lblIngredient" Visible="false" runat="server">enter ingredient</asp:Label>
                            <asp:Panel ID="pnlIngredient" runat="server"></asp:Panel>
                        </div>
                        <div class="create-form-amount">
                            <asp:Label ID="lblAmount" Visible="false" runat="server">enter ingredient amount</asp:Label>
                            <asp:Panel ID="pnlAmount" runat="server"></asp:Panel>
                        </div>
                        <div class="create-form-unit">
                            <asp:Label ID="lblUnit" Visible="false" runat="server">enter unit</asp:Label>
                            <asp:Panel ID="pnlUnit" runat="server"></asp:Panel>
                        </div>
                    </div>
                    <div style="clear: both"></div>
                    <div class="bottom-form">
                        <div class="create-form-save">
                            <center>
                            <asp:button id="btnSave" runat="server" text="Save Recipe" onclick="btnSave_Click" />
                                </center>
                        </div>
                        <div class="create-form-feedback">
                            <asp:Label ID="lblFeedback" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                
            </div>
            <div class="foot">
                <span class="span-foot">© 2018 Copyright: Benjamin Williams </span>
            </div>
            
            <div runat="server" id="AlertBox" class="alertBox" visible="false">
                <div runat="server" id="AlertBoxMessage"></div>
                <button onclick="closeAlert.call(this, event)">Ok</button>
            </div>
        </div>
    </form>
</body>
</html>
