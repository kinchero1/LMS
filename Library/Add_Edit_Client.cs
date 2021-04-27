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
    public partial class Add_Edit_Client : UserControl
    {
        public Add_Edit_Client()
        {
            InitializeComponent();
        }
        public bool clientExist(string cin)
        {
            string sql = "select count(*) from client where cin = @cin";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@cin", SqlDbType.NVarChar).Value = cin;
            int x =(int) cmd.ExecuteScalar();
            return x > 0;
        }
        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (clientExist(Cin_TB.Text))
            {
                MessageBox.Show("Client avec le Cin ou le numero : " + Cin_TB.Text + " est deja exist");
                return;
            }
            string sql = "insert into client values(@cin,@nom,@prenom,@tel,@email,@adresse)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            if (Program.CheckEmpty(Cin_TB) || Program.CheckEmpty(Nom_TB))
            {
                return;
            }
            cmd.Parameters.Add("@cin", SqlDbType.VarChar).Value = Cin_TB.Text;
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = Nom_TB.Text;
            cmd.Parameters.Add("@prenom", SqlDbType.VarChar).Value = Prenom_TB.Text;
            cmd.Parameters.Add("@tel", SqlDbType.VarChar).Value = Tel_TB.Text;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email_TB.Text;
            cmd.Parameters.Add("@adresse", SqlDbType.VarChar).Value = Adresse_TB.Text;
            int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Ajout bien Fait");
            }
        }

        private void Add_Edit_Client_Load(object sender, EventArgs e)
        {

        }

        public void loadEditEnv(string cin)
        {
            gunaAdvenceButton1.Visible = false;
            gunaAdvenceButton2.Visible = true;
            gunaAdvenceButton4.Visible = (checkClientTransaction(cin)==0);
            Cin_TB.ReadOnly = true;
            string sql = "select * from client where cin = @cin";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@cin", SqlDbType.VarChar).Value = cin;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                Cin_TB.Text = dr.GetValue(0).ToString();
                Nom_TB.Text = dr.GetValue(1).ToString();
                Prenom_TB.Text = dr.GetValue(2).ToString();
                Tel_TB.Text = dr.GetValue(3).ToString();
                Email_TB.Text = dr.GetValue(4).ToString();
                Adresse_TB.Text = dr.GetValue(5).ToString();


            }
            dr.Close();

        }
        public void loadAddEnv()
        {
            gunaAdvenceButton1.Visible = true;
            gunaAdvenceButton2.Visible = false;
            gunaAdvenceButton4.Visible = false;
            Cin_TB.ReadOnly = false;
            Cin_TB.Clear();
            Nom_TB.Clear();
            Tel_TB.Clear();
            Prenom_TB.Clear();
            Email_TB.Clear();
            Adresse_TB.Clear();


        }

        int checkClientTransaction(string cin)
        {
            string sql = "select count(*) from bill where client_CIN=@cin";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@cin", SqlDbType.VarChar).Value = cin;
            return (int)cmd.ExecuteScalar();
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            string sql = "delete from client where cin=@cin";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
          
            cmd.Parameters.Add("@cin", SqlDbType.VarChar).Value = Cin_TB.Text;
            int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Suppression bien Fait");
            }


        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            string sql = "update client set nom=@nom,prenom=@prenom,tel=@tel,email=@email,adresse=@adresse where cin=@cin ";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            if (Program.CheckEmpty(Nom_TB))
            {
                return;
            }
            cmd.Parameters.Add("@cin", SqlDbType.VarChar).Value = Cin_TB.Text;
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = Nom_TB.Text;
            cmd.Parameters.Add("@prenom", SqlDbType.VarChar).Value = Prenom_TB.Text;
            cmd.Parameters.Add("@tel", SqlDbType.VarChar).Value = Tel_TB.Text;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email_TB.Text;
            cmd.Parameters.Add("@adresse", SqlDbType.VarChar).Value = Adresse_TB.Text;
            int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Modification bien Fait");
            }
        }

        private void Cin_TB_MouseClick(object sender, MouseEventArgs e)
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
