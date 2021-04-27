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
    public partial class Edit_Product_Delivery_Form : Form
    {
        int productId;
        int factureId;
        MainForm Mform;
        public Edit_Product_Delivery_Form()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }

        private void Edit_Product_Delivery_Form_Load(object sender, EventArgs e)
        {
           
        }
        static  void fillcombobox(Guna.UI2.WinForms.Guna2ComboBox g)
        {
            string sql = "select (nom +' ' +matiere+ ' '+Nscolaire) as 'Nom',id from produit";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            g.ValueMember = "id";
            g.DisplayMember = "Nom";
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            g.DataSource = dt;

        }

     
        public void ShowDialogAdd(string text,int facture_id,MainForm form)
        {

            
            fillcombobox(this.Designation);
            
            this.Mform = form;
            this.factureId = facture_id;

            this.gunaAdvenceButton2.Visible = false;
            this.gunaAdvenceButton1.Visible = false;
            gunaAdvenceButton3.Visible = true;
            this.label1.Text = text;

            this.Designation.Enabled = true;
            this.unite_TB.Text = "";
            this.Quantite_TB.Text = "";
            this.prix_TB.Text = "";
            this.Remise_TB.Text = "";
            this.TVA_TB.Text = "";
            this.Designation.Width = 305;
            gunaAdvenceButton5.Visible = true;
            this.Show();
        }
        public void ShowDialogEdit(string text,int facture_id,int product_id,string unite, string quantite,string prix,string remise,string tva,MainForm form)
        {

            this.Designation.Enabled = false;
            this.Mform = form;
            fillcombobox(this.Designation);
            this.gunaAdvenceButton3.Visible = false;
            this.factureId = facture_id;
            this.productId = product_id;
            this.label1.Text = text;
            this.Designation.SelectedValue = product_id;
            this.unite_TB.Text = unite;
            this.Quantite_TB.Text = quantite;
            this.prix_TB.Text = prix;
            this.Remise_TB.Text = remise;
            this.TVA_TB.Text = tva;
            this.gunaAdvenceButton2.Visible = true;
            this.gunaAdvenceButton1.Visible = true;
            gunaAdvenceButton5.Visible = false;
            this.Designation.Width = 482;
        
            this.Show();
        }

     
        bool checkifexist(int Pid,int Bid)
        {
            string sql = "select count(*) from produit_Bon_Livraison where produit_id = @Pid and facture_id =@Bid";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@Bid", SqlDbType.Int).Value = factureId;
            cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = Designation.SelectedValue;
            int x = (int)cmd.ExecuteScalar();
            return x >= 1;

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (checkifexist(factureId, int.Parse(Designation.SelectedValue.ToString())))
            {
                MessageBox.Show("Produit deja exist");
                return;
            }
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
            string sql = "insert into produit_Bon_Livraison values(@Bid,@Pid,@Unite,@qte,@Prix,@Remise,@TVA)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@Bid", SqlDbType.Int).Value = factureId;
            cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = Designation.SelectedValue;
            cmd.Parameters.Add("@Unite", SqlDbType.VarChar).Value = unite_TB.Text;
            cmd.Parameters.Add("@qte", SqlDbType.Int).Value = Quantite_TB.Text;
            cmd.Parameters.Add("@Prix", SqlDbType.Float).Value = prix_TB.Text;
            cmd.Parameters.Add("@Remise", SqlDbType.Float).Value = Remise_TB.Text;
            cmd.Parameters.Add("@TVA", SqlDbType.Float).Value = TVA_TB.Text.Replace("%","");
            cmd.ExecuteNonQuery();
            Mform.deliveyForm_ProductList1.loaddata(factureId);
            Mform.deliveyForm_ProductList1.calculte();
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
            string sql = "update produit_Bon_Livraison set unite=@unite,QteEntre=@qte,prixunitaire=@prix,remise=@remise,tva=@tva from produit_Bon_Livraison where facture_id=@Bid and produit_id=@Pid";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@Bid", SqlDbType.Int).Value = factureId;
            cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = productId;
            cmd.Parameters.Add("@unite", SqlDbType.VarChar).Value = unite_TB.Text;
            cmd.Parameters.Add("@qte", SqlDbType.Int).Value = Quantite_TB.Text;
            cmd.Parameters.Add("@prix", SqlDbType.Float).Value = prix_TB.Text ;
            cmd.Parameters.Add("@remise", SqlDbType.Float).Value = Remise_TB.Text;
            cmd.Parameters.Add("@tva", SqlDbType.Float).Value = TVA_TB.Text.Replace("%","");
            cmd.ExecuteNonQuery();
            this.Hide();
            Mform.deliveyForm_ProductList1.calculte();
            Mform.deliveyForm_ProductList1.loadDG(int.Parse(Mform.deliveyForm_ProductList1.Tag.ToString()));


        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            string sql = "delete from produit_Bon_Livraison  where facture_id=@Bid and produit_id=@Pid";
            SqlCommand cmd = new SqlCommand(sql,Program.getcon());
            cmd.Parameters.Add("@Bid", SqlDbType.Int).Value = factureId;
            cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = productId;
            cmd.ExecuteNonQuery();
            Mform.deliveyForm_ProductList1.calculte();
            Mform.deliveyForm_ProductList1.loadDG(int.Parse(Mform.deliveyForm_ProductList1.Tag.ToString()));
            this.Hide();
        }
      
        Product_List_Form productList;
        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            if (productList == null)
            {
                productList = new Product_List_Form((MainForm)this.ParentForm, 6,null,this);
                productList.Show();
            }
            else
            {
                productList.Show();
                productList.TopMost = true;
                productList.TopMost = false;
            }
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (productList != null)
            {
                productList.Hide();
            }
    
        }
    }
}
