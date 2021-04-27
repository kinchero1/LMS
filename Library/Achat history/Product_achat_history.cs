using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Achat_history
{
    public partial class Product_achat_history : UserControl
    {
        bool FilterDate=false;
      

        public Product_achat_history()
        {
            InitializeComponent();
        }

        public void loadData(int Fid,int Pid)
        {
            this.Tag = Fid+"/"+Pid;
            string sql = @"select   QteEntre as 'Quantité',prixunitaire as 'Prix.U',produit_Bon_Livraison.remise as 'Remise',tva as 'T.V.A',( (prixunitaire * QteEntre) -((prixunitaire * QteEntre) *(produit_Bon_Livraison.remise/100.0))) as 'Montant HT',((prixunitaire * QteEntre) *(produit_Bon_Livraison.tva/100.0)) as 'Montant T.V.A',( (prixunitaire * QteEntre) -((prixunitaire * QteEntre) *(produit_Bon_Livraison.remise/100.0)) + ((prixunitaire * QteEntre) *(produit_Bon_Livraison.tva/100.0))) as 'Total',dateentre as 'Date' from produit
                          inner join produit_Bon_Livraison on produit_Bon_Livraison.produit_id=produit.id
                          inner join Bon_Livraison on Bon_Livraison.Nentre=produit_Bon_Livraison.facture_id
                          where Bon_Livraison.fournisseur=@Fid and produit.id=@Pid and Bon_Livraison.Verification='Verifié'";



            SqlCommand cmd = new SqlCommand(sql,Program.getcon());
            cmd.Parameters.Add("@Fid", SqlDbType.Int).Value = Fid;
            cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = Pid;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            guna2DataGridView1.Columns[0].DefaultCellStyle.Format = "x 0";
            guna2DataGridView1.Columns[1].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[2].DefaultCellStyle.Format = "0 '%'";
            guna2DataGridView1.Columns[3].DefaultCellStyle.Format = "0 '%'";
            guna2DataGridView1.Columns[4].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[6].DefaultCellStyle.Format = "0.00 DH";

            label7.Text = getProduct(Pid);
            label1.Text = getFournisseur(Fid);
            Program.checkheight(442, guna2DataGridView1, empty_panel);


        }

        public string getFournisseur(int id)
        {
            string sql = "select nom from Fournisseur ";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            return cmd.ExecuteScalar().ToString();
        }

        public string getProduct(int id)
        {
            string sql = "select (nom+ ' ' + matiere +' ' + Nscolaire)  as 'Produit' from produit ";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            return cmd.ExecuteScalar().ToString();
        }

        public string[] getValueFromTag()
        {
            string[] x = this.Tag.ToString().Split('/');
            return x;
        }


        void filters()
        {
            string sql = @"select   QteEntre as 'Quantité',prixunitaire as 'Prix.U',produit_Bon_Livraison.remise as 'Remise',tva as 'T.V.A',( (prixunitaire * QteEntre) -((prixunitaire * QteEntre) *(produit_Bon_Livraison.remise/100.0))) as 'Montant HT',((prixunitaire * QteEntre) *(produit_Bon_Livraison.tva/100.0)) as 'Montant T.V.A',( (prixunitaire * QteEntre) -((prixunitaire * QteEntre) *(produit_Bon_Livraison.remise/100.0)) + ((prixunitaire * QteEntre) *(produit_Bon_Livraison.tva/100.0))) as 'Total',dateentre as 'Date' from produit
                          inner join produit_Bon_Livraison on produit_Bon_Livraison.produit_id=produit.id
                          inner join Bon_Livraison on Bon_Livraison.Nentre=produit_Bon_Livraison.facture_id
                          where Bon_Livraison.fournisseur="+getValueFromTag()[0]+ " and produit.id=" + getValueFromTag()[1] + " and Bon_Livraison.Verification='Verifié' and 1 =1";
            SqlCommand cmd = new SqlCommand();



           
           

           

            if (FilterDate)
            {
                sql += "and dateentre between  @first and @second";
                cmd.Parameters.Add("@first", SqlDbType.DateTime).Value = guna2DateTimePicker1.Value;
                cmd.Parameters.Add("@second", SqlDbType.DateTime).Value = guna2DateTimePicker2.Value;

            }

            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();


            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            Program.checkheight(442, guna2DataGridView1, empty_panel);

            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            label7.Text = getProduct(int.Parse(getValueFromTag()[1]));
            label1.Text = getFournisseur(int.Parse(getValueFromTag()[0]));

        }

        private void ProviderPerProduct_Load(object sender, EventArgs e)
        {

        }

        private void fournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            FilterDate = false;
            loadData(int.Parse(getValueFromTag()[0]), int.Parse(getValueFromTag()[1]));

        }

        private void guna2DateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            FilterDate = true;
            filters();
        }

        private void guna2DateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            FilterDate = true;
            filters();
        }

        private void N_Bon_TextChanged(object sender, EventArgs e)
        {
            filters();
        }
    }
}
