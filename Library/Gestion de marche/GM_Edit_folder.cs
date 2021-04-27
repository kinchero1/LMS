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

namespace Library.Gestion_de_marche
{
    public partial class GM_Edit_folder : UserControl
    {
        public GM_Edit_folder()
        {
            InitializeComponent();
        }

        public void loadData(string id)
        {
            try
            {
                string sql = "select id,Designation,Unite,Quantite,Prix,Tva,productid from marcheProduit where Nmarche =@id";

                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                guna2DataGridView1.DataSource = dt;
                guna2DataGridView1.Columns[0].Visible = false;
                guna2DataGridView1.Columns[6].Visible = false;
                guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                guna2DataGridView1.Columns[3].DefaultCellStyle.Format = "x 0";
                guna2DataGridView1.Columns[4].DefaultCellStyle.Format = "0.00 DH";
                guna2DataGridView1.Columns[5].DefaultCellStyle.Format = "00 '%'";

                label7.Text = guna2DataGridView1.Rows.Count + "en Totale";
            }
            catch (Exception)
            {

                
            }
          

        }

        public void insertProductToMarcheDB(string Nmarch, string desg, string unite, int qu, float prix, int tva, int idproduit)
        {
            string sql = "insert into marcheProduit values(@N,@desg,@unite,@qu,@prix,@tva,@idProduit)";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = Nmarch;
            cmd.Parameters.Add("@desg", SqlDbType.NVarChar).Value = desg;
            cmd.Parameters.Add("@unite", SqlDbType.VarChar).Value = unite;
            cmd.Parameters.Add("@qu", SqlDbType.Int).Value = qu;
            cmd.Parameters.Add("@prix", SqlDbType.Float).Value = prix;
            cmd.Parameters.Add("@tva", SqlDbType.Int).Value = tva;
            cmd.Parameters.Add("@idProduit", SqlDbType.Int).Value = idproduit;
            cmd.ExecuteNonQuery();
        }

        public void deleteProduct(int id)
        {
            string sql = "delete from marcheProduit where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
            loadData(this.Tag.ToString());
        }

        public void updateProduct(int id, string desg, string unite, int qu, float prix, int tva, int idproduit)
        {
            string sql = "update marcheProduit set designation = @desg,unite= @unite, quantite = @qu, prix=@prix, tva=@tva where id = @id  ";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
     
            cmd.Parameters.Add("@desg", SqlDbType.NVarChar).Value = desg;
            cmd.Parameters.Add("@unite", SqlDbType.VarChar).Value = unite;
            cmd.Parameters.Add("@qu", SqlDbType.Int).Value = qu;
            cmd.Parameters.Add("@prix", SqlDbType.Float).Value = prix;
            cmd.Parameters.Add("@tva", SqlDbType.Int).Value = tva;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
            loadData(this.Tag.ToString());
        }

        private void GM_third_page_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (Program.CheckEmpty(Nom_produit) || Program.CheckEmpty(Unite_TB) || Program.CheckEmpty(Quantite) || Program.CheckEmpty(TVA, -1))
            {
                return;
            }
            insertProductToMarcheDB(this.Tag.ToString(), Nom_produit.Text, Unite_TB.Text, int.Parse(Quantite.Text), int.Parse(PrixTB.Text), int.Parse(TVA.Text.Replace("%", "")), 0);


            loadData(this.Tag.ToString());

        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.RowCount > 0)
            {
                int selectedRowCount = guna2DataGridView1.SelectedRows.Count;
                for (int i = 0; i < selectedRowCount; i++)
                {
                    int x = guna2DataGridView1.SelectedRows[0].Index;
                    deleteProduct(int.Parse(guna2DataGridView1.Rows[x].Cells[0].Value.ToString()));

                }
            }

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
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

            if (e.ColumnIndex == 4)
            {
                string value = x.Replace("DH", "");
                if(value.Count(n => n == '.') > 1)
                {
                    guna2DataGridView1.CancelEdit();
                }

            }
            if (e.ColumnIndex == 5)
            {
                string value = x.Replace("%", "");
                bool cancel = int.Parse(value) == 7 || int.Parse(value) == 14 || int.Parse(value) == 20;
                if (!cancel )
                {
                    guna2DataGridView1.CancelEdit();
                }

            }
         
        }

        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
          
    
        }


        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            Product_List_Form x = new Product_List_Form((MainForm)this.ParentForm, 5);
            x.Show();
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {


        }

        

        private void guna2DataGridView1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {

        }



        private void guna2DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string value = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {

                guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value.Replace("x", "").Replace("%", "").Replace("DH", "");

            }
        }

        private void guna2DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

          
            if (guna2DataGridView1.CurrentCell.ColumnIndex == 3 || guna2DataGridView1.CurrentCell.ColumnIndex == 5) //Desired Column
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

            if (guna2DataGridView1.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                tb.Text = tb.Text.Replace("x", "").Replace("%", "").Replace("DH", "");
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler((ob,ev)=>
                    {
                        Program.onlyDegit(ev, true);
                    });
                }
            }
        }

      
     
       

        private void guna2DataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            
        }

        private void guna2DataGridView1_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {

            string desg = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string unite = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            int quantite = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Replace("x", ""));
            float prix = float.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Replace("DH", ""));
            int tva = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString().Replace("%", ""));
            int id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Replace("%", ""));

            updateProduct(id, desg, unite, quantite, prix, tva, id);
        }

        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
