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
using System.Text.RegularExpressions;

namespace Library
{
    public partial class Delivery_Form : UserControl
    {
        public Delivery_Form()
        {
            InitializeComponent();
        }
        bool isCommand = false;
        private void label7_Click(object sender, EventArgs e)
        {

        }

        float montant_HT;
        float montant_TVA;

        bool checkQteAvialable(int Pid, int Cid, int value)
        {
            string sql = "select quantiteReste from produit_commande where commande_id = @Cid and product_id = @Pid";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@Cid", SqlDbType.Int).Value = Cid;
            cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = Pid;
            int quantiteReste = (int)cmd.ExecuteScalar();

            if (quantiteReste >= value)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            /*  for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
              {
                  if (guna2DataGridView1.Rows[i].Cells[0].Tag.ToString() == Designation.SelectedValue.ToString())
                  {
                      if (Program.CheckEmpty(Quantite_TB))
                      {
                          return;
                      }
                      guna2DataGridView1.Rows[i].Cells[2].Value = int.Parse(guna2DataGridView1.Rows[i].Cells[2].Value.ToString().Replace("x","")) + int.Parse(Quantite_TB.Text);
                      calculte();
                      return;
                  }

              }

              if (Program.CheckEmpty(unite_TB) || Program.CheckEmpty(Quantite_TB) || Program.CheckEmpty(prix_TB) || Program.CheckEmpty(Remise_TB) || Program.CheckEmpty(TVA_TB))
              {
                  return;
              }


              int row = guna2DataGridView1.Rows.Add(Designation.Text, unite_TB.Text, "x " + Quantite_TB.Text, prix_TB.Text + " DH", Remise_TB.Text + " %", TVA_TB.Text + " %");
              guna2DataGridView1.Rows[row].Cells[0].Tag = Designation.SelectedValue;
              guna2DataGridView1.ColumnHeadersVisible = true;
              guna2DataGridView1.Parent.Visible = true;
              Program.checkheight(392, guna2DataGridView1, empty_panel);
              calculte();
              unite_TB.Text = "U";
              Quantite_TB.Text = "1";
              Program.ClearControl(prix_TB);
              Remise_TB.Text = "0";
              TVA_TB.Text = "0";
              guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;*/
            guna2DataGridView1.Rows.Clear();
            calculte();
            Program.checkheight(392, guna2DataGridView1, empty_panel);










        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {

            if (guna2DataGridView1.RowCount > 0)
            {
                int selectedRowCount = guna2DataGridView1.SelectedRows.Count;
                for (int i = 0; i < selectedRowCount; i++)
                {
                    int x = guna2DataGridView1.SelectedRows[0].Index;
                    guna2DataGridView1.Rows.RemoveAt(x);

                }
            }

            guna2DataGridView1.ColumnHeadersVisible = (guna2DataGridView1.Rows.Count > 0);
            gunaSeparator1.Visible = (guna2DataGridView1.Rows.Count > 0);
            guna2DataGridView1.Parent.Visible = (guna2DataGridView1.Rows.Count > 0);
            calculte();
            Program.checkheight(392, guna2DataGridView1, empty_panel);

        }
        public void calculte()
        {
            if (guna2DataGridView1.Rows.Count > 0)
            {
                montant_HT = 0;
                montant_TVA = 0;
                for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                {

                    int quantite = int.Parse(guna2DataGridView1.Rows[i].Cells[2].Value.ToString().Replace("x", " "));
                    float Prix = float.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("DH", ""));
                    float Remise = float.Parse(guna2DataGridView1.Rows[i].Cells[4].Value.ToString().Replace("%", ""));
                    float TVA = float.Parse(guna2DataGridView1.Rows[i].Cells[5].Value.ToString().Replace("%", ""));

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





        int add_bonLivraison(int NBon, int Fournisseur, DateTime date, float HT, float TVA, float TTC, string CommandeID = "NULL")
        {
            string sql = "insert into Bon_Livraison values(@N,@nom,@date,@HT,@TVA,@TTC,@ver,@cmdID)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@N", SqlDbType.Int).Value = NBon;
            cmd.Parameters.Add("@nom", SqlDbType.Int).Value = Fournisseur;
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;
            cmd.Parameters.Add("@HT", SqlDbType.Float).Value = HT;
            cmd.Parameters.Add("@TVA", SqlDbType.Float).Value = TVA;
            cmd.Parameters.Add("@TTC", SqlDbType.Float).Value = TTC;
            cmd.Parameters.Add("@ver", SqlDbType.VarChar).Value = "Non Verifie";
            cmd.Parameters.Add("@cmdID", SqlDbType.VarChar).Value = CommandeID;


            int x = (int)cmd.ExecuteScalar();
            return x;
        }
        void add_produit_bonLivraison(int Bid, int Pid, string unite, int Qte, float prix, float remise, float Tva)
        {
            string sql = "insert into produit_Bon_Livraison values(@Bid,@Pid,@Unite,@qte,@Prix,@Remise,@TVA)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@Bid", SqlDbType.Int).Value = Bid;
            cmd.Parameters.Add("@Pid", SqlDbType.Int).Value = Pid;
            cmd.Parameters.Add("@Unite", SqlDbType.VarChar).Value = unite;
            cmd.Parameters.Add("@qte", SqlDbType.Int).Value = Qte;
            cmd.Parameters.Add("@Prix", SqlDbType.Float).Value = prix;
            cmd.Parameters.Add("@Remise", SqlDbType.Float).Value = remise;
            cmd.Parameters.Add("@TVA", SqlDbType.Float).Value = Tva;
            cmd.ExecuteNonQuery();
        }



        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void prix_TB_KeyPress(object sender, KeyPressEventArgs e)
        {

            Program.onlyDegit(e, true);
        }

        private void Quantite_TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
        }

