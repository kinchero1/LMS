namespace Library
{
    partial class Entry_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Entry_form));
            this.LabelName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Societe = new Guna.UI2.WinForms.Guna2ComboBox();
            this.Ok_Button = new Guna.UI.WinForms.GunaAdvenceButton();
            this.SuspendLayout();
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.LabelName.Location = new System.Drawing.Point(82, 24);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(439, 40);
            this.LabelName.TabIndex = 2;
            this.LabelName.Text = "Bienvenu dans votre application";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(111)))), ((int)(((byte)(122)))));
            this.label4.Location = new System.Drawing.Point(86, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 17);
            this.label4.TabIndex = 129;
            this.label4.Text = "Nom de la bibliothèque";
            // 
            // Societe
            // 
            this.Societe.Animated = true;
            this.Societe.BackColor = System.Drawing.Color.Transparent;
            this.Societe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Societe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Societe.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(194)))), ((int)(((byte)(232)))));
            this.Societe.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(194)))), ((int)(((byte)(232)))));
            this.Societe.FocusedState.Parent = this.Societe;
            this.Societe.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Societe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.Societe.FormattingEnabled = true;
            this.Societe.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(194)))), ((int)(((byte)(232)))));
            this.Societe.HoverState.Parent = this.Societe;
            this.Societe.ItemHeight = 30;
            this.Societe.ItemsAppearance.Parent = this.Societe;
            this.Societe.Location = new System.Drawing.Point(429, 104);
            this.Societe.Name = "Societe";
            this.Societe.ShadowDecoration.Parent = this.Societe;
            this.Societe.Size = new System.Drawing.Size(92, 36);
            this.Societe.TabIndex = 130;
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
            this.Ok_Button.Location = new System.Drawing.Point(89, 170);
            this.Ok_Button.Name = "Ok_Button";
            this.Ok_Button.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.Ok_Button.OnHoverBorderColor = System.Drawing.Color.Black;
            this.Ok_Button.OnHoverForeColor = System.Drawing.Color.White;
            this.Ok_Button.OnHoverImage = null;
            this.Ok_Button.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.Ok_Button.OnPressedColor = System.Drawing.Color.Black;
            this.Ok_Button.Size = new System.Drawing.Size(92, 42);
            this.Ok_Button.TabIndex = 131;
            this.Ok_Button.Text = "Confirmer";
            this.Ok_Button.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Ok_Button.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.ClearTypeGridFit;
            this.Ok_Button.Click += new System.EventHandler(this.Ok_Button_Click);
            // 
            // Entry_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 236);
            this.Controls.Add(this.Ok_Button);
            this.Controls.Add(this.Societe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LabelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Entry_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entry_form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Entry_form_FormClosed);
            this.Load += new System.EventHandler(this.Entry_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2ComboBox Societe;
        private Guna.UI.WinForms.GunaAdvenceButton Ok_Button;
    }
}