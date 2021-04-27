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
    public partial class ProductsPerProvider : UserControl
    {
        bool FilterDate=false;
        public ProductsPerProvider()
        {
            InitializeComponent();
        }

        public void loadData(int Fid)
        {
            this.Tag = Fid;
            string sql = @"select distinct produit.id,productid,Nom,Categorie,matiere,Nscolaire from produit
                           inner join produit_Bon_Livraison on produit_Bon_Livraison.produit_id=produit.id
                           inner join Bon_Livraison on Bon_Livraison.Nentre=produit_Bon_Livraison.facture_id
                           where Bon_Livraison.fournisseur=@id and Bon_Livraison.Verification='Verifié' ";


            SqlCommand cmd = new SqlCommand(sql,Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = Fid;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].Visible = false;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";
            Program.checkheight(442, guna2DataGridView1, empty_panel);

        }


        void filters()
        {
            string sql = @"select distinct produit.id,productid,Nom,Categorie,matiere,Nscolaire from produit
                           inner join produit_Bon_Livraison on produit_Bon_Livraison.produit_id=produit.id
                           inner join Bon_Livraison on Bon_Livraison.Nentre=produit_Bon_Livraison.facture_id
                           where Bon_Livraison.fournisseur=" + int.Parse(this.Tag.ToString()) + " and Bon_Livraison.Verification='Verifié' and 1=1";
            SqlCommand cmd = new SqlCommand();



            if (N_Bon.Text != "")
            {

                sql += "and productid like @id ";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value ="%"+ N_Bon.Text +"%";
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

            guna2DataGridView1.Columns[0].Visible = false;

            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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

        private void N_Bon_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }


            int x = guna2DataGridView1.CurrentRow.Index;
            Program.main.product_achat_history1.loadData(int.Parse(this.Tag.ToString()), int.Parse(guna2DataGridView1.Rows[x].Cells[0].Value.ToString()));
            Program.main.product_achat_history1.BringToFront();
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }



            Program.main.product_achat_history1.loadData(int.Parse(this.Tag.ToString()), int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
            Program.main.product_achat_history1.BringToFront();
        }
    }
}
