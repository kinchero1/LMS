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
    public partial class Edit_Order : UserControl
    {
        public Edit_Order()
        {
            InitializeComponent();
        }

        private void Edit_Order_Load(object sender, EventArgs e)
        {

        }
        int GetStatusCommand(int cid)
        {
            SqlCommand cmd = new SqlCommand("select sum(quantiteReste) from produit_commande where commande_id=@Cid", Program.getcon());
            cmd.Parameters.Add("@Cid", SqlDbType.Int).Value = cid;
            return (int)cmd.ExecuteScalar();
        }
        void loadHeader(int i)
        {
            string sql = @"select commande.id,Fournisseur.nom,datecommande from commande
                          inner join Fournisseur on Fournisseur.id = commande.Fournisseur_id
                          where commande.id=@id";

            string sqlAR = @"select commandeAR.id,Fournisseur.nom,datecommande from commandeAR
                          inner join Fournisseur on Fournisseur.id = commandeAR.Fournisseur_id
                          where commandeAR.id=@id";

            SqlCommand cmd = new SqlCommand(Program.normalMOD? sql:sqlAR, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = i;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                DateTime date = DateTime.Parse(dr.GetValue(2).ToString());
                Facture_N.Text = dr.GetValue(0).ToString() + "/" + date.ToString("yy");
                Date_commande.Text = date.ToShortDateString();
                Provider_name.Text = dr.GetValue(1).ToString();
            }
            dr.Close();
            gunaAdvenceButton1.Visible = (GetStatusCommand(i) != 0);
        }

        public void loadData(int i)
        {
            string sql = @"select product_id as 'ID PD',(nom+' '+matiere+' '+Nscolaire) as 'Designation',produit_commande.unite as 'unité',produit_commande.quantiteDemande as 'Quantite demandé',produit_commande.quantiteLivrée as 'Quantite Livré', produit_commande.quantiteReste as 'Quantite Reste' from produit_commande
                           inner join produit on produit_commande.product_id=produit.id
                           where  produit_commande.commande_id=@commandeId";

            string sqlAR = @"select product_id as 'ID PD',(nom+' '+matiere+' '+Nscolaire) as 'Designation',produit_commandeAR.unite as 'unité',produit_commandeAR.quantiteDemande as 'Quantite demandé',produit_commandeAR.quantiteLivrée as 'Quantite Livré', produit_commandeAR.quantiteReste as 'Quantite Reste' from produit_commandeAR
                           inner join produit on produit_commandeAR.product_id=produit.id
                           where  produit_commandeAR.commande_id=@commandeId";

            SqlCommand cmd = new SqlCommand(Program.normalMOD? sql:sqlAR, Program.getcon());
            cmd.Parameters.Add("@commandeId", SqlDbType.Int).Value = i;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            loadHeader(i);
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Tag = i;
            Program.checkheight(392, guna2DataGridView1);
            label9.Text = guna2DataGridView1.RowCount + " en Total";
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.ShowDialog("Vous Voulez vraiment Supprimer cette commande", true) == DialogResult.OK)
            {
                string sql = "delete from produit_commande where produit_commande.commande_id=@commandeId";
                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@commandeId", SqlDbType.Int).Value = this.Tag;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from commande where id=@commandeId";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Suppression bien fait");
                Program.insertLog("Suppression commande  : " + Facture_N.Text, "Order", "");

            }

        }

        void export(ExcelHelper ex,DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ex.insertLine(11 + i);

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ex.writecell(i, j, dt.Rows[i][j].ToString(), 11, 1, false);
                }

            }

            ex.writecell("C4", "Date Le :" + DateTime.Now.ToShortDateString());
            ex.writecell("C7", "Fournisseur :" + Provider_name.Text);
            ex.writecell("B6", "Commande N°  :" + this.Tag + "/" + DateTime.Now.Year.ToString());

            ex.SaveAndClose(true);
        }
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            string sql = @"select row_number() over ( order by produit.id),(nom+' '+matiere+' '+Nscolaire),produit_commande.unite,produit_commande.quantiteDemande from produit_commande
                           inner join produit on produit_commande.product_id=produit.id
                          where  produit_commande.commande_id=@commandeId";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@commandeId", SqlDbType.Int).Value = this.Tag;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
         
            ExcelHelper ex = ExcelHelper.Export(@"Models\commande.xlsx", "Commande", true);
            if (ex == null)
            {
                return;
            }
            if (Program.main.exporting)
            {
                return;
            }
            Program.main.setProgressBarVisisblité(true);
            Task.Run(() => export(ex, dt));
            Program.main.setProgressBarVisisblité(false);



        }


        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            MainForm form = (MainForm)this.ParentForm;
            form.Delivery_Form1.Add_delivryForm_Commande(int.Parse(this.Tag.ToString()), guna2DataGridView1, Provider_name.Text);
        }
    }
}
