using Cafe_Flow.DataBase;
using Cafe_Flow.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe_Flow.Forms
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void Users_Load(object sender, EventArgs e)
        {
            if (Session.Role != "Admin")
            {
                MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                this.Close();
            }
        }
        // Create new system user with secure password hashing
        // Ensures passwords are NOT stored in plain text using BCrypt encryption
        private void btnAddUser_Click(object sender, EventArgs e) // add new user
        {
            if (txtUsername.Text.Trim() == "" ||
       txtPassword.Text.Trim() == "" ||
       cmbRole.Text == "")
            {
                MessageBox.Show("Fill all fields");
                return;
            }

            User u = new User();

            u.Username = txtUsername.Text;
            // Securely hash password using BCrypt before storing in database
            // Prevents storage of plain text passwords and improves security
            u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);  
            u.Role = cmbRole.Text;

            UserDB db = new UserDB();

            db.AddUser(u);

            MessageBox.Show("User Added Successfully");

            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
        }
    }
}
