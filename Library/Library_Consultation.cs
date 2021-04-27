using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Newtonsoft.Json;


namespace Library
{
    public partial class Library_Consultation : UserControl
    {
        public static DataTable GlobalDatatable;
        public Library_Consultation()
        {
            InitializeComponent();
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {



        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {


            /* jsonstorage_net_helper Bin_io = new jsonstorage_net_helper();

             var response = Bin_io.Read(Properties.Settings.Default.DBperle);
             guna2TextBox1.Text += response.Content.ReadAsStringAsync().Result;*/
            guna2DataGridView1.DataSource = getData();
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {

        }

        public static DataTable getData()
        {
            try
            {
                jsonstorage_net_helper Bin_io = new jsonstorage_net_helper();
                var response = Bin_io.Read(Properties.Settings.Default.Library_name == "bahij" ? Properties.Settings.Default.DBperle : Properties.Settings.Default.DBbahij);
                if (response == null)
                {
                    return null;
                }
                var content = response.Content.ReadAsStringAsync().Result;

                ProductRoot product = JsonConvert.DeserializeObject<ProductRoot>(content);
                DataTable dt = new DataTable();

                dt.Columns.Add("Id");
                dt.Columns.Add("Nom");
                dt.Columns.Add("Categorie");
                dt.Columns.Add("Matiere");
                dt.Columns.Add("Nscolaire");
                dt.Columns.Add("Prix");
                dt.Columns.Add("Stock");
                dt.Columns.Add("Valeur");


                for (int i = 0; i < product.productList.Count; i++)
                {
                    dt.Rows.Add(product.productList[i].id, product.productList[i].nom, product.productList[i].categorie, product.productList[i].matiere, product.productList[i].Nscolaire, product.productList[i].prix, product.productList[i].stock, product.productList[i].valeur);
                }

                return dt;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        static bool insertData()
        {
            try
            {
                ProductRoot productR = new ProductRoot();
                productR.productList = setUpdata();
                productR.lastUpdateDate = DateTime.Now.ToShortDateString();
                var json = JsonConvert.SerializeObject(productR);

                jsonstorage_net_helper Bin_io = new jsonstorage_net_helper();

                var response = Bin_io.Update(Properties.Settings.Default.Library_name == "bahij" ? Properties.Settings.Default.DBbahij : Properties.Settings.Default.DBperle, json);
                if (response==null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e )
            {
                Console.WriteLine(e);
                return false;

            }
        }

        static List<Product> setUpdata()
        {

            List<Product> productlist = new List<Product>();
            string sql = "select id,nom,categorie,matiere,Nscolaire,prix,qte,valeur from produit";
            SqlConnection con = new SqlConnection(Program.conS);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int id = int.Parse(dr.GetValue(0).ToString());
                string nom = dr.GetValue(1).ToString();
                string categorie = dr.GetValue(2).ToString();
                string matiere = dr.GetValue(3).ToString();
                string Nscolaire = dr.GetValue(4).ToString();
                float prix = float.Parse(dr.GetValue(5).ToString());
                int stock = int.Parse(dr.GetValue(6).ToString());
                float valeur = float.Parse(dr.GetValue(7).ToString());
                var product = new Product(id, nom, categorie, matiere, Nscolaire, prix, stock, valeur);
                productlist.Add(product);
            }
            dr.Close();
            con.Close();
            return productlist;

        }

        private void Library_Consultation_Load(object sender, EventArgs e)
        {

        }

        public static bool loadData()
        {
            bool datainsert = insertData();
            GlobalDatatable = getData();
            return (datainsert && GlobalDatatable != null);

        }
        public void setupData()
        {
            guna2DataGridView1.DataSource = GlobalDatatable;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            label7.Text = guna2DataGridView1.Rows.Count + " en Totale";
        }

        void filter()
        {
            DataView dv = new DataView(GlobalDatatable);
            if (Designation.Text != "")
            {
                dv.RowFilter = "Nom like '%" + Designation.Text + "%'";
            }
            if (Categorie.Text != "")
            {
                dv.RowFilter = "Categorie like '%" + Categorie.Text + "%'";
            }
            if (Matiere.Text != "")
            {
                dv.RowFilter = "Matiere like '%" + Matiere.Text + "%'";
            }
            if (Nscolaire.Text != "")
            {
                dv.RowFilter = "Nscolaire like '%" + Nscolaire.Text + "%'";
            }

            guna2DataGridView1.DataSource = dv;


        }

        private void CIN_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void Matiere_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void Nscolaire_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void Categorie_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            visualKeyboard1.ShowKeyboard(Designation, new Point(187, 43));
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            visualKeyboard1.ShowKeyboard(Categorie, new Point(369, 47));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            visualKeyboard1.ShowKeyboard(Matiere, new Point(558, 48));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            visualKeyboard1.ShowKeyboard(Nscolaire, new Point(623, 75));
        }
    }
}
