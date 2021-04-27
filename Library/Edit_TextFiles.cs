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

namespace Library
{
    public partial class Edit_TextFiles : UserControl
    {
        public Edit_TextFiles()
        {
            InitializeComponent();
        }
        

        private void Edit_TextFiles_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            loadData();

        }
        public void loadData()
        {
            readfile("cat.txt", guna2DataGridView1,true);
            readfile("mat.txt", guna2DataGridView2);
            readfile("Ns.txt", guna2DataGridView3,true);
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
      
            guna2DataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
         
        }

        void readfile(string name, Control ctrl, bool withColumns =false)
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LMS\\" + name);
            string[] lines = File.ReadAllLines(filename);
            

            foreach (string line in lines)
            {
                if (withColumns)
                {
                    string[] columns = line.Split(',');
                    if (columns.Length > 1)
                    {
                        ((Guna.UI2.WinForms.Guna2DataGridView)ctrl).Rows.Add(columns[0],columns[1]);
                    }
                }
                else
                {
                    ((Guna.UI2.WinForms.Guna2DataGridView)ctrl).Rows.Add(line);
                }
               
              
            }
        }

        void alterTextfile(string name, Control ctrl,bool withColumns =false)
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LMS\\" + name);
            Guna.UI2.WinForms.Guna2DataGridView guna = ((Guna.UI2.WinForms.Guna2DataGridView)ctrl);

            List<string> List = new List<string>();
            for (int i = 0; i < guna.RowCount; i++)
            {
                if (withColumns)
                {
                    if (guna.Rows[i].Cells[0].Value != null )
                    {
                        string value = guna.Rows[i].Cells[1].Value != null ? guna.Rows[i].Cells[1].Value.ToString() : " ";
                        List.Add(guna.Rows[i].Cells[0].Value.ToString() + "," +value);
                    }
                }
                else
                {
                    List.Add(guna.Rows[i].Cells[0].Value.ToString());
                }
               
            }
            File.WriteAllLines(filename, List);

        }


        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Add(Cat_TB.Text,Cat_TB.Text.Substring(0,3));
            Cat_TB.Clear();
        }
       

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            alterTextfile("cat.txt", guna2DataGridView1,true);
            alterTextfile("mat.txt", guna2DataGridView2);
            alterTextfile("Ns.txt", guna2DataGridView3,true);
            MessageBox.Show("Bien enregistrée");
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            int selectedrow = guna2DataGridView1.CurrentRow.Index;
            guna2DataGridView1.Rows.RemoveAt(selectedrow);
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            guna2DataGridView2.Rows.Add(Matiere_TB.Text);
            Matiere_TB.Clear();

        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            guna2DataGridView3.Rows.Add(NS_TB.Text);
            NS_TB.Clear();
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            int selectedrow = guna2DataGridView2.CurrentRow.Index;
            guna2DataGridView2.Rows.RemoveAt(selectedrow);
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            int selectedrow = guna2DataGridView3.CurrentRow.Index;
            guna2DataGridView3.Rows.RemoveAt(selectedrow);
        }
      
    }
}
