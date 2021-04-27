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
    public partial class Log_userC : UserControl
    {
        public Log_userC()
        {
            InitializeComponent();
        }

        private void Log_userC_Load(object sender, EventArgs e)
        {

        }

        public void loadData()
        {
            string sql = "select log_id as 'LOG id',log_description as 'Description',log_date as 'Date execution',log_time as 'temps' from db_log order by log_date desc";
            SqlCommand cmd = new SqlCommand(sql, Program.getcon());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            guna2DataGridView1.DataSource = dt;
            guna2DataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            guna2DataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
    }
}
