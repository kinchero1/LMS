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
    public partial class Client_List : UserControl
    {
        public Client_List()
        {
            InitializeComponent();
        }

        private void Client_List_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();
        }
        public void loadData()
        {
            string sql = "Select cin as 'C.I.N',(nom+' '+prenom) as 'Nom',tel as 'Tel',email as 'Email',adresse as 'Adresse' from client where cin not like 1";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            label7.Text = guna2DataGridView1.RowCount + "en Total";
            Program.checkheight(442, guna2DataGridView1, empty_panel);
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        void filters()
        {
            string sql = "Select cin as 'C.I.N',(nom+' '+prenom) as 'Nom',tel as 'Tel',email as 'Email',adresse as 'Adresse' from client where cin not like 1";
            SqlCommand cmd = new SqlCommand();

            if (CIN.Text != "")
            {

                sql += "and cin like @cin ";
                cmd.Parameters.Add("@cin", SqlDbType.VarChar).Value = "%"+ CIN.Text+"%";
            }
            if (Nom.Text != "")
            {

                sql += "and nom like @nom ";
                cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = "%"+ Nom.Text+"%";
            }

            if (Prenom.Text != "")
            {

                sql += "and prenom like @prenom ";
                cmd.Parameters.Add("@prenom", SqlDbType.VarChar).Value = "%" + Prenom.Text+ "%" ;
            }

            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";
            Program.checkheight(442, guna2DataGridView1, empty_panel);

        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex!=-1)
            {
                string cin = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                MainForm form = (MainForm)this.ParentForm;
                form.Add_Client.loadEditEnv(cin);
                form.Add_Client.BringToFront();
                form.setText("Client CIN : " + cin);

            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }

            int selectedrow = guna2DataGridView1.SelectedRows[0].Index;
            string cin = guna2DataGridView1.Rows[selectedrow].Cells[0].Value.ToString();
            MainForm form = (MainForm)this.ParentForm;
            form.Add_Client.loadEditEnv(cin);
            form.Add_Client.BringToFront();
            form.setText("Client CIN : " + cin);


        }

        private void CIN_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Nom_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Prenom_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }

            ExcelHelper ex = ExcelHelper.Export(@"Models\List des client.xlsx", "list des client", false);
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
            visualKeyboard1.ShowKeyboard(Nom, new Point(313, 50));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            visualKeyboard1.ShowKeyboard(Prenom, new Point(472, 54));
        }
    }
}
