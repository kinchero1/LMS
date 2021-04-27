using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Guna.UI2.WinForms;

namespace Library.Gestion_de_marche
{
    public partial class GM_front_page : UserControl
    {
        public GM_front_page()
        {
            InitializeComponent();
        }
        public void loadData()
        {
            foreach (var control in this.Controls)
            {
                if (control.GetType() == typeof(Guna2TextBox))
                {
                    ((Guna2TextBox)control).ResetText();
                }
            }
            Date.Value = DateTime.Now;
        }
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (Program.CheckEmpty(Numero) || Program.CheckEmpty(Client) || Program.CheckEmpty(nomMarche) || Program.CheckEmpty(Adresse))
            {
                return;

            }
            SqlCommand cmd = new SqlCommand("select count(*) from marche where Nmarche=@N", Program.getcon());
            cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = Numero.Text + "/" + Date.Value.ToString("yy");
            int x = (int)cmd.ExecuteScalar();
            if (x > 0)
            {
                MessageBox.Show("Il ya deja un dossier de marche avec ce numero : " + Numero.Text + "/" + Date.Value.ToString("yy"));
                return;
            }
            Program.main.gM_Start1.marcheBahij.SetHeader(Numero.Text + "/" + Date.Value.ToString("yy"), Date.Value.ToShortDateString(), String.IsNullOrEmpty(Du.Text) ? " " : "à " + Du.Text + "  heures", Objet.Text, province.Text, nomMarche.Text, Client.Text, Adresse.Text);
            Program.main.gM_Start1.marchePerle.SetHeader(Numero.Text + "/" + Date.Value.ToString("yy"), Date.Value.ToShortDateString(), String.IsNullOrEmpty(Du.Text) ? " " : "à " + Du.Text + "  heures", Objet.Text, province.Text, nomMarche.Text, Client.Text, Adresse.Text);
            Program.main.gM_secondpage_Bahij.BringToFront();



        }

        private void GM_front_page_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            Program.main.gM_Start1.BringToFront();

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
