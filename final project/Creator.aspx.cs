using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;

public partial class Creator : System.Web.UI.Page
{
    //pre loads page in order to create multiple dynamic textboxes

    List<string> controlIdList = new List<string>();
    List<string> controlIdAmountList = new List<string>();
    List<string> controlIdUnitList = new List<string>();
    int counter = 0;

    protected override void LoadViewState(object savedState)
    {
        base.LoadViewState(savedState);
        controlIdList = (List<string>)ViewState["controlIdList"];
        controlIdAmountList = (List<string>)ViewState["controlIdAmountList"];
        controlIdUnitList = (List<string>)ViewState["controlIdUnitList"];


        foreach (string Id in controlIdList)
        {
            counter++;
            TextBox tbi = new TextBox();
            LiteralControl lineBreak = new LiteralControl("<br />");

            tbi.ID = Id;
            tbi.CssClass = "dynamic-controls-textbox";

            pnlIngredient.Controls.Add(tbi);
            pnlIngredient.Controls.Add(lineBreak);
        }
        foreach (string IdA in controlIdAmountList)
        {
            TextBox tba = new TextBox();
            LiteralControl lineBreak = new LiteralControl("<br />");

            tba.ID = IdA;
            tba.CssClass = "dynamic-controls-textbox";

            pnlAmount.Controls.Add(tba);
            pnlAmount.Controls.Add(lineBreak);
        }
        foreach (string IdU in controlIdUnitList)
        {
            DropDownList ddl = new DropDownList();
            LiteralControl lineBreak = new LiteralControl("<br />");

            ddl.ID = IdU;

            ddl.Items.Add("");
            ddl.Items.Add("tsp");
            ddl.Items.Add("tbsp");
            ddl.Items.Add("fl oz");
            ddl.Items.Add("oz");
            ddl.Items.Add("g");
            ddl.Items.Add("ml");

            ddl.CssClass = "dynamic-controls";

            pnlUnit.Controls.Add(ddl);
            pnlUnit.Controls.Add(lineBreak);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            btnLogin.Visible = false;
            btnSignUp.Visible = false;
            btnSignOut.Visible = true;
            btnAccount.Visible = true;
        }
        else
        {
            Session["redir"] = "Creator.aspx";
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e) //calls functions for new textboxes when button clicked
    {
        var UID = Session["New"];

        lblAmount.Visible = true;
        lblIngredient.Visible = true;
        lblUnit.Visible = true;

        counter++;
        TextBox tbi = new TextBox();
        TextBox tba = new TextBox();
        DropDownList ddl = new DropDownList();

        tbi.ID = "txtIngredient" + counter;
        tbi.CssClass = "dynamic-controls-textbox";
        //tbi.Text = "txtIngredient" + counter;

        tba.ID = "txtAmount" + counter;
        tba.CssClass = "dynamic-controls-textbox";
       // tba.Text = "txtAmount" + counter;

        ddl.ID = "ddlUnit" + counter;
        ddl.CssClass = "dynamic-controls";
        ddl.Items.Add("");
        ddl.Items.Add("tsp");
        ddl.Items.Add("tbsp");
        ddl.Items.Add("fl oz");
        ddl.Items.Add("oz");
        ddl.Items.Add("mg");
        ddl.Items.Add("ml");

        pnlIngredient.Controls.Add(tbi);
        pnlAmount.Controls.Add(tba);
        pnlUnit.Controls.Add(ddl);

        controlIdList.Add(tbi.ID);
        controlIdAmountList.Add(tba.ID);
        controlIdUnitList.Add(ddl.ID);

        ViewState["controlIdList"] = controlIdList;
        ViewState["controlIdAmountList"] = controlIdAmountList;
        ViewState["controlIdUnitList"] = controlIdUnitList;
    }

    protected void btnSave_Click(object sender, EventArgs e) //saves all user inputed data to the correct database fields 
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecipeBookConnectionString"].ConnectionString);
        List<Int32> IID = new List<Int32>();
        var RID = "";
        var UID = Session["New"];
        var amountCounter = 1;
        var idCounter = 0;
        var counter = 0;
        var reset = 0;

        con.Open();

        if (txtRecipeName.Text == "")
        {
            lblFeedback.Visible = true;
            lblFeedback.Text = "Please enter recipe name";
        }
        else if (txtMethod.Text == "")
        {
            lblFeedback.Visible = true;
            lblFeedback.Text = "Please enter recipe name";
        }
        else if (txtTime.Text == "")
        {
            lblFeedback.Visible = true;
            lblFeedback.Text = "Please enter recipe name";
        }
        else
        {
            try
            {
                SqlCommand InsertCommand = con.CreateCommand();

                InsertCommand.CommandText = "INSERT Recipe(Recipe_Name, Instructions, Time) VALUES(@RName, @RInstructions, @RTime); SELECT SCOPE_IDENTITY()";
                InsertCommand.Parameters.AddWithValue("@RName", txtRecipeName.Text);
                InsertCommand.Parameters.AddWithValue("@RInstructions", txtMethod.Text);
                InsertCommand.Parameters.AddWithValue("@RTime", txtTime.Text);

                RID = InsertCommand.ExecuteScalar().ToString();

                lblFeedback.Visible = true;
                lblFeedback.Text = "Recipe Created";

                foreach (TextBox textBox in pnlIngredient.Controls.OfType<TextBox>())
                {
                    if (textBox.Text == "")
                    {
                        
                    }
                    else
                    {

                    SqlCommand IngredientCommand = con.CreateCommand();

                    IngredientCommand.CommandText = "SELECT COUNT(*) FROM Ingredients WHERE Ingredient = @ingredients";
                    IngredientCommand.Parameters.AddWithValue("@ingredients", textBox.Text);
                    int Temp = Convert.ToInt32(IngredientCommand.ExecuteScalar().ToString());

                    

                        if (Temp == 1)
                        {
                            SqlCommand getIdCommand = con.CreateCommand();

                            getIdCommand.CommandText = "SELECT Ingredients_ID FROM Ingredients WHERE Ingredient = @ingredient";
                            getIdCommand.Parameters.AddWithValue("@ingredient", textBox.Text);
                            IID.Add(Convert.ToInt32(getIdCommand.ExecuteScalar().ToString()));
                            getIdCommand.Parameters.RemoveAt("@ingredient");

                            SqlCommand insCommand = con.CreateCommand();
                            insCommand.CommandText = "INSERT RecipeIngredient(Ingredient_ID, Recipe_ID, Amount_of) VALUES(@ing, @RID, @reset)";
                            var ing = IID[counter];
                            insCommand.Parameters.AddWithValue("@ing", ing);
                            insCommand.Parameters.AddWithValue("@RID", RID);
                            insCommand.Parameters.AddWithValue("@reset", reset);
                            insCommand.ExecuteNonQuery();
                            IngredientCommand.Parameters.RemoveAt("@Ingredients");

                        }
                        else
                        {
                            SqlCommand setIdCommand = con.CreateCommand();

                            setIdCommand.CommandText = "INSERT Ingredients(ingredient) VALUES(@ingredient); SELECT SCOPE_IDENTITY()";
                            setIdCommand.Parameters.AddWithValue("@ingredient", textBox.Text);
                            IID.Add(Convert.ToInt32(setIdCommand.ExecuteScalar().ToString()));
                            setIdCommand.Parameters.RemoveAt("@ingredient");

                            SqlCommand insCommand = con.CreateCommand();

                            insCommand.CommandText = "INSERT RecipeIngredient(Ingredient_ID, Recipe_ID, Amount_of) VALUES(@ing, @RID, @reset)";

                            var ing = IID[counter];
                            insCommand.Parameters.AddWithValue("@ing", ing);
                            insCommand.Parameters.AddWithValue("@RID", RID);
                            insCommand.Parameters.AddWithValue("@reset", reset);
                            insCommand.ExecuteNonQuery();
                            IngredientCommand.Parameters.RemoveAt("@Ingredients");

                        }
                        counter++;
                    }
                }

                foreach (TextBox textBox in pnlAmount.Controls.OfType<TextBox>())
                {

                    if (textBox.Text == "")
                    {

                    }
                    else
                    {
                        SqlCommand AmountCommand = con.CreateCommand();

                        AmountCommand.CommandText = "UPDATE RecipeIngredient SET Amount_of = @temp WHERE Ingredient_ID = @ing";

                        string amount = "txtAmount" + amountCounter;
                        string ID = "ddlUnit" + amountCounter;

                        TextBox text = this.FindControl(amount) as TextBox;
                        DropDownList ddl = this.FindControl(ID) as DropDownList;

                        string test = text.Text;
                        string unit = (ddl.SelectedItem.Value).ToString();

                        var temp = test + unit;
                        var ing = IID[idCounter];
                        idCounter++;

                        AmountCommand.Parameters.AddWithValue("@temp", temp);
                        AmountCommand.Parameters.AddWithValue("@ing", ing);


                        AmountCommand.ExecuteNonQuery();

                        AmountCommand.Parameters.RemoveAt("@temp");
                        AmountCommand.Parameters.RemoveAt("@ing");

                        amountCounter++;
                    }
                }
                    SqlCommand setRecipeUserCommand = con.CreateCommand();

                    setRecipeUserCommand.CommandText = "INSERT RecipeUsers(Recipe_ID, ID) VALUES(@RID, @UID)";
                    setRecipeUserCommand.Parameters.AddWithValue("@RID", RID);
                    setRecipeUserCommand.Parameters.AddWithValue("@UID", UID);
                    setRecipeUserCommand.ExecuteNonQuery();
                    con.Close();
                }
            
            catch (SqlException exc)
            {
                lblFeedback.Visible = true;
                lblFeedback.Text = "ERROR IN CREATING RECIPE" + exc;
            }
        }
    }

        private void MessageBoxShow(string message)
    {
        this.AlertBoxMessage.InnerText = message;
        this.AlertBox.Visible = true;
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session["New"] = null;
        Response.Redirect("First.aspx");
    }
    protected void btnAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("Account.aspx");
    }
}
