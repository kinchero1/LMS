using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Gestion_de_marche
{
    public partial class GM_secondpage : UserControl
    {
        public GM_secondpage()
        {
            InitializeComponent();
        }

        private void GM_secondpage_Load(object sender, EventArgs e)
        {
            if (this.Tag.ToString() != "Bahij")
            {
                foreach (var control in this.Controls)
                {
                    if (control.GetType()==typeof(Guna.UI2.WinForms.Guna2TextBox))
                    {
                        ((Guna.UI2.WinForms.Guna2TextBox)control).Tag = ((Guna.UI2.WinForms.Guna2TextBox)control).Text;
                    }
                }
            }
        }

        public void loadData()
        {
            if (this.Tag.ToString() == "Bahij")
            {
                Gmarche g = Program.main.gM_Start1.marcheBahij;
                Sign.Text = g.sign;
                Tel.Text = g.Ntel;
                Fax.Text = g.Nfax;
                Email.Text = g.Email;
                nom_compte.Text = g.nomCompte;
                Capital.Text = g.montantCapital;
                AdresseSS.Text = g.adresseSiegeSocial;
                AdresseD.Text = g.adresseDomicile;
                CNSS.Text = g.nCNSS;
                regisreLoc.Text = g.registreCommerceLoc;
                registerN.Text = g.registreCommerceN;
                pattenteN.Text = g.Npattente;
                RIB.Text = g.RIB;
            }
            else
            {
                foreach (var control in this.Controls)
                {
                    if (control.GetType() == typeof(Guna.UI2.WinForms.Guna2TextBox))
                    {
                        ((Guna.UI2.WinForms.Guna2TextBox)control).Text = ((Guna.UI2.WinForms.Guna2TextBox)control).Tag.ToString();
                    }
                }
            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (this.Tag.ToString()=="Bahij")
            {
                Program.main.gM_Start1.marcheBahij.setSecondPage(Sign.Text, Tel.Text, Fax.Text, Email.Text, nom_compte.Text, Capital.Text, AdresseSS.Text, AdresseD.Text, CNSS.Text, regisreLoc.Text, registerN.Text, pattenteN.Text, RIB.Text);
                Program.main.gM_secondpage_perle.BringToFront();

            }
            else
            {
                Program.main.gM_Start1.marchePerle.setSecondPage(Sign.Text, Tel.Text, Fax.Text, Email.Text, nom_compte.Text, Capital.Text, AdresseSS.Text, AdresseD.Text, CNSS.Text, regisreLoc.Text, registerN.Text, pattenteN.Text, RIB.Text);
                Program.main.gM_third_page1.BringToFront();
            }
          

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (this.Tag.ToString()=="Perle")

            {
                Program.main.gM_secondpage_Bahij.BringToFront();
               
            }
            else
            {
                Program.main.gM_front_page1.BringToFront();

            }
          
        }
    }
}
