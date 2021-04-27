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
    public partial class Delivery_Form_List : UserControl
    {
        bool FilterDate = false;

        public Delivery_Form_List()
        {
            InitializeComponent();
        }
        
        private void Delivery_Form_List_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            FilterDate = false;
            loaddata();
        }

        void color()
        {
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                if (guna2DataGridView1.Rows[i].Cells[7].Value.ToString() == "Verifié")
                {
                    guna2DataGridView1.Rows[i].Cells[7].Style.ForeColor = Color.FromArgb(0, 179, 89);
                }
                else
                {
                    guna2DataGridView1.Rows[i].Cells[7].Style.ForeColor = Color.FromArgb(204, 0, 0);
                }
            }
        }


      
        public void loaddata()
        {
            
            string sql = Program.normalMOD ? "select Nentre as 'ID',Nbon as 'N° de Bon', Fournisseur.nom as 'Fournisseur',dateentre as 'Date',Montant_Ht as 'Montant HT',montant_Tva as 'Montant Tva',montant_TTC as 'Montant_TTC',Verification as 'Status'  from Bon_Livraison inner join Fournisseur on Fournisseur.id = Bon_Livraison.fournisseur" : "select Nentre as 'ID',Nbon as 'N° de Bon', Fournisseur.nom as 'Fournisseur',dateentre as 'Date',Montant_Ht as 'Montant HT',montant_Tva as 'Montant Tva',montant_TTC as 'Montant_TTC',Verification as 'Status'  from Bon_LivraisonAR inner join Fournisseur on Fournisseur.id = Bon_LivraisonAR.fournisseur";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            Program.checkheight(442, guna2DataGridView1,empty_panel);
            Program.fillcomboboxproduit(Designation, true);
            Program.fillcomboboxFournisseur(fournisseur, true);
            color();
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[4].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[6].DefaultCellStyle.Format = "0.00 DH";
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void filters()
        {
            string sql =Program.normalMOD ? "select Nentre as 'ID',Nbon as 'N° de Bon', Fournisseur.nom as 'Fournisseur',dateentre as 'Date',Montant_Ht as 'Montant HT',montant_Tva as 'Montant Tva',montant_TTC as 'Montant_TTC',Verification as 'Status'  from Bon_Livraison inner join Fournisseur on Fournisseur.id = Bon_Livraison.fournisseur where 1 =1": "select Nentre as 'ID',Nbon as 'N° de Bon', Fournisseur.nom as 'Fournisseur',dateentre as 'Date',Montant_Ht as 'Montant HT',montant_Tva as 'Montant Tva',montant_TTC as 'Montant_TTC',Verification as 'Status'  from Bon_LivraisonAR inner join Fournisseur on Fournisseur.id = Bon_LivraisonAR.fournisseur where 1 =1 ";
            SqlCommand cmd = new SqlCommand();

            if (N_Bon.Text != "")
            {

                sql += "and Nbon=@id ";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = N_Bon.Text;
            }

            if (fournisseur.SelectedIndex > 0)
            {
                sql += "and fournisseur like @nom ";
                cmd.Parameters.Add("@nom", SqlDbType.Int).Value = fournisseur.SelectedValue;


            }

            if (Designation.SelectedIndex > 0)
            {
                sql +=Program.normalMOD ? "and Nentre in (select facture_id from produit_Bon_Livraison where produit_id = @pid) " : "and Nentre in (select facture_id from produit_Bon_LivraisonAR where produit_id = @pid)";
                cmd.Parameters.Add("@pid", SqlDbType.Int).Value = Designation.SelectedValue;

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
            Program.checkheight(442, guna2DataGridView1,empty_panel);
            color();
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[4].DefaultCellStyle.Format = "N2";
            guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "N2";
            guna2DataGridView1.Columns[6].DefaultCellStyle.Format = "N2";
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";

        }

        private void id_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void fournisseur_TextChanged(object sender, EventArgs e)
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
            
            if(e.RowIndex == -1)
            {
                return;
            }

            MainForm x = (MainForm)this.ParentForm;
            int id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            x.deliveyForm_ProductList1.loaddata(id);
            x.setText("Bon de Livraison N° : " + id);

        }

        private void guna2DataGridView1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            FilterDate = false;
            loaddata();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count<=0)
            {
                return;
            }
            int selectedrow = guna2DataGridView1.SelectedRows[0].Index;
            int id = int.Parse(guna2DataGridView1.Rows[selectedrow].Cells[0].Value.ToString());
            MainForm x = (MainForm)this.ParentForm;
            x.deliveyForm_ProductList1.loaddata(id);

            x.setText("Bon de Livraison N° : " + id);

        }
        void export(ExcelHelper ex)
        {
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                for (int j = 0; j < guna2DataGridView1.ColumnCount; j++)
                {
                    ex.writecell(i, j, guna2DataGridView1.Rows[i].Cells[j].Value.ToString(), 2, 1, false);
                }
            }
            ex.SaveAndClose();
        }
        private async void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if(guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }

            ExcelHelper ex = ExcelHelper.Export(@"Models\DelForm.xlsx", "Bon livraison", false);
            if (ex == null)
            {
                return;
            }

            Program.main.setProgressBarVisisblité(true);
            await Task.Run(() => { export(ex); });
            Program.main.setProgressBarVisisblité(false);
      
           
        }

        private void N_Bon_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
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
    }
}
