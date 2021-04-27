using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Library.Gestion_de_marche
{
    public partial class GM_fourth_page : UserControl
    {
        public GM_fourth_page()
        {
            InitializeComponent();
        }
        public Gmarche GMbahij = null;
        public Gmarche GMperle = null;
        public void loadData()
        {
            Gmarche g = Program.main.gM_Start1.marcheBahij;
            Id_fiscal_bahij.Text = g.idFiscal;
            Date_bahij.Value = DateTime.Parse(g.doneDate);
            doneLocation_bahij.Text = g.doneLocation;
            Id_fiscal_perle.Text = "45874219";
            doneLocation_perle.Text = "Beni Mellal";
            Date_bahij.Value = DateTime.Now;
            Date_perle.Value = DateTime.Now;


        }
        private void GM_fourth_page_Load(object sender, EventArgs e)
        {
            
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            Program.main.gM_third_page1.BringToFront();
        }
        void insertMarcheToDb()
        {
            string sql = "insert into marche values(@N,@nom,@fromDate,@addDate,@client,@adresse,@objet)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = GMbahij.Nmarche;
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = GMbahij.nomMarche;
            cmd.Parameters.Add("@fromDate", SqlDbType.Date).Value = GMbahij.fromDate;
            cmd.Parameters.Add("@addDate", SqlDbType.Date).Value = DateTime.Now.ToShortDateString();
            cmd.Parameters.Add("@client", SqlDbType.VarChar).Value = GMbahij.Client;
            cmd.Parameters.Add("@adresse", SqlDbType.VarChar).Value = GMbahij.Adresse;
            cmd.Parameters.Add("@objet", SqlDbType.VarChar).Value = GMbahij.objet;
            cmd.ExecuteNonQuery();
            Guna.UI2.WinForms.Guna2DataGridView DG = Program.main.gM_third_page1.guna2DataGridView1;
            for (int i = 0; i < DG.Rows.Count; i++)
            {
                string desg = DG.Rows[i].Cells[0].Value.ToString();
                string unite = DG.Rows[i].Cells[1].Value.ToString();
                int quantite = int.Parse(DG.Rows[i].Cells[2].Value.ToString().Replace("x",""));
                float prix =float.Parse( DG.Rows[i].Cells[3].Value.ToString().Replace("DH",""));
                int tva = int.Parse(DG.Rows[i].Cells[4].Value.ToString().Replace("%",""));
                int idproduit = int.Parse(DG.Rows[i].Cells[0].Tag.ToString());
                insertProductToMarcheDB(GMbahij.Nmarche, desg, unite, quantite, prix, tva,idproduit);
            }
        }

        void insertProductToMarcheDB(string Nmarch,string desg,string unite,int qu,float prix,int tva,int idproduit)
        {
            string sql = "insert into marcheProduit values(@N,@desg,@unite,@qu,@prix,@tva,@idProduit)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = Nmarch;
            cmd.Parameters.Add("@desg", SqlDbType.NVarChar).Value = desg;
            cmd.Parameters.Add("@unite", SqlDbType.VarChar).Value = unite;
            cmd.Parameters.Add("@qu", SqlDbType.Int).Value = qu;
            cmd.Parameters.Add("@prix", SqlDbType.Float).Value = prix;
            cmd.Parameters.Add("@tva", SqlDbType.Int).Value = tva;
            cmd.Parameters.Add("@idProduit", SqlDbType.Int).Value = idproduit;
            cmd.ExecuteNonQuery();
        }
        void printFolder(string toPath,string ste,Gmarche GM)
        {
            print1(toPath,ste,GM);
            print2(toPath,ste, GM);
            print3(toPath,ste, GM);
            print4(toPath,ste, GM);
            print5(toPath,ste, GM);
            print6(toPath,ste, GM);
            print7(toPath,ste, GM);
        }
        void wholeProcess(string originalPath,string toPath)
        {

         
            Program.Copy(originalPath, toPath);
            printFolder(toPath, "bahij", GMbahij);
            printFolder(toPath, "perle", GMperle);

            if (GMbahij.pdpath != null)
            {
                File.Copy(GMbahij.pdpath, toPath + "/ste bahij/Dossier Financiére/" + Path.GetFileName(GMbahij.pdpath));
                File.Copy(GMbahij.pdpath, toPath + "/ste perle/Dossier Financiére/" + Path.GetFileName(GMbahij.pdpath));
            }
        }
        private async void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog FB = new FolderBrowserDialog();
            
            if (FB.ShowDialog() == DialogResult.OK)
            {
                string originalPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Dossier GM";
                string toPath = FB.SelectedPath;

                Program.main.setProgressBarVisisblité(true);
                await Task.Run(() => { wholeProcess(originalPath, toPath); });
                Program.main.setProgressBarVisisblité(false);


                insertMarcheToDb();
                MessageBox.Show("Ajout de dossier bien fait");
                Program.main.gM_Start1.BringToFront();
                Process.Start("explorer.exe", toPath);
                Program.insertLog("Ajout Dossier marche   : " + GMbahij.nomMarche, "Marche", GMbahij.nomMarche);

            }
        }
        public void printDoc(string SavedPath, string[] toFind, string[] toReplace, bool pritantable = false)
        {
           

            wordHelper word = new wordHelper();
            word.CreateWordDocument( SavedPath, toFind, toReplace, pritantable, 0);
        }

        void print1(string toPath,string ste,Gmarche GM)
        {
            string[] tofind = { "<nmarche>", "<datemarche>", "<timemarche>", "<objet>", "<province>" };
            string[] toReplace = { GM.Nmarche, GM.fromDate,GM.fromTime,GM.objet,GM.province };
            string completePath = toPath + @"\ste "+ste+@"\Dossier Administratif et technique\Dossier Administratif\Papier sur envolope Dossier Administratif.docx";
            printDoc(completePath, tofind, toReplace); 

        }

        void print2(string toPath,string ste,Gmarche GM)
        {
            string[] tofind = { "<nmarche>", "<timemarche>", "<datemarche>", "<objet>", "<sign>", "<tel>", "<fax>", "<email>", "<nomCompte>", "<capital>", "<AdresseSS>", "<AdresseD>", "<cnss>", "<registreLoc>", "<registreN>", "<pattenteN>", "<RIB>", "<doneL>", "<doneD>", "<sign>" };
            string[] toReplace = { GM.Nmarche,GM.fromTime,GM.fromDate,GM.objet,GM.sign,GM.Ntel,GM.Nfax,GM.Email,GM.nomCompte,GM.montantCapital,GM.adresseSiegeSocial,GM.adresseDomicile,GM.nCNSS,GM.registreCommerceLoc,GM.registreCommerceN,GM.Npattente,GM.RIB,GM.doneLocation,GM.doneDate,GM.sign };
            string completePath = toPath + @"\ste "+ste+@"\Dossier Administratif et technique\Dossier Administratif\DECLARATION SUR L'HONNEUR.docx";
            printDoc(completePath, tofind, toReplace);

        }

        void print3(string toPath,string ste,Gmarche GM)
        {
            string[] tofind = { "<nmarche>", "<datemarche>", "<timemarche>", "<objet>", "<province>" };
            string[] toReplace = { GM.Nmarche, GM.fromDate, GM.fromTime, GM.objet, GM.province };
            string completePath = toPath + @"\ste "+ste+@"\Dossier Administratif et technique\Dossier technique\Papier sur envolope Dossier Technique.docx";
            printDoc(completePath, tofind, toReplace);
        
        }
        void print4(string toPath,string ste,Gmarche GM)
        {
            string[] tofind = { "<nmarche>", "<datemarche>", "<timemarche>", "<objet>", "<province>" };
            string[] toReplace = { GM.Nmarche, GM.fromDate, GM.fromTime, GM.objet, GM.province };
            string completePath = toPath + @"\ste "+ste+@"\Dossier Administratif et technique\Papier sur envolope Dossier administratif et Technique.docx";
            printDoc(completePath, tofind, toReplace);

        }
        void print5(string toPath,string ste,Gmarche GM)
        {
            string[] tofind = { "<nmarche>", "<datemarche>", "<timemarche>", "<objet>", "<province>" };
            string[] toReplace = { GM.Nmarche, GM.fromDate, GM.fromTime, GM.objet, GM.province };
            string completePath = toPath + @"\ste "+ste+@"\Dossier Financiére\Papier sur envolope Dossier Financiére.docx";
            printDoc(completePath, tofind, toReplace);

        }
        void print6(string toPath,string ste,Gmarche GM)
        {
            string[] tofind = { "<nmarche>", "<timemarche>", "<datemarche>", "<objet>", "<sign>",  "<nomCompte>", "<capital>", "<AdresseSS>", "<AdresseD>", "<cnss>", "<idF>", "<registreLoc>", "<registreN>", "<pattenteN>","<HT>", "<TVA>", "<TTC>", "<doneL>", "<doneD>", "<sign>" };
            if(ste== "bahij")
            {
                string[] toReplace = { GM.Nmarche, GM.fromTime, GM.fromDate, GM.objet, GM.sign, GM.nomCompte, GM.montantCapital, GM.adresseSiegeSocial, GM.adresseDomicile, GM.nCNSS, Id_fiscal_bahij.Text, GM.registreCommerceLoc, GM.registreCommerceN, GM.Npattente, GM.montantHT.ToString(), GM.montantTVA.ToString(), Montant_TTC.ToString(), doneLocation_bahij.Text, Date_bahij.Value.ToShortDateString(), GM.sign };
                string completePath = toPath + @"\ste " + ste + @"\Dossier Financiére\ACTE D'ENGAGEMENT.docx";
                printDoc(completePath, tofind, toReplace);

            }
            else
            {
                string[] toReplace = { GM.Nmarche, GM.fromTime, GM.fromDate, GM.objet, GM.sign, GM.nomCompte, GM.montantCapital, GM.adresseSiegeSocial, GM.adresseDomicile, GM.nCNSS, Id_fiscal_perle.Text, GM.registreCommerceLoc, GM.registreCommerceN, GM.Npattente, GM.montantHT.ToString(), GM.montantTVA.ToString(), Montant_TTC.ToString(), doneLocation_perle.Text, Date_perle.Value.ToShortDateString(), GM.sign };
                string completePath = toPath + @"\ste " + ste + @"\Dossier Financiére\ACTE D'ENGAGEMENT.docx";
                printDoc(completePath, tofind, toReplace);

            }

        }
        void print7(string toPath,string ste,Gmarche GM)
        {
            string[] tofind = { "<nmarche>", "<datemarche>", "<timemarche>", "<objet>", "<province>" };
            string[] toReplace = { GM.Nmarche, GM.fromDate, GM.fromTime, GM.objet, GM.province };
            string completePath = toPath + @"\ste "+ste+@"\Dossier Financiére\Papier sur cps.docx";
            printDoc(completePath, tofind, toReplace);

        }




    }
}
