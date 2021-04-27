using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library
{
    public partial class Password_Recovery_form : Form
    {
        public Password_Recovery_form()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }
        string getPassword(string email)
        {
            string sql = "select password from users where email=@email";
            SqlConnection con = new SqlConnection(Program.conS);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
           string x = (string)cmd.ExecuteScalar();
            return x;

        }

        string smtp = "smtp.gmail.com";
        private void Yes_Button_Click(object sender, EventArgs e)
        {
            string password = getPassword(guna2TextBox1.Text);
            if (password==null)
            {
                return;
            }
            
            string body="Vote mot de passe est : " + password;
            string subject = "Password Recovery";
            string to = guna2TextBox1.Text;
            Program.SendEmail(to, subject, body, null, Properties.Settings.Default.EmailAccountName, Properties.Settings.Default.EmailPassword);


        }

       

    }
}
