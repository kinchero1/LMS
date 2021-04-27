using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library
{
    public partial class Login : Form
    {
        public static string username;
        public static string name;
        public static string accountype;
        public static int id;
        bool connecting;
        public Login()
        {
            InitializeComponent();


            Program.conS = Properties.Settings.Default.connectionstring;
            Program.setConnectionString(Program.conS);
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }

        public void RestartAPP()
        {
            Program.main.Close();
            Program.closeconnection();
            guna2TileButton1.PerformClick();
        }

        private void Login_Load(object sender, EventArgs e)
        {

            Program.copyFileToDocument("cat.txt");
            Program.copyFileToDocument("mat.txt");
            Program.copyFileToDocument("Ns.txt");
            loaddata();
            DatabaseSetting_Panel.Visible = false;



            Password.PasswordChar = '\u25cf';
            Ins_MDP.PasswordChar = '\u25cf';
            PasswordDB_TB.PasswordChar = '\u25cf';


            pictureBox2.BackgroundImage = Properties.Resources.icons8_invisible_20px_3;
            Program.LoginForm = this;
            gunaAnimateWindow1.Start();
        }

        void loaddata()
        {
            gunaCheckBox1.Checked = Properties.Settings.Default.Remember;
            if (gunaCheckBox1.Checked)
            {
                Username.Text = Properties.Settings.Default.Username;
            }
            loadDbsetting();
        }
        void loadDbsetting()
        {
            gunaSwitch1.Checked = Properties.Settings.Default.LocalDB;
            if (!gunaSwitch1.Checked)
            {
                ServerIP_TB.Text = Properties.Settings.Default.ServerIP;
                UsernameDB_TB.Text = Properties.Settings.Default.UsernameDb;
                PasswordDB_TB.Text = Properties.Settings.Default.PasswordDB;
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (connecting)
            {
                return;

            }
            connecting = true;
            gunaCircleProgressBar1.Visible = true;
            guna2TileButton1.Visible = false;
            bool ConnectionStatus = await Task.Run(() => checkConnection());
            if (!ConnectionStatus)
            {
                MessageBox.Show("Data base connection Error");
                guna2TileButton1.Visible = true;
                connecting = false;
                return;
            }
            bool connect = await Task.Run(() => Connect(Username.Text, Password.Text));
            guna2TileButton1.Visible = true;
            gunaCircleProgressBar1.Visible = false;
            if (connect)
            {
                Properties.Settings.Default.Remember = gunaCheckBox1.Checked;
                Properties.Settings.Default.Username = Username.Text;
                Properties.Settings.Default.Save();


                MainForm main = new MainForm();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Mot de passe ou nom de compte incorrecte");
            }
            connecting = false;
        }

        bool checkConnection()
        {
            SqlConnection con = new SqlConnection(Program.conS);

            try
            {
                con.Open();
            }
            catch (Exception)
            {

                return false;

            }
            con.Close();
            return true;
        }
        bool Connect(string Username, string PW)
        {
            SqlConnection con = new SqlConnection(Program.conS);

            try
            {
                con.Open();
            }
            catch (Exception)
            {

                return false;

            }
            string sql = "select * from users where username=@user and password = @pass COLLATE SQL_Latin1_General_Cp1_CS_AS";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Username;
            cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = PW;

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                username = dr.GetValue(1).ToString();
                name = dr.GetValue(3).ToString();
                accountype = dr.GetValue(5).ToString();
                id = (int)dr["id"];
                con.Close();
                if (accountype != "en attente" && accountype != "bloqué")
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
            dr.Close();
            con.Close();
            return false;

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Password_Recovery_form form = new Password_Recovery_form();
            form.Show();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            Password.PasswordChar = '\0';
            pictureBox2.BackgroundImage = Properties.Resources.icons8_eye_20px;

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            Password.PasswordChar = '\u25cf';
            pictureBox2.BackgroundImage = Properties.Resources.icons8_invisible_20px_3;

        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            //DatabaseSetting_Panel.Visible = true;
            //LoginPanel.Visible = false;
            loadDbsetting();
            gunaTransition1.ShowSync(DatabaseSetting_Panel);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            gunaTransition1.HideSync(DatabaseSetting_Panel);

        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            if (gunaSwitch1.Checked)
            {
                Properties.Settings.Default.connectionstring = "Data Source=" + Environment.MachineName + @"\SQLEXPRESS;Initial Catalog=Library_db;Integrated Security=True";

                Properties.Settings.Default.LocalDB = gunaSwitch1.Checked;
                Properties.Settings.Default.Save();

            }
            else
            {
                Properties.Settings.Default.ServerIP = ServerIP_TB.Text;
                Properties.Settings.Default.UsernameDb = UsernameDB_TB.Text;
                Properties.Settings.Default.PasswordDB = PasswordDB_TB.Text;

                Properties.Settings.Default.connectionstring = "Data Source=" + Properties.Settings.Default.ServerIP + "; Initial Catalog=Library_db;Persist Security Info=True;User ID= " + Properties.Settings.Default.UsernameDb + ";Password=" + Properties.Settings.Default.PasswordDB;
                Properties.Settings.Default.LocalDB = gunaSwitch1.Checked;
                Properties.Settings.Default.Save();
            }
            Program.conS = Properties.Settings.Default.connectionstring;
            Program.setConnectionString(Program.conS);
            MessageBox.Show(Properties.Settings.Default.connectionstring);
        }

        private void gunaSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaSwitch1.Checked)
            {
                ServerIP_TB.Enabled = false;
                UsernameDB_TB.Enabled = false;
                PasswordDB_TB.Enabled = false;
            }
            else
            {
                ServerIP_TB.Enabled = true;
                UsernameDB_TB.Enabled = true;
                PasswordDB_TB.Enabled = true;

            }
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.FromArgb(107, 99, 198);
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {

            label6.ForeColor = Color.FromArgb(112, 118, 143);
        }

        private void DatabaseSetting_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            InscriptionPanel.Visible = true;
            gunaTransition1.ShowSync(DatabaseSetting_Panel);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            gunaTransition1.HideSync(DatabaseSetting_Panel);
            InscriptionPanel.Visible = false;
        }

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            if (Program.CheckEmpty(Ins_nomCompte) || Program.CheckEmpty(Ins_Email) ||  Program.CheckEmpty(Ins_Nom) ||  Program.CheckEmpty(Ins_MDP) )
            {
                return;
            }
            string sql = "insert into users Values(@userName,@pw,@nom,@email,@type)";
            SqlConnection con = new SqlConnection(Program.conS);
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
            }
            catch (Exception)
            {

                MessageBox.Show("DataBase Connection Error");

            }
            if (Program.availableUserName(Ins_nomCompte.Text))
            {
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = Ins_nomCompte.Text;
                cmd.Parameters.Add("@pw", SqlDbType.VarChar).Value = Ins_MDP.Text;
                cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = Ins_Nom.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Ins_Email.Text;
                cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "en attente";
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    MessageBox.Show("Demande d'inscription bien fait");
                }
            }
            else
            {
                MessageBox.Show("il ya deja un utilisateur avec ce nom de compte");
            }



        }

        private async void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (connecting)
                {
                    return;

                }
                connecting = true;
                gunaCircleProgressBar1.Visible = true;
                guna2TileButton1.Visible = false;
                bool ConnectionStatus = await Task.Run(() => checkConnection());
                if (!ConnectionStatus)
                {
                    MessageBox.Show("Data base connection Error");
                    guna2TileButton1.Visible = true;
                    connecting = false;

                    return;
                }
                bool connect = await Task.Run(() => Connect(Username.Text, Password.Text));
                guna2TileButton1.Visible = true;
                gunaCircleProgressBar1.Visible = false;
                if (connect)
                {
                    Properties.Settings.Default.Remember = gunaCheckBox1.Checked;
                    Properties.Settings.Default.Username = Username.Text;
                    Properties.Settings.Default.Save();


                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Mot de passe ou nom de compte incorrecte");
                }
                connecting = false;
              
               

            }
           
            

        }
        public void restart()
        {

        }

        private void gunaControlBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.appBanner.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
           
        }
    }
}
