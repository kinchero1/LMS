using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Library
{
    public partial class Add_Bill : UserControl
    {
        public Add_Bill()
        {
            InitializeComponent();
        }
        public Add_Bill GetBill()
        {
            return this;
        }
        public string[] GetData()
        {
            string[] x = { Client_ComboBox.Text, montant_Total.ToString(), DateTime.Now.ToShortTimeString() };
            return x;
        }

        public void setBarcodeReader()
        {
            gunaAdvenceButton3.Checked = false;
        }

        float montant_Total = 0;
        int addbill(float montantTotal, string stats, string client, float montantReste)
        {
            string sql = "insert into Bill values(@client,@date,@montant,@stats,@Mr)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Now.ToShortDateString();
            cmd.Parameters.Add("montant", SqlDbType.Float).Value = montantTotal;
            cmd.Parameters.Add("@client", SqlDbType.VarChar).Value = client;
            cmd.Parameters.Add("@stats", SqlDbType.VarChar).Value = stats;
            cmd.Parameters.Add("@Mr", SqlDbType.Float).Value = montantReste;
            return (int)cmd.ExecuteScalar();
        }
        void addproductBill(int product_id, int Bill_id, string unite, int quantite, int remise)
        {
            string sql = "insert into Bill_products values (@product_id,@Bill_id,@unite,@qte,@remise)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = product_id;
            cmd.Parameters.Add("@Bill_id", SqlDbType.Int).Value = Bill_id;
            cmd.Parameters.Add("@unite", SqlDbType.VarChar).Value = unite;
            cmd.Parameters.Add("@qte", SqlDbType.Int).Value = quantite;
            cmd.Parameters.Add("@remise", SqlDbType.Int).Value = remise;
            cmd.ExecuteNonQuery();

        }
        void calcualteReturnedMoney()
        {
            if (string.IsNullOrEmpty(Cash.Text) || Cash.Text == "0" || (Cash.Text.IndexOf('.') != Cash.Text.LastIndexOf('.')))
            {
                Returned_Money.Text = "0 DH";
                return;

            }
            Returned_Money.Text = (float.Parse(Cash.Text) - montant_Total).ToString() + " DH";
        }
        void calculate()
        {
            montant_Total = 0;
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                float montant = float.Parse(guna2DataGridView1.Rows[i].Cells[7].Value.ToString().Replace("DH", " "));
                montant_Total += montant;
            }
            Montant_Total_Label.Text = montant_Total.ToString("0.00 DH");
            calcualteReturnedMoney();
        }
        bool checkstock(int id, int quantite, int valueadd = 0)
        {
            string sql = "select qte ,(nom +' '+matiere+' '+categorie) as 'Nom' from produit where id = @id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int stock = int.Parse(dr["qte"].ToString());
            string nom = dr["Nom"].ToString();
            dr.Close();
            if (quantite > stock)
            {
                MessageBox.ShowDialog("il ne reste plus que  " + (stock - valueadd) + "  de ce produit :" + "\n\n " + nom, false);
                return false;
            }
            return true;
        }
        void addProductBybarcode()
        {
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                if (guna2DataGridView1.Rows[i].Tag.ToString() == textBox1.Text)
                {

                    int qte = 1 + int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("x", " "));

                    guna2DataGridView1.Rows[i].Cells[3].Value = "x" + qte;

                    return;
                }
            }
            if (Program.CheckEmpty(Unite_TB) || Program.CheckEmpty(Quantite) || Program.CheckEmpty(Remise_TB))
            {
                return;
            }
            string sql = "select (nom + ' ' + matiere + ' ' + Nscolaire) as 'designation' ,* from produit where codebar = @codebar";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@codebar", SqlDbType.VarChar).Value = textBox1.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (!dr.HasRows)
            {
                MessageBox.Show("Produit Introuvable");
                dr.Close();
                return;
            }

            string unite = Unite_TB.Text;
            string codebar = dr["codebar"].ToString();
            int quantite = int.Parse(Quantite.Text);
            string designation = dr["Designation"].ToString();
            float prix = float.Parse(dr["prix"].ToString());
            float prixT = prix * quantite;
            float remise = float.Parse(Remise_TB.Text);
            float prix_achat = float.Parse(dr["prix_achat"].ToString());
            if (Client_ComboBox.SelectedIndex != 0)
            {
                remise = float.Parse(dr["remise"].ToString());
            }
            float total = (quantite * prix) - Program.percentage((quantite * prix), remise);

            int id = (int)dr["id"];
            dr.Close();
            if (!checkstock(id, quantite))
            {
                return;
            }

            int x = guna2DataGridView1.Rows.Add(guna2DataGridView1.Rows.Count + 1, designation, unite, quantite, prix, prixT, remise , total, prix_achat);
            guna2DataGridView1.Rows[x].Cells[1].Tag = id;
            guna2DataGridView1.Rows[x].Cells[3].Tag = quantite;
            guna2DataGridView1.Rows[x].Tag = codebar;

            calculate();

        }
        string getClientAdresse(string cin)
        {
            string sql = "select adresse from client where cin = @cin";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@cin", SqlDbType.VarChar).Value = cin;
            return (string)cmd.ExecuteScalar();
        }

        void printBill(int id)
        {
            int facture = 2;
            if (facture == 1)
            {
                string sql = @"select produit.id as 'N° PD',(nom+ ' ' + matiere +' ' + Nscolaire)  as 'Produit','',Bill_products.Unite as 'Uté',Bill_products.quantite as 'Quantité',produit.prix as 'Prix.U',(produit.prix * Bill_products.quantite) as 'Prix.T',Bill_products.remise as 'Remise',((produit.prix * Bill_products.quantite) -((produit.prix * Bill_products.quantite) *(Bill_products.remise/100.0))) as 'Total' from produit
                           inner join Bill_products on Bill_products.product_id =produit.id
                           inner join Bill on Bill.id = Bill_products.Bill_id
                           where Bill_id = @id";

                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());


                ExcelHelper ex = ExcelHelper.Export(@"Models\facture.xlsx", "Facture", true);
                if (ex == null)
                {
                    return;
                }
                ex.writecell("G13", montant_Total.ToString() + " ");
                ex.writecell("A3", id + "/" + DateTime.Now.ToString("yy"));
                ex.writecell("A5", DateTime.Now.ToShortDateString() + " ");
                ex.writecell("F2", Client_ComboBox.Text);
                ex.writecell("F5", getClientAdresse(Client_ComboBox.SelectedValue.ToString()));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ex.insertLine(11 + i);
                    ex.merge("B" + (11 + i), "C" + (11 + i));
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j == 5 || j == 6 || j == 8)
                        {
                            ex.writecell(i, j, dt.Rows[i][j].ToString(), 11, 1, false, true, true);
                        }
                        else if (j == 7)
                        {
                            ex.writecell(i, j, dt.Rows[i][j].ToString() + " %", 11, 1, false);
                        }
                        else
                        {
                            ex.writecell(i, j, dt.Rows[i][j].ToString(), 11, 1, false);
                        }



                    }

                }


                ex.SaveAndClose(true, true);
            }
            else
            {
                string sql = @"select ' ',produit.id as 'N° PD',(nom+ ' ' + matiere +' ' + Nscolaire)  as 'Produit','',Bill_products.Unite as 'Uté',Bill_products.quantite as 'Quantité',produit.prix as 'Prix.U',(produit.prix * Bill_products.quantite) as 'Prix.T',Bill_products.remise as 'Remise',((produit.prix * Bill_products.quantite) -((produit.prix * Bill_products.quantite) *(Bill_products.remise/100.0))) as 'Total' from produit
                           inner join Bill_products on Bill_products.product_id =produit.id
                           inner join Bill on Bill.id = Bill_products.Bill_id
                           where Bill_id = @id";

                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());


                ExcelHelper ex = ExcelHelper.Export(@"Models\facture2.xlsx", "Facture", true);
                if (ex == null)
                {
                    return;
                }
                ex.writecell("H12", montant_Total.ToString() + " ");
                ex.writecell("D2", id + "/" + DateTime.Now.ToString("yy"));
                ex.writecell("D3", DateTime.Now.ToShortDateString() + " ");
                ex.writecell("G2", Client_ComboBox.Text);
                ex.writecell("G5", getClientAdresse(Client_ComboBox.SelectedValue.ToString()));
                Num2Letter_Helper num2letter = Num2Letter_Helper.getValue("https://number-to-letter-micro-service.kinchero1.repl.co", montant_Total.ToString());
                if (num2letter != null)
                {
                    ex.writecell("B19", num2letter.value + " dirham et " + num2letter.Decimal + " centimes");
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ex.insertLine(11 + i, false, null, null, false);
                    ex.merge("C" + (11 + i), "D" + (11 + i));

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j == 6 || j == 7 || j == 9)
                        {
                            ex.writecell2(i, j, float.Parse(dt.Rows[i][j].ToString()).ToString("N"), 11, 1, true);
                         
                        }
                        else if (j == 8)
                        {
                            ex.writecell2(i, j, float.Parse(dt.Rows[i][j].ToString()) + " %", 11, 1, false);
                        }
                        else
                        {
                            ex.writecell2(i, j, dt.Rows[i][j].ToString(), 11, 1, false);
                        }



                    }

                }


                ex.SaveAndClose(true, true);
            }

        }


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void Payment_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();
            guna2DataGridView1.Columns[3].ValueType = typeof(int);
            guna2DataGridView1.Columns[4].ValueType = typeof(double);
            guna2DataGridView1.Columns[5].ValueType = typeof(double);

            guna2DataGridView1.Columns[6].ValueType = typeof(double);
            guna2DataGridView1.Columns[7].ValueType = typeof(double);
            guna2DataGridView1.Columns[8].ValueType = typeof(double);

            guna2DataGridView1.Columns[3].DefaultCellStyle.Format = "x 0";
            guna2DataGridView1.Columns[4].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[6].DefaultCellStyle.Format = "0.00 '%'";
            guna2DataGridView1.Columns[7].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[8].DefaultCellStyle.Format = "0.00 DH";
      



        }
        public void loadData()
        {
            Program.fillcomboboxproduit(Designation);
            Program.fillcomboboxClient(Client_ComboBox);
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
          

            calculate();

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                if (guna2DataGridView1.Rows[i].Cells[1].Tag.ToString() == Designation.SelectedValue.ToString())
                {
                    if (!Program.CheckEmpty(Quantite))
                    {

                        int qte = int.Parse(Quantite.Text) + int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("x", " "));

                        guna2DataGridView1.Rows[i].Cells[3].Value = "x" + qte;


                    }
                    return;
                }
            }

            if (Program.CheckEmpty(Unite_TB) || Program.CheckEmpty(Quantite) || Program.CheckEmpty(Remise_TB))
            {
                return;
            }
            if (Designation.SelectedValue == null)
            {
                return;
            }
            AddRow(guna2DataGridView1, Designation.SelectedValue.ToString(), Designation.Text);
        }


        public void AddRow(Guna.UI2.WinForms.Guna2DataGridView DG, string product_Id, string desgination)
        {
           
            string sql = "select * from produit where id = @id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = product_Id;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string unite = Unite_TB.Text;
            int quantite = int.Parse(Quantite.Text);
            string codebar = dr["codebar"].ToString();
            float prix = float.Parse(dr["prix"].ToString());
            float prixT = prix * quantite;
            float remise = float.Parse(Remise_TB.Text);
            float prix_achat = float.Parse(dr["prix_achat"].ToString());
            if (Client_ComboBox.SelectedIndex != 0)
            {
                remise = float.Parse(dr["remise"].ToString());
            }

            float total = (quantite * prix) - Program.percentage((quantite * prix), remise);

            int id = (int)dr["id"];
            dr.Close();
            if (!checkstock(id, quantite))
            {
                return;
            }

            int x = DG.Rows.Add(DG.Rows.Count + 1, desgination, unite, quantite, prix, prixT, remise , total, prix_achat);
            DG.Rows[x].Cells[1].Tag = id;
            DG.Rows[x].Cells[3].Tag = quantite;
            DG.Rows[x].Tag = codebar;
            calculate();
        }
        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.RowCount > 0)
            {
                if (e.ColumnIndex == 3)
                {


                    int Qte = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace("x", " "));
                    int id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[1].Tag.ToString());
                    int oldQte = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[3].Tag.ToString());
                    if (!checkstock(id, Qte))
                    {

                        guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldQte;

                    }
                    else
                    {
                        guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Qte;
                        guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = Qte;

                    }

                }

              
                float prix = float.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Replace("DH", " "));
                int quantite = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Replace("x", " "));
                int remise = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().Replace("%", " "));
                float total = (prix * quantite) - Program.percentage((prix * quantite), remise);
                guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value = (prix * quantite);
                guna2DataGridView1.Rows[e.RowIndex].Cells[7].Value = total;
                calculate();



            }
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }
            string status = float.Parse(Cash.Text) >= montant_Total ? "Payé" : "Non Payé";
            float montantLeft = float.Parse(Cash.Text) >= montant_Total ? 0f : montant_Total - float.Parse(Cash.Text);
            int Bill_id = addbill(montant_Total, status, Client_ComboBox.SelectedValue.ToString(), montantLeft);
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                int quantite = int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("x", " "));
                int remise = int.Parse(guna2DataGridView1.Rows[i].Cells[6].Value.ToString().Replace("%", " "));

                int product_id = int.Parse(guna2DataGridView1.Rows[i].Cells[1].Tag.ToString());
                string unite = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                addproductBill(product_id, Bill_id, unite, quantite, remise);
                Program.update_stock(product_id, -quantite);

            }


            if (MessageBox.ShowDialog("Vous vouler imprimer cette facture", true) == DialogResult.OK)
            {
                printBill(Bill_id);
            }

            guna2DataGridView1.Rows.Clear();
            calculate();
            MessageBox.Show("Paiment bien Fait");
            Program.main.temp_Bills1.BringToFront();
            Program.main.temp_Bills1.RemoveBill(this);

            Program.insertLog("Ajout Facture  : " + Bill_id.ToString(), "Bill", Bill_id.ToString());

        }


        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }
            guna2DataGridView1.Rows.Remove(guna2DataGridView1.SelectedRows[0]);
            calculate();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Separator2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (gunaAdvenceButton3.Checked)
                {
                    addProductBybarcode();
                    textBox1.Clear();
                }

                e.Handled = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            gunaAdvenceButton3.Checked = false;
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {

            if (gunaAdvenceButton3.Checked)
            {
                textBox1.Focus();
            }
        }

        private void guna2DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string x = e.FormattedValue.ToString();
            if (String.IsNullOrEmpty(x) || x==null)
            {
                guna2DataGridView1.CancelEdit();
       
                return;
            }


            if (e.ColumnIndex == 6)
            {
                string value = x.Replace("%", "").Replace(" ","");
                 
                if (float.Parse(value) >100)
                {
                    
                    guna2DataGridView1.CancelEdit();
                }
            }

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= 0)
            {
                return;
            }
            guna2DataGridView1.BeginEdit(true);

        }

        private void Remise_TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
            
        }

        private void Quantite_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if (Prouctlist == null)
            {
                Prouctlist = new Product_List_Form((MainForm)this.ParentForm, 4, this);
                Prouctlist.Show();
            }
            else
            {
                Prouctlist.Show();
                Prouctlist.TopMost = true;
                Prouctlist.TopMost = false;
            }
        }
        Product_List_Form Prouctlist = null;

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e, true);
        }

        private void Cash_TextChanged(object sender, EventArgs e)
        {
            calcualteReturnedMoney();
        }

      
        bool first = true;
        private void guna2DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (first)
            {
                guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int widthCol = guna2DataGridView1.Columns[1].Width;
                guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                guna2DataGridView1.Columns[1].Width = widthCol;
                first = false;
            }
          
        }

        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void guna2DataGridView1_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.ColumnIndex == 3 ) //Desired Column
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

            if (guna2DataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                tb.Text = tb.Text.Replace("x", "").Replace("%", "").Replace("DH", "");
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler((ob, ev) =>
                    {
                        Program.onlyDegit(ev,true);
                    });
                }
            }


        }
    }

}

