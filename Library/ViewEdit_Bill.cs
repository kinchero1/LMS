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

namespace Library
{
    public partial class ViewEdit_Bill : UserControl
    {
        public ViewEdit_Bill()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        float LefttoPay;
        float totalAmount;
        private void ViewEdit_Bill_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

        }
        void loadHeader(int i)
        {
            string sql = @"select id,datesortie,(client.nom+' '+client.prenom),client.adresse,montant,status,montantRest from Bill
                           inner join client on client.CIN = client_CIN
                           where Bill.id=@id";

            string sqlAR = @"select id,datesortie,(client.nom+' '+client.prenom),client.adresse,montant,status,montantRest from BillAR
                           inner join client on client.CIN = client_CIN
                           where BillAR.id=@id";

            SqlCommand cmd = new SqlCommand(Program.normalMOD ? sql : sqlAR, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = i;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                Facture_N.Text = dr.GetValue(0).ToString();
                Date_sortie.Text = dr.GetValue(1).ToString();
                Client_name.Text = dr.GetValue(2).ToString();
                Client_Adresse.Text = dr.GetValue(3).ToString();
                Montant_Total.Text = dr.GetValue(4).ToString() + " DH";
                totalAmount = float.Parse(dr.GetValue(4).ToString());
                string status = dr.GetValue(5).ToString();
                LefttoPay = float.Parse(dr.GetValue(6).ToString());
                leftTopay_tb.Text = LefttoPay.ToString() + " DH";
                panel1.Visible = (Client_name.Text == "Normal Client");
                gunaAdvenceButton2.Visible = (status != "Payé" && Login.accountype == "ADMIN" && Program.normalMOD);
                gunaAdvenceButton4.Visible = (status != "Payé" && Login.accountype == "ADMIN" && Program.normalMOD);
                gunaAdvenceButton1.Visible = (status != "Payé" && Login.accountype == "ADMIN" && Program.normalMOD);

            }
            dr.Close();

        }
        public void loadData(int i)
        {
            loadHeader(i);
            string sql = @"select produit.id as 'N° PD',(nom+ ' ' + matiere +' ' + Nscolaire)  as 'Produit',Bill_products.Unite as 'Uté',Bill_products.quantite as 'Quantité',produit.prix as 'Prix.U',(produit.prix * Bill_products.quantite) as 'Prix.T',Bill_products.remise as 'Remise',((produit.prix * Bill_products.quantite) -((produit.prix * Bill_products.quantite) *(Bill_products.remise/100.0))) as 'Total' from produit
                           inner join Bill_products on Bill_products.product_id =produit.id
                           inner join Bill on Bill.id = Bill_products.Bill_id
                           where Bill_id = @id";

            string sqlAR = @"select produit.id as 'N° PD',(nom+ ' ' + matiere +' ' + Nscolaire)  as 'Produit',Bill_productsAR.Unite as 'Uté',Bill_productsAR.quantite as 'Quantité',produit.prix as 'Prix.U',(produit.prix * Bill_productsAR.quantite) as 'Prix.T',Bill_productsAR.remise as 'Remise',((produit.prix * Bill_productsAR.quantite) -((produit.prix * Bill_productsAR.quantite) *(Bill_productsAR.remise/100.0))) as 'Total' from produit
                           inner join Bill_productsAR on Bill_productsAR.product_id =produit.id
                           inner join BillAR on BillAR.id = Bill_productsAR.Bill_id
                           where Bill_id = @id";

            SqlCommand cmd = new SqlCommand(Program.normalMOD ? sql : sqlAR, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = i;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            
            guna2DataGridView1.Columns[3].DefaultCellStyle.Format = "'x' 0";
            guna2DataGridView1.Columns[4].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "0.00 DH";
            guna2DataGridView1.Columns[6].DefaultCellStyle.Format = "0 '%'";
            guna2DataGridView1.Columns[7].DefaultCellStyle.Format = "0.00 DH";
            this.Tag = i;


        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.ShowDialog("Vous Voulez Vraiment Verifier Cette Facture", true) == DialogResult.OK)
            {
                string sql = "update Bill set status=@status where id=@id";
                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Tag;
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "Payé";
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    MessageBox.Show("Verification bien fait");
                    Program.insertLog("Verification Facture  : " + Facture_N.Text, "Bill", Facture_N.Text);

                }

            }


            loadData(int.Parse(this.Tag.ToString()));
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                int id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Value.ToString());
                int qte = int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("%", " "));
                Program.update_stock(id, qte);
            }
            if (MessageBox.ShowDialog("Vous Voulez Vraiment Supprimer Cette Facture", true) == DialogResult.OK)
            {
                string sql = "delete from Bill_products where Bill_id=@id";
                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Tag;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from Bill where id=@id";
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    MessageBox.Show("Suppression bien fait");
                }

            }
            MainForm form = (MainForm)this.ParentForm;
            form.gunaAdvenceButton9.PerformClick();
            Program.insertLog("Suppression Facture  : " + Facture_N.Text, "Bill", "");
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            printBill(int.Parse(this.Tag.ToString()));
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

                string montantTotal = Montant_Total.Text.ToString();
                ex.writecell("G13", montantTotal);
                ex.writecell("A3", id + "/" + DateTime.Now.ToString("yy"));
                ex.writecell("A5", DateTime.Now.ToShortDateString() + " ");
                ex.writecell("F2", Client_name.Text);
                ex.writecell("F5", Client_Adresse.Text);

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
                
                string montantTotal = Montant_Total.Text.ToString();
            
               
                ex.writecell("H12", montantTotal.ToString() + " ");
                ex.writecell("D2", id + "/" + DateTime.Now.ToString("yy"));
                ex.writecell("D3", DateTime.Now.ToShortDateString() + " ");
                ex.writecell("G2", Client_name.Text);
                ex.writecell("G5", Client_Adresse.Text);
                Num2Letter_Helper num2letter = Num2Letter_Helper.getValue("https://number-to-letter-micro-service.kinchero1.repl.co", float.Parse(montantTotal.Replace("DH", "")).ToString());
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
        Payment_box box = null;
        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (box != null)
            {
                box.Show();
                box.TopMost = true;
                box.TopMost = false;
            }
            else
            {
                box = new Payment_box(int.Parse(this.Tag.ToString()), totalAmount, LefttoPay, this);
                box.Show();
            }

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
    }
}
