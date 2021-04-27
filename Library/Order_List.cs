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
    public partial class Order_List : UserControl
    {
        bool FilterDate = false;
        public Order_List()
        {
            InitializeComponent();
        }
        
        private void Order_List_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();
        }
        public void loadData()
        {
            FilterDate = false;
            string sql = Program.normalMOD? "select commande.id as 'N°',Fournisseur.nom as 'Fournisseur',datecommande as 'Date Commande',Status from commande inner join Fournisseur on commande.Fournisseur_id = Fournisseur.id" : "select commandeAR.id as 'N°',Fournisseur.nom as 'Fournisseur',datecommande as 'Date Commande',Status from commandeAR inner join Fournisseur on commandeAR.Fournisseur_id = Fournisseur.id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Program.fillcomboboxFournisseur(fournisseur, true);
            Program.fillcomboboxproduit(Designation, true);

        }

        void filters()
        {
            string sql = Program.normalMOD ? "select commande.id as 'ID',Fournisseur.nom as 'Fournisseur',datecommande as 'Date Commande',Status from commande inner join Fournisseur on commande.Fournisseur_id = Fournisseur.id where 1 = 1" : "select commandeAR.id as 'ID',Fournisseur.nom as 'Fournisseur',datecommande as 'Date Commande',Status from commandeAR inner join Fournisseur on commandeAR.Fournisseur_id = Fournisseur.id where 1 = 1";
            SqlCommand cmd = new SqlCommand();

            if (N_commande.Text != "")
            {

                sql += "and commande.id=@id ";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = N_commande.Text;
            }

            if (fournisseur.SelectedIndex > 0)
            {
                sql += "and Fournisseur_id = @F_id ";
                cmd.Parameters.Add("@F_id", SqlDbType.Int).Value = fournisseur.SelectedValue;


            }

            if (Designation.SelectedIndex > 0)
            {
                sql += Program.normalMOD ? " and commande.id in (select produit_commande.commande_id from produit_commande where produit_commande.product_id=@pid)": "  and commandeAR.id in (select produit_commande.commande_id from produit_commandeAR where produit_commandeAR.product_id = @pid)";
                cmd.Parameters.Add("@pid", SqlDbType.Int).Value = Designation.SelectedValue;

            }
            if (FilterDate)
            {
                sql += " and datecommande between  @first and @second";
                cmd.Parameters.Add("@first", SqlDbType.DateTime).Value = guna2DateTimePicker1.Value;
                cmd.Parameters.Add("@second", SqlDbType.DateTime).Value = guna2DateTimePicker2.Value;
            }
            

            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();


            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            Program.checkheight(442, guna2DataGridView1, empty_panel);
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";


        }

        private void N_commande_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void fournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Designation_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            int i = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            MainForm form = (MainForm)this.ParentForm;
            form.edit_Order1.loadData(i);
            form.edit_Order1.BringToFront();
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }
            ExcelHelper ex = ExcelHelper.Export(@"Models\list des commande.xlsx", "List Des commandes", false);
            if (ex == null)
            {
                return;
            }
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                for (int j = 0; j < guna2DataGridView1.ColumnCount; j++)
                {
                    ex.writecell(i, j, guna2DataGridView1.Rows[i].Cells[j].Value.ToString(), 2, 1, false);
                }
            }
            ex.SaveAndClose();
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <=0)
            {
                return;
            }
            int selectedrow = guna2DataGridView1.SelectedRows[0].Index;
            int i = int.Parse(guna2DataGridView1.Rows[selectedrow].Cells[0].Value.ToString());
            MainForm form = (MainForm)this.ParentForm;
            form.edit_Order1.loadData(i);
            form.edit_Order1.BringToFront();
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

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            FilterDate = false;
            loadData();
        }
    }
}
