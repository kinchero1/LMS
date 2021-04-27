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
    public partial class MessageBox : Form
    {
        public MessageBox(string message)
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
            label1.Text = message;
        }

        private void MessageBox_Load(object sender, EventArgs e)
        {
            gunaAnimateWindow1.Start();
        }

        private void Yes_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void No_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public static DialogResult ShowDialog(object message,bool YesNo)  
        {
            MessageBox x = new MessageBox(message.ToString());
            x.ShowInTaskbar = false;
            if (YesNo)
            {
                x.Yes_Button.Visible = true;
                x.No_Button.Visible = true;
                x.Ok_Button.Visible = false;
            }
            else
            {
                x.Yes_Button.Visible = false;
                x.No_Button.Visible = false;
                x.Ok_Button.Visible = true;
            }
            x.ShowDialog();
            return x.DialogResult;

        }
        public static void Show(object message)
        {
            MessageBox x = new MessageBox(message.ToString());
            x.Yes_Button.Visible = false;
            x.No_Button.Visible = false;
            x.Ok_Button.Visible = true;
            x.Show();
        }

        private void Ok_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
