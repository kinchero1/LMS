namespace Library
{
    partial class Payment_box
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Montant_Total_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Montant_reste_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.montant_paye_TB = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.montant_retourne_label = new System.Windows.Forms.Label();
            this.Yes_Button = new Guna.UI.WinForms.GunaAdvenceButton();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 10);
            this.panel1.TabIndex = 2;
            // 
            // gunaControlBox1
            // 
            this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox1.AnimationSpeed = 0.03F;
            this.gunaControlBox1.IconColor = System.Drawing.Color.Black;
            this.gunaControlBox1.IconSize = 15F;
            this.gunaControlBox1.Location = new System.Drawing.Point(417, 16);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(33, 29);
            this.gunaControlBox1.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.label11.Location = new System.Drawing.Point(53, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 21);
            this.label11.TabIndex = 95;
            this.label11.Text = "Montant Total :";
            // 
            // Montant_Total_label
            // 
            this.Montant_Total_label.AutoSize = true;
            this.Montant_Total_label.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Montant_Total_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Montant_Total_label.Location = new System.Drawing.Point(220, 47);
            this.Montant_Total_label.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Montant_Total_label.Name = "Montant_Total_label";
            this.Montant_Total_label.Size = new System.Drawing.Size(17, 20);
            this.Montant_Total_label.TabIndex = 96;
            this.Montant_Total_label.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.label1.Location = new System.Drawing.Point(53, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 97;
            this.label1.Text = "Montant reste:";
            // 
            // Montant_reste_label
            // 
            this.Montant_reste_label.AutoSize = true;
            this.Montant_reste_label.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Montant_reste_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Montant_reste_label.Location = new System.Drawing.Point(220, 92);
            this.Montant_reste_label.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Montant_reste_label.Name = "Montant_reste_label";
            this.Montant_reste_label.Size = new System.Drawing.Size(17, 20);
            this.Montant_reste_label.TabIndex = 98;
            this.Montant_reste_label.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.label3.Location = new System.Drawing.Point(53, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 21);
            this.label3.TabIndex = 99;
            this.label3.Text = "Paiment :";
            // 
            // montant_paye_TB
            // 
            this.montant_paye_TB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.montant_paye_TB.DefaultText = "";
            this.montant_paye_TB.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.montant_paye_TB.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.montant_paye_TB.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.montant_paye_TB.DisabledState.Parent = this.montant_paye_TB;
            this.montant_paye_TB.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.montant_paye_TB.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.montant_paye_TB.FocusedState.Parent = this.montant_paye_TB;
            this.montant_paye_TB.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.montant_paye_TB.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.montant_paye_TB.HoverState.Parent = this.montant_paye_TB;
            this.montant_paye_TB.Location = new System.Drawing.Point(224, 182);
            this.montant_paye_TB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.montant_paye_TB.Name = "montant_paye_TB";
            this.montant_paye_TB.PasswordChar = '\0';
            this.montant_paye_TB.PlaceholderText = "";
            this.montant_paye_TB.SelectedText = "";
            this.montant_paye_TB.ShadowDecoration.Parent = this.montant_paye_TB;
            this.montant_paye_TB.Size = new System.Drawing.Size(200, 33);
            this.montant_paye_TB.TabIndex = 101;
            this.montant_paye_TB.TextChanged += new System.EventHandler(this.montant_paye_TextChanged);
            this.montant_paye_TB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.montant_paye_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.label4.Location = new System.Drawing.Point(53, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 21);
            this.label4.TabIndex = 120;
            this.label4.Text = "Montant routourné :";
            // 
            // montant_retourne_label
            // 
            this.montant_retourne_label.AutoSize = true;
            this.montant_retourne_label.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.montant_retourne_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.montant_retourne_label.Location = new System.Drawing.Point(220, 134);
            this.montant_retourne_label.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.montant_retourne_label.Name = "montant_retourne_label";
            this.montant_retourne_label.Size = new System.Drawing.Size(17, 20);
            this.montant_retourne_label.TabIndex = 121;
            this.montant_retourne_label.Text = "0";
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
            this.Yes_Button.Location = new System.Drawing.Point(57, 235);
            this.Yes_Button.Name = "Yes_Button";
            this.Yes_Button.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.Yes_Button.OnHoverBorderColor = System.Drawing.Color.Black;
            this.Yes_Button.OnHoverForeColor = System.Drawing.Color.White;
            this.Yes_Button.OnHoverImage = null;
            this.Yes_Button.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.Yes_Button.OnPressedColor = System.Drawing.Color.Black;
            this.Yes_Button.Size = new System.Drawing.Size(92, 35);
            this.Yes_Button.TabIndex = 122;
            this.Yes_Button.Text = "Confirmer";
            this.Yes_Button.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Yes_Button.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.ClearTypeGridFit;
            this.Yes_Button.Click += new System.EventHandler(this.Yes_Button_Click);
            // 
            // Payment_box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(242)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(462, 291);
            this.Controls.Add(this.Yes_Button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.montant_retourne_label);
            this.Controls.Add(this.montant_paye_TB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Montant_reste_label);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Montant_Total_label);
            this.Controls.Add(this.gunaControlBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Payment_box";
            this.Text = "Payment_box";
            this.Load += new System.EventHandler(this.Payment_box_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label Montant_Total_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Montant_reste_label;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox montant_paye_TB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label montant_retourne_label;
        private Guna.UI.WinForms.GunaAdvenceButton Yes_Button;
    }
}