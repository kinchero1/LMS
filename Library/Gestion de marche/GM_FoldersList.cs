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

namespace Library.Gestion_de_marche
{
    public partial class GM_FoldersList : UserControl
    {
        public GM_FoldersList()
        {
            InitializeComponent();
        }
        bool FilterDate = false;

        private void GM_FoldersList_Load(object sender, EventArgs e)
        {
            Societe.Items.Add("Bahij");
            Societe.Items.Add("Perle");
            Societe.SelectedIndex = 0;
        }

        public void loadData()
        {
            FilterDate = false;
            string sql = "select  Nmarche as 'N° marche', nomMarch as 'Nom de marche', Dudate as 'Date de debut',addDate as 'Date d''ajout de dossier' from marche";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;


        }

        void filters()
        {
            string sql = "select Nmarche as 'N° marche', nomMarch as 'Nom de marche', Dudate as 'Date de debut',addDate as 'Date d''ajout de dossier' from marche where 1 = 1";
            SqlCommand cmd = new SqlCommand();

            if (N_marche.Text != "")
            {

                sql += "and marche.Nmarche like @id ";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "%" + N_marche.Text + "%";
            }


            if (nom_marche.Text != "")
            {

                sql += "and marche.nomMarch like @nom";
                cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = "%" + nom_marche.Text + "%";
            }

            if (FilterDate)
            {
                sql += " and marche.Dudate=@dateDU";
                sql += " and marche.addDate=@dateADD";
                cmd.Parameters.Add("@dateDU", SqlDbType.DateTime).Value = date_du.Value;
                cmd.Parameters.Add("@dateADD", SqlDbType.DateTime).Value = date_add.Value;
            }


            cmd.CommandText = sql;
            cmd.Connection = Program.getcon();


            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            Program.checkheight(442, guna2DataGridView1, empty_panel);
            label7.Text = guna2DataGridView1.Rows.Count.ToString() + " en Total";


        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            FilterDate = false;
            loadData();
        }

        private void date_add_CloseUp(object sender, EventArgs e)
        {
            FilterDate = true;
            filters();
        }

        private void date_du_CloseUp(object sender, EventArgs e)
        {
            FilterDate = true;
            filters();
        }

        private void N_marche_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void nom_marche_TextChanged(object sender, EventArgs e)
        {
            filters();
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }

            string Nmarch = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (MessageBox.ShowDialog("Vous voulez vraiment supprimer le dossier N°: " + Nmarch, true) == DialogResult.OK)
            {
                string sql = "delete from marcheProduit where Nmarche=@N";

                SqlCommand cmd = new SqlCommand(sql, Program.getcon());

                cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = Nmarch;


                cmd.ExecuteNonQuery();

                cmd.CommandText = "delete from marche where Nmarche=@N";
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    MessageBox.Show("suppresion bien fait");

                }
                loadData();
            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }
            if (Societe.Text == "Bahij")
            {
                string Nmarch = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                string[] FolderInfo = getFolderInformation(Nmarch);
                string sql = "select '', ROW_NUMBER() over (order by id),designation,'',unite,quantite from marcheProduit where Nmarche =@N";

                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = Nmarch;

                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());

