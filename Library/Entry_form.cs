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
    public partial class Entry_form : Form
    {
        public Entry_form()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }

        private void Entry_form_Load(object sender, EventArgs e)
        {
          
            Societe.Items.Add("Bahij");
            Societe.Items.Add("Perle");
            Societe.SelectedIndex = 0;
        }

        private void Ok_Button_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Library_name = Societe.Text.ToLower();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void Entry_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Properties.Settings.Default.Library_name != "bahij" && Properties.Settings.Default.Library_name != "perle")
            {
                Properties.Settings.Default.Library_name = "bahij";
                Properties.Settings.Default.Save();
            }
          
        }
    }
}
