using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;

namespace Library
{
    public partial class DashBoard : UserControl
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        int[] GetbillNumber()
        {
            string sql = "select count(*) from bill where Status like 'payé'";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            int payed = (int)cmd.ExecuteScalar();
            cmd.CommandText = "select count(*) from bill where Status not like 'payé'";
            int Nonpayed = (int)cmd.ExecuteScalar();
            int[] x = { payed, Nonpayed };
            return x;
        } 

        string clientFidele()
        {
            string sql = @"select Top 1 (nom+' '+prenom) as 'Nom' from Bill
                           inner join client on client.CIN = Bill.client_CIN
                           where client_CIN not like '1'
                           group by(nom+' ' + prenom) 
                           order by COUNT(*) desc";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            return (string)cmd.ExecuteScalar();
        }

        string produitPlusVendu()
        {
            string sql = @"select top 1 (nom+' '+matiere+' '+Nscolaire) from Bill_products
                           inner join produit on produit.id = Bill_products.product_id
                           group by (nom+' '+matiere+' '+Nscolaire)
                           order by count(*) desc";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            return (string)cmd.ExecuteScalar();
        }

        int TotalProduitVendu()
        {
            string sql = "select sum(quantite) from Bill_products inner join produit on produit.id = Bill_products.product_id";

            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            if (cmd.ExecuteScalar() == DBNull.Value)
            {
                return 0;
            }
            return (int)cmd.ExecuteScalar();
        }

        int TotalClient()
        {
            string sql = "select count(*) from client";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            return (int)cmd.ExecuteScalar();

        }

        int getSalesperDay(int x)
        {
            string sql = "SELECT count(*) FROM bill WHERE datesortie = CONVERT(date, DATEADD(day, @day, GETDATE()))";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@day", SqlDbType.Int).Value = x;
            return (int)cmd.ExecuteScalar();
        }

        int getSalesperMonth(int x)
        {
            string sql = "SELECT count(*) FROM bill WHERE month(datesortie) =month( CONVERT(date, DATEADD(month, @month, GETDATE())))";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = x;
            return (int)cmd.ExecuteScalar();
        }

        string getDay(int x)
        {
            DateTime date = DateTime.Now.AddDays(x);
            return date.ToString("MMM") + " " + date.Day;
        }
        string getMonth(int x)
        {
            DateTime date = DateTime.Now.AddMonths(x);
            return date.ToString("MMMM");
        }


        private void DashBoard_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            loadData();
            Program.checkheight(240, guna2DataGridView1);
            
        }
        public void loadDG()
        {
            string sql = "select commande.id as 'N°',Fournisseur.nom as 'Fournisseur',datecommande as 'Date Commande' from commande inner join Fournisseur on commande.Fournisseur_id = Fournisseur.id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
           
        }

        void loadChart(int x)
        {
            if (x==0)
            {
                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Jour",
                    Labels = new[] { getDay(-6), getDay(-5), getDay(-4), getDay(-3), getDay(-2), getDay(-1), getDay(0) }

                });

                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Total des ventes",
                    LabelFormatter = value => value.ToString("0"),



                });
                List<int> values = new List<int>();
                for (int i = -6; i <= 0; i++)
                {
                    values.Add(getSalesperDay(i));
                }
                SeriesCollection series = new SeriesCollection();

                series.Add(new LineSeries() { Values = new ChartValues<int>(values) });
                cartesianChart1.Series = series;
                label1.Text = "Ventes (dernier 7 jour)";

            }
            else
            {
                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Mois",
                    Labels = new[] { getMonth(-2),getMonth(-1),getMonth(0) }

                });

                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Total des ventes",
                    LabelFormatter = value => value.ToString("0"),



                });
                List<int> values = new List<int>();
                for (int i = -2; i <= 0; i++)
                {
                    values.Add(getSalesperMonth(i));
                }
                SeriesCollection series = new SeriesCollection();

                series.Add(new LineSeries() { Values = new ChartValues<int>(values) });
                cartesianChart1.Series = series;
                label1.Text = "Ventes (dernier 3 mois)";


            }

        }
        public void loadData()
        {
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("Par jour");
            guna2ComboBox1.Items.Add("Par Mois");
            guna2ComboBox1.SelectedIndex = 0;
            loadChart(0);
            Loyal_Client_Label.Text = clientFidele();
            Most_sold_product_tb.Text = produitPlusVendu();
            Total_Customers_TB.Text = TotalClient().ToString();
            Total_Sold_Products.Text = TotalProduitVendu().ToString();
            loadPie();
            loadDG();
        }
        void loadPie()
        {
            var x = GetbillNumber();
            int total = x[0] + x[1];
            Paied.Text = x[0].ToString();
            NonPaid.Text = x[1].ToString();
            Paidprogressbar.Maximum = total;
            Nonpaidprogressbar.Maximum = total;
            Paidprogressbar.Value = x[0];
            Nonpaidprogressbar.Value = x[1];
            float percentage_Value = (float)x[0] / total * 100;
            percentage_label.Text = percentage_Value.ToString("0'%'")+" %";


        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadChart(guna2ComboBox1.SelectedIndex);
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            MainForm form = (MainForm)this.ParentForm;
            form.gunaAdvenceButton9.PerformClick();
            
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            MainForm form = (MainForm)this.ParentForm;
            form.gunaAdvenceButton14.PerformClick();
        }
    }
}
