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
    public partial class ViewRoles : TemplateForm
    {
        public ViewRoles()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            using (SqlConnection con = new SqlConnection(Connection.GetConnectionStrings()))
            {
                using (SqlCommand cmd = new SqlCommand("Proc_Role", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "GetAllRole");

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    DataTable dt = new DataTable();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;

                }
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (txtSearchRoleName.Text.Trim()!=string.Empty)
            {
                using (SqlConnection con = new SqlConnection(Connection.GetConnectionStrings()))
                {
                    using (SqlCommand cmd = new SqlCommand("Proc_Role", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "GetRoleByRoleName");
                        cmd.Parameters.AddWithValue("@RoleName", txtSearchRoleName.Text);
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        DataTable dt = new DataTable();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No matching Record Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                     

                    }
                }
            }
            else
            {
                MessageBox.Show("Role Name does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void addNewRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserRole role = new UserRole();

            role.ShowDialog();
            LoadDataGrid();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                UserRole role = new UserRole();
                role.RoleId = id;
                role.IsUpdate = true;

                role.ShowDialog();

                LoadDataGrid();
            }
        }
    }
}
