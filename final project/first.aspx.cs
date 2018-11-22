using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

public partial class first : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecipeBookConnectionString"].ConnectionString);
    
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Visible = false;
            lblSave.Visible = true;
        }
        else
        {
            btnSave.Visible = true;
            lblSave.Visible = false;
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
            btnSave.Visible = true;
            lblSave.Visible = false;

        }
        var R_Id = "";

        R_Id = getRecipeId();

        if (R_Id == null)
        {
            recipeCard.Visible = false;
        }
        else
        {
            Session["recipe"] = R_Id;
            //get recipe name
            getRecipeName(R_Id);
            //gets the recipe pic
            getRecipePic(R_Id);
            //gets the recipe ingredients
            getRecipeIngredients(R_Id);
            //gets the recipe method
            getRecipeMethod(R_Id);
            //gets the rating
            getRecipeRating(R_Id);

            con.Close();
        }
    }



    protected string getRecipeId()
    {

        SqlCommand getIdCommand = con.CreateCommand();
        getIdCommand.CommandText = "SELECT TOP 1 Recipe_ID FROM Recipe ORDER BY NEWID() ";

        con.Open();

        var result = getIdCommand.ExecuteScalar();

        if (result != null)
        {
            var R_Id = result.ToString();
            return R_Id;
        }
        else
        {
            return null;
        }
        
    }

    protected void getRecipeName(string R_Id)
    {
        try
        {
            SqlCommand getNameCommand = con.CreateCommand();
            getNameCommand.CommandText = "SELECT Recipe_Name FROM Recipe WHERE Recipe_ID = " + R_Id;

            var result = getNameCommand.ExecuteScalar();

            if (result != null)
            {
                lblRecipeName.Text = result.ToString();
            }
            

        }
        catch (SqlException exc)
        {
            MessageBoxShow("ERROR IN GETTING RECIPE NAME " + exc);

        }
    }
   
    protected void getRecipePic(string R_Id)
    {
        try
        {
            SqlCommand PicCommand = con.CreateCommand();
            PicCommand.CommandText = "SELECT Picture FROM Recipe WHERE Recipe_ID =" + R_Id;

            string result = PicCommand.ExecuteScalar().ToString();
            
            if (result != null)
            {
                imgFood.ImageUrl = result;
            }
            
            
        }
        catch (SqlException exc)
        {
            MessageBoxShow("ERROR IN GETTING RECIPE PICTURE " + exc);
        }
    }

    protected void getRecipeIngredients(string R_Id)
    {
        try
        {
            SqlCommand IngredientsCommand = con.CreateCommand();
            IngredientsCommand.CommandText = "SELECT COUNT(DISTINCT Ingredient_ID) FROM RecipeIngredient WHERE Recipe_ID =" + R_Id;

            var result = IngredientsCommand.ExecuteScalar();

            if (result != null)
            {  
                int counter = Convert.ToInt32(result);

                SqlCommand AmountCommand = con.CreateCommand();
                AmountCommand.CommandText = "SELECT CONCAT(Amount_of, ' ',Ingredient) FROM RecipeIngredient LEFT JOIN Ingredients ON RecipeIngredient.Ingredient_ID = Ingredients.Ingredients_ID WHERE Recipe_ID = " + R_Id;
                SqlDataReader reader = AmountCommand.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var Amount = reader.GetString(0);
                        Amount += "<br/>";
                        lblIngredients.Text += Amount;
                        Amount = "";
                    }
                        
                        reader.NextResult();
                    
                }
                    reader.Close();
            }
        }
        catch (SqlException exc)
        {
            MessageBoxShow("ERROR IN GETTING RECIPE INGREDIENTS " + exc);
        }
    }

    protected void getRecipeMethod(string R_Id)
    {
        try
        {
            SqlCommand MethodCommand = con.CreateCommand();
            MethodCommand.CommandText = "SELECT Instructions FROM Recipe WHERE Recipe_ID =" + R_Id;

            var result = MethodCommand.ExecuteScalar();

            if (result != null)
            {
                lblMethod.Text = result.ToString();
            }
             
        }
        catch (SqlException exc)
        {
            MessageBoxShow("ERROR IN GETTING RECIPE METHOD " + exc);
        }

    }

    protected void getRecipeRating(string R_Id)
    {
        try
        {
            SqlCommand RatingCommand = con.CreateCommand();
            RatingCommand.CommandText = "SELECT Rating FROM Recipe WHERE Recipe_ID =" + R_Id;

            var result = RatingCommand.ExecuteScalar();

            if (result != null)
            {
                
                var star = "~\\Images\\star.jpg";

                imgStar1.ImageUrl = star;
                lblScore.Visible = true;
                lblScore.Text = result.ToString();
            }
            else
            {
                imgStar1.Visible = false;
                lblScore.Text = "Unrated";
            }
        }
        catch (SqlException exc)
        {
            MessageBoxShow("ERROR IN GETTING REIPE RATING " + exc);
        }

    }

    private void MessageBoxShow(string message)
    {
        this.AlertBoxMessage.InnerText = message;
        this.AlertBox.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            string UID = (string)(Session["New"]);
            var R_Id = (string)(Session["recipe"]);
            con.Open();
            SqlCommand TempCommand = con.CreateCommand();
            TempCommand.CommandText = "SELECT COUNT(*) FROM RecipeUsers WHERE Recipe_ID = @recipeid AND ID = @userid";
            TempCommand.Parameters.AddWithValue("@recipeid", R_Id);
            TempCommand.Parameters.AddWithValue("@userid", UID);
            int temp = Convert.ToInt32(TempCommand.ExecuteScalar().ToString());

            con.Close();

            if (temp != 1)
            {
                con.Open();
                SqlCommand InsertCommand = con.CreateCommand();

                InsertCommand.CommandText = "INSERT RecipeUsers(Recipe_ID, ID) VALUES(@recipeid, @userid)";
                InsertCommand.Parameters.AddWithValue("@recipeid", R_Id);
                InsertCommand.Parameters.AddWithValue("@userid", UID);

                InsertCommand.ExecuteScalar();

                con.Close();

                lblControl.Text = "Recipe successfully saved";
                lblControl.Visible = true;
            }
            else
            {
                lblControl.Text = "You already have this recipe saved";
                lblControl.Visible = true;
            }
        }
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Session["redir"] = "First.aspx";
        Response.Redirect("Login.aspx");
    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session["New"] = null;
        Response.Redirect("First.aspx");
    
    }
    protected void btnAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("Account.aspx");
    }

}
