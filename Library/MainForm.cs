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
    public partial class MainForm : Form
    {
        public bool exporting;
        bool isbillatFront = false;
        public MainForm()
        {
          
            Program.main = this;
            Program.openconnection();
            Program.normalMOD = Properties.Settings.Default.normalMOD;
            InitializeComponent();
          
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }
        public void setText(string Text)
        {
            LabelName.Text = Text;
            foreach (Control addbill in UserControlPanel.Controls)
            {
                if (addbill.GetType() == typeof(Add_Bill))
                {
                    Add_Bill Bill = (Add_Bill)addbill;
         
                    if (!Program.IsControlAtFront(Bill)) Bill.setBarcodeReader();

                }

            }
            if (!Program.IsControlAtFront(add_product1)) add_product1.setBarcodeReader();

            gunaAdvenceButton37.Checked = Program.IsControlAtFront(library_Consultation1);
            gunaAdvenceButton7.Checked = Program.IsControlAtFront(settingPanel);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            notifyIcon1.Visible = Properties.Settings.Default.minimizeTosystemTray;
            gunaAdvenceButton28.Visible = Properties.Settings.Default.minimizeTosystemTray;

            string[] x = Login.name.Split(' ');
            if (x.Length > 1)
            {
                guna2CircleButton1.Text = x[0][0] + "" + x[1][0];
            }
            else
            {
                guna2CircleButton1.Text = Login.name[0].ToString();
            }
           
            gunaAdvenceButton1.PerformClick();
            gunaAnimateWindow1.Start();
            gunaAdvenceButton30.Visible = (Login.accountype == "ADMIN");
            label17.Visible= (Login.accountype == "ADMIN");
            label18.Visible = !Program.normalMOD;
            if (!Program.normalMOD)
            {
                foreach (Control control in FlowLayoutPanel.Controls)
                {
                    control.Visible = false;
                }
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    control.Visible = false;
                }

                label2.Visible = true;
                label2.Text = "LISTE ARCHIVE";
                gunaAdvenceButton6.Visible = true;
                gunaAdvenceButton9.Visible = true;
                gunaAdvenceButton14.Visible = true;
                FlowLayoutPanel.Controls.Add(gunaAdvenceButton14);
            }
            gunaAdvenceButton37.Visible = Application_Banner.dataLoaded;
          



        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            dashBoard1.loadData();
            dashBoard1.BringToFront();
            setText(((Control)sender).Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {



        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void add_product1_Load(object sender, EventArgs e)
        {
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            add_product1.loadData();
            add_product1.BringToFront();
            setText(((Control)sender).Text);
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            product_list1.load_data();
            product_list1.BringToFront();
            setText(((Control)sender).Text);

        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            Delivery_Form1.loadData();
            Delivery_Form1.BringToFront();
            setText(((Control)sender).Text);


        }

        private void view_product1_Load(object sender, EventArgs e)
        {

        }

        private void add_bill1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }

        private void product_list1_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            delivery_Form_List1.loaddata();

            delivery_Form_List1.BringToFront();
            setText(((Control)sender).Text);
        }

        private void deliveyForm_ProductList1_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            setText(((Control)sender).Text);
            temp_Bills1.BringToFront();
            temp_Bills1.loadData();

        }

        private void payment1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaAdvenceButton9_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton9_Click_1(object sender, EventArgs e)
        {
            Bills_List.loadData();
            Bills_List.BringToFront();
            setText(((Control)sender).Text);

        }

        private void Bills_List_Load(object sender, EventArgs e)
        {

        }

        private void viewEdit_Bill1_Load(object sender, EventArgs e)
        {

        }

        private void gunaHScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {


        }

        private void MainSidePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainSidePanel_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaAdvenceButton10_Click(object sender, EventArgs e)
        {
            SecondarySideMenu.SendToBack();
            achat_history_panel.SendToBack();
            settingPanel.SendToBack();
            foreach (Control control in FlowLayoutPanel.Controls)
            {
                if (control.GetType()==typeof(Guna.UI.WinForms.GunaAdvenceButton))
                {
                    Guna.UI.WinForms.GunaAdvenceButton button = (Guna.UI.WinForms.GunaAdvenceButton)control;
                    if (button.Checked)
                    {
                        button.PerformClick();
                    }
                }
            }
            


        }

        private void gunaAdvenceButton15_Click(object sender, EventArgs e)
        {

        
            SecondarySideMenu.BringToFront();
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control.GetType() == typeof(Guna.UI.WinForms.GunaAdvenceButton))
                {
                    Guna.UI.WinForms.GunaAdvenceButton button = (Guna.UI.WinForms.GunaAdvenceButton)control;
                    if (button.Checked)
                    {
                        button.PerformClick();
                    }
                }
            }

        }

        private void gunaAdvenceButton12_Click(object sender, EventArgs e)
        {
            Add_Client.BringToFront();
            Add_Client.loadAddEnv();
            setText(((Control)sender).Text);
        }

        private void gunaAdvenceButton13_Click(object sender, EventArgs e)
        {
            client_List1.BringToFront();
            client_List1.loadData();
            setText(((Control)sender).Text);

        }

        private void gunaAdvenceButton16_Click(object sender, EventArgs e)
        {
            add_Provider1.BringToFront();
            add_Provider1.loadAddEnv();
            setText(((Control)sender).Text);

        }

        private void gunaAdvenceButton17_Click(object sender, EventArgs e)
        {
            provider_List1.BringToFront();
            provider_List1.loadData();
            setText(((Control)sender).Text);

        }

        private void gunaAdvenceButton11_Click(object sender, EventArgs e)
        {
            add_Order1.BringToFront();
            add_Order1.loadData();
            setText(((Control)sender).Text);
        }

        private void gunaAdvenceButton14_Click(object sender, EventArgs e)
        {
            order_List1.BringToFront();
            order_List1.loadData();
            setText(((Control)sender).Text);

        }

        private void edit_Order1_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton18_Click(object sender, EventArgs e)
        {
            print_BarCodes1.BringToFront();
            print_BarCodes1.loadData();
            setText(((Control)sender).Text);
        }

        private void dashBoard1_Load(object sender, EventArgs e)
        {

        }

        private void temp_Bills1_Load(object sender, EventArgs e)
        {

        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        public void setProgressBarVisisblité(bool status)
        {
            gunaCircleProgressBar1.Visible = status;
            exporting = status;

        }
        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.minimizeTosystemTray)
            {
                this.Hide();
            }
            else
            {

                Program.closeconnection();
                Program.LoginForm.Show();

                notifyIcon1.Visible = false;
                this.Close();
            }
        
        }

        private void edit_TextFiles1_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton25_Click(object sender, EventArgs e)
        {
            edit_TextFiles1.BringToFront();
            setText(((Control)sender).Text);
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            gunaAdvenceButton10.Checked = false;
            gunaAdvenceButton15.Checked = false;
            settingPanel.BringToFront();
            foreach (Control control in flowLayoutPanel3.Controls)
            {
                if (control.GetType() == typeof(Guna.UI.WinForms.GunaAdvenceButton))
                {
                    Guna.UI.WinForms.GunaAdvenceButton button = (Guna.UI.WinForms.GunaAdvenceButton)control;
                    if (button.Checked)
                    {
                        button.PerformClick();
                    }
                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            edit_profile1.BringToFront();
            edit_profile1.loadData();
            setText("Profile");
        }

        private void notifymenu_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            this.Show();
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            gunaAdvenceButton7.PerformClick();
            gunaAdvenceButton25.PerformClick();

        }

        private void paimentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            gunaAdvenceButton10.PerformClick();
            gunaAdvenceButton8.PerformClick();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.closeconnection();
            Program.LoginForm.Show();

            notifyIcon1.Visible = false;
            this.Close();
        }

        private void gunaAdvenceButton26_Click(object sender, EventArgs e)
        {

            gM_Start1.BringToFront();
            setText(((Control)sender).Text);

        }

        private void gunaAdvenceButton27_Click(object sender, EventArgs e)
        {
            gM_FoldersList1.loadData();
            gM_FoldersList1.BringToFront();
            setText(((Control)sender).Text);
        }

        private void gunaAdvenceButton28_Click(object sender, EventArgs e)
        {
            Program.closeconnection();
            Program.LoginForm.Show();

            notifyIcon1.Visible = false;
            this.Close();
        }

        private void gunaAdvenceButton29_Click(object sender, EventArgs e)
        {
            setText(((Control)sender).Text);
            application_Settings1.BringToFront();
        }

        private void gunaAdvenceButton30_Click(object sender, EventArgs e)
        {
            setText(((Control)sender).Text);
            user_Setting1.loadData();
            user_Setting1.BringToFront();

        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            setText(((Control)sender).Text);
            product_Statistique1.loadData();
            product_Statistique1.BringToFront();
        }

        private void delivery_Form_List1_Load(object sendter, EventArgs e)
        {

        }

        private  void gunaAdvenceButton37_Click(object sender, EventArgs e)
        {

            library_Consultation1.setupData();
            library_Consultation1.BringToFront();
            setText("Library Consultation");
           
           


        }

        private void gunaCircleProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void LabelName_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton38_Click(object sender, EventArgs e)
        {
            setText(((Control)sender).Text);
            log_userC1.loadData();
            log_userC1.BringToFront();
        }

        private void gunaAdvenceButton46_Click(object sender, EventArgs e)
        {
            setText(((Control)sender).Text);
            choose_history1.BringToFront();
        }

        private void gunaAdvenceButton39_Click(object sender, EventArgs e)
        {
            achat_history_panel.BringToFront();
            gunaAdvenceButton46.PerformClick();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("yes");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F1))
            {
                temp_Bills1.gunaAdvenceButton3.PerformClick();
                gunaAdvenceButton8.Checked = true;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
