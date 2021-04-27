namespace Library
{
    partial class MessageBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Yes_Button = new Guna.UI.WinForms.GunaAdvenceButton();
            this.No_Button = new Guna.UI.WinForms.GunaAdvenceButton();
            this.Ok_Button = new Guna.UI.WinForms.GunaAdvenceButton();
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaAnimateWindow1 = new Guna.UI.WinForms.GunaAnimateWindow(this.components);
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 10);
            this.panel1.TabIndex = 0;
            // 
            // gunaControlBox1
            // 
            this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox1.AnimationSpeed = 0.03F;
            this.gunaControlBox1.IconColor = System.Drawing.Color.Black;
            this.gunaControlBox1.IconSize = 15F;
            this.gunaControlBox1.Location = new System.Drawing.Point(461, 16);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(33, 29);
            this.gunaControlBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label1.Location = new System.Drawing.Point(44, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 75);
            this.label1.TabIndex = 2;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Yes_Button
            // 
            this.Yes_Button.Animated = true;
            this.Yes_Button.AnimationHoverSpeed = 0.07F;
            this.Yes_Button.AnimationSpeed = 0.03F;
            this.Yes_Button.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.Yes_Button.BorderColor = System.Drawing.Color.Black;
            this.Yes_Button.CheckedBaseColor = System.Drawing.Color.Gray;
            this.Yes_Button.CheckedBorderColor = System.Drawing.Color.Black;
            this.Yes_Button.CheckedForeColor = System.Drawing.Color.White;
            this.Yes_Button.CheckedImage = null;
            this.Yes_Button.CheckedLineColor = System.Drawing.Color.DimGray;
            this.Yes_Button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.Yes_Button.FocusedColor = System.Drawing.Color.Empty;
            this.Yes_Button.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Yes_Button.ForeColor = System.Drawing.Color.White;
            this.Yes_Button.Image = null;
            this.Yes_Button.ImageSize = new System.Drawing.Size(20, 20);
            this.Yes_Button.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.Yes_Button.Location = new System.Drawing.Point(136, 165);
            this.Yes_Button.Name = "Yes_Button";
            this.Yes_Button.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.Yes_Button.OnHoverBorderColor = System.Drawing.Color.Black;
            this.Yes_Button.OnHoverForeColor = System.Drawing.Color.White;
            this.Yes_Button.OnHoverImage = null;
            this.Yes_Button.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.Yes_Button.OnPressedColor = System.Drawing.Color.Black;
            this.Yes_Button.Size = new System.Drawing.Size(92, 42);
            this.Yes_Button.TabIndex = 3;
            this.Yes_Button.Text = "Yes";
            this.Yes_Button.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Yes_Button.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.ClearTypeGridFit;
            this.Yes_Button.Visible = false;
            this.Yes_Button.Click += new System.EventHandler(this.Yes_Button_Click);
            // 
            // No_Button
            // 
            this.No_Button.Animated = true;
            this.No_Button.AnimationHoverSpeed = 0.07F;
            this.No_Button.AnimationSpeed = 0.03F;
            this.No_Button.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.No_Button.BorderColor = System.Drawing.Color.Black;
            this.No_Button.CheckedBaseColor = System.Drawing.Color.Gray;
            this.No_Button.CheckedBorderColor = System.Drawing.Color.Black;
            this.No_Button.CheckedForeColor = System.Drawing.Color.White;
            this.No_Button.CheckedImage = null;
            this.No_Button.CheckedLineColor = System.Drawing.Color.DimGray;
            this.No_Button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.No_Button.FocusedColor = System.Drawing.Color.Empty;
            this.No_Button.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.No_Button.ForeColor = System.Drawing.Color.White;
            this.No_Button.Image = null;
            this.No_Button.ImageSize = new System.Drawing.Size(20, 20);
            this.No_Button.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.No_Button.Location = new System.Drawing.Point(283, 165);
            this.No_Button.Name = "No_Button";
            this.No_Button.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.No_Button.OnHoverBorderColor = System.Drawing.Color.Black;
            this.No_Button.OnHoverForeColor = System.Drawing.Color.White;
            this.No_Button.OnHoverImage = null;
            this.No_Button.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.No_Button.OnPressedColor = System.Drawing.Color.Black;
            this.No_Button.Size = new System.Drawing.Size(92, 42);
            this.No_Button.TabIndex = 4;
            this.No_Button.Text = "No";
            this.No_Button.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.No_Button.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.ClearTypeGridFit;
            this.No_Button.Visible = false;
            this.No_Button.Click += new System.EventHandler(this.No_Button_Click);
            // 
            // Ok_Button
            // 
            this.Ok_Button.Animated = true;
            this.Ok_Button.AnimationHoverSpeed = 0.07F;
            this.Ok_Button.AnimationSpeed = 0.03F;
            this.Ok_Button.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.Ok_Button.BorderColor = System.Drawing.Color.Black;
            this.Ok_Button.CheckedBaseColor = System.Drawing.Color.Gray;
            this.Ok_Button.CheckedBorderColor = System.Drawing.Color.Black;
            this.Ok_Button.CheckedForeColor = System.Drawing.Color.White;
            this.Ok_Button.CheckedImage = null;
            this.Ok_Button.CheckedLineColor = System.Drawing.Color.DimGray;
            this.Ok_Button.DialogResult = System.Windows.Forms.DialogResult.None;
            this.Ok_Button.FocusedColor = System.Drawing.Color.Empty;
            this.Ok_Button.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ok_Button.ForeColor = System.Drawing.Color.White;
            this.Ok_Button.Image = null;
            this.Ok_Button.ImageSize = new System.Drawing.Size(20, 20);
            this.Ok_Button.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.Ok_Button.Location = new System.Drawing.Point(211, 165);
            this.Ok_Button.Name = "Ok_Button";
            this.Ok_Button.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.Ok_Button.OnHoverBorderColor = System.Drawing.Color.Black;
            this.Ok_Button.OnHoverForeColor = System.Drawing.Color.White;
            this.Ok_Button.OnHoverImage = null;
            this.Ok_Button.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.Ok_Button.OnPressedColor = System.Drawing.Color.Black;
            this.Ok_Button.Size = new System.Drawing.Size(92, 42);
            this.Ok_Button.TabIndex = 5;
            this.Ok_Button.Text = "Ok";
            this.Ok_Button.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Ok_Button.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.ClearTypeGridFit;
            this.Ok_Button.Visible = false;
            this.Ok_Button.Click += new System.EventHandler(this.Ok_Button_Click);
            // 
            // gunaDragControl1
            // 
            this.gunaDragControl1.TargetControl = this.panel1;
            // 
            // gunaAnimateWindow1
            // 
            this.gunaAnimateWindow1.AnimationType = Guna.UI.WinForms.GunaAnimateWindow.AnimateWindowType.AW_BLEND;
            this.gunaAnimateWindow1.Interval = 100;
            this.gunaAnimateWindow1.TargetControl = this;
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 229);
            this.Controls.Add(this.Ok_Button);
            this.Controls.Add(this.No_Button);
            this.Controls.Add(this.Yes_Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gunaControlBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBox";
            this.Load += new System.EventHandler(this.MessageBox_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaAdvenceButton Yes_Button;
        private Guna.UI.WinForms.GunaAdvenceButton No_Button;
        private Guna.UI.WinForms.GunaAdvenceButton Ok_Button;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaAnimateWindow gunaAnimateWindow1;
    }
}