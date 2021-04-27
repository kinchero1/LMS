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

namespace Library
{
    public partial class Product_Finder : Form
    {
        public Product_Finder()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }

        private void Product_Finder_Load(object sender, EventArgs e)
        {

        }

        void findProductByBarCode(string barcode)
        {
            string sql = "select nom,categorie,matiere,Nscolaire,prix,qte,emplacement,codebar from produit where codebar like @cdbar and codebarStatus='generated'";
            SqlCommand cmd = new SqlCommand(sql,Program.getcon());
            cmd.Parameters.Add("@cdbar", SqlDbType.VarChar).Value = "STE" + barcode;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                Nom.Text = dr.GetValue(0).ToString();
                Cat.Text = dr.GetValue(1).ToString();
                Matire.Text = dr.GetValue(2).ToString();
                Ns.Text = dr.GetValue(3).ToString();
                Prix.Text = dr.GetValue(4).ToString() + " DH";
                Quantite.Text = dr.GetValue(5).ToString();
                Emplacement.Text = dr.GetValue(6).ToString();
                BarCode_view.Text = dr.GetValue(7).ToString();
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
            dr.Close();
        }

        private void CodeBar_KeyPress(object sender, KeyPressEventArgs e)
        {

            
            Program.onlyDegit(e);
         

            
            
        }
      
        private void CodeBar_TextChanged(object sender, EventArgs e)
        {
            findProductByBarCode(CodeBar.Text);
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (gunaAdvenceButton3.Checked)
            {
                CodeBar.Focus();
            }
        }

        private void CodeBar_Leave(object sender, EventArgs e)
        {
            gunaAdvenceButton3.Checked = false;
        }

        private void CodeBar_KeyDown(object sender, KeyEventArgs e)
        {
          
        }
    }
}
