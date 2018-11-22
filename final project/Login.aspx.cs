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
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Console.WriteLine("hello");
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

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        emailValidator.Enabled = true;
        passwordValidator.Enabled = true;

        String HashedPassword = Encrypt(txtPassword.Text);

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecipeBookConnectionString"].ConnectionString);
        con.Open();

        SqlCommand userCommand = con.CreateCommand();

        userCommand.CommandText = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
        userCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
        int Temp = Convert.ToInt32(userCommand.ExecuteScalar().ToString());

        con.Close();

        if(Temp == 1)
        {
            HttpCookie cookie = new HttpCookie("");
            cookie.Values.Add("Email", txtEmail.Text);
            Response.Cookies.Add(cookie);

            con.Open();

            SqlCommand passwordCommand = con.CreateCommand();

            passwordCommand.CommandText = "SELECT Password FROM Users WHERE Email = @Email";
            passwordCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
            String password = passwordCommand.ExecuteScalar().ToString();

            con.Close();

            if (password == HashedPassword)
            {
                con.Open();

                SqlCommand idCommand = con.CreateCommand();

                idCommand.CommandText = "SELECT ID FROM Users WHERE Email = @Email";
                idCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                var Uid = idCommand.ExecuteScalar().ToString();
                Session["New"] = Uid;
                Response.Redirect((string)Session["redir"]);

                //if (this.cbxRemeber != null && this.cbxRemeber.Checked == true)
                //{
                //    bool rememberMe = false;
                //    FormsAuthentication.RedirectFromLoginPage(txtEmail.Text, cbxRemeber.Checked);
                //    int timeout = rememberMe ? 525600 : 30;
                //    var ticket = new FormsAuthenticationTicket(txtEmail.Text, rememberMe, timeout);
                //    string encrypted = FormsAuthentication.Encrypt(ticket);
                //    var rememberCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                //    cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                //    cookie.HttpOnly = true;
                //    Response.Cookies.Add(cookie);
                //}

                con.Close();
            }
            else
            {
                lblFeedback.Text = "ERROR, INCORRECT PASSWORD";
            }
        }
        else
        {
            lblFeedback.Text = "ERROR, INCORRECT PASSWORD";
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

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx");
    }

    protected void btnLost_Click(object sender, EventArgs e)
    {
        //add funcionality to email user their password after some verification
    }
}
