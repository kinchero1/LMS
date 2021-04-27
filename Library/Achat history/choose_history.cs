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
    public partial class choose_history : UserControl
    {
        public choose_history()
        {
            InitializeComponent();
        }

        private void choose_history_Load(object sender, EventArgs e)
        {

        }

        public void showPerProduct(int i)
        {

            Program.main.providerPerProduct1.loadData(i);
            Program.main.providerPerProduct1.BringToFront();
        }

        public void showPerProvider(int i)
        {

            Program.main.productsPerProvider1.loadData(i);
            Program.main.productsPerProvider1.BringToFront();
        }

        Product_List_Form Prouctlist;
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (Prouctlist == null)
            {
                Prouctlist = new Product_List_Form((MainForm)this.ParentForm, 7,null,null,this);
                Prouctlist.Show();
            }
            else
            {
                Prouctlist.Show();
                Prouctlist.TopMost = true;
                Prouctlist.TopMost = false;
            }
        }

        Provider_List_Form providerlist;
        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (providerlist == null)
            {
                providerlist = new Provider_List_Form(this);
                providerlist.Show();
            }
            else
            {
                providerlist.Show();
                providerlist.TopMost = true;
                providerlist.TopMost = false;
            }
        }
    }
}
