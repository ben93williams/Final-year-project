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
using System.Text.RegularExpressions;


public partial class Account : System.Web.UI.Page
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
            Response.Redirect("Login.aspx"); 
        }
        var UID = Session["New"];

        con.Open();

        //gets first name
        SqlCommand fnameCommand = con.CreateCommand();
        fnameCommand.CommandText = "SELECT Firstname FROM Users WHERE ID= @UID";
        fnameCommand.Parameters.AddWithValue("@UID", UID);
        var temp1 = fnameCommand.ExecuteScalar().ToString();

        //gets last name
        SqlCommand lnameCommand = con.CreateCommand();
        lnameCommand.CommandText = "SELECT Lastname FROM Users WHERE ID= @UID";
        lnameCommand.Parameters.AddWithValue("@UID", UID);
        var temp2 = lnameCommand.ExecuteScalar().ToString();

        //concatenate name results
        lblGetName.Text = temp1 + "," + temp2; 
            
        //gets users email
        SqlCommand emailCommand = con.CreateCommand();
        emailCommand.CommandText = "SELECT Email FROM Users WHERE ID= @UID";
        emailCommand.Parameters.AddWithValue("@UID", UID);
        lblGetEmail.Text = emailCommand.ExecuteScalar().ToString();

        //gets users recipe count
        SqlCommand numberCommand = con.CreateCommand();
        numberCommand.CommandText = "SELECT COUNT(*) FROM RecipeUsers WHERE ID= @UID";
        numberCommand.Parameters.AddWithValue("@UID", UID);
        lblGetAccount.Text = numberCommand.ExecuteScalar().ToString();

        con.Close();
    }

    static string Encrypt(string value)
    {
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] data = md5.ComputeHash(utf8.GetBytes(value));
            return Convert.ToBase64String(data);
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        reset.Visible = true;
        info.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        con.Open();
        var UID = Session["New"];

        SqlCommand validateCommand = con.CreateCommand();
        validateCommand.CommandText = "SELECT Password FROM Users WHERE ID= @UID";
        validateCommand.Parameters.AddWithValue("@UID", UID);
        var temp = validateCommand.ExecuteScalar().ToString();

        if (txtNewPass.Text != "")
        {
            if (txtNewPass.Text == txtConfirmPass.Text)
            {
                var error = "";
                bool validate = ValidatePassword(txtNewPass.Text, out error);
                if (validate == true)
                {
                    var temp2 = Encrypt(txtOldPass.Text);

                    if (temp2 == temp)
                    {
                        var pass = Encrypt(txtNewPass.Text);

                        SqlCommand passCommand = con.CreateCommand();
                        passCommand.CommandText = "UPDATE Users SET Password = @Pass WHERE ID= @UID";
                        passCommand.Parameters.AddWithValue("@UID", UID);
                        passCommand.Parameters.AddWithValue("@Pass", pass);
                        passCommand.ExecuteNonQuery();

                        Session["New"] = null;
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    lblFeedback.Visible = true;
                    lblFeedback.Text = error;
                }
            }
            else
            {
                lblFeedback.Visible = true;
                lblFeedback.Text = "New passwords do not match";
            }
        }
        else
        {
            lblFeedback.Visible = true;
            lblFeedback.Text = "No Password entered";
        }
        con.Close();

        Response.Redirect("Account.aspx");
    }

    private bool ValidatePassword(string password, out string ErrorMessage)
    {
        var input = password;
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new Exception("Password should not be empty");
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMiniMaxChars = new Regex(@".{8,8}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain At least one lower case letter";
            return false;
        }
        else if (!hasUpperChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain At least one upper case letter";
            return false;
        }
        else if (!hasMiniMaxChars.IsMatch(input))
        {
            ErrorMessage = "Password should not be less than or greater than 8 characters";
            return false;
        }
        else if (!hasNumber.IsMatch(input))
        {
            ErrorMessage = "Password should contain At least one numeric value";
            return false;
        }

        else if (!hasSymbols.IsMatch(input))
        {
            ErrorMessage = "Password should contain At least one special case characters";
            return false;
        }
        else
        {
            return true;
        }
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
