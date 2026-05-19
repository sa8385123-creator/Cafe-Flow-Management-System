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
    public partial class Dashboard : Form
    {
        
        public Dashboard()
        {
            InitializeComponent();
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
        // open order form
        private void btnOrders_Click(object sender, EventArgs e) 
        {
           Orders om = new Orders();
            om.Show();
        }
        // open product from
        private void btnProducts_Click(object sender, EventArgs e) 
        {
            Products p=new Products();
            p.Show();
        }
        // open customer form
        private void btnCustomers_Click(object sender, EventArgs e) 
        {
            Customers c = new Customers();
            c.Show();
        }
        // open report from
        private void btnReports_Click(object sender, EventArgs e) 
        {
            Reports r=new Reports();
            r.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Display current logged-in user role on dashboard for role-based UI awareness
            lblUser.Text = Session.Role;
            
        }
        // update live date and time at every interval
        private void timer1_Tick(object sender, EventArgs e)  
        {
            lblDateTime.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void picCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btnUsers_Click(object sender, EventArgs e) // open user form
        {
            Users users = new Users();
            users.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
