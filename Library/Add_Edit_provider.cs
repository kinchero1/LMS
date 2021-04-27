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
using Guna.UI2.WinForms;

namespace Library
{
    public partial class Add_provider : UserControl
    {
        public Add_provider()
        {
            InitializeComponent();
        }

        int checkproviderTransaction(int id )
        {
            string sql = "select count(*) from Bon_Livraison where fournisseur=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
            return (int)cmd.ExecuteScalar();
        }

        public void loadEditEnv(int id)
        {
            gunaAdvenceButton1.Visible = false;
            gunaAdvenceButton2.Visible = true;
            gunaAdvenceButton4.Visible = (checkproviderTransaction(id) == 0);
         
            string sql = "select * from fournisseur where id = @id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
               
                this.Tag = dr.GetValue(0).ToString();
                Nom_TB.Text = dr.GetValue(1).ToString();
                email_TB.Text = dr.GetValue(2).ToString();
                Adresse_TB.Text = dr.GetValue(3).ToString();
                Tel_TB.Text = dr.GetValue(4).ToString();

                
              


            }
            dr.Close();

        }
        public void loadAddEnv()
        {
            gunaAdvenceButton1.Visible = true;
            gunaAdvenceButton2.Visible = false;
            gunaAdvenceButton4.Visible = false;
            Nom_TB.Clear();
            Tel_TB.Clear();
            email_TB.Clear();
            Adresse_TB.Clear();
           
            
        }


        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            string sql = "insert into Fournisseur values(@nom,@email,@adresse,@tel)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            if ( Program.CheckEmpty(Nom_TB))
            {
                return;
            }
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = Nom_TB.Text;
            cmd.Parameters.Add("@tel", SqlDbType.VarChar).Value = Tel_TB.Text;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email_TB.Text;
            cmd.Parameters.Add("@adresse", SqlDbType.VarChar).Value = Adresse_TB.Text;
            int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Ajout bien Fait");
            }
        }

        private void Add_provider_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            string sql = "update  Fournisseur set nom=@nom,email=@email,adresse=@adresse,tel=@tel where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            if (Program.CheckEmpty(Nom_TB))
            {
                return;
            }
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = Nom_TB.Text;
            cmd.Parameters.Add("@tel", SqlDbType.VarChar).Value = Tel_TB.Text;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email_TB.Text;
            cmd.Parameters.Add("@adresse", SqlDbType.VarChar).Value = Adresse_TB.Text;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Tag;
            int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Modification bien Fait");
            }
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            string sql = "delete from Fournisseur where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
         
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Tag;
            int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Suppression bien Fait");
            }
        }

        private void Nom_TB_MouseClick(object sender, MouseEventArgs e)
        {
            Guna2TextBox t = (Guna2TextBox)sender;
            if (!visualKeyboard1.Visible)
            {
                visualKeyboard1.Visible = true;
            }

            visualKeyboard1.SetTarget(t);
        }
    }
}
