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
    public partial class Provider_List : UserControl
    {
        public Provider_List()
        {
            InitializeComponent();
        }

        private void Provider_List_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;

            }
            loadData();
            Program.checkheight(442, guna2DataGridView1, empty_panel);

        }

        public void loadData()
        {
            string sql = "select id as 'ID', nom as 'Nom',email as 'Email',adresse as 'Adresse',tel as 'Tel' from Fournisseur";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            label7.Text = guna2DataGridView1.RowCount + "en Total";

        }


        void filters()
        {
            string sql = "select id as 'ID', nom as 'Nom',email as 'Email',adresse as 'Adresse',tel as 'Tel' from Fournisseur where 1=1";
            SqlCommand cmd = new SqlCommand();

            if (ID.Text != "")
            {

                sql += "and id=@id ";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID.Text;
            }
            if (Nom.Text != "")
            {

                sql += "and nom like @nom ";
                cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = "%"+ Nom.Text+ "%";
            }

            if (Tel.Text != "")
            {

                sql += "and Tel like @Tel ";
                cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = "%"+ Tel.Text+ "%";
            }
            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            Program.checkheight(442, guna2DataGridView1, empty_panel);


            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";



        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            Program.checkheight(442, guna2DataGridView1, empty_panel);

        }

        private void ID_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Nom_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Tel_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex !=-1)
            {
                int id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                MainForm form = (MainForm)this.ParentForm;
                form.add_Provider1.loadEditEnv(id);
                form.add_Provider1.BringToFront();
                form.setText("Fournisseur N° : " + id);
            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }

            int selectedrow = guna2DataGridView1.SelectedRows[0].Index;
            int id = int.Parse(guna2DataGridView1.Rows[selectedrow].Cells[0].Value.ToString());
            MainForm form = (MainForm)this.ParentForm;
            form.add_Provider1.loadEditEnv(id);
            form.add_Provider1.BringToFront();
            form.setText("Fournisseur N° : " + id);

        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }
            ExcelHelper ex = ExcelHelper.Export(@"Models\list des fournisseur.xlsx", "list des fournisseur", false);
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            visualKeyboard1.ShowKeyboard(Tel, new Point(473, 49));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            visualKeyboard1.ShowKeyboard(Nom, new Point(317, 51));
        }
    }
}
