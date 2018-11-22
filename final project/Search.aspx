<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Recipe search</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="Default.css" />
    <link rel="stylesheet" href="Search.css" />
    <link rel="stylesheet" href="RecipeCard.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.0/umd/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtSearch]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Search.aspx/GetCustomers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1
            });
        });
    </script>
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
                            <li class="nav-item active"><a class="nav-link" href="Search.aspx">Recipe search</a></li>
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

            <div class="search-form">
                <center>
                       <label id="title">Recipe search</label>
                </center>
                <center>
                    <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" Width="400px" placeholder="Enter recipe name"></asp:TextBox><br />
                    <asp:Button ID="btnSearch" runat="server" Width="400px" OnClick="btnSearch_Click" Text="Search" />
                    <asp:GridView ID="GridView1"
                        runat="server"
                        AutoGenerateSelectButton="True"
                        CssClass="table table-hover table-striped"
                        GridLines="None"
                        SelectedIndex="1"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
<%--                        <Columns>
         <asp:BoundField DataField="nameField" HeaderText="Recipe name" 
            SortExpression="nameField" />
    </Columns>--%>
                    </asp:GridView>
                        </center>
                <div runat="server" id="container" visible="false">
                    <div class="grid-container">
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
                            <asp:Image ID="imgStar1" runat="server" alt="star" />
                        </div>
                    </div>
                    <div class="button-save">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save recipe" Visible="false" />
                        <asp:Label ID="lblSave" runat="server" Visible="false">Log in to save recipe</asp:Label>
                        <asp:Label ID="lblControl" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>

                <asp:Label ID="lblFeedback" runat="server" Visible="false"> You already have this recipe saved to your cookbook</asp:Label>
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
