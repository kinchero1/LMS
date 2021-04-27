using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace Library
{
    public partial class product_list : UserControl
    {
        public product_list()
        {
            InitializeComponent();

        }

        private void product_list_Load(object sender, EventArgs e)
        {


            if (this.DesignMode)
            {
                return;
            }
      
            load_data();
        }
        public void load_data()
        {
            string sql = "select id,(nom +' ' +matiere+ ' '+Nscolaire) as 'Nom',categorie as 'Categorie',prix as 'Prix',qte as 'Stock',emplacement as 'Emplacement' from produit";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            Program.checkheight(342, guna2DataGridView1,empty_panel);
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            /*cat.Items.Add("Tout");
            matiere.Items.Add("Tout");
            Ns.Items.Add("Tout");*/
          
            Program.readfile("cat.txt", cat,true,true);
            Program.readfile("mat.txt", matiere,false,true);
            Program.readfile("Ns.txt", Ns,true,true);
            cat.SelectedIndex = 0;
            matiere.SelectedIndex = 0;
            Ns.SelectedIndex = 0;
        }

      

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
          
        }

        
        void filters()
        {
            string sql = "select id,(nom +' ' +matiere+ ' '+Nscolaire) as 'Nom',categorie as 'Categorie',prix as 'Prix',qte as 'Stock',emplacement as 'Emplacement' from produit where 1 = 1";
            SqlCommand cmd = new SqlCommand();

            if(id.Text != "")
            {
                sql += "and id=@id ";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id.Text;
            }

            if (nom.Text != "")
            {
                sql += "and nom like @nom ";
                cmd.Parameters.Add("@nom", SqlDbType.NVarChar).Value = "%"+ nom.Text+"%";


            }

            if (cat.SelectedIndex > 0)
            {
                sql += "and categorie like @cat ";
                cmd.Parameters.Add("@cat", SqlDbType.VarChar).Value = cat.Text;

            }
            if (matiere.SelectedIndex > 0)
            {
               
                sql += "and matiere like @matiere ";
                cmd.Parameters.Add("@matiere", SqlDbType.NVarChar).Value = matiere.Text;
            }


            if (Ns.SelectedIndex > 0)
            {
                sql += "and Nscolaire like @Ns ";
                cmd.Parameters.Add("@Ns", SqlDbType.NVarChar).Value = Ns.Text.Trim();

            }

            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();


            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            Program.checkheight(342, guna2DataGridView1, empty_panel);



        }

        private void id_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void nom_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void guna2DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            guna2DataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void guna2DataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            guna2DataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }

        private void matiere_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Ns_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            MainForm form = (MainForm)this.ParentForm;
            form.view_product1.Tag = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            form.view_product1.load_data(int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
            form.setText("Produit N° : " + guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if(guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }


            int x = guna2DataGridView1.CurrentRow.Index;
            MainForm form = (MainForm)this.ParentForm;
            form.view_product1.Tag = int.Parse(guna2DataGridView1.Rows[x].Cells[0].Value.ToString());
            form.view_product1.load_data(int.Parse(guna2DataGridView1.Rows[x].Cells[0].Value.ToString()));
            form.setText("Produit N° : " + guna2DataGridView1.Rows[x].Cells[0].Value.ToString());
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }
        void export(ExcelHelper ex,DataTable dt)
        {
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ex.insertLine(4 + i);

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ex.writecell(i, j, dt.Rows[i][j].ToString(), 4, 1, false);
                }

            }
            ex.SaveAndClose();
        }
        private async void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            string sql = "select id, (nom +' '+matiere+' '+Nscolaire) as 'nom',prix as 'Prix',qte as 'Stock',valeur as 'Valeur Stock', emplacement as 'Emplacement' from produit";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            
            dt.Load(cmd.ExecuteReader());
            if(dt.Rows.Count <= 0)
            {
                return;
            }

            ExcelHelper ex = ExcelHelper.Export(@"Models\List Des produit.xlsx", "List Des produit", false);
            if (ex == null)
            {
                return;
            }

            Program.main.setProgressBarVisisblité(true);
            await Task.Run(() => export(ex,dt));
            Program.main.setProgressBarVisisblité(false);
            

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            visualKeyboard1.ShowKeyboard(nom, new Point(223, 123));
        }

        private void nom_Leave(object sender, EventArgs e)
        {
           
        }

       
    }
}
