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
    public partial class User_Setting : UserControl
    {
        public User_Setting()
        {
            InitializeComponent();
        }

        private void User_Setting_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            Permission.Items.Add("Admin");
            Permission.Items.Add("Normal Compte");
            loadData();
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }
            checkAccountStatus(int.Parse(guna2DataGridView1.CurrentRow.Cells[0].Value.ToString()));
        }
        public void loadData()
        {
            string sql = "select id as 'Id', nom_complet as 'Nom Complet', email as 'Email',account_Type as 'Compte Status' from Users where id !=@id ";
            
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = Login.id;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            label7.Text = guna2DataGridView1.Rows.Count + " en Totale";
            
        }

        public void checkAccountStatus(int id)
        {
           
            string sql = "select account_Type from users where id=@id";
            SqlCommand cmd = new SqlCommand(sql,Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            string status = (string)cmd.ExecuteScalar();
            block_button.Visible = (status != "bloqué" && status != "en attente");
            if (status != "bloqué" && status != "en attente")
            {
                Permission.Text = status;
            }
            
        }

        private void block_button_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            string sql = "update users set account_Type=@type where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "bloqué";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
            loadData();

        }

        private void Check_button_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            string sql = "update users set account_Type=@type where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = Permission.Text.ToUpper();
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
            loadData();
            checkAccountStatus(int.Parse(guna2DataGridView1.CurrentRow.Cells[0].Value.ToString()));

        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.ShowDialog("Vous vouler vraiment supprimer ce utilisateur",true)==DialogResult.OK)
            {
                string id = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sql = "delete from users where id=@id";
                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
                loadData();
            }
           
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows.Count<=0)
            {
                return;
            }
            int id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            checkAccountStatus(id);
        }
    }
}
