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
    public partial class ProviderPerProduct : UserControl
    {
        bool FilterDate=false;
        public ProviderPerProduct()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void loadData(int product_id)
        {
            this.Tag = product_id;
            string sql = @"select distinct  id ,Nom,Email,Adresse,Tel from Fournisseur
                         inner join Bon_Livraison on Bon_Livraison.fournisseur=Fournisseur.id
                         inner join produit_Bon_Livraison on produit_Bon_Livraison.facture_id=Bon_Livraison.Nentre
                         where produit_id =@id and Bon_Livraison.Verification='Verifié'";

            SqlCommand cmd = new SqlCommand(sql,Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = product_id;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";
            Program.fillcomboboxFournisseur(fournisseur, true);

        }


        void filters()
        {
            string sql = @"select distinct id ,Nom,Email,Adresse,Tel from Fournisseur
                         inner join Bon_Livraison on Bon_Livraison.fournisseur=Fournisseur.id
                         inner join produit_Bon_Livraison on produit_Bon_Livraison.facture_id=Bon_Livraison.Nentre
                         where produit_id ="+int.Parse(this.Tag.ToString())+" and Bon_Livraison.Verification='Verifié' and 1=1";
            SqlCommand cmd = new SqlCommand();

           

            if (fournisseur.SelectedIndex > 0)
            {
                sql += "and fournisseur like @nom ";
                cmd.Parameters.Add("@nom", SqlDbType.Int).Value = fournisseur.SelectedValue;


            }

           

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
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";

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
            loadData(int.Parse(this.Tag.ToString()));
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

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }


            int x = guna2DataGridView1.CurrentRow.Index;

            Program.main.product_achat_history1.loadData(int.Parse(guna2DataGridView1.Rows[x].Cells[0].Value.ToString()), int.Parse(this.Tag.ToString()));
            Program.main.product_achat_history1.BringToFront();
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }



            Program.main.product_achat_history1.loadData( int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()), int.Parse(this.Tag.ToString()));
            Program.main.product_achat_history1.BringToFront();

        }
    }
}
