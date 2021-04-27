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
    public partial class Loading_Control : UserControl
    {
        public Loading_Control()
        {
            InitializeComponent();
        }

        private void Loading_Control_Load(object sender, EventArgs e)
        {
            Program.centercontrolH(gunaCircleProgressBar1);
            Program.centercontrolV(gunaCircleProgressBar1);
        }
    }
}
