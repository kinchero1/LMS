using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Guna.UI2.WinForms;

using ZXing;
using ZXing.QrCode;

namespace Library
{
    public partial class Add_product : UserControl
    {
        float remise = 0;
        string codebarStatus="";
        public Add_product()
        {
            InitializeComponent();
        }
        public void setBarcodeReader()
        {
            gunaAdvenceButton5.Checked = false;
        }
        void checkifproductnameExist()
        {
            string sql = "";
            string x = "";
            if (Categorie_produit.SelectedIndex == 0 && Matiere_produit.SelectedIndex != -1 && Ns_produit.SelectedIndex != -1 && !String.IsNullOrEmpty(Nom_produit.Text))
            {
                sql = "select count(*) from produit where (nom+' '+matiere+' '+Nscolaire) like @Des";
                x = Nom_produit.Text.Trim() + " " + Matiere_produit.Text + " " + Ns_produit.Text;
            }
            else if (Categorie_produit.SelectedIndex != 0 && !String.IsNullOrEmpty(Nom_produit.Text))
            {
                sql = "select count(*) from produit where nom like @Des and categorie not like 'livre'";
                x = Nom_produit.Text.Trim();
            }
            else
            {
                Warning_Panel.Visible = false;
                return;
            }

            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@Des", SqlDbType.NVarChar).Value = x;
            int result = (int)cmd.ExecuteScalar();
            Warning_text.Text = x;
            Warning_Panel.Visible = (result == 1);

        }

        string getGeneratedBarCode()
        {
            string sql = "select max(id) from  produit";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
           

            if (!(cmd.ExecuteScalar().Equals(DBNull.Value)))
            {
                int x = (int)cmd.ExecuteScalar()+1;

                return "STE" + x.ToString();

            }
            else
            {
                return "STE1";
            }
        }

        int getLatestId()
        {
            string sql = "select max(id) from  produit";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());

