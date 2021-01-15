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
        public int UserId { get; set; }
        public bool  IsUpdate { get; set; }
        private void UserForm_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
               

                using (SqlConnection con= new SqlConnection(Connection.GetConnectionStrings()))
                {
                    using (SqlCommand cmd= new SqlCommand("Proc_User",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "GetUserbyId");
                        cmd.Parameters.AddWithValue("@UserId", this.UserId);
                        if (con.State!=ConnectionState.Open)
                        {
                            con.Open();
                        }
                        SqlDataReader da = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(da);

                        DataRow row = dt.Rows[0];

                        txtUserName.Text = row["UserName"].ToString();
                        txtDescription.Text = row["Description"].ToString();
                        //cboRole.SelectedIndex=row[""]
                        SaveButton.Text = "Update Information";
                        DeleteButton.Enabled = true;

                    }
                }
            }
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
                    cmd.Parameters.AddWithValue("@Password", StaticData.EncryptData(txtPassword.Text));
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
                    Resetvalue();
                }
            }
        }

        private void Resetvalue()
        {
            txtUserName.Clear();
            txtDescription.Clear();
            txtPassword.Clear();
            chkIsActive.Checked = true;
            cboRole.SelectedIndex = -1;
        }
    }
}
