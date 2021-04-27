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
using System.Diagnostics;

namespace Library
{
    public partial class Bills_List : UserControl
    {
        
        public Bills_List()
        {
            InitializeComponent();
        }
        bool FilterDate = false;
        void color()
        {
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                if (guna2DataGridView1.Rows[i].Cells[4].Value.ToString() == "Payé")
                {
                    guna2DataGridView1.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(0, 179, 89);
                }
                else
                {
                    guna2DataGridView1.Rows[i].Cells[4].Style.ForeColor = Color.FromArgb(204, 0, 0);
                }
            }
        }


        public void loadData()
        {
            
            FilterDate = false;
            Program.fillcomboboxClient(Client, true);
            Program.fillcomboboxproduit(Designation, true);

            string sql =Program.normalMOD? "select id as'N°',(client.nom +' '+client.prenom) As 'Nom Client' ,datesortie as 'Date de sortie',montant as 'Montant Total',Status,montantRest as 'Montant a payé' from Bill inner join client on client.CIN = Bill.client_CIN": "select id as'N°',(client.nom +' '+client.prenom) As 'Nom Client' ,datesortie as 'Date de sortie',montant as 'Montant Total',Status,montantRest as 'Montant a payé' from BillAR inner join client on client.CIN = BillAR.client_CIN";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[3].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "0.00 DH";
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";
            Program.checkheight(442, guna2DataGridView1, empty_panel);
            color();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Payment_list_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void filters()
        {
            string sql = Program.normalMOD? "select id as'N°',(client.nom +' '+client.prenom) As 'Nom Client' ,datesortie as 'Date de sortie',montant as 'Montant Total',Status from Bill inner join client on client.CIN = Bill.client_CIN where 1=1 ": "select id as'N°',(client.nom +' '+client.prenom) As 'Nom Client' ,datesortie as 'Date de sortie',montant as 'Montant Total',Status from BillAR inner join client on client.CIN = BillAR.client_CIN where 1=1 ";
            SqlCommand cmd = new SqlCommand();

            if (N_Facture.Text != "")
            {

                sql += "and id=@id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = N_Facture.Text;
            }

            if (Client.SelectedIndex > 0)
            {
                sql += "and CIN like @cin ";
                cmd.Parameters.Add("@cin", SqlDbType.VarChar).Value = Client.SelectedValue;


            }

            if (Designation.SelectedIndex > 0)
            {
                sql += Program.normalMOD?"and id in (select Bill_id from Bill_products  where Bill_products.product_id=@pid) ":"and id in (select Bill_id from Bill_productsAR  where Bill_productsAR.product_id=@pid) ";
                cmd.Parameters.Add("@pid", SqlDbType.Int).Value = Designation.SelectedValue;

            }
            if (FilterDate)
            {
                sql += " and datesortie between  @first and @second";
                cmd.Parameters.Add("@first", SqlDbType.DateTime).Value = guna2DateTimePicker1.Value;
                cmd.Parameters.Add("@second", SqlDbType.DateTime).Value = guna2DateTimePicker2.Value;

            }

            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();


            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            Program.checkheight(442, guna2DataGridView1, empty_panel);
            color();
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[3].DefaultCellStyle.Format = "0.00 DH";
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";


        }

        private void N_Facture_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Client_SelectedIndexChanged(object sender, EventArgs e)
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

        private void gunaAdvenceButton1_Click_1(object sender, EventArgs e)
        {
            loadData();
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                MainForm form = (MainForm)this.ParentForm;
                form.viewEdit_Bill1.loadData(int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                form.viewEdit_Bill1.BringToFront();

            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }

            int selectedrow = guna2DataGridView1.SelectedRows[0].Index;
            int id = int.Parse(guna2DataGridView1.Rows[selectedrow].Cells[0].Value.ToString());
            MainForm form = (MainForm)this.ParentForm;
            form.viewEdit_Bill1.loadData(id);
            form.viewEdit_Bill1.BringToFront();

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if(guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }
            ExcelHelper ex = ExcelHelper.Export(@"Models\List Des facture.xlsx", "Facture list", false);
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
