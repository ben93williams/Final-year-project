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
using System.Web.Services;

public partial class Search : System.Web.UI.Page
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
 
        }
    }

    [WebMethod]
    public static string[] GetCustomers(string prefix)
    {
        if (prefix.Length >= 3)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["RecipeBookConnectionString"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Recipe_Name from Recipe where Recipe_Name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["Recipe_Name"]));
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToArray();
        }
        return null;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        String search = "SELECT Recipe_Name, Rating FROM Recipe WHERE (Recipe_Name LIKE '%' + @Name + '%')";
        SqlCommand searchCommand = new SqlCommand(search, con);
        searchCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtSearch.Text;

        con.Open();

        searchCommand.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = searchCommand;
        DataSet ds = new DataSet();
        da.Fill(ds, "Name");
        GridView1.DataSource = ds;
        GridView1.DataBind();

        GridView1.HeaderRow.Cells[1].Text = "Recipe name";

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (GridView1.Rows[i].Cells[2].Text != null)
            {
                GridView1.Rows[i].Cells[2].Text += " stars";
            }
            else
            {
                GridView1.Rows[i].Cells[2].Text += "";
            }
        }

        if (Session["New"] != null)
        {
    
        }
        else
        {
            lblSave.Visible = true;
        }
        
        con.Close();

        container.Visible = false;
        lblFeedback.Visible = false;
        //hide previous searched recipecard
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var UID = (string)(Session["New"]);
        var RID = getRecipeId();

        SqlCommand SaveCommand = con.CreateCommand();
        SaveCommand.CommandText = "INSERT RecipeUsers(Recipe_ID, ID) VALUES(@R_Id, @U_Id)";
        SaveCommand.Parameters.AddWithValue("@R_Id", RID);
        SaveCommand.Parameters.AddWithValue("@U_Id", UID);
        SaveCommand.ExecuteNonQuery();
        lblFeedback.Text = "Successfully saved recipe, it can be viewed in your cookbook for later use";
        btnSave.Enabled = false;

        con.Close();
        btnSave.Visible = false;
    }

    protected string getRecipeId()
    {
        var UID = (string)(Session["New"]);

        string RName = GridView1.SelectedRow.Cells[1].Text;

        SqlCommand getIdCommand = con.CreateCommand();
        getIdCommand.CommandText = "SELECT DISTINCT Recipe_Id FROM Recipe WHERE Recipe_Name = @RName";
        getIdCommand.Parameters.AddWithValue("@RName", RName);

        con.Open();

        var result = getIdCommand.ExecuteScalar().ToString();

        if (result != null)
        {
            lblRecipeName.Text = RName;
            return result;
        }
        else
        {
            return null;
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var R_Id = getRecipeId();

        container.Visible = true;

        SqlCommand validateCommand = con.CreateCommand();

        validateCommand.CommandText = "SELECT COUNT(Recipe_ID) FROM RecipeUsers WHERE Recipe_ID = " + R_Id;
        var temp = Convert.ToInt32(validateCommand.ExecuteScalar());

        if (temp == 1)
        {
            var UID = (string)(Session["New"]);

            if (UID != null)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT COUNT(Recipe_ID) FROM RecipeUsers WHERE ID =" + UID + "AND Recipe_ID = " + R_Id;
                temp = Convert.ToInt32(cmd.ExecuteScalar());
                if (temp == 1)
                {
                    btnSave.Visible = false;
                    lblFeedback.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                }
            }
            else
            {
                lblSave.Visible = true;
            }
            con.Close();
        }
        else
        {
            con.Close();
        }

        if (!IsPostBack)
        {
            container.Visible = false;
            con.Close();
        }

        con.Open();

        if (R_Id != null)
        {
            //gets the recipe pic
            getRecipePic(R_Id);
            //gets the recipe ingredients
            getRecipeIngredients(R_Id);
            //gets the recipe method
            getRecipeMethod(R_Id);
            //gets the rating
            getRecipeRating(R_Id);
        }
        con.Close();
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
                imgStar1.ImageUrl = "~/Images/star.jpg";
                imgStar1.Visible = true;
            }
            else
            {
                lblScore.Text = "Unrated";
                imgStar1.Visible = false;
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

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Session["redir"] = "Search.aspx";
        Response.Redirect("Login.aspx");
    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session["New"] = null;
        Response.Redirect("first.aspx");
    }
    protected void btnAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("Account.aspx");
    }
}
