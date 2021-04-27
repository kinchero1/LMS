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

namespace Library
{
    public partial class Application_Settings : UserControl
    {
        public Application_Settings()
        {
            InitializeComponent();
        }
        public void loadData()
        {
            gunaSwitch1.Checked = Properties.Settings.Default.minimizeTosystemTray;
            gunaSwitch2.Checked = Properties.Settings.Default.open_Files;
            account_name.Text = Properties.Settings.Default.EmailAccountName;
            account_pw.Text = Properties.Settings.Default.EmailPassword;
            quantiteTB.Text = Properties.Settings.Default.epuise.ToString();
        }
        private void Application_Settings_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();
            label10.Visible = !Program.normalMOD;
            gunaAdvenceButton2.Visible = !Program.normalMOD;
        }

        private void gunaSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.minimizeTosystemTray = gunaSwitch1.Checked;
            Properties.Settings.Default.Save();
            Program.main.notifyIcon1.Visible = Properties.Settings.Default.minimizeTosystemTray;
            Program.main.gunaAdvenceButton28.Visible = Properties.Settings.Default.minimizeTosystemTray;
        }

        private void gunaSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.open_Files = gunaSwitch2.Checked;
            Properties.Settings.Default.Save();
        }

        private void account_name_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.EmailAccountName = account_name.Text;
            Properties.Settings.Default.Save();

        }

        private void account_pw_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.EmailPassword = account_pw.Text;
            Properties.Settings.Default.Save();

        }
        public int queryExcuter(string query)
        {
            SqlCommand cmd = new SqlCommand(query, Program.getcon());
            if ((int)cmd.ExecuteNonQuery()>0)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            string SavingSql = @"insert into Bon_LivraisonAR select Nentre,NBon,fournisseur,dateentre,Montant_Ht,montant_Tva,montant_TTC,Verification,CommandeId from Bon_Livraison where Verification like 'Verifié'/
                           insert into produit_Bon_LivraisonAR select * from produit_Bon_Livraison where produit_Bon_Livraison.facture_id in (select facture_id from Bon_Livraison where Verification like 'Verifié')/

                       
                          

                           insert into BillAR select id,client_CIN,datesortie,montant,Status,montantRest from Bill where Bill.Status='Payé'/
                           insert into Bill_productsAR select * from Bill_products where Bill_id in (select Bill_id from Bill where Bill.Status='Payé')/

                         

                          

                           insert into commandeAR select id,Fournisseur_id,datecommande,status from commande where status='Livré'/
                           insert into produit_commandeAR select * from produit_commande where produit_commande.commande_id in (select id from commande where status='Livré')/

                           ";


            string deletingSql = @"delete from produit_Bon_Livraison where produit_Bon_Livraison.facture_id in (select facture_id from Bon_Livraison where Verification like 'Verifié') /
                                       delete from Bon_Livraison where Verification like 'Verifié' / 

                                         delete from Bill_products where Bill_id in (select Bill_id from Bill where Bill.Status='Payé')/
                                         delete from Bill where Bill.Status='Payé' /

                                         delete from produit_commande where produit_commande.commande_id in (select id from commande where status='Livré')/
                                         delete from commande where status='Livré'/
";



            try
            {
                string[] Savingqueries = SavingSql.Split('/');
                foreach (string query in Savingqueries)
                {
                    queryExcuter(query);
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er);
                MessageBox.Show("Erreur Archivation \n" +
                    "erreur code : 0");
                return;
            }

            try
            {
                string[] deletingQueries = deletingSql.Split('/');
                foreach (string query in deletingQueries)
                {
                    queryExcuter(query);
                }
            }
            catch (Exception er)
            {

                MessageBox.Show("Erreur Archivation \n" +
                    "erreur code : 2");
                Console.WriteLine(er);
                return;
            }

            MessageBox.Show("Archivage bien fait");

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.normalMOD = false;
            Properties.Settings.Default.Save();
            Program.LoginForm.RestartAPP();
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.normalMOD = true;
            Properties.Settings.Default.Save();
            Program.LoginForm.RestartAPP();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
        }

        private void quantiteTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (quantiteTB.Text !="")
            {
                Properties.Settings.Default.epuise = int.Parse(quantiteTB.Text);
                Properties.Settings.Default.Save();
            }
          
        }
    }
}
