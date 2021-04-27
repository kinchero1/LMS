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
using System.IO;
using ZXing;
using ZXing.QrCode;

namespace Library
{
    public partial class View_product : UserControl
    {
        public View_product()
        {
            InitializeComponent();
        }

        private void View_product_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }


            Program.readfile("cat.txt", Categorie_produit, true);
            Program.readfile("mat.txt", Matiere_produit);
            Program.readfile("Ns.txt", Ns_produit, true);


        }
       

        public void load_data(int i)
        {
            this.BringToFront();
            string sql = "select * from produit where id =@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = i;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            Nom_produit.Text = dr["nom"].ToString();
            Categorie_produit.SelectedItem = dr["categorie"].ToString();
            Matiere_produit.SelectedItem = dr["matiere"].ToString();
            Ns_produit.SelectedItem = dr["Nscolaire"].ToString();
            prix_produit.Text = dr["prix"].ToString();
            Stock_produit.Text = dr["qte"].ToString();
            emp_produit.Text = dr["emplacement"].ToString();
            Valeur_produit.Text = dr["valeur"].ToString();
            Description.Text = dr["description"].ToString();
            codebar.Text = dr["codebar"].ToString();
            Remise.Text = dr["Remise"].ToString();
            prix_achat.Text = dr["prix_achat"].ToString();
            if (dr["imagedata"] != DBNull.Value)
            {
                byte[] imagedata = (byte[])dr["imagedata"];
                MemoryStream ms = new MemoryStream(imagedata);
                pictureBox1.BackgroundImage = Image.FromStream(ms);
            }
          
            
            dr.Close();

            

            CheckIfenabled();
            this.Tag = i;

        }
        private void Categorie_produit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Matiere_produit.Enabled = (Categorie_produit.Text == "Livre" || Categorie_produit.Text == "Livre d'Enfant");
            Ns_produit.Enabled = (Categorie_produit.Text == "Livre" || Categorie_produit.Text == "Livre d'Enfant");

        }

        

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item.Visible && item.Enabled)
                {
                    if (Program.CheckEmpty(item, -1))
                    {
                        return;
                    }
                }
            }
            string sql = "update produit set nom=@nom,categorie=@cat,prix = @prix,matiere=@mat,Nscolaire=@Ns,emplacement=@emp,description=@desc,codebar=@codebar,remise=@remise,codebarStatus=@CodeBarStatus, productid =@productId,prix_achat=@pAchat where id =@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@nom", SqlDbType.NVarChar).Value = Nom_produit.Text;
            cmd.Parameters.Add("@cat", SqlDbType.VarChar).Value = Categorie_produit.Text;
            cmd.Parameters.Add("@prix", SqlDbType.VarChar).Value = prix_produit.Text;
            cmd.Parameters.Add("@mat", SqlDbType.NVarChar).Value = Matiere_produit.Text;
            cmd.Parameters.Add("@Ns", SqlDbType.NVarChar).Value = Ns_produit.Text;
            cmd.Parameters.Add("@emp", SqlDbType.VarChar).Value = emp_produit.Text;
            cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = Description.Text;
            cmd.Parameters.Add("@codebar", SqlDbType.VarChar).Value = codebar.Text;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Tag;
            cmd.Parameters.Add("@remise", SqlDbType.Float).Value = Program.CheckEmpty(Remise.Text) ? 0 : float.Parse(Remise.Text);
            cmd.Parameters.Add("@CodeBarStatus", SqlDbType.VarChar).Value = codebar.Text.Contains("STE") ? "generated" : "scanned";
            cmd.Parameters.Add("@pAchat", SqlDbType.Float).Value = Program.CheckEmpty(prix_achat.Text) ? 0 : float.Parse(prix_achat.Text);
            string id = "";
            id += Categorie_produit.SelectedValue;
            id += String.IsNullOrEmpty(Ns_produit.SelectedValue.ToString()) ? "" : "-" + Ns_produit.SelectedValue.ToString() + "-";
       
            id += this.Tag.ToString();
            cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = id;

            int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Modification bien fait");
                Program.insertLog("Suppresion produit : " + Nom_produit.Text, "Produit", "");

            }


        }

        void CheckIfenabled()
        {
            string sql = "select count(*) from produit_Bon_Livraison where produit_id = @id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Tag;
            int x = (int)cmd.ExecuteScalar();
            gunaAdvenceButton4.Visible = (x == 0);
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            string sql = "delete from produit where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Tag;
            int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Suppresion bien fait");
                Program.insertLog("Suppresion produit : " + Nom_produit.Text, "Produit", "");

            }

            MainForm form = (MainForm)this.ParentForm;
            form.product_list1.BringToFront();
            form.product_list1.load_data();


        }

        private void codebar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!gunaAdvenceButton5.Checked)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                gunaAdvenceButton5.Checked = false;
            }
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            codebar.Text = "STE" + this.Tag.ToString();
            if (!Program.CheckEmpty(codebar))
            {

                BarcodeWriter br = new BarcodeWriter() { Format = BarcodeFormat.CODE_128, Options = new QrCodeEncodingOptions { Width = 240, Height = 60 } };
                Image img = br.Write(codebar.Text);

                pictureBox2.Image = img;

            }
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            codebar.Focus();
            if (gunaAdvenceButton5.Checked)
            {
                codebar.Clear();
            }
          
        }
    }
}
