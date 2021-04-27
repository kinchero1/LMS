using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Application_Banner : Form
    {
        public static bool dataLoaded = false;
        public Application_Banner()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
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

        private  void Application_Banner_Load(object sender, EventArgs e)
        {


         


        }

        private void gunaCircleProgressBar1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private async void Application_Banner_Shown(object sender, EventArgs e)
        {
            bool authorisation = await Task.Run(() => AppAuth.getAuth());
            if (authorisation==false)
            {
                this.Close();
            }
            if (Properties.Settings.Default.connectionstring=="")
            {
                Properties.Settings.Default.connectionstring = "Data Source=" + Environment.MachineName + @"\SQLEXPRESS;Initial Catalog=Library_db;Integrated Security=True";
                Properties.Settings.Default.Save();
                Program.conS = Properties.Settings.Default.connectionstring;
            }
           


            if (this.DesignMode)
            {
                return;
            }
            if (Properties.Settings.Default.Library_name != "bahij" && Properties.Settings.Default.Library_name != "perle")
            {
                Entry_form entry = new Entry_form();
                entry.ShowDialog();
            }
            bool connectionState = checkConnection();

            if (connectionState && Program.CheckForInternetConnection())
            {

                dataLoaded =await  Task.Run(() => Library_Consultation.loadData());
             
                Login login = new Login();
                login.Show();
                Program.appBanner = this;

              
            }
            else
            {
                Login login = new Login();
                login.Show();
                Program.appBanner = this;

            }
            this.Hide();
        }
    }
}
