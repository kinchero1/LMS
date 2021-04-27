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
    public partial class Add_Order : UserControl
    {
        public Add_Order()
        {
            InitializeComponent();
        }
        int Add_order(object fournisseurID)
        {
            string sql = "insert into commande values (@fournisseur,@date,@status)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@fournisseur", SqlDbType.Int).Value = fournisseurID;
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Now.ToShortDateString();
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "Non Livré";
            return (int)cmd.ExecuteScalar();

        }

        public string getFournisseurEmail(string id)
        {
            string sql = "select email from Fournisseur where id=@id ";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            if (cmd.ExecuteScalar() == DBNull.Value)
            {
                return "";
            }
            else
            {
                return cmd.ExecuteScalar().ToString();
            }
        }

        void add_order_product(object proID, int commande_id, string unite, int qte)
        {
            string sql = "insert into produit_commande values (@product_id,@commande_id,@unite,@qte,@qteLivre,@qteReste)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = proID;
            cmd.Parameters.Add("@commande_id", SqlDbType.Int).Value = commande_id;
            cmd.Parameters.Add("@unite", SqlDbType.VarChar).Value = unite;
            cmd.Parameters.Add("@qte", SqlDbType.Int).Value = qte;
            cmd.Parameters.Add("@qteLivre", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@qteReste", SqlDbType.Int).Value = qte;
            cmd.ExecuteNonQuery();

        }



        void printOrder(int id,bool send,bool print)
        {


            if(!send && !print)
            {
                return;
            }

            ExcelHelper ex = ExcelHelper.Export(@"Models\commande.xlsx", "Commande", true);
            if (ex == null)
            {
                return;
            }
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                ex.insertLine(11 + i);

                for (int j = 0; j < guna2DataGridView1.Columns.Count; j++)
                {
                    if (j==3)
                    {
                        ex.writecell(i, j, guna2DataGridView1.Rows[i].Cells[j].Value.ToString().Replace("x",""), 11, 1, false);
                    }
                    else
                    {
                        ex.writecell(i, j, guna2DataGridView1.Rows[i].Cells[j].Value.ToString(), 11, 1, false);

                    }

                }

            }

            ex.writecell("C4", "Date Le :" + DateTime.Now.ToShortDateString());
            ex.writecell("C7", "Fournisseur :" + Fournisseur.Text);
            ex.writecell("B6", "Commande N°  :" + id + "/" + DateTime.Now.Year.ToString());

            ex.SaveAndClose(print);

            if (send)
            {
                string email = getFournisseurEmail(Fournisseur.SelectedValue.ToString());
                if (String.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Email de fournisseur est invalid");
                }
                else
                {
                    Program.SendEmail(email, "Commande", "Commande N°  :" + id + "/" + DateTime.Now.Year.ToString(), ex.savePath, Properties.Settings.Default.EmailAccountName, Properties.Settings.Default.EmailPassword);

                }

            }



        }


        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                if (guna2DataGridView1.Rows[i].Cells[0].Tag.ToString() == Designation.SelectedValue.ToString())
                {
                    if (Program.CheckEmpty(Quantite))
                    {
                        return;
                    }
                    guna2DataGridView1.Rows[i].Cells[3].Value = int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("x","")) + int.Parse(Quantite.Text);
                    return;
                }

            }

            if (Program.CheckEmpty(Unite_TB) || Program.CheckEmpty(Quantite))
            {
                return;
            }

            if (Designation.Text == "")
            {
                return;
            }
            int row = guna2DataGridView1.Rows.Add(guna2DataGridView1.RowCount + 1, Designation.Text, Unite_TB.Text,int.Parse(Quantite.Text));
            guna2DataGridView1.Rows[row].Cells[0].Tag = Designation.SelectedValue;
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
        }

        private void Add_Order_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            loadData();
            guna2DataGridView1.Columns[3].ValueType = typeof(int);
          
            guna2DataGridView1.Columns[3].DefaultCellStyle.Format = "x 0";
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
        public void loadData()
        {
            Program.fillcomboboxproduit(Designation);
            Program.fillcomboboxFournisseur(Fournisseur);

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            
            bool print = false;
            bool send = false;
            if (guna2DataGridView1.RowCount <= 0)
            {
                return;
            }
            int commande_ID = Add_order(Fournisseur.SelectedValue);
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                int product_id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Tag.ToString());
                string unite = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                int quantite = int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("x",""));

                add_order_product(product_id, commande_ID, unite, quantite);

            }
            MessageBox.Show("Ajout Bient Fait");
            if (MessageBox.ShowDialog("Vous Voulez imprimer cette commande", true) == DialogResult.OK)
            {
                print = true;
                
            }

            if (MessageBox.ShowDialog("Vous Voulez envoyer cette commande", true) == DialogResult.OK)
            {
                send = true;
            }
            printOrder(commande_ID,send,print);
            Program.insertLog("Ajout Commande  : " + commande_ID.ToString(), "Order", commande_ID.ToString());

        }
        Product_List_Form productList;
        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if (productList == null)
            {
                productList =  new Product_List_Form((MainForm)this.ParentForm);
                productList.Show();
            }
            else
            {
                productList.Show();
                productList.TopMost = true;
                productList.TopMost = false;
            }
          
        }

        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

          
        }

        private void guna2DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string x = e.FormattedValue.ToString();
            if (String.IsNullOrEmpty(x))
            {
                guna2DataGridView1.CancelEdit();
                return;
            }
           
        }

        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void guna2DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.ColumnIndex == 3) //Desired Column
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

        }
    }
}
