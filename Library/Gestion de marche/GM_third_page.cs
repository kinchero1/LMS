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

namespace Library.Gestion_de_marche
{
    public partial class GM_third_page : UserControl
    {
        public GM_third_page()
        {
            InitializeComponent();
        }

        public void loadData()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
        private void GM_third_page_Load(object sender, EventArgs e)
        {
            Program.centercontrolH(panel1);
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (Program.CheckEmpty(Nom_produit) || Program.CheckEmpty(Unite_TB) || Program.CheckEmpty(Quantite) || Program.CheckEmpty(TVA,-1))
            {
                return;
            }
            int x = guna2DataGridView1.Rows.Add(Nom_produit.Text, Unite_TB.Text, "x " + Quantite.Text, PrixTB.Text.ToString() + " DH", TVA.Text );
            guna2DataGridView1.Rows[x].Cells[0].ReadOnly = false;
            guna2DataGridView1.Rows[x].Cells[0].Tag = "0";
            label7.Text = guna2DataGridView1.Rows.Count + "en Totale";
            calculte();
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
            label7.Text = guna2DataGridView1.Rows.Count + "en Totale";
            calculte();
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            Program.main.gM_fourth_page1.BringToFront();
            Program.main.gM_fourth_page1.Montant_HT.Text = montant_HT.ToString();
            Program.main.gM_fourth_page1.Montant_TVA.Text = montant_TVA.ToString();
            Program.main.gM_fourth_page1.Montant_TTC.Text = montant_TTC.ToString();
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            Program.main.gM_secondpage_perle.BringToFront();
            
        }

        private void guna2DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string x = e.FormattedValue.ToString();
            if (String.IsNullOrEmpty(x))
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
           else  if (e.ColumnIndex == 4)
            {
                string value = x.Replace("%", "");
                bool cancel = int.Parse(value) == 7 || int.Parse(value) == 14 || int.Parse(value) == 20;
                if (!cancel)
                {
                    guna2DataGridView1.CancelEdit();
                }
            }

        }

        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows.Count > 0)
            {
                if (e.ColumnIndex == 2)
                {
                    int Qte = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace("x", " ").Replace(" ", ""));
                    guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Qte.ToString("'x' 0");

                }

                if (e.ColumnIndex == 3)
                {
                    string formatedvalue = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace("DH", " ").Replace(" ", "");
                    float Prix = float.Parse(formatedvalue);
                    guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Prix.ToString("0.00 'DH'");

                }

                if (e.ColumnIndex == 4)
                {
                    int Qte = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace("%", " ").Replace(" ",""));
                    guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Qte.ToString("0 '%'");

                }
                calculte();
            }
        }
        Product_List_Form Prouctlist;
        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if (Prouctlist == null)
            {
                Prouctlist = new Product_List_Form((MainForm)this.ParentForm, 1); ;
                Prouctlist.Show();
            }
            else
            {
                Prouctlist.Show();
                Prouctlist.TopMost = true;
                Prouctlist.TopMost = false;
            }
            
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
  
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            
                label11.Text = file[0];
                Program.main.gM_Start1.marcheBahij.pdpath = file[0];
                label11.Tag = true;
            
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
           
            if (op.ShowDialog() == DialogResult.OK)
            {
                label11.Text = op.FileName;
                Program.main.gM_Start1.marcheBahij.pdpath = op.FileName;

                label11.Tag = true;

            }


        }
         float montant_HT;
         float montant_TVA;
         float montant_TTC;
        public void calculte()
        {

            montant_HT = 0;
            montant_TVA = 0;
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {

                int quantite = int.Parse(guna2DataGridView1.Rows[i].Cells[2].Value.ToString().Replace("x", " "));
                float Prix = float.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString().Replace("DH", ""));

                float TVA = float.Parse(guna2DataGridView1.Rows[i].Cells[4].Value.ToString().Replace("%", ""));

                float value = (Prix * quantite);
                float TVA_value = Program.percentage(value, TVA);
                montant_HT += value;
                montant_TVA += TVA_value;




            }
            montant_TTC = montant_HT + montant_TVA;
            


        }

        private void guna2DataGridView1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.ColumnIndex == 2 || guna2DataGridView1.CurrentCell.ColumnIndex == 4) //Desired Column
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

            if (guna2DataGridView1.CurrentCell.ColumnIndex == 3) //Desired Column
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
    }
}
