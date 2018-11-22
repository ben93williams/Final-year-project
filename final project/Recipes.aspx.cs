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
using System.Windows.Forms;


public partial class Recipe : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecipeBookConnectionString"].ConnectionString);

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
            Session["redir"] = "Recipes.aspx";
            Response.Redirect("Login.aspx");
        }

        con.Open();

        string UID = (string)(Session["New"]);
        var R_Id = getRecipeId(UID); //gets the recipe name and id
        Session["Recipe"] = R_Id;
        if (R_Id != null)
        {
            //getRecipePic(R_Id); //gets the recipe pic

            //getRecipeIngredients(R_Id);//gets the recipe ingredients

            //getRecipeMethod(R_Id); //gets the recipe method

            //getRecipeRating(R_Id); //gets the recipe rating

            SqlCommand populateCommand = con.CreateCommand();
            populateCommand.CommandText = "SELECT Recipe_Name FROM Recipe LEFT JOIN RecipeUsers ON Recipe.Recipe_ID = RecipeUsers.Recipe_ID WHERE ID =  " + UID;

            populateCommand.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = populateCommand;
            DataSet ds = new DataSet();
            da.Fill(ds, "Name");
            GridView1.DataSource = ds;
            GridView1.DataBind();

            con.Close();
        }
        else
        {
            lblFeedback.Visible = true;
            lblFeedback.Text = "You currently dont have any recipes saved.";

            theGrid.Visible = false;
        }

        con.Close();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var RName = GridView1.SelectedRow.Cells[1].Text;
        //code to reset recipecard on changed selection
        lblRecipeName.Text = "";
        lblIngredients.Text = "";
        lblMethod.Text = "";
        lblScore.Text = "";
        imgFood.ImageUrl = "";
        imgStar1.Visible = false;

        SqlCommand getIdCommand = con.CreateCommand();
        getIdCommand.CommandText = "SELECT DISTINCT Recipe_ID FROM Recipe WHERE Recipe_Name = @RName";
        getIdCommand.Parameters.AddWithValue("@RName", RName);
        con.Open();
        var R_Id = getIdCommand.ExecuteScalar().ToString();
        //gets the recipe pic
        getRecipePic(R_Id);
        //gets the recipe ingredients
        getRecipeIngredients(R_Id);
        //gets the recipe method
        getRecipeMethod(R_Id);
        //gets the rating
        getRecipeRating(R_Id);
        con.Close();


        lblRecipeName.Visible = true;
        imgFood.Visible = true;
        lblIngredients.Visible = true;
        lblMethod.Visible = true;

        btnDelete.Visible = true;
        btnNo.Visible = false;
        btnYes.Visible = false;

        Session["Recipe"] = R_Id;
    }

    protected string getRecipeId(object id)
    {
        try
        {

            SqlCommand IdCommand = con.CreateCommand();
            id = id.ToString();
            IdCommand.CommandText = "SELECT DISTINCT Recipe_Id FROM RecipeUsers WHERE ID = @UserID"; //gets recipe id from user id
            IdCommand.Parameters.AddWithValue("@UserID", id);
            object R_Id = IdCommand.ExecuteScalar();

            if (R_Id == null) //test if user has a recipe saved to their account
            {
                return null;
            }
            else
            {
                string Temp = R_Id.ToString();
                SqlCommand NameCommand = con.CreateCommand();
                NameCommand.CommandText = "SELECT Recipe_Name FROM Recipe WHERE Recipe_ID =" + R_Id; //use recipe id to get recipe name
                lblRecipeName.Text = NameCommand.ExecuteScalar().ToString();
                return Temp;
            }
        }
        catch (SqlException exc)
        {
            MessageBoxShow("ERROR IN GETTING RECIPE NAME " + exc);
            return null;

        }
    }

    protected void getRecipePic(string R_Id)
    {
        try
        {
            SqlCommand PicCommand = con.CreateCommand();
            PicCommand.CommandText = "SELECT Picture FROM Recipe WHERE Recipe_ID =" + R_Id;
            var tester = PicCommand.ExecuteScalar().ToString();
            imgFood.ImageUrl = tester;
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
            lblMethod.Text = MethodCommand.ExecuteScalar().ToString();
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

            var temp = RatingCommand.ExecuteScalar().ToString();

            if (temp != null)
            {
                lblScore.Text = temp;
                lblScore.Visible = true;
                imgStar1.Visible = true;
                imgStar1.ImageUrl = "~/Images/star.jpg";
            }
            else
            {
                lblScore.Text = "Unrated";
            }
        }
        catch (SqlException exc)
        {
            MessageBoxShow("ERROR IN GETTING REIPE RATING " + exc);
        }
        con.Close();
    }

    private void MessageBoxShow(string message)
    {
        this.AlertBoxMessage.InnerText = message;
        this.AlertBox.Visible = true;
    }

    //public void btnNext_Click(object sender, EventArgs e)
    //{
    //    con.Open();

    //    string UID = (string)(Session["New"]);
    //    var R_Id = getRecipeId(UID); //gets the recipe name and id

    //    if (R_Id != null)
    //    {
    //        getRecipePic(R_Id); //gets the recipe pic

    //        getRecipeIngredients(R_Id);//gets the recipe ingredients

    //        getRecipeMethod(R_Id); //gets the recipe method

    //        getRecipeRating(R_Id); //gets the recipe rating

    //        lblRecipeName.Visible = true;
    //        imgFood.Visible = true;
    //        lblIngredients.Visible = true;
    //        lblMethod.Visible = true;
    //        btnNext.Visible = true;
    //    }
    //    else
    //    {
    //        lblFeedback.Visible = true;
    //        lblFeedback.Text = "You currently dont have any recipes saved.";
    //    }
    //}

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnNo.Visible = true;
        btnYes.Visible = true;
        btnDelete.Visible = false;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        con.Open();
        var UID = Session["New"];
        SqlCommand deleteCommand = con.CreateCommand();
        deleteCommand.CommandText = "DELETE FROM RecipeUsers WHERE ID = " + UID + "AND Recipe_ID = " + (string)(Session["Recipe"]);
        deleteCommand.ExecuteNonQuery();
        con.Close();

        Response.Redirect("Recipes.aspx");

        btnNo.Visible = false;
        btnYes.Visible = false;
        btnDelete.Visible = true;

    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        btnNo.Visible = false;
        btnYes.Visible = false;
        btnDelete.Visible = true;
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
