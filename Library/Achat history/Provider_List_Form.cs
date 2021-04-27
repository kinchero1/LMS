using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace Library
{
    public partial class Provider_List_Form : Form
    {
        
        choose_history Choose;
        int index;
        //index 0 commande
        //index 1 dossier de marche
        //index 2 barcode printer
        //index 3 del form
        //index 4 payment
        //index 5 edit folder
       // index 6 edit del form
       //index 7 achat history
        public Provider_List_Form(choose_history choose)
        {
            InitializeComponent();
            Choose = choose;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Product_List_Form_Load(object sender, EventArgs e)
        {

            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
            string sql = "select distinct  id ,Nom,Email,Adresse,Tel from Fournisseur where 1 = 1";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].Visible = false;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            gunaAnimateWindow1.Start();
        }

        /*   void loadData()
           {
               string sql = "select id as 'ID',(nom+' '+matiere+' '+Nscolaire) as 'Designation' from produit";
               SqlCommand cmd = new SqlCommand(sql, Program.getcon());
               DataTable dt = new DataTable();
               dt.Load(cmd.ExecuteReader());
               guna2DataGridView1.DataSource = dt;
               guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



           }

           void loadGeneratedProducts()
           {
               string sql = "select id as 'ID',(nom+' '+matiere+' '+Nscolaire) as 'Designation' from produit where codebarStatus like generated";
               SqlCommand cmd = new SqlCommand(sql, Program.getcon());
               DataTable dt = new DataTable();
               dt.Load(cmd.ExecuteReader());
               guna2DataGridView1.DataSource = dt;
               guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



           }*/

       



    


        void filters()
        {
            string sql = "select distinct  id ,Nom,Email,Adresse,Tel from Fournisseur where 1 = 1";
            SqlCommand cmd = new SqlCommand();


            

            if (nom.Text != "")
            {
                sql += "and nom like @nom ";
                cmd.Parameters.Add("@nom", SqlDbType.NVarChar).Value = "%" + nom.Text + "%";


            }

            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();


            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;

            guna2DataGridView1.Columns[0].Visible = false;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nom_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();

        }

        private void matiere_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Ns_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            visualKeyboard1.Parent = this;
            visualKeyboard1.BringToFront();
            visualKeyboard1.ShowKeyboard(nom, new Point(343, 97));
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void produit_id_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            Choose.showPerProvider(int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
            this.Hide();
        }
    }
}
