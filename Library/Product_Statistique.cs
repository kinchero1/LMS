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

namespace Library
{
    public partial class Product_Statistique : UserControl
    {
        public Product_Statistique()
        {
            InitializeComponent();
        }

        private void Product_Statistique_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();
        }
        float TotaldeVente()
        {
            string sql = "select sum(Bill.montant) from Bill where Bill.datesortie=CONVERT(date, getdate())";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            if (cmd.ExecuteScalar() == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return float.Parse(cmd.ExecuteScalar().ToString());
            }
        }

        int TotaldeFactureEffectue()
        {
            string sql = "select COUNT(*) from Bill where datesortie=CONVERT(date, getdate())";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            return (int)cmd.ExecuteScalar();
        }

        string matierePlusVendu()
        {
            string sql = @"select top 1 matiere from  produit
                          inner join Bill_products on product_id = id
                          group by matiere
                          order by count(*) desc";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            if (cmd.ExecuteScalar() == null)
            {
                return "introuvable";
            }
            else
            {
                return (string)cmd.ExecuteScalar();
            }
           

        }

        string NsPlusVendu()
        {
            string sql = @"select top 1 Nscolaire from  produit
                          inner join Bill_products on product_id = id
                          group by Nscolaire
                          order by count(*) desc";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
         
            if (cmd.ExecuteScalar() == null)
            {
                return "introuvable";
            }
            else
            {
                return cmd.ExecuteScalar().ToString();
            }


        }

        void loadDG(int qte)
        {
            string sql = "select id,(nom +' ' +matiere+ ' '+Nscolaire) as 'Nom',categorie as 'Categorie',prix as 'Prix',qte as 'Stock',emplacement as 'Emplacement' from produit where qte<=@qte";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@qte", SqlDbType.Int).Value = qte;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

        }

        public void loadData()
        {
            total_de_vente.Text = TotaldeVente().ToString("0.00 DH");
            total_de_facture.Text = TotaldeFactureEffectue().ToString();
            matiere_plus_vendu.Text = matierePlusVendu().ToString();
            N_scolaire_plus_vendu.Text = NsPlusVendu().ToString();
            loadDG(Properties.Settings.Default.epuise);

        }


    }
}
