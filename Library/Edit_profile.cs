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
    public partial class Edit_profile : UserControl
    {
        public Edit_profile()
        {
            InitializeComponent();
        }

        private void Edit_profile_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            Curren_PW__TB.PasswordChar = '\u25cf';
            NEW_PW__TB.PasswordChar = '\u25cf';
            loadData();

        }
        public void loadData()
        {

            string sql = "select * from users where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = Login.id;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                username_TB.Text = dr.GetValue(1).ToString();
                Name__TB.Text = dr.GetValue(3).ToString();
                Email__TB.Text = dr.GetValue(4).ToString();

            }
            dr.Close();
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            if (Program.CheckEmpty(username_TB) || Program.CheckEmpty(Name__TB) || Program.CheckEmpty(Email__TB))
            {

                return;
            }

            if (!Program.CheckEmpty(Curren_PW__TB))
            {
                string sql = "";
                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                if (!String.IsNullOrWhiteSpace(NEW_PW__TB.Text))
                {
                    sql= "update Users set username=@username,nom_complet=@nom,email=@email,password=@NewPW where id=@id and password=@CurrentPW";
                    cmd.Parameters.Add("@NewPW", SqlDbType.VarChar).Value = NEW_PW__TB.Text;
                }
                else
                {
                    sql = "update Users set username=@username,nom_complet=@nom,email=@email where id=@id and password=@CurrentPW";
                }
                
                cmd.CommandText = sql;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username_TB.Text;
                cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = Name__TB.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email__TB.Text;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Login.id;
                cmd.Parameters.Add("@CurrentPW", SqlDbType.VarChar).Value = Curren_PW__TB.Text;
                int x = cmd.ExecuteNonQuery();
                if (x>0)
                {
                    MessageBox.Show("Bien enregistrée");
                    string[] s = Name__TB.Text.Split(' ');
                    if(s.Length > 1)
                    {

                        ((MainForm)this.ParentForm).guna2CircleButton1.Text = s[0][0] + "" + s[1][0];
                    }
                    else
                    {
                        ((MainForm)this.ParentForm).guna2CircleButton1.Text = Name__TB.Text[0].ToString();
                    }

                }
                else
                {
                    MessageBox.Show("Mot de passe incrorrecte");
                }

            }
        }
    }
}
