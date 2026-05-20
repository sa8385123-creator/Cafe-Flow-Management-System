using Cafe_Flow.DataBase;
using Cafe_Flow.Model;
using Cafe_Flow.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe_Flow
{
    public partial class Login : Form
    {
        // Constructor initialize login From controls
        public Login() 
        {
            InitializeComponent();
        }
        // close the entire application
        private void btnExit_Click(object sender, EventArgs e) 
        {
            Application.Exit();
        }
        // Authenticate user and open dashboard
        private void btnLogin_Click(object sender, EventArgs e)  
        {
            // database object for user operations
            UserDB db = new UserDB();
            // Retrieve user data
            User user = db.GetUser(txtUsername.Text); 

            if (user != null)
            {
                // verify password using BCrypt hashing
                bool isValid = BCrypt.Net.BCrypt.Verify  
                (
                    txtPassword.Text,
                    user.PasswordHash
                );

                if (isValid)
                {
                    // Store logged-in user information globally for access across forms
                    Session.Username = user.Username;
                    Session.Role = user.Role;

                    MessageBox.Show("Login Successful", "Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    Dashboard d = new Dashboard(); // open dashboard
                    d.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Password", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("User not found");
                }

            }
    }
}
