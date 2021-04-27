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

namespace Library
{
    public partial class Payment_box : Form
    {
        float montant;
        float montant_reste;
        int Id;
        ViewEdit_Bill editForm;
        public Payment_box(int id,float montant_total,float montantApaye,ViewEdit_Bill edit)
        {
            InitializeComponent();
            if (this.DesignMode)
            {
                return;
            }
            Montant_Total_label.Text = montant_total.ToString("0.00 DH");
            Montant_reste_label.Text = montantApaye.ToString("0.00 DH");
            montant = montantApaye;
            Id = id;
            editForm = edit;
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }

        private void Payment_box_Load(object sender, EventArgs e)
        {

        }

        private void montant_paye_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.onlyDegit(e,true);

        }

        private void montant_paye_TextChanged(object sender, EventArgs e)
        {
            
            if (montant_paye_TB.Text.IndexOf('.')!= montant_paye_TB.Text.LastIndexOf('.') || String.IsNullOrEmpty(montant_paye_TB.Text))
            {
                montant_retourne_label.Text = "0 DH";
                Montant_reste_label.Text = montant.ToString("0.00 DH");
                return;
            }
            float montant_paye = float.Parse(montant_paye_TB.Text);
            montant_reste = montant_paye >= montant ? 0 : montant - montant_paye;
            float montant_retourne = montant_paye - montant;

            Montant_reste_label.Text = montant_reste.ToString("0.00 DH");
            montant_retourne_label.Text = montant_retourne.ToString("0.00 DH");
         
        }
        void updateBill(int id,string status,float montantReste)
        {
            string sql = "update Bill set status=@status,montantRest=@Mr where id=@id";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
            cmd.Parameters.Add("@Mr", SqlDbType.Float).Value = montantReste;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
        }
        private void Yes_Button_Click(object sender, EventArgs e)
        {
            string status = montant_reste == 0 ? "Payé" : "Non Payé";
            updateBill(Id, status, montant_reste);
            editForm.loadData(Id);
            this.Hide();
        }
    }
}
