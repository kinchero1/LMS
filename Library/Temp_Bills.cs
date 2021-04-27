using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Temp_Bills : UserControl
    {
        public Temp_Bills()
        {
            InitializeComponent();
        }
        Add_Bill bill;
        public void loadData()
        {
            guna2DataGridView1.Rows.Clear();
            MainForm form = (MainForm)this.ParentForm;

            foreach (Control addbill in form.UserControlPanel.Controls)
            {
                if (addbill.GetType() == typeof(Add_Bill))
                {

                    Add_Bill Bill = (Add_Bill)addbill;
                    bill = Bill;
                    string[] Billdata = Bill.GetData();
                    int x = guna2DataGridView1.Rows.Add(guna2DataGridView1.Rows.Count + 1, Billdata[0], Billdata[1], Billdata[2]);
                    guna2DataGridView1.CellDoubleClick += new DataGridViewCellEventHandler((es, Row) =>
                     {
                         if (Row.RowIndex == x)
                         {
                             Bill.BringToFront();
                         }
                     });
                    guna2DataGridView1.CellClick += new DataGridViewCellEventHandler((es, Row) =>
                    {
                        if (Row.RowIndex == x)
                        {
                            bill = Bill;
                        }
                    });

                }
            }
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void Temp_Bills_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            Add_Bill bill = new Add_Bill();
            MainForm form = (MainForm)this.ParentForm;
            form.UserControlPanel.Controls.Add(bill);
            bill.BringToFront();
        
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if(bill != null)
            {
                bill.BringToFront();

            }
        
        }


        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if(bill != null)
            {
                MainForm form = (MainForm)this.ParentForm;
                form.UserControlPanel.Controls.Remove(bill);
                loadData();
            }
           

        }
        public void RemoveBill(Add_Bill Billl)
        {
            MainForm form = (MainForm)this.ParentForm;
            form.UserControlPanel.Controls.Remove(Billl);
            loadData();
        }
    }
}
