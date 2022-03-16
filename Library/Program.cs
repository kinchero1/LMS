using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Guna.UI2.WinForms;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Library
{
    static class Program
    {
        static SqlConnection con = new SqlConnection();
        public static Application_Banner appBanner;
        public static Login LoginForm;
        public static bool normalMOD;
        public static MainForm main;
        public static bool IsFileLocked(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            bool lockStatus = false;
            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    // File/Stream manipulating code here

                    lockStatus = !fileStream.CanWrite;

                }
            }
            catch
            {
                //check here why it failed and ask user to retry if the file is in use.
                lockStatus = true;
            }
            return lockStatus;
        }

        public static void readfile(string name, Control ctrl, bool withColumns = false, bool allOption = false)
        {

            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LMS\\" + name);

            string[] lines = File.ReadAllLines(filename);

            Guna.UI2.WinForms.Guna2ComboBox guna = ((Guna.UI2.WinForms.Guna2ComboBox)ctrl);

            guna.ValueMember = "id";
            guna.DisplayMember = "Nom";

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Nom");

           

            if (withColumns)
            {
                if (allOption)
                {
                    dt.Rows.Add("", "Tout");

                }
            }
            else
            {

                if (allOption)
                {
                    guna.Items.Clear();
                    guna.Items.Add("Tout");
                }
            }
        
            foreach (string line in lines)
            {
                if (withColumns)
                {

                    string[] columns = line.Split(',');
                    if (columns.Length > 1) dt.Rows.Add(columns[1], columns[0]);

                }
                else
                {


                    guna.Items.Add(line);

                }

            }


            if (withColumns)
            {

                guna.DataSource = dt;
            }


            guna.SelectedIndex = allOption ? 0 : -1;


        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool killProcessByMainWindow(string title)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.Contains(title))
                {
                    // p.CloseMainWindow();
                    p.Kill();
                    return true;
                }
            }
            return false;
        }


        public static void insertLog(string description, string log_type, string reference_id)
        {
            DateTime now = DateTime.Now;
            string sql = "insert into DB_LOG values(@desc,@type,@date,@time,@ref)";
            SqlCommand cmd = new SqlCommand(sql, getcon());
            cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = description;
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = log_type;
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = now.ToShortDateString();
            cmd.Parameters.Add("@time", SqlDbType.VarChar).Value = now.ToShortTimeString();
            cmd.Parameters.Add("@ref", SqlDbType.VarChar).Value = reference_id;
            cmd.ExecuteNonQuery();
        }
        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }
        static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        public static void openProcess(string path)
        {
            if (Properties.Settings.Default.open_Files)
            {
                Process.Start(path);
            }
        }

        /// 
        public static void SendEmail(string to, string subject, string body, string attachmentPath = null, string nomCompte = "", string mdp = "")
        {
            string smtp = "smtp.gmail.com";
            NetworkCredential login = new NetworkCredential(nomCompte, mdp);
            SmtpClient client = new SmtpClient(smtp);
            client.Port = Convert.ToInt32(587);
            client.EnableSsl = true;
            client.Credentials = login;
            MailMessage msg = new MailMessage { From = new MailAddress(nomCompte + smtp.Replace("smtp.", "@"), "ST BAH ALFA BOT", Encoding.UTF8) };
            msg.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(""))
                msg.To.Add(new MailAddress(""));
            msg.Subject = subject;
            msg.Body = body;
            if (attachmentPath != null)
            {
                msg.Attachments.Add(new Attachment(attachmentPath));
            }

            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.SendCompleted += new SendCompletedEventHandler((ob, e) =>
            {
                if (e.Cancelled)
                    MessageBox.Show("Canceled");
                if (e.Error != null)
                {
                    Console.WriteLine(e.Error);
                    MessageBox.Show("Erreur");
                }

                else
                {
                    MessageBox.Show("Email sent");
              

                }
                  


            });
            string userstate = "Sending...";
            //Send email async
            client.SendAsync(msg, userstate);
            msg.Attachments.Dispose();
        }
        public static bool availableUserName(string username)
        {
            string sql = "select count(*) from users where username=@username";
            SqlConnection con = new SqlConnection(Program.getcon().ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
            bool result = ((int)cmd.ExecuteScalar() <= 0);
            return result;

        }
        public static void copyFileToDocument(string name)
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LMS")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LMS"));
            }

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LMS\\" + name);
            if (File.Exists(fileName))
            {
                return;
            }

            File.Copy("Resources/" + name, fileName);
        }
        public static string conS = Properties.Settings.Default.connectionstring;

       
        public static void checkheight(int desiredheight, Guna.UI2.WinForms.Guna2DataGridView dg, Control emptylist = null)
        {



            if (emptylist != null)
            {
                centercontrolH(emptylist);
                dg.Parent.Visible = dg.Rows.Count > 0;
                emptylist.Visible = dg.Rows.Count <= 0;

            }
            int x = ((dg.Rows.Count) * 50) + 40;

            if (x < desiredheight)
            {
                dg.Parent.Height = x + 2;
                foreach (Control ctrl in dg.Parent.Controls)
                {

                    if (ctrl.GetType() == typeof(Guna.UI.WinForms.GunaSeparator))
                    {
                        ctrl.Width = dg.Parent.Width + 10;
                    }
                }
            }
            else
            {
                dg.Parent.Height = desiredheight + 2;
                foreach (Control ctrl in dg.Parent.Controls)
                {

                    if (ctrl.GetType() == typeof(Guna.UI.WinForms.GunaSeparator))
                    {
                        ctrl.Width = dg.Parent.Width - 16;
                    }
                }
            }



        }
        public static float percentage(float value, float percentage)
        {
            return (value * (percentage / 100));
        }

        public static bool CheckEmpty(object control, int i = 0)
        {
            if (control.GetType() == typeof(Guna2TextBox))
            {
                Guna2TextBox TB = (Guna2TextBox)control;
                if (String.IsNullOrEmpty(TB.Text))
                {
                    Color originalColor = TB.FocusedState.BorderColor;

                    TB.Focus();
                    TB.FocusedState.BorderColor = Color.Red;
                    TB.KeyPress += new KeyPressEventHandler((object o, KeyPressEventArgs e) =>
                    {

                        TB.FocusedState.BorderColor = originalColor;
                    });

                    TB.Leave += new EventHandler((object o, EventArgs e) =>
                    {

                        TB.FocusedState.BorderColor = originalColor;
                    });
                    return true;
                }



            }
            else if (control.GetType() == typeof(Guna2ComboBox))
            {
                Guna2ComboBox TB = (Guna2ComboBox)control;
                if (TB.SelectedIndex <= i)
                {
                    Color originalColor = TB.FocusedState.BorderColor;

                    TB.Focus();
                    TB.FocusedState.BorderColor = Color.Red;
                    TB.Click += new EventHandler((object o, EventArgs e) =>
                    {

                        TB.FocusedState.BorderColor = originalColor;
                    });

                    TB.Leave += new EventHandler((object o, EventArgs e) =>
                    {

                        TB.FocusedState.BorderColor = originalColor;
                    });
                    return true;
                }

            }
            return false;
        }
        public static void ClearControl(object control, int i = 0, string defaultValue = "")
        {
            if (control.GetType() == typeof(Guna2TextBox))
            {
                Guna2TextBox TB = (Guna2TextBox)control;
                TB.Text = "";




            }
            else if (control.GetType() == typeof(Guna2ComboBox))
            {
                Guna2ComboBox TB = (Guna2ComboBox)control;
                TB.SelectedIndex = i;
            }
            else if (control.GetType() == typeof(Guna2DataGridView))
            {
                Guna2DataGridView TB = (Guna2DataGridView)control;
                TB.Rows.Clear();
            }

        }

        public static void setConnectionString(string conString)
        {
            con.ConnectionString = conString;
        }
        public static void openconnection()
        {
            con.Open();
        }
        public static void closeconnection()
        {
            con.Close();
        }
        public static SqlConnection getcon()
        {
            return con;
        }
        public static void centercontrolH(Control ctrl)
        {
            ctrl.Left = (ctrl.Parent.Width - ctrl.Width) / 2;
        }
        public static void centercontrolV(Control ctrl)
        {
            ctrl.Top = (ctrl.Parent.Height - ctrl.Height) / 2;
        }

        public static void fillcomboboxproduit(Guna2ComboBox guna, bool Alloption = false)
        {
            string sql = "select (nom +' ' +matiere+ ' '+Nscolaire) as 'Nom',id from produit";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            guna.ValueMember = "id";
            guna.DisplayMember = "Nom";
            DataTable dt = new DataTable();
            if (Alloption)
            {
                dt.Columns.Add("id");
                dt.Columns.Add("nom");
                DataRow x = dt.NewRow();
                x[0] = "1";
                x[1] = "Tous";
                dt.Rows.Add(x);

            }
            dt.Load(cmd.ExecuteReader());
            guna.DataSource = dt;

        }
        public static void fillcomboboxFournisseur(Guna2ComboBox guna, bool Alloption = false)
        {
            string sql = "select id,Nom from fournisseur";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            guna.ValueMember = "id";
            guna.DisplayMember = "Nom";
            DataTable dt = new DataTable();
            if (Alloption)
            {
                dt.Columns.Add("id");
                dt.Columns.Add("Nom");
                DataRow x = dt.NewRow();
                x[0] = "1";
                x[1] = "Tous";
                dt.Rows.Add(x);

            }

            dt.Load(cmd.ExecuteReader());
            guna.DataSource = dt;

        }
        public static void fillcomboboxClient(Guna2ComboBox guna, bool Alloption = false)
        {
            string sql = "select cin,(nom+' '+prenom) as 'Name' from client";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            guna.ValueMember = "cin";
            guna.DisplayMember = "Name";
            DataTable dt = new DataTable();
            if (Alloption)
            {
                dt.Columns.Add("cin");
                dt.Columns.Add("Name");
                DataRow x = dt.NewRow();
                x[0] = "1";
                x[1] = "Tous";
                dt.Rows.Add(x);

            }

            dt.Load(cmd.ExecuteReader());
            guna.DataSource = dt;

        }

        public static void onlyDegit(KeyPressEventArgs e, bool isfloat = false)
        {
            if (isfloat)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
                {

                    e.Handled = true;

                }
            }
            else
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        public static void update_stock(int id, int qte)
        {
            string sql = "update produit set qte += @stock where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@stock", SqlDbType.Int).Value = qte;
            cmd.ExecuteNonQuery();
            update_Value(id);
        }
        static void update_Value(int id)
        {
            string sql = "update produit set valeur = prix*qte where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();

        }
        public static bool IsControlAtFront(Control control)
        {
            return control.Parent.Controls.GetChildIndex(control) == 0;
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        private static Mutex mutex = null;

        [STAThread]
        static void Main()
        {
            string app = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            bool creatednew;
            mutex = new Mutex(true, app, out creatednew);

            if (!creatednew)
            {
                System.Windows.Forms.MessageBox.Show("l'application est deja ouvert");

                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Application_Banner());
            
        }
    }
}