            if (!(cmd.ExecuteScalar().Equals(DBNull.Value)))
            {
                int x = (int)cmd.ExecuteScalar() + 1;

                return x;

            }
            else
            {
                return 1;
            }
        }
        private void Add_product_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            loadData();
           
        }
        public void loadData()
        {
            Program.readfile("cat.txt", Categorie_produit, true);
            Program.readfile("mat.txt", Matiere_produit);
            Program.readfile("Ns.txt", Ns_produit, true);

        }

       


        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
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

            string sql = "insert into produit values(@nom,@cat,@prix,@matiere,@Ns,@stock,@valeur,@emp,@imagedata,@desc,@codebar,@remise,@CodeBarStatus,@productId,@pAchat)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@nom", SqlDbType.NVarChar).Value = Nom_produit.Text;
            cmd.Parameters.Add("@cat", SqlDbType.VarChar).Value = Categorie_produit.Text;
            cmd.Parameters.Add("@prix", SqlDbType.Float).Value = prix_produit.Text;
            cmd.Parameters.Add("@matiere", SqlDbType.NVarChar).Value = Matiere_produit.Text;
            cmd.Parameters.Add("@Ns", SqlDbType.NVarChar).Value = Ns_produit.Text;
            if (gunaAdvenceButton4.Checked) { cmd.Parameters.Add("@stock", SqlDbType.Int).Value = Stock.Text; } else { cmd.Parameters.Add("@stock", SqlDbType.Int).Value = 0; }

            cmd.Parameters.Add("@valeur", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@emp", SqlDbType.VarChar).Value = emp_produit.Text;

            if (bool.Parse(label11.Tag.ToString()) == true)
            {

                cmd.Parameters.Add("@imagedata", SqlDbType.Image).Value = File.ReadAllBytes(label11.Text);
            }
            else
            {
                cmd.Parameters.Add("@imagedata", SqlDbType.Image).Value = DBNull.Value;

            }


            cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = Description.Text;
            cmd.Parameters.Add("@codebar", SqlDbType.VarChar).Value = code_bar.Text;
            cmd.Parameters.Add("@remise", SqlDbType.Float).Value = Program.CheckEmpty(Remise.Text) ? 0 : float.Parse(Remise.Text);
            cmd.Parameters.Add("@CodeBarStatus", SqlDbType.VarChar).Value = code_bar.Text.Contains("STE") ? "generated" : "scanned";
            cmd.Parameters.Add("@pAchat", SqlDbType.Float).Value = Program.CheckEmpty(prix_achat.Text) ? 0 : float.Parse(prix_achat.Text);
            string id = "";
            if (Categorie_produit.SelectedValue != null && !String.IsNullOrEmpty(Categorie_produit.SelectedValue.ToString()))
            {
                id += Categorie_produit.SelectedValue;
            }

               
            if(Ns_produit.SelectedValue != null && Ns_produit.SelectedValue.ToString() !="" && Ns_produit.Enabled)
            {
                id += "-" + Ns_produit.SelectedValue.ToString() + "-";
            }
        
            id += getLatestId();
     
            cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = id;
                   int x = cmd.ExecuteNonQuery();
            if (x >= 1)
            {
                MessageBox.Show("Ajout Bien Fait");
                label11.Text = "glisser et déposez votre image";
                label11.Tag = false;
                foreach (Control item in Controls)
                {
                    if (item.Visible && item.Enabled)
                    {
                        Program.ClearControl(item, -1);
                    }
                }
            }
            Program.insertLog("Ajout produit : " + Nom_produit.Text,"Produit", code_bar.Text);

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }



        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg;*.png";
            if (op.ShowDialog() == DialogResult.OK)
            {
                label11.Text = op.FileName;
                label11.Tag = true;

            }


        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] ImageExtension = { ".jpg", ".jpeg", ".png" };
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (ImageExtension.Contains(Path.GetExtension(file[0]).ToLower()))
            {
                label11.Text = file[0];
                label11.Tag = true;
            }



        }

        private void Categorie_produit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Matiere_produit.SelectedIndex = (Categorie_produit.Text == "Livre" || Categorie_produit.Text == "Livre d'Enfant") ? -1 :-1;
            Ns_produit.SelectedIndex = (Categorie_produit.Text == "Livre" || Categorie_produit.Text == "Livre d'Enfant") ? -1: -1;

            Matiere_produit.Enabled = (Categorie_produit.Text == "Livre" || Categorie_produit.Text == "Livre d'Enfant");
            Ns_produit.Enabled = (Categorie_produit.Text == "Livre" || Categorie_produit.Text == "Livre d'Enfant");
            checkifproductnameExist();
        }

        private void prix_produit_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e, true);

        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            label1.Visible = gunaAdvenceButton4.Checked;
            Stock.Visible = gunaAdvenceButton4.Checked;
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            label1.Visible = gunaAdvenceButton4.Checked;
            Stock.Visible = gunaAdvenceButton4.Checked;
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)

        {
            code_bar.Text = getGeneratedBarCode();
            if (!Program.CheckEmpty(code_bar))
            {

                BarcodeWriter br = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 ,Options = new QrCodeEncodingOptions { Width = 270, Height = 70 } };
                Image img = br.Write(code_bar.Text);
                
                pictureBox1.Image = img;

            }
        }
       
        private void Ns_produit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ns_produit.Enabled)
            {
                checkifproductnameExist();

            }
        }

        private void Matiere_produit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Matiere_produit.Enabled)
            {
                checkifproductnameExist();

            }

        }

        private void Nom_produit_TextChanged(object sender, EventArgs e)
        {
            checkifproductnameExist();

        }

        private void gunaAdvenceButton5_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            code_bar.Focus();
            if (gunaAdvenceButton5.Checked)
            {
                code_bar.Clear();

            }
        }

        private void code_bar_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Remise_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e, true);
        }

       

        
    }
}
