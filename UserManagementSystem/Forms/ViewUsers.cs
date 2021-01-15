using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManagementSystem.General;

namespace UserManagementSystem.Forms
{
    public partial class ViewUsers : TemplateForm
    {
        public ViewUsers()
        {
            InitializeComponent();
            LoadUsergrid();
        }

        private void LoadUsergrid()
        {
            using (SqlConnection con=new SqlConnection(Connection.GetConnectionStrings()))
            {
                using (SqlCommand cmd= new SqlCommand("Proc_User",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "GetAllUser");
                    if (con.State!=ConnectionState.Open)
                    {
                        con.Open();
                    }
                    DataTable dt = new DataTable();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    UserGridView.DataSource = dt;
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
