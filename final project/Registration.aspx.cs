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

public partial class Registration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecipeBookConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            Response.Redirect("First.aspx");
        }  
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

    protected void btnRegister_Click(object sender, EventArgs e)
    {

        firstNameValidator.Enabled = true;
        lastNameValidator.Enabled = true;
        emailValidator.Enabled = true;
        passwordValidator.Enabled = true;
        confirmValidator.Enabled = true;


        con.Open();

        if (string.IsNullOrEmpty(txtPassword.Text))
        {
            lblFeedback.Visible = true;
            lblFeedback.Text = "One or more fields were blank, please re-enter details";
        }
        else
        {
            var error = "";
            bool validation = ValidatePassword(txtPassword.Text, out error);

            if (validation == true)
            {
                String HashedPassword = Encrypt(txtPassword.Text);
                try
                {
                    if (txtPassword.Text == txtPasswordValidator.Text)
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT Users(Email, Password, Firstname, Lastname) VALUES(@UserEmail, @UserPassword, @UserFirstName, @UserLastName)";
                        cmd.Parameters.AddWithValue("@UserEmail", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@UserPassword", HashedPassword);
                        cmd.Parameters.AddWithValue("@UserFirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@UserLastName", txtLastName.Text);

                        cmd.ExecuteNonQuery();
                        con.Close();
                        lblFeedback.Visible = true;
                        lblFeedback.Text = "Thank you for registering";

                        txtEmail.Text = "";
                        txtFirstName.Text = "";
                        txtLastName.Text = "";
                        txtPassword.Text = "";
                        txtPasswordValidator.Text = "";

                        txtEmail.Visible = false;
                        txtFirstName.Visible = false;
                        txtLastName.Visible = false;
                        txtPassword.Visible = false;
                        txtPasswordValidator.Visible = false;
                        btnRegister.Visible = false;
                    }
                    else
                    {
                        lblFeedback.Visible = true;
                        lblFeedback.Text = "Passwords did not match";
                    }
                }

                catch (SqlException exc)
                {
                    Console.WriteLine("error creating account");
                }
            }
            else
            {
                lblFeedback.Visible = true;
                lblFeedback.Text = error;
            }
        }
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
    protected void btnLogger_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

}
