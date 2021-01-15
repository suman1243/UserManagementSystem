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
    public partial class UserRole : TemplateForm
    {
        public UserRole()
        {
            InitializeComponent();
        }

        //properties 
        public int RoleId { get; set; }
        public bool IsUpdate { get; set; }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                if (this.IsUpdate)
                {
                    using (SqlConnection con = new SqlConnection(Connection.GetConnectionStrings()))
                    {
                        using (SqlCommand cmd = new SqlCommand("Proc_Role", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Flag", "Update");
                            cmd.Parameters.AddWithValue("@RoleName", txtRoleName.Text);
                            cmd.Parameters.AddWithValue("@RoleDescription", txtRoleDescription.Text);
                            cmd.Parameters.AddWithValue("@RoleId", this.RoleId);

                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Role Updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ResetForm();
                        }
                    }
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(Connection.GetConnectionStrings()))
                    {
                        using (SqlCommand cmd = new SqlCommand("Proc_Role", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Flag", "Insert");
                            cmd.Parameters.AddWithValue("@RoleName", txtRoleName.Text);
                            cmd.Parameters.AddWithValue("@RoleDescription", txtRoleDescription.Text);

                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Role inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ResetForm();
                        }
                    }
                }
            }
        }

        private void ResetForm()
        {
            txtRoleName.Clear();
            txtRoleDescription.Clear();
            txtRoleName.Focus();

            if (this.IsUpdate)
            {
                this.RoleId = 0;
                this.IsUpdate = false;
                SaveButton.Text = "Save Information";
                DeleteButton.Enabled = false;
            }
        }

        private bool IsValid()
        {
            if (txtRoleName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Role Name should not be empty","Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRoleName.Focus();
                return false;
            }

            if (txtRoleName.Text.Length >=50 )
            {
                MessageBox.Show("Role Name should not be greater than 50 character", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRoleName.Focus();
                return false;
            }
            return true;
        }

        private void UserRole_Load(object sender, EventArgs e)
        {
            if (this.IsUpdate)
            {
                using (SqlConnection con =new SqlConnection(Connection.GetConnectionStrings()))
                {
                    using (SqlCommand cmd=new SqlCommand("Proc_Role",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "GetRoleById");
                        cmd.Parameters.AddWithValue("@RoleId", this.RoleId);
                        if (con.State!=ConnectionState.Open)
                        {
                            con.Open();
                        }

                        DataTable dt = new DataTable();
                        SqlDataReader dr = cmd.ExecuteReader();
                        dt.Load(dr);

                        DataRow row = dt.Rows[0];
                        txtRoleName.Text = row["RoleName"].ToString();
                        txtRoleDescription.Text = row["RoleDescription"].ToString();

                        SaveButton.Text = "Update Role";
                        DeleteButton.Enabled = true;
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            if (this.IsUpdate)
            {
                DialogResult res = MessageBox.Show("DO yu want to Delete", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res==DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Connection.GetConnectionStrings()))
                    {
                        using (SqlCommand cmd = new SqlCommand("Proc_Role", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Flag", "Delete");
                            cmd.Parameters.AddWithValue("@RoleId", this.RoleId);

                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetForm();

                        }
                    }
                }
               
            }
           
        }
    }
}