                ExcelHelper ex = ExcelHelper.Export(@"devis marche\ste " + Societe.Text + "\\Bon livraison.xlsx", "Bon de livraison");
                if (ex == null)
                {
                    return;
                }
                ex.writecell("D2", " " + Nmarch);
                ex.writecell("B4", FolderInfo[0], true);
                ex.writecell("G2", FolderInfo[1]);
                ex.writecell("G5", FolderInfo[2]);
                ex.writecell("D3", " " + Date_le.Value.ToShortDateString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ex.insertLine(11 + i, false, null, null, false);
                    ex.merge("C" + (11 + i), "D" + (11 + i));
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ex.writecell2(i, j, dt.Rows[i][j].ToString(), 11, 1, false);

                    }

                }
                ex.SaveAndClose();
            }
            else
            {
                string Nmarch = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                string[] FolderInfo = getFolderInformation(Nmarch);
                string sql = "select  ROW_NUMBER() over (order by id),designation,'','',unite,quantite from marcheProduit where Nmarche =@N";

                SqlCommand cmd = new SqlCommand(sql, Program.getcon());
                cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = Nmarch;

                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());

                ExcelHelper ex = ExcelHelper.Export(@"devis marche\ste " + Societe.Text + "\\Bon livraison.xlsx", "Bon de livraison");
                if (ex == null)
                {
                    return;
                }
                ex.writecell("A4", " " + Nmarch);
                ex.writecell("A7", FolderInfo[0], true);
                ex.writecell("F4", FolderInfo[1]);
                ex.writecell("F8", FolderInfo[2]);
                ex.writecell("A6", " " + Date_le.Value.ToShortDateString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    for (int j = 0; j < dt.Columns.Count; j++)
                    {

                        ex.writecell(i, j, dt.Rows[i][j].ToString(), 13, 1, false, true);
                    }

                }
                ex.SaveAndClose();
            }
        }
        string[] getFolderInformation(string id)
        {
            string[] array = null;
            string sql = "select objet,client,adresse from marche where Nmarche=@n";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@n", SqlDbType.VarChar).Value = id;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                array = new string[] { dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString() };
            }
            dr.Close();
            return array;
        }

        void GeneralExport(string filetype, string ste, string Numero)
        {
            float montantBrut = 0;
            float montantTVA7 = 0;
            float montantTVA14 = 0;
            float montantTVA20 = 0;
            float montantTTC = 0;
            string filePath = "";
            int tvaIndex = 0;
            string newModelSqlQuery = "select '', ROW_NUMBER() over (order by id),designation,'',unite,quantite,prix,(prix*quantite),tva  from marcheProduit where Nmarche=@N";
            string oldModelSqlQuery = "select ROW_NUMBER() over (order by id),designation,'','',unite,quantite,prix,(prix*quantite),tva  from marcheProduit where Nmarche=@N";
            string sql = (ste == "Bahij") ? newModelSqlQuery : oldModelSqlQuery;
            string Nmarch = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            string[] FolderInfo = getFolderInformation(Nmarch);
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = Numero;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            tvaIndex = ste == "Bahij" ? 8 : 8;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                montantBrut += float.Parse(dt.Rows[i][6].ToString());
                if (dt.Rows[i][tvaIndex].ToString() == "7")
                {
                    montantTVA7 += Program.percentage(float.Parse(dt.Rows[i][7].ToString()), 7);
                }
                else if (dt.Rows[i][tvaIndex].ToString() == "14")
                {
                    montantTVA14 += Program.percentage(float.Parse(dt.Rows[i][7].ToString()), 14);
                }
                else if (dt.Rows[i][tvaIndex].ToString() == "20")
                {
                    montantTVA20 += Program.percentage(float.Parse(dt.Rows[i][7].ToString()), 20);
                }
            }

            montantTTC = montantBrut + montantTVA7 + montantTVA14 + montantTVA20;

            if (ste == "Bahij")
            {
                if (montantTVA14 > 0 || montantTVA7 > 0)
                {
                    filePath = @"devis marche\ste " + ste + "\\" + filetype + " 7 14 20.xlsx";
                    ExcelHelper ex = ExcelHelper.Export(filePath, filetype + " " + Societe.Text);
                    if (ex == null)
                    {
                        return;
                    }
                    ex.writecell("H12", montantBrut.ToString("N"));
                    ex.writecell("H13", montantTVA20.ToString("N"));
                    ex.writecell("H14", montantTVA14.ToString("N"));
                    ex.writecell("H15", montantTVA7.ToString("N"));
                    ex.writecell("H16", montantTTC.ToString("N"));
                    ex.writecell("D2", " " + Nmarch);
                    ex.writecell("B4", FolderInfo[0], true);
                    ex.writecell("G2", FolderInfo[1]);
                    ex.writecell("G5", FolderInfo[2]);
                    ex.writecell("D3", " " + Date_le.Value.ToShortDateString());
                    Num2Letter_Helper num2letter = Num2Letter_Helper.getValue("https://number-to-letter-micro-service.kinchero1.repl.co", montantTTC.ToString());
                    if (num2letter != null)
                    {
                        ex.writecell("B19", num2letter.value + " dirham et " + num2letter.Decimal + " centimes");
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ex.insertLine(11 + i, false, null, null, false);
                        ex.merge("C" + (11 + i), "D" + (11 + i));
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (j == 5 || j == 6)
                            {
                                ex.writecell2(i, j, float.Parse(dt.Rows[i][j].ToString()).ToString("N"), 11, 1, true);
                            }
                            else if (j == 7)
                            {
                                ex.writecell2(i, j, float.Parse(dt.Rows[i][j].ToString()) + " %", 11, 1, false);
                            }
                            else
                            {
                                ex.writecell2(i, j, dt.Rows[i][j].ToString(), 11, 1, false);
                            }

                        }

                    }
                    ex.SaveAndClose();
                }
                else
                {
                    filePath = @"devis marche\ste " + ste + "\\" + filetype + " 20.xlsx";
                    ExcelHelper ex = ExcelHelper.Export(filePath, " " + filetype + " " + Societe.Text);
                    if (ex == null)
                    {
                        return;
                    }
                    ex.writecell("H12", montantBrut.ToString("N"));
                    ex.writecell("H13", montantTVA20.ToString("N"));
                    ex.writecell("H14", montantTTC.ToString("N"));
                    ex.writecell("D2", " " + Nmarch);
                    ex.writecell("B4", FolderInfo[0], true);
                    ex.writecell("G2", FolderInfo[1]);
                    ex.writecell("G5", FolderInfo[2]);
                    ex.writecell("D3", " " + Date_le.Value.ToShortDateString());
                    Num2Letter_Helper num2letter = Num2Letter_Helper.getValue("https://number-to-letter-micro-service.kinchero1.repl.co", montantTTC.ToString());
                    if (num2letter != null)
                    {
                        ex.writecell("B17", num2letter.value + " dirham et " + num2letter.Decimal + " centimes");
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ex.insertLine(11 + i, false, null, null, false);
                        ex.merge("C" + (11 + i), "D" + (11 + i));
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (j == 5 || j == 6)
                            {
                                ex.writecell(i, j, float.Parse(dt.Rows[i][j].ToString()).ToString("N"), 11, 1, true);
                            }
                            else if (j == 7)
                            {
                                ex.writecell(i, j, float.Parse(dt.Rows[i][j].ToString()) + " %", 11, 1, false);
                            }
                            else
                            {
                                ex.writecell(i, j, dt.Rows[i][j].ToString(), 11, 1, false);
                            }

                        }

                    }
                    ex.SaveAndClose();

                }
            }
            else
            {
                if (montantTVA14 > 0 || montantTVA7 > 0)
                {
                    filePath = @"devis marche\ste " + Societe.Text + "\\" + filetype + " 7 14 20.xlsx";
                    ExcelHelper ex = ExcelHelper.Export(filePath, " " + filetype + " " + Societe.Text);
                    if (ex == null)
                    {
                        return;
                    }
                    ex.writecell("E13", montantBrut.ToString("N"));
                    ex.writecell("G13", montantTVA20.ToString("N"));
                    ex.writecell("H13", montantTVA14.ToString("N"));
                    ex.writecell("I13", montantTVA7.ToString("N"));
                    ex.writecell("H16", montantTTC.ToString("N"));
                    ex.writecell("A3", " " + Nmarch);
                    ex.writecell("A6", FolderInfo[0], true);
                    ex.writecell("F3", FolderInfo[1]);
                    ex.writecell("F5", FolderInfo[2]);
                    ex.writecell("A5", " " + Date_le.Value.ToShortDateString());
                    Num2Letter_Helper num2letter = Num2Letter_Helper.getValue("https://number-to-letter-micro-service.kinchero1.repl.co", montantTTC.ToString());
                    if (num2letter != null)
                    {
                        ex.writecell("A16", num2letter.value + " dirham et" + num2letter.Decimal + " centimes");
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ex.insertLine(11 + i);
                        ex.merge("B" + (11 + i), "D" + (11 + i));
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (j == 6 || j == 7)
                            {
                                ex.writecell(i, j, float.Parse(dt.Rows[i][j].ToString()).ToString("N"), 11, 1, false, false, true);
                            }
                            else if (j == 8)
                            {
                                ex.writecell(i, j, float.Parse(dt.Rows[i][j].ToString()) + " %", 11, 1, false);
                            }
                            else
                            {
                                ex.writecell(i, j, dt.Rows[i][j].ToString(), 11, 1, false);
                            }

                        }

                    }
                    ex.SaveAndClose();
                }
                else
                {
                    filePath = @"devis marche\ste " + ste + "\\" + filetype + " 20.xlsx";
                    ExcelHelper ex = ExcelHelper.Export(filePath, " " + filetype + " " + Societe.Text);
                    if (ex == null)
                    {
                        return;
                    }

                    ex.writecell("G13", montantBrut.ToString("N"));
                    ex.writecell("I13", montantTVA20.ToString("N"));

                    ex.writecell("H16", montantTTC.ToString("N"));
                    ex.writecell("A3", " " + Nmarch);
                    ex.writecell("A6", FolderInfo[0], true);
                    ex.writecell("F3", FolderInfo[1]);
                    ex.writecell("F5", FolderInfo[2]);
                    ex.writecell("A5", " " + Date_le.Value.ToShortDateString());
                    Num2Letter_Helper num2letter = Num2Letter_Helper.getValue("https://number-to-letter-micro-service.kinchero1.repl.co", montantTTC.ToString());
                    if (num2letter != null)
                    {
                        ex.writecell("A16", num2letter.value + " dirham  et" + num2letter.Decimal + " centimes");
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ex.insertLine(11 + i);
                        ex.merge("B" + (11 + i), "D" + (11 + i));
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {

                            if (j == 6 || j == 7)
                            {
                                ex.writecell(i, j, float.Parse(dt.Rows[i][j].ToString()).ToString("N"), 11, 1, false, false, true);
                            }
                            else if (j == 8)
                            {
                                ex.writecell(i, j, float.Parse(dt.Rows[i][j].ToString()) + " %", 11, 1, false);
                            }
                            else
                            {
                                ex.writecell(i, j, dt.Rows[i][j].ToString(), 11, 1, false);
                            }
                        }

                    }
                    ex.SaveAndClose();

                }
            }


        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }
            string Nmarche = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            GeneralExport("facture", Societe.Text, Nmarche);
            MessageBox.Show("Exportation bien fait");
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            if(guna2DataGridView1.Rows.Count <= 0)
            {
                return;
            }

            string Nmarche = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            GeneralExport("devis", Societe.Text, Nmarche);
            MessageBox.Show("Exportation bien fait");
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            MainForm x = (MainForm)this.ParentForm;
            string id = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            x.gM_Edit_folder1.loadData(id);
            x.gM_Edit_folder1.Tag = id;
            x.gM_Edit_folder1.BringToFront();
        }

        private void guna2DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }
    }
}