        private void N_Fournisseur_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
        }

        private void gunaAdvenceButton3_Click_1(object sender, EventArgs e)
        {
            if (Program.CheckEmpty(N_Bon) || Program.CheckEmpty(Fournisseur_nom, -1) || guna2DataGridView1.RowCount <= 0)
            {
                return;
            }
            int x;
            if (!isCommand)
            {
                x = add_bonLivraison(int.Parse(N_Bon.Text), int.Parse(Fournisseur_nom.SelectedValue.ToString()), Date_entre.Value, montant_HT, montant_TVA, montant_TVA + montant_HT);

            }
            else
            {
                x = add_bonLivraison(int.Parse(N_Bon.Text), int.Parse(Fournisseur_nom.SelectedValue.ToString()), Date_entre.Value, montant_HT, montant_TVA, montant_TVA + montant_HT, this.Tag.ToString());

            }
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                int Pid = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Tag.ToString());
                string unite = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
                int qte = int.Parse(guna2DataGridView1.Rows[i].Cells[2].Value.ToString().Replace("x", " "));
                float prix = float.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("DH", ""));
                float remise = float.Parse(guna2DataGridView1.Rows[i].Cells[4].Value.ToString().Replace("%", ""));
                float tva = float.Parse(guna2DataGridView1.Rows[i].Cells[5].Value.ToString().Replace("%", ""));
                add_produit_bonLivraison(x, Pid, unite, qte, prix, remise, tva);

            }
            Program.insertLog("Ajout bon livraison  : " + N_Bon.Text, "BonL", N_Bon.Text);

            MessageBox.Show("Ajout bien Fait \n \n en attent de verification");
            N_Bon.Clear();
            loadData();


        }

        private void Date_entre_ValueChanged(object sender, EventArgs e)
        {


        }

        private void prix_TB_TextChanged(object sender, EventArgs e)
        {

        }

        private void N_Bon_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Separator2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_Enter(object sender, EventArgs e)
        {

        }

        private void Delivery_Form_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();
            Date_entre.Value = DateTime.Now;
            guna2DataGridView1.Columns[2].ValueType = typeof(int);
            guna2DataGridView1.Columns[3].ValueType = typeof(double);
            guna2DataGridView1.Columns[4].ValueType = typeof(double);
            guna2DataGridView1.Columns[5].ValueType = typeof(int);

            guna2DataGridView1.Columns[2].DefaultCellStyle.Format = "x 0";
            guna2DataGridView1.Columns[3].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[4].DefaultCellStyle.Format = "0.00 '%'";
            guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "00 '%'";
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            guna2DataGridView1.Columns[0].Width = 200;

        }

        float GetProductPrix(int i)
        {
            string sql = "select prix from produit where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = i;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            float result = float.Parse(dr.GetValue(0).ToString());
            dr.Close();
            return result;
        }

        public void loadData()
        {
            isCommand = false;
            Fournisseur_nom.Enabled = true;
            this.Tag = null;
            ListCountPanel.Visible = false;
            Add_Panel.Visible = true;
            guna2DataGridView1.Rows.Clear();
            //Program.fillcomboboxproduit(Designation);
            Program.fillcomboboxFournisseur(Fournisseur_nom);

            Program.checkheight(392, guna2DataGridView1, empty_panel);

        }

        public void Add_delivryForm_Commande(int commandeId, Guna.UI2.WinForms.Guna2DataGridView DG, string fournisseurID)
        {
            isCommand = true;
            ListCountPanel.Visible = true;
            guna2DataGridView1.Rows.Clear();
            this.BringToFront();
            //Program.fillcomboboxproduit(Designation);
            Program.fillcomboboxFournisseur(Fournisseur_nom);
            Add_Panel.Visible = false;
            MainForm form = (MainForm)this.ParentForm;
            form.setText("Ajouter un Bon de Livraison(N° de commande N° :" + commandeId + ")");
            Fournisseur_nom.Text = fournisseurID;
            Fournisseur_nom.Enabled = false;
            this.Tag = commandeId;
            for (int i = 0; i < DG.Rows.Count; i++)
            {

                int ProductID = int.Parse(DG.Rows[i].Cells[0].Value.ToString());
                float prix = GetProductPrix(ProductID);
                string productName = DG.Rows[i].Cells[1].Value.ToString();
                string unite = DG.Rows[i].Cells[2].Value.ToString();
                int quanite = int.Parse(DG.Rows[i].Cells[3].Value.ToString().Replace("x", " "));
                int row = guna2DataGridView1.Rows.Add(productName, unite, "x " + quanite, GetProductPrix(ProductID) + "DH", "0 %", "0 %");
                guna2DataGridView1.Rows[row].Cells[0].Tag = ProductID;
            }
            guna2DataGridView1.ColumnHeadersVisible = true;
            guna2DataGridView1.Parent.Visible = true;
            calculte();
            Program.checkheight(392, guna2DataGridView1, empty_panel);
            calculte();
            label16.Text = guna2DataGridView1.RowCount + " en Total";


        }

        private void guna2DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string x = e.FormattedValue.ToString();
            if (String.IsNullOrEmpty(x) || x == null)
            {
                guna2DataGridView1.CancelEdit();
                return;
            }

            if (e.ColumnIndex == 3)
            {
                string value = x.Replace("DH", "");
                if (value.Count(n => n == '.') > 1)
                {
                    guna2DataGridView1.CancelEdit();
                }

            }
            else if (e.ColumnIndex == 4 )
            {
                string value = x.Replace("%", "");
                if (float.Parse(value) > 100)
                {

                    guna2DataGridView1.CancelEdit();
                }

            }
            else if (e.ColumnIndex == 5)
            {
                string value = x.Replace("%", "");
                bool cancel = int.Parse(value) == 7 || int.Parse(value) == 14 || int.Parse(value) == 20;
                if (!cancel)
                {
                    guna2DataGridView1.CancelEdit();
                }

            }
            else if (e.ColumnIndex == 2)
            {
                string value = x.Replace("x", "");

                if (isCommand)
                {
                    int Pid = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Tag.ToString());
                    int cid = int.Parse(this.Tag.ToString());
                    if (!checkQteAvialable(Pid, cid, int.Parse(value)))
                    {
                        guna2DataGridView1.CancelEdit();

                    }
                }
            }


        }

        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows.Count > 0)
            {

                calculte();

            }
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {



            for (int i = 0; i < guna2DataGridView1.SelectedRows.Count; i++)
            {

                if (isCommand)
                {
                    int Pid = int.Parse(guna2DataGridView1.SelectedRows[i].Cells[0].Tag.ToString());
                    int cid = int.Parse(this.Tag.ToString());
                    if (checkQteAvialable(Pid, cid, int.Parse(Edit_qteTB.Text)))
                    {
                        guna2DataGridView1.SelectedRows[i].Cells[2].Value = !String.IsNullOrEmpty(Edit_qteTB.Text) ? Edit_qteTB.Text : guna2DataGridView1.SelectedRows[i].Cells[2].Value;


                    }
                }
                guna2DataGridView1.SelectedRows[i].Cells[3].Value = !String.IsNullOrEmpty(Edit_PrixTB.Text) ? Edit_PrixTB.Text : guna2DataGridView1.SelectedRows[i].Cells[3].Value;
                guna2DataGridView1.SelectedRows[i].Cells[4].Value = !String.IsNullOrEmpty(Edit_RemiseTB.Text) ? Edit_RemiseTB.Text : guna2DataGridView1.SelectedRows[i].Cells[4].Value;
                guna2DataGridView1.SelectedRows[i].Cells[5].Value = !String.IsNullOrEmpty(Edit_TVAtb.Text) ? Edit_TVAtb.Text : guna2DataGridView1.SelectedRows[i].Cells[5].Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        Product_List_Form productList = null;
        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            if (productList == null)
            {
                productList = new Product_List_Form((MainForm)this.ParentForm, 3);
                productList.Show();
            }
            else
            {
                productList.Show();
                productList.TopMost = true;
                productList.TopMost = false;
            }


        }

        private void guna2DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string value = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {

                guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value.Replace("x", "").Replace("%", "").Replace("DH", "");

            }

        }

        private void guna2DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (guna2DataGridView1.CurrentCell.ColumnIndex == 2 || guna2DataGridView1.CurrentCell.ColumnIndex == 5) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                tb.Text = tb.Text.Replace("x", "").Replace("%", "").Replace("DH", "");
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler((ob, ev) =>
                    {
                        Program.onlyDegit(ev);
                    });
                   


                }
            }
            
            if (guna2DataGridView1.CurrentCell.ColumnIndex == 3 || guna2DataGridView1.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                tb.Text = tb.Text.Replace("x", "").Replace("%", "").Replace("DH", "");
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler((ob, ev) =>
                    {
                        Program.onlyDegit(ev, true);
                    });
                }
            }
        }

        private void N_Bon_Leave(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {





        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {


        }

        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
