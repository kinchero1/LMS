using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;


namespace Library
{
    public partial class VisualKeyboard : UserControl
    {
        Guna.UI2.WinForms.Guna2TextBox TargetTextbox;
        public VisualKeyboard()
        {
            InitializeComponent();
            foreach (Control ctrl in FrenchPanel.Controls)
            {
                if (ctrl.GetType() == typeof(GunaAdvenceButton))
                {
                    GunaAdvenceButton guna = (GunaAdvenceButton)ctrl;
                    if (guna.Name != "backspace" && guna.Name != "space" && guna.Name != "shift" && guna.Name != "Lang1" && guna.Name != "Lang2")
                    {
                        guna.Click += new EventHandler((ob, e) =>
                        {
                            TargetTextbox.Text += ((GunaAdvenceButton)ob).Text;

                        });
                    }
                }
            }

            foreach (Control ctrl in ArabicPanel.Controls)
            {
                if (ctrl.GetType() == typeof(GunaAdvenceButton))
                {
                    GunaAdvenceButton guna = (GunaAdvenceButton)ctrl;
                    if (guna.Name != "backspace1" && guna.Name != "space1" && guna.Name != "shift1" && guna.Name != "lang3" && guna.Name != "lang4")
                    {
                        guna.Click += new EventHandler((ob, e) =>
                        {
                            TargetTextbox.Text += ((GunaAdvenceButton)ob).Text;

                        });
                    }
                }
            }

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {

        }

        public void ShowKeyboard(Guna.UI2.WinForms.Guna2TextBox textbox, Point location)
        {
            TargetTextbox = textbox;

            this.BringToFront();
            this.Visible = true;
            this.Location = location;

        }
        public void SetTarget(Guna.UI2.WinForms.Guna2TextBox textbox)
        {
            TargetTextbox = textbox;
           
        }

        public void HideKeyboard()
        {
            TargetTextbox = null;

            this.Visible = false;
        }

        private void gunaAdvenceButton39_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in FrenchPanel.Controls)
            {
                if (ctrl.GetType() == typeof(GunaAdvenceButton))
                {
                    GunaAdvenceButton guna = (GunaAdvenceButton)ctrl;
                    if (guna.Name != "backspace" && guna.Name != "space" && guna.Name != "shift" && guna.Name != "Lang1" && guna.Name != "Lang2")
                    {
                        if (shift.Checked)
                        {
                            guna.Text = guna.Text.ToUpper();
                        }
                        else
                        {
                            guna.Text = guna.Text.ToLower();
                        }

                    }
                }
            }


        }

        private void space_Click(object sender, EventArgs e)
        {
            TargetTextbox.Text += " ";
        }

        private void backspace_Click(object sender, EventArgs e)
        {
            if (TargetTextbox.Text.Length == 0)
            {
                return;
            }
            TargetTextbox.Text = TargetTextbox.Text.Remove(TargetTextbox.Text.Length - 1);
        }

        private void gunaLinePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Lang1_Click(object sender, EventArgs e)
        {
            ArabicPanel.Visible = true;
            FrenchPanel.Visible = false;

        }

        private void VisualKeyboard_Load(object sender, EventArgs e)
        {
            FrenchPanel.Parent = this;
            ArabicPanel.Parent = this;
            FrenchPanel.Dock = DockStyle.Fill;
            ArabicPanel.Dock = DockStyle.Fill;
            ArabicPanel.Visible = false;
            FrenchPanel.Visible = true;
        }

        private void gunaAdvenceButton11_Click(object sender, EventArgs e)
        {
            ArabicPanel.Visible = false;
            FrenchPanel.Visible = true;
        }

        private void gunaAdvenceButton41_Click(object sender, EventArgs e)
        {
            TargetTextbox.Text += " ";
        }

        private void backspace1_Click(object sender, EventArgs e)
        {

            if (TargetTextbox.Text.Length == 0)
            {
                return;
            }
            TargetTextbox.Text = TargetTextbox.Text.Remove(TargetTextbox.Text.Length - 1);
        }

        private void shift1_Click(object sender, EventArgs e)
        {
            if (shift1.Checked)
            {
                gunaAdvenceButton62.Text = "إ";
                gunaAdvenceButton63.Text = "لإ";
                gunaAdvenceButton52.Text = "أ";
                gunaAdvenceButton53.Text = "لأ";
                gunaAdvenceButton42.Text = "آ";
                gunaAdvenceButton43.Text = "لآ";
                gunaAdvenceButton79.Text = "ذ";
            }
            else
            {
                gunaAdvenceButton62.Text = "غ";
                gunaAdvenceButton63.Text = "ف";
                gunaAdvenceButton52.Text = "ا";
                gunaAdvenceButton53.Text = "ل";
                gunaAdvenceButton42.Text = "ى";
                gunaAdvenceButton43.Text = "لآ";
                gunaAdvenceButton79.Text = "د";
            }

        }

        private void VisualKeyboard_MouseLeave(object sender, EventArgs e)
        {

        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            HideKeyboard();
        }

        private void ArabicPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
