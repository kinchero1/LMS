using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace Library
{
    public partial class Product_List_Form : Form
    {
        MainForm MainForm;
        Add_Bill Bill;
        Edit_Product_Delivery_Form editDelform;
        choose_history Choose;
        int index;
        //index 0 commande
        //index 1 dossier de marche
        //index 2 barcode printer
        //index 3 del form
        //index 4 payment
        //index 5 edit folder
       // index 6 edit del form
       //index 7 achat history
        public Product_List_Form(MainForm form, int x = 0, Add_Bill bill = null,Edit_Product_Delivery_Form edit = null,choose_history choose=null)
        {
            InitializeComponent();
            MainForm = form;
            index = x;
            Bill = bill;
            editDelform = edit;
            Choose = choose;
            if (index == 5)
            {
                gunaSwitch1.Enabled = false;
            }
            else
            {
                gunaSwitch1.Enabled = true;
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Product_List_Form_Load(object sender, EventArgs e)
        {

            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
            cat.Items.Add("Tout");
            matiere.Items.Add("Tout");
            Ns.Items.Add("Tout");
            readfile("cat.txt", cat);
            readfile("mat.txt", matiere);
            readfile("Ns.txt", Ns);
            cat.SelectedIndex = 0;
            matiere.SelectedIndex = 0;
            Ns.SelectedIndex = 0;
          
        }

        /*   void loadData()
           {
               string sql = "select id as 'ID',(nom+' '+matiere+' '+Nscolaire) as 'Designation' from produit";
               SqlCommand cmd = new SqlCommand(sql, Program.getcon());
               DataTable dt = new DataTable();
               dt.Load(cmd.ExecuteReader());
               guna2DataGridView1.DataSource = dt;
               guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



           }

           void loadGeneratedProducts()
           {
               string sql = "select id as 'ID',(nom+' '+matiere+' '+Nscolaire) as 'Designation' from produit where codebarStatus like generated";
               SqlCommand cmd = new SqlCommand(sql, Program.getcon());
               DataTable dt = new DataTable();
               dt.Load(cmd.ExecuteReader());
               guna2DataGridView1.DataSource = dt;
               guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



           }*/

        public string getProductPrice(int i)
        {
            string sql = "select prix from produit where id = @id";

            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Float).Value = i;
            return cmd.ExecuteScalar().ToString();

        }

        public string getProductbarcode(int i)
        {
            string sql = "select codebar from produit where id = @id";

            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@id", SqlDbType.Float).Value = i;
            return cmd.ExecuteScalar().ToString();

        }

        bool Skip = false;

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                Guna.UI2.WinForms.Guna2DataGridView OrderDataGrid = MainForm.add_Order1.guna2DataGridView1;

                if (gunaSwitch1.Checked)
                {
                    for (int i = 0; i < guna2DataGridView1.SelectedRows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[0].Tag.ToString() == id.ToString())
                            {
                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            int row = OrderDataGrid.Rows.Add(OrderDataGrid.RowCount + 1, designation, MainForm.add_Order1.Unite_TB.Text, int.Parse(MainForm.add_Order1.Quantite.Text));
                            OrderDataGrid.Rows[row].Cells[0].Tag = id;

                        }

                    }


                }
                else
                {
                    for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[0].Tag.ToString() == id.ToString())
                            {
                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            int row = OrderDataGrid.Rows.Add(OrderDataGrid.RowCount + 1, designation, MainForm.add_Order1.Unite_TB.Text, int.Parse(MainForm.add_Order1.Quantite.Text));
                            OrderDataGrid.Rows[row].Cells[0].Tag = id;

                        }

                    }

                }

            }
            else if (index == 1)
            {
                Guna.UI2.WinForms.Guna2DataGridView OrderDataGrid = MainForm.gM_third_page1.guna2DataGridView1;
                string unite = MainForm.gM_third_page1.Unite_TB.Text;
                string quantite = MainForm.gM_third_page1.Quantite.Text;
                string TVA = MainForm.gM_third_page1.TVA.Text;
                if (gunaSwitch1.Checked)
                {
                    for (int i = 0; i < guna2DataGridView1.SelectedRows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[0].Tag.ToString() == id.ToString())
                            {
                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            int row = OrderDataGrid.Rows.Add(designation, unite, "x " + quantite, getProductPrice(id).ToString() + " DH", TVA);
                            OrderDataGrid.Rows[row].Cells[0].Tag = id;

                        }

                    }


                }
                else
                {
                    for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[0].Tag.ToString() == id.ToString())
                            {
                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            int row = OrderDataGrid.Rows.Add(designation, unite, "x " + quantite, getProductPrice(id).ToString() + " DH", TVA);
                            OrderDataGrid.Rows[row].Cells[0].Tag = id;

                        }

                    }

                }
                Program.main.gM_third_page1.calculte();
            }
            else if (index == 2)
            {
                Guna.UI2.WinForms.Guna2DataGridView OrderDataGrid = MainForm.print_BarCodes1.guna2DataGridView1;

                if (gunaSwitch1.Checked)
                {
                    for (int i = 0; i < guna2DataGridView1.SelectedRows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[0].Tag.ToString() == id.ToString())
                            {
                                OrderDataGrid.Rows[i].Cells[3].Value = int.Parse(OrderDataGrid.Rows[i].Cells[3].Value.ToString()) + 1;

                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            int row = OrderDataGrid.Rows.Add(id, designation, getProductbarcode(id), MainForm.print_BarCodes1.Quanite_tb.Text);
                            OrderDataGrid.Rows[row].Cells[0].Tag = id;

                        }

                    }
                    MainForm.print_BarCodes1.calculateQuAndLoss();


                }
                else
                {
                    for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[0].Tag.ToString() == id.ToString())
                            {
                                OrderDataGrid.Rows[i].Cells[3].Value = int.Parse(OrderDataGrid.Rows[i].Cells[3].Value.ToString()) + 1;

                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            int row = OrderDataGrid.Rows.Add(id, designation, getProductbarcode(id), MainForm.print_BarCodes1.Quanite_tb.Text);
                            OrderDataGrid.Rows[row].Cells[0].Tag = id;

                        }

                    }
                    MainForm.print_BarCodes1.calculateQuAndLoss();


                }

            }
            else if (index == 3)
            {
                Guna.UI2.WinForms.Guna2DataGridView OrderDataGrid = MainForm.Delivery_Form1.guna2DataGridView1;
                if (Program.CheckEmpty(MainForm.Delivery_Form1.unite_TB) || Program.CheckEmpty(MainForm.Delivery_Form1.Quantite_TB) || Program.CheckEmpty(MainForm.Delivery_Form1.prix_TB) || Program.CheckEmpty(MainForm.Delivery_Form1.Remise_TB))
                {
                    return;
                }
                if (gunaSwitch1.Checked)
                {
                    for (int i = 0; i < guna2DataGridView1.SelectedRows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[0].Tag.ToString() == id.ToString())
                            {
                                OrderDataGrid.Rows[i].Cells[2].Value = int.Parse(OrderDataGrid.Rows[i].Cells[2].Value.ToString().Replace("x", "")) + int.Parse(MainForm.Delivery_Form1.Quantite_TB.Text);

                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            int row = OrderDataGrid.Rows.Add(designation, MainForm.Delivery_Form1.unite_TB.Text, int.Parse(MainForm.Delivery_Form1.Quantite_TB.Text), double.Parse(MainForm.Delivery_Form1.prix_TB.Text), int.Parse(MainForm.Delivery_Form1.Remise_TB.Text), int.Parse(MainForm.Delivery_Form1.TVA_TB.Text.Replace("%", "")));
                            OrderDataGrid.Rows[row].Cells[0].Tag = id;

                        }

                    }
                    OrderDataGrid.Parent.Visible = true;
                    OrderDataGrid.ColumnHeadersVisible = true;

                    Program.checkheight(392, OrderDataGrid, MainForm.Delivery_Form1.empty_panel);
                    MainForm.Delivery_Form1.calculte();
                    OrderDataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


                }
                else
                {
                    for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[0].Tag.ToString() == id.ToString())
                            {
                                guna2DataGridView1.Rows[i].Cells[2].Value = int.Parse(guna2DataGridView1.Rows[i].Cells[2].Value.ToString().Replace("x", "")) + int.Parse(MainForm.Delivery_Form1.Quantite_TB.Text);

                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            int row = OrderDataGrid.Rows.Add(designation, MainForm.Delivery_Form1.unite_TB.Text, int.Parse( MainForm.Delivery_Form1.Quantite_TB.Text), double.Parse(MainForm.Delivery_Form1.prix_TB.Text),int.Parse( MainForm.Delivery_Form1.Remise_TB.Text ),int.Parse( MainForm.Delivery_Form1.TVA_TB.Text.Replace("%","")));
                            OrderDataGrid.Rows[row].Cells[0].Tag = id;

                        }

                    }
                    OrderDataGrid.Parent.Visible = true;
                    OrderDataGrid.ColumnHeadersVisible = true;

                    Program.checkheight(392, OrderDataGrid, MainForm.Delivery_Form1.empty_panel);
                    MainForm.Delivery_Form1.calculte();
                    OrderDataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }

            }
            else if (index == 4)
            {
                if (Program.CheckEmpty(Bill.Unite_TB) || Program.CheckEmpty(Bill.Quantite) || Program.CheckEmpty(Bill.Remise_TB))
                {
                    return;
                }
                Guna.UI2.WinForms.Guna2DataGridView OrderDataGrid = Bill.guna2DataGridView1;

                if (gunaSwitch1.Checked)
                {
                    for (int i = 0; i < guna2DataGridView1.SelectedRows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[1].Tag.ToString() == id.ToString())
                            {
                                int qte = 1 + int.Parse(OrderDataGrid.Rows[i].Cells[3].Value.ToString().Replace("x", " "));

                                OrderDataGrid.Rows[i].Cells[3].Value = "x" + qte;
                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            Bill.AddRow(OrderDataGrid, id.ToString(), designation);

                        }

                    }


                }
                else
                {
                    for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[1].Tag.ToString() == id.ToString())
                            {
                                int qte = 1 + int.Parse(OrderDataGrid.Rows[i].Cells[3].Value.ToString().Replace("x", " "));
                                OrderDataGrid.Rows[i].Cells[3].Value = "x" + qte;

                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            Bill.AddRow(OrderDataGrid, id.ToString(), designation);

                        }

                    }

                }
            }
            else if (index == 5)
            {
                Guna.UI2.WinForms.Guna2DataGridView OrderDataGrid = MainForm.gM_Edit_folder1.guna2DataGridView1;
                string unite = MainForm.gM_third_page1.Unite_TB.Text;
                string quantite = MainForm.gM_third_page1.Quantite.Text;
                string TVA = MainForm.gM_third_page1.TVA.Text;
                if (gunaSwitch1.Checked)
                {
                    for (int i = 0; i < guna2DataGridView1.SelectedRows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[6].Value.ToString() == id.ToString())
                            {
                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                            MainForm.gM_Edit_folder1.insertProductToMarcheDB(MainForm.gM_Edit_folder1.Tag.ToString(), designation, unite, int.Parse(quantite), float.Parse(getProductPrice(id)) , int.Parse( TVA.Replace("%","")),id);
                           

                        }

                    }
                    MainForm.gM_Edit_folder1.loadData(MainForm.gM_Edit_folder1.Tag.ToString());

                }
                else
                {
                    for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                    {
                        Skip = false;
                        int id = int.Parse(guna2DataGridView1.Rows[i].Cells[0].Value.ToString());
                        string designation = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
                        for (int j = 0; j < OrderDataGrid.Rows.Count; j++)
                        {
                            if (OrderDataGrid.Rows[j].Cells[6].Value.ToString() == id.ToString())
                            {
                                Skip = true;
                            }

                        }

                        if (Skip == false)
                        {
                          /*  int row = OrderDataGrid.Rows.Add(0, designation, unite, "x " + quantite, getProductPrice(id).ToString() + " DH", TVA, id);*/


                        }

                    }

                }
                Program.main.gM_third_page1.calculte();
            }
        }


        void readfile(string name, Control ctrl)
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LMS\\" + name);

            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                ((Guna.UI2.WinForms.Guna2ComboBox)ctrl).Items.Add(line);
            }
        }
        void filters()
        {
            string sql = "select id as'hidden',productid as 'ID',(nom+' '+matiere+' '+Nscolaire) as 'Designation' from produit where 1 = 1";
            SqlCommand cmd = new SqlCommand();


            if (produit_id.Text != "")
            {
                sql += "and productid like @pId ";
                cmd.Parameters.Add("@pId", SqlDbType.VarChar).Value = "%" + produit_id.Text + "%";


            }

            if (nom.Text != "")
            {
                sql += "and nom like @nom ";
                cmd.Parameters.Add("@nom", SqlDbType.NVarChar).Value = "%" + nom.Text + "%";


            }

            if (cat.SelectedIndex > 0)
            {
                sql += "and categorie like @cat ";
                cmd.Parameters.Add("@cat", SqlDbType.VarChar).Value = cat.Text;

            }
            if (matiere.SelectedIndex > 0)
            {

                sql += "and matiere like @matiere ";
                cmd.Parameters.Add("@matiere", SqlDbType.NVarChar).Value = matiere.Text;
            }


            if (Ns.SelectedIndex > 0)
            {
                sql += "and Nscolaire like @Ns ";
                cmd.Parameters.Add("@Ns", SqlDbType.NVarChar).Value = Ns.Text.Trim();

            }

            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();


            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;

            guna2DataGridView1.Columns[0].Visible = false;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nom_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();

        }

        private void matiere_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void Ns_SelectedIndexChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            visualKeyboard1.Parent = this;
            visualKeyboard1.BringToFront();
            visualKeyboard1.ShowKeyboard(nom, new Point(343, 97));
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void produit_id_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (index == 6)
            {
                editDelform.Designation.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.Hide();
            }else if(index == 7)
            {
                Choose.showPerProduct(int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                this.Hide();
            }
        }

        private void Product_List_Form_Shown(object sender, EventArgs e)
        {
         
            
        }

        
    }
}
