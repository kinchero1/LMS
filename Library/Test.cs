using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            jsonstorage_net_helper io = new jsonstorage_net_helper();
              io.Update("bahij", "{\"name\":\"Johnfff Doe\",\"age\":353}");


        }
    }
}
