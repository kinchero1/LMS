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
    public partial class DeliveyForm_ProductList : UserControl
    {
        
        public DeliveyForm_ProductList()
        {
            InitializeComponent();
        }
        Edit_Product_Delivery_Form editProductForm = null;
        private void DeliveyForm_ProductList_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            
        }
        float montant_HT;
        float montant_TVA;

        void updateProductCommand(int cmdID, int Pid, int QteLivre)
        {
            string sql = "update produit_commande set quantiteLivrée=@QteLivre where commande_id = @Cid and product_id = @Pid";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@QteLivre", SqlDbType.Int).Value = QteLivre;
            cmd.Parameters.Add("@Cid", SqlDbType.Int).Value = cmdID;
            cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = Pid;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "update produit_commande set quantiteReste = quantiteDemande - quantiteLivrée where commande_id = @Cid and product_id = @Pid ";
            cmd.ExecuteNonQuery();

        }
        void updateStatusCommand(int cid)
        {
            SqlCommand cmd = new SqlCommand("select sum(quantiteReste),sum(quantiteDemande) from produit_commande where commande_id=@Cid", Program.getcon());
            cmd.Parameters.Add("@Cid", SqlDbType.Int).Value = cid;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int quantiteReste=1;
            int quantiteDemande=1;
            if (dr.HasRows)
            {
                 quantiteReste = (int)dr.GetValue(0);
                 quantiteDemande = (int)dr.GetValue(1);

            }
            dr.Close();
            string Value = "";
            if (quantiteReste == 0)
            {
                Value = "Livré";
            }
            else if (quantiteDemande == quantiteReste)
            {
                Value = "Non Livré";
            }
            else
            {
                Value = "livré partiellement ";
            }
         
            SqlCommand cmd2 = new SqlCommand("update commande set status=@stat where id=@id", Program.getcon());
          
            cmd2.Parameters.Add("@stat", SqlDbType.VarChar).Value = Value;
            cmd2.Parameters.Add("@id", SqlDbType.Int).Value = cid;
            cmd2.ExecuteNonQuery();

        }
        public void calculte()
        {
            if (guna2DataGridView1.Rows.Count > 0)
            {
                montant_HT = 0;
                montant_TVA = 0;
                for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                {

                    int quantite = int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString());
                    float Prix = float.Parse(guna2DataGridView1.Rows[i].Cells[4].Value.ToString().Replace("DH", ""));
                    float Remise = float.Parse(guna2DataGridView1.Rows[i].Cells[5].Value.ToString().Replace("%", ""));
                    float TVA = float.Parse(guna2DataGridView1.Rows[i].Cells[6].Value.ToString().Replace("%", ""));

                    float Discounted_value = (Prix * quantite) - Program.percentage((Prix * quantite), Remise);
                    float TVA_value = Program.percentage(Discounted_value, TVA);
                    montant_HT += Discounted_value;
                    montant_TVA += TVA_value;




                }
                Montatnt_HT_label.Text = montant_HT.ToString() + " DH";
                Montant_TVA_label.Text = montant_TVA.ToString() + " DH";
                Montant_TTC_label.Text = (montant_HT + montant_TVA).ToString() + " DH ";
            }
            else
            {
                Montatnt_HT_label.Text = "0" + " DH ";
                Montant_TVA_label.Text = "0" + " DH ";
                Montant_TTC_label.Text = "0" + " DH ";
            }
        }

        string CommandId;
        bool verified;
        public void loaddata(int i)
        {
          
            Program.fillcomboboxFournisseur(Fournisseur_nom);
            string sql =Program.normalMOD ? " select * from Bon_Livraison where Nentre=@id":" select * from Bon_LivraisonAR where Nentre = @id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = i;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr == null)
            {
                return;
            }
            N_Bon.Text = dr["NBon"].ToString();
            Fournisseur_nom.SelectedValue = dr["fournisseur"].ToString();
            Date_entre.Value = (DateTime)dr["dateentre"];
            gunaAdvenceButton1.Visible = (dr["Verification"].ToString() != "Verifié" && Login.accountype == "ADMIN" && Program.normalMOD);
            gunaAdvenceButton2.Visible = (dr["Verification"].ToString() != "Verifié" && dr["CommandeId"].ToString() == "NULL" && Program.normalMOD);
            gunaAdvenceButton3.Visible = (dr["Verification"].ToString() != "Verifié" && dr["CommandeId"].ToString() == "NULL" && Program.normalMOD);
            gunaAdvenceButton4.Visible = (dr["Verification"].ToString() != "Verifié" && Login.accountype == "ADMIN" && Program.normalMOD);
            CommandId = dr["CommandeId"].ToString();
            verified = (dr["Verification"].ToString() == "Verifié");

            dr.Close();
            loadDG(i);

            this.Tag = i;
            calculte();
            this.BringToFront();
            guna2DataGridView1.Columns[4].DefaultCellStyle.Format = "0 DH";
            guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "0 '%'";
            guna2DataGridView1.Columns[6].DefaultCellStyle.Format = "0 '%'";
          
        }
        public void loadDG(int i)
        {
            string sql = @"select id as 'ID',(nom+ ' ' + matiere +' ' + Nscolaire)  as 'Nom',Unite as 'Unite',QteEntre as 'Quantite',prixunitaire as 'Prix Unitaire',produit_Bon_Livraison.remise as 'Remise',tva as 'T.V.A' from produit
                           inner join produit_Bon_Livraison on produit.id =produit_Bon_Livraison.produit_id
                           where produit_Bon_Livraison.facture_id = @id";

            string sqlAR= @"select id as 'ID',(nom+ ' ' + matiere +' ' + Nscolaire)  as 'Nom',Unite as 'Unite',QteEntre as 'Quantite',prixunitaire as 'Prix Unitaire',produit_Bon_LivraisonAR.remise as 'Remise',tva as 'T.V.A' from produit
                           inner join produit_Bon_LivraisonAR on produit.id =produit_Bon_LivraisonAR.produit_id
                           where produit_Bon_LivraisonAR.facture_id = @id";

            SqlCommand cmd = new SqlCommand(Program.normalMOD ? sql:sqlAR, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = i;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Program.checkheight(442, guna2DataGridView1, empty_panel);

        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (CommandId!="NULL" || verified)
            {
                return;
            }
           
            int id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            string unite = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string quantite = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            string prix = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            string remise = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            string TVA = guna2DataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            if (editProductForm == null)
            {
                editProductForm = new Edit_Product_Delivery_Form();
            }
            else
            {
                editProductForm.TopMost = true;
                editProductForm.TopMost = false;
            }
            editProductForm.ShowDialogEdit("Gestion de produit", int.Parse(this.Tag.ToString()), id, unite, quantite, prix, remise, TVA, (MainForm)this.ParentForm);
            loadDG(int.Parse(this.Tag.ToString()));

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (editProductForm == null)
            {
                editProductForm = new Edit_Product_Delivery_Form();
            }
            else
            {
                editProductForm.TopMost = true;
                editProductForm.TopMost = false;
            }
            editProductForm.ShowDialogAdd("Ajouter un produit", int.Parse(this.Tag.ToString()), (MainForm)this.ParentForm);
            loadDG(int.Parse(this.Tag.ToString()));
            Program.checkheight(442, guna2DataGridView1, empty_panel);
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                int id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Value.ToString());
                int quantite = int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString());
                Program.update_stock(id, quantite);
                if (CommandId !="NULL")
                {
                    updateProductCommand(int.Parse(CommandId), id, quantite);
                }
              

            }
            if (CommandId != "NULL")
            {
                updateStatusCommand(int.Parse(CommandId));
            }

            string sql = "update Bon_Livraison set Verification=@verification,Nbon=@Nbon,fournisseur=@f where Nentre=@N";

            SqlCommand cmd = new SqlCommand(sql, Program.getcon());

            cmd.Parameters.Add("@verification", SqlDbType.VarChar).Value = "Verifié";
            cmd.Parameters.Add("@N", SqlDbType.Int).Value = this.Tag;

            cmd.Parameters.Add("@Nbon", SqlDbType.Int).Value = N_Bon.Text;
            cmd.Parameters.Add("@f", SqlDbType.Int).Value = Fournisseur_nom.SelectedValue;

            int x = cmd.ExecuteNonQuery();
            if (x > 0)
            {
                Program.insertLog("Verification bon livraison  : " + N_Bon.Text, "BonL", N_Bon.Text);

                MessageBox.Show("Verification bien fait");
                ((MainForm)this.ParentForm).delivery_Form_List1.loaddata();
                ((MainForm)this.ParentForm).delivery_Form_List1.BringToFront();


            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.RowCount <= 0)
            {
                return;
            }
            int x = guna2DataGridView1.CurrentRow.Index;
            int id = int.Parse(guna2DataGridView1.Rows[x].Cells[0].Value.ToString());

            string unite = guna2DataGridView1.Rows[x].Cells[2].Value.ToString();
            string quantite = guna2DataGridView1.Rows[x].Cells[3].Value.ToString();
            string prix = guna2DataGridView1.Rows[x].Cells[4].Value.ToString();
            string remise = guna2DataGridView1.Rows[x].Cells[5].Value.ToString();
            string TVA = guna2DataGridView1.Rows[x].Cells[6].Value.ToString();
            if (editProductForm == null)
            {
                editProductForm = new Edit_Product_Delivery_Form();
            }
            else
            {
                editProductForm.TopMost = true;
                editProductForm.TopMost = false;
            }
            editProductForm.ShowDialogEdit("Gestion de produit", int.Parse(this.Tag.ToString()), id, unite, quantite, prix, remise, TVA, (MainForm)this.ParentForm);

        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.ShowDialog("Vous vouler vraiment supprimer le Bon N : " + this.Tag.ToString(), true) == DialogResult.OK)
            {
                string sql2 = "delete  from produit_Bon_Livraison where facture_id = @id";
                SqlCommand cmd = new SqlCommand(sql2, Program.getcon());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(this.Tag.ToString());
                cmd.ExecuteNonQuery();
                string sql = "delete from Bon_Livraison where Nentre = @id";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Program.insertLog("Suppression bon livraison  : " + N_Bon.Text, "BonL", "");

                MainForm form = (MainForm)this.ParentForm;
                form.delivery_Form_List1.loaddata();
                form.delivery_Form_List1.BringToFront();

            }

        }

        private void Designation_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void N_Bon_TextChanged(object sender, EventArgs e)
        {

        }

        private void Date_entre_ValueChanged(object sender, EventArgs e)
        {

        }

        private void N_Bon_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
