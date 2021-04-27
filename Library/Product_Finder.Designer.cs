namespace Library
{
    partial class Product_Finder
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
            this.LabelName = new System.Windows.Forms.Label();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.label2 = new System.Windows.Forms.Label();
            this.Prefixe = new Guna.UI2.WinForms.Guna2TextBox();
            this.gunaAdvenceButton3 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.CodeBar = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.BarCode_view = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Emplacement = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Quantite = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Prix = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Ns = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Matire = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cat = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Nom = new System.Windows.Forms.Label();
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 10);
            this.panel1.TabIndex = 2;
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.LabelName.Location = new System.Drawing.Point(12, 23);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(142, 20);
            this.LabelName.TabIndex = 8;
            this.LabelName.Text = "Cherche un produit";
            // 
            // gunaControlBox1
            // 
            this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox1.AnimationSpeed = 0.03F;
            this.gunaControlBox1.IconColor = System.Drawing.Color.Black;
            this.gunaControlBox1.IconSize = 15F;
            this.gunaControlBox1.Location = new System.Drawing.Point(755, 23);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(33, 29);
            this.gunaControlBox1.TabIndex = 7;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(16, 53);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(772, 10);
            this.guna2Separator1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.label2.Location = new System.Drawing.Point(251, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "BarCode";
            // 
            // Prefixe
            // 
            this.Prefixe.Animated = true;
            this.Prefixe.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Prefixe.DefaultText = "STE";
            this.Prefixe.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Prefixe.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Prefixe.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Prefixe.DisabledState.Parent = this.Prefixe;
            this.Prefixe.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Prefixe.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Prefixe.FocusedState.Parent = this.Prefixe;
            this.Prefixe.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prefixe.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Prefixe.HoverState.Parent = this.Prefixe;
            this.Prefixe.Location = new System.Drawing.Point(321, 75);
            this.Prefixe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Prefixe.Name = "Prefixe";
            this.Prefixe.PasswordChar = '\0';
            this.Prefixe.PlaceholderText = "";
            this.Prefixe.ReadOnly = true;
            this.Prefixe.SelectedText = "";
            this.Prefixe.ShadowDecoration.Parent = this.Prefixe;
            this.Prefixe.Size = new System.Drawing.Size(146, 36);
            this.Prefixe.TabIndex = 15;
            // 
            // gunaAdvenceButton3
            // 
            this.gunaAdvenceButton3.Animated = true;
            this.gunaAdvenceButton3.AnimationHoverSpeed = 0.07F;
            this.gunaAdvenceButton3.AnimationSpeed = 0.03F;
            this.gunaAdvenceButton3.BackColor = System.Drawing.Color.Transparent;
            this.gunaAdvenceButton3.BaseColor = System.Drawing.Color.White;
            this.gunaAdvenceButton3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(99)))), ((int)(((byte)(198)))));
            this.gunaAdvenceButton3.BorderSize = 2;
            this.gunaAdvenceButton3.ButtonType = Guna.UI.WinForms.AdvenceButtonType.ToogleButton;
            this.gunaAdvenceButton3.CheckedBaseColor = System.Drawing.Color.White;
            this.gunaAdvenceButton3.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(61)))), ((int)(((byte)(0)))));
            this.gunaAdvenceButton3.CheckedForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton3.CheckedImage = global::Library.Properties.Resources.icons8_barcode_reader_30px_2;
            this.gunaAdvenceButton3.CheckedLineColor = System.Drawing.Color.DimGray;
            this.gunaAdvenceButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gunaAdvenceButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaAdvenceButton3.FocusedColor = System.Drawing.Color.Empty;
            this.gunaAdvenceButton3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaAdvenceButton3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(99)))), ((int)(((byte)(198)))));
            this.gunaAdvenceButton3.Image = global::Library.Properties.Resources.icons8_barcode_reader_30px;
            this.gunaAdvenceButton3.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaAdvenceButton3.ImageOffsetX = 2;
            this.gunaAdvenceButton3.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaAdvenceButton3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.gunaAdvenceButton3.Location = new System.Drawing.Point(474, 74);
            this.gunaAdvenceButton3.Name = "gunaAdvenceButton3";
            this.gunaAdvenceButton3.OnHoverBaseColor = System.Drawing.Color.White;
            this.gunaAdvenceButton3.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(61)))), ((int)(((byte)(0)))));
            this.gunaAdvenceButton3.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton3.OnHoverImage = global::Library.Properties.Resources.icons8_barcode_reader_30px_2;
            this.gunaAdvenceButton3.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.gunaAdvenceButton3.OnPressedColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton3.Radius = 4;
            this.gunaAdvenceButton3.Size = new System.Drawing.Size(37, 37);
            this.gunaAdvenceButton3.TabIndex = 83;
            this.gunaAdvenceButton3.TextOffsetX = 2;
            this.gunaAdvenceButton3.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.ClearTypeGridFit;
            this.gunaAdvenceButton3.Click += new System.EventHandler(this.gunaAdvenceButton3_Click);
            // 
            // CodeBar
            // 
            this.CodeBar.Animated = true;
            this.CodeBar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CodeBar.DefaultText = "";
            this.CodeBar.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.CodeBar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CodeBar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.CodeBar.DisabledState.Parent = this.CodeBar;
            this.CodeBar.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.CodeBar.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CodeBar.FocusedState.Parent = this.CodeBar;
            this.CodeBar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeBar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CodeBar.HoverState.Parent = this.CodeBar;
            this.CodeBar.Location = new System.Drawing.Point(357, 75);
            this.CodeBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CodeBar.Name = "CodeBar";
            this.CodeBar.PasswordChar = '\0';
            this.CodeBar.PlaceholderText = "";
            this.CodeBar.SelectedText = "";
            this.CodeBar.ShadowDecoration.Parent = this.CodeBar;
            this.CodeBar.Size = new System.Drawing.Size(110, 36);
            this.CodeBar.TabIndex = 84;
            this.CodeBar.TextChanged += new System.EventHandler(this.CodeBar_TextChanged);
            this.CodeBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeBar_KeyDown);
            this.CodeBar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CodeBar_KeyPress);
            this.CodeBar.Leave += new System.EventHandler(this.CodeBar_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.BarCode_view);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Emplacement);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.Quantite);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.Prix);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.Ns);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.Matire);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Cat);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.Nom);
            this.panel2.Location = new System.Drawing.Point(16, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(772, 304);
            this.panel2.TabIndex = 85;
            this.panel2.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label5.Location = new System.Drawing.Point(495, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 99;
            this.label5.Text = "Code bar :";
            // 
            // BarCode_view
            // 
            this.BarCode_view.AutoSize = true;
            this.BarCode_view.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BarCode_view.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.BarCode_view.Location = new System.Drawing.Point(647, 89);
            this.BarCode_view.Name = "BarCode_view";
            this.BarCode_view.Size = new System.Drawing.Size(59, 17);
            this.BarCode_view.TabIndex = 100;
            this.BarCode_view.Text = "BarCode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label1.Location = new System.Drawing.Point(495, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 97;
            this.label1.Text = "Emplacement :";
            // 
            // Emplacement
            // 
            this.Emplacement.AutoSize = true;
            this.Emplacement.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Emplacement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Emplacement.Location = new System.Drawing.Point(647, 37);
            this.Emplacement.Name = "Emplacement";
            this.Emplacement.Size = new System.Drawing.Size(59, 17);
            this.Emplacement.TabIndex = 98;
            this.Emplacement.Text = "BarCode";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label12.Location = new System.Drawing.Point(41, 248);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 20);
            this.label12.TabIndex = 95;
            this.label12.Text = "Quantite :";
            // 
            // Quantite
            // 
            this.Quantite.AutoSize = true;
            this.Quantite.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Quantite.Location = new System.Drawing.Point(193, 251);
            this.Quantite.Name = "Quantite";
            this.Quantite.Size = new System.Drawing.Size(59, 17);
            this.Quantite.TabIndex = 96;
            this.Quantite.Text = "BarCode";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label10.Location = new System.Drawing.Point(41, 209);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 20);
            this.label10.TabIndex = 93;
            this.label10.Text = "Prix :";
            // 
            // Prix
            // 
            this.Prix.AutoSize = true;
            this.Prix.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Prix.Location = new System.Drawing.Point(193, 212);
            this.Prix.Name = "Prix";
            this.Prix.Size = new System.Drawing.Size(59, 17);
            this.Prix.TabIndex = 94;
            this.Prix.Text = "BarCode";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label8.Location = new System.Drawing.Point(41, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 20);
            this.label8.TabIndex = 91;
            this.label8.Text = "Niveau Scolaire :";
            // 
            // Ns
            // 
            this.Ns.AutoSize = true;
            this.Ns.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Ns.Location = new System.Drawing.Point(193, 171);
            this.Ns.Name = "Ns";
            this.Ns.Size = new System.Drawing.Size(59, 17);
            this.Ns.TabIndex = 92;
            this.Ns.Text = "BarCode";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label6.Location = new System.Drawing.Point(41, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 20);
            this.label6.TabIndex = 89;
            this.label6.Text = "Matiere :";
            // 
            // Matire
            // 
            this.Matire.AutoSize = true;
            this.Matire.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Matire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Matire.Location = new System.Drawing.Point(193, 128);
            this.Matire.Name = "Matire";
            this.Matire.Size = new System.Drawing.Size(59, 17);
            this.Matire.TabIndex = 90;
            this.Matire.Text = "BarCode";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label4.Location = new System.Drawing.Point(41, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.TabIndex = 87;
            this.label4.Text = "Categorie :";
            // 
            // Cat
            // 
            this.Cat.AutoSize = true;
            this.Cat.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Cat.Location = new System.Drawing.Point(193, 86);
            this.Cat.Name = "Cat";
            this.Cat.Size = new System.Drawing.Size(59, 17);
            this.Cat.TabIndex = 88;
            this.Cat.Text = "BarCode";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.label3.Location = new System.Drawing.Point(43, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 86;
            this.label3.Text = "Nom :";
            // 
            // Nom
            // 
            this.Nom.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(118)))), ((int)(((byte)(143)))));
            this.Nom.Location = new System.Drawing.Point(195, 34);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(294, 45);
            this.Nom.TabIndex = 86;
            this.Nom.Text = "BarCode";
            // 
            // gunaDragControl1
            // 
            this.gunaDragControl1.TargetControl = this.panel1;
            // 
            // Product_Finder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(242)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.CodeBar);
            this.Controls.Add(this.gunaAdvenceButton3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Prefixe);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.gunaControlBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Product_Finder";
            this.Text = "Product_Finder";
            this.Load += new System.EventHandler(this.Product_Finder_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabelName;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox Prefixe;
        private Guna.UI.WinForms.GunaAdvenceButton gunaAdvenceButton3;
        private Guna.UI2.WinForms.Guna2TextBox CodeBar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Nom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Cat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label Prix;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Ns;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Matire;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label Quantite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Emplacement;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label BarCode_view;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
    }
}