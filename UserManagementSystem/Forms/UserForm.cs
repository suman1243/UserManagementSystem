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
    public partial class UserForm : TemplateForm
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

            DeleteButton.Enabled = false;
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            using (SqlConnection con=new SqlConnection(Connection.GetConnectionStrings()))
            {
                using (SqlCommand cmd= new SqlCommand("Proc_User",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "GetUserRole");
                    if (con.State!=ConnectionState.Open)
                    {
                        con.Open();
                    }

                    DataTable dt = new DataTable();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    cboRole.DataSource = dt;
                    cboRole.DisplayMember = "RoleName";
                    cboRole.ValueMember = "RoleId";

                        
                }
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con=new SqlConnection(Connection.GetConnectionStrings()))
            {
                using (SqlCommand cmd= new SqlCommand("Proc_User",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Insert");
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@RoleId", cboRole.SelectedValue);
                    cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", "Admin");

                    if (con.State!=ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
