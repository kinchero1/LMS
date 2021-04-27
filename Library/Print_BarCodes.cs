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
using ZXing;
using ZXing.QrCode;
using System.IO;

namespace Library
{
    public partial class Print_BarCodes : UserControl
    {
        public Print_BarCodes()
        {
            InitializeComponent();
        }
        int taille = 60;
        public void printbarcodes(string[] cellsRangeImage,int count,int width=115,int height=60,int top=15,int left =5)
        {
            int x = 1;
            int index = 0;
            ExcelHelperImage ex = new ExcelHelperImage(Environment.CurrentDirectory + @"\Models\barcodeprinter"+count+".xlsx");
            BarcodeWriter br = new BarcodeWriter() { Format = BarcodeFormat.CODE_128, Options = new QrCodeEncodingOptions { Width = width*2, Height = height*2 } };
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                
                string barcode = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                int quantite = int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString());
                for (int j = 0; j < quantite; j++)
                {
                    Image img = br.Write(barcode);
                    img.Save(Path.GetTempPath() + i + ".png");

                    ex.addimage(cellsRangeImage[index] + x, Path.GetTempPath() + i + ".png", width, height,left,top);
                    File.Delete(Path.GetTempPath() + i + ".png");
                    index++;
                    if (index >= cellsRangeImage.Length)
                    {
                        x++;
                        index = 0;
                    }
                }
               
            }
            ex.Save(Path.GetTempPath() + "BarCodes.xlsx");
        }

        private void Print_BarCodes_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            loadData();
        }

        public void loadData()
        {
            Taille.Items.Clear();
            Taille.Items.Add("60");
            Taille.Items.Add("65");
            Taille.Items.Add("87");
            Taille.SelectedIndex = 0;

            string sql = "select (nom +' ' +matiere+ ' '+Nscolaire) as 'Nom',(CONVERT(varchar,id)+'/'+codebar) as 'id' from produit";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            Designation.ValueMember = "id";
            Designation.DisplayMember = "Nom";
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            Designation.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

        }

        public void calculateQuAndLoss()
        {
            int taille = int.Parse(Taille.Text);
    
            int quantite = 0;
            for (int i = 0; i < guna2DataGridView1.RowCount; i++)
            {
                quantite += int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString());
            }
            int Loss = quantite % taille;
          
            total.Text = quantite.ToString();
            if (Loss==0)
            {
                loss.Text = "0";
            }
            else
            {
                loss.Text = (taille - Loss).ToString();

            }
          


        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (Program.CheckEmpty(Quanite_tb)|| int.Parse(Quanite_tb.Text)==0)
            {
                return;
            }
            if (Designation.SelectedValue == null)
            {
                return;
            }
            string[] x = Designation.SelectedValue.ToString().Split('/');
            string id = x[0];
            string barcode = x[1];
            string quantite = Quanite_tb.Text;

            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                if (guna2DataGridView1.Rows[i].Cells[0].Value.ToString()==x[0])
                {
                    guna2DataGridView1.Rows[i].Cells[3].Value = int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString()) + 1;
                    return;

                }
            }


        
            int row =guna2DataGridView1.Rows.Add(id, Designation.Text, barcode,Quanite_tb.Text);
            guna2DataGridView1.Rows[row].Cells[0].Tag = id;
            total.Text = guna2DataGridView1.Rows.Count.ToString();
            calculateQuAndLoss();
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


           
            calculateQuAndLoss();
        }

        private async void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (Program.main.exporting)
            {
                return;
            }
            if (guna2DataGridView1.Rows.Count<=0)
            {
                return;
            }
            
            if (Taille.SelectedIndex==0)
            {
                string[] x = { "A", "B", "C", "D", "E" };
                Program.main.setProgressBarVisisblité(true);
                await Task.Run(()=> printbarcodes(x, 60));
                Program.main.setProgressBarVisisblité(false);
                ExcelHelper exH = new ExcelHelper(Path.GetTempPath() + "BarCodes.xlsx", 1);
                exH.deleteWorkSheet(1);
                try
                {
                    exH.SaveAndClose(true,true);
                    MessageBox.Show("Exportation bien Fait");
                   
                }
                catch (Exception)
                {
                    exH.SaveAndClose(false);
                    MessageBox.Show("Erreur d'impresssion");

                }

            }
            else if(Taille.SelectedIndex == 1)
            {
                string[] x = { "A", "B", "C", "D", "E" };
                Program.main.setProgressBarVisisblité(true);
                await Task.Run(() => printbarcodes(x, 65));
                Program.main.setProgressBarVisisblité(false);
                ExcelHelper exH = new ExcelHelper(Path.GetTempPath() + "BarCodes.xlsx", 1);
                try
                {
                    exH.SaveAndClose(true,true);
                    MessageBox.Show("Exportation bien Fait");
                   
                }
                catch (Exception)
                {
                    exH.SaveAndClose(false);
                    MessageBox.Show("Erreur d'impresssion");

                }
                
                File.Delete(Path.GetTempPath() + "BarCodes.xlsx");
            }
            else
            {
                string[] x = { "A", "B", "C"};
                Program.main.setProgressBarVisisblité(true);
                await Task.Run(() => printbarcodes(x, 87,115,35,0,40));
                Program.main.setProgressBarVisisblité(false);
                ExcelHelper exH = new ExcelHelper(Path.GetTempPath() + "BarCodes.xlsx", 1);
                try
                {
                    exH.SaveAndClose(true,true);
                    MessageBox.Show("Exportation bien Fait");

                }
                catch (Exception)
                {
                    exH.SaveAndClose(false);
                    MessageBox.Show("Erreur d'impresssion");

                }
                File.Delete(Path.GetTempPath() + "BarCodes.xlsx");
            }

            guna2DataGridView1.Rows.Clear();
            total.Text = "0";
            calculateQuAndLoss();

        }

        private void Taille_SelectedIndexChanged(object sender, EventArgs e)
        {
            taille = int.Parse(Taille.Text);
            calculateQuAndLoss();
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            Product_Finder x = new Product_Finder();
            x.Show();
        }

        private void N_Bon_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e);
        }
        Product_List_Form Prouctlist;
        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {

            if (Prouctlist == null)
            {
                Prouctlist = new Product_List_Form((MainForm)this.ParentForm, 2);
                Prouctlist.Show();
            }
            else
            {
                Prouctlist.Show();
                Prouctlist.TopMost = true;
                Prouctlist.TopMost = false;
            }

        }

        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.RowCount <=0)
            {
                return;
            }
            calculateQuAndLoss();

        }

        private void guna2DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
        }
    }
}
