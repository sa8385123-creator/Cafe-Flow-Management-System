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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Restrict Reports access to Admin users only for security reasons
        // Prevent unauthorized access to business analytics and sales data
        private void Reports_Load(object sender, EventArgs e)
        {
            if (Session.Role != "Admin")
            {
                MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                this.Close();
                this.Dispose();
            }
            LoadReports();
        }
        // Generates system-wide business reports by calculating:
        // - Total products in inventory
        // - Total orders placed
        // - Total registered customers
        // - Total sales revenue using LINQ aggregation
        void LoadReports()
        {
            ProductDB pdb = new ProductDB();
            lblTotalProducts.Text = pdb.GetProducts().Count.ToString();
            OrderDB odb = new OrderDB();
           lblTOrder.Text = odb.GetOrders().Count.ToString();

            CustomerDB db = new CustomerDB();
            lblTotalCustomers.Text = db.GetCustomers().Count.ToString();
            // Calculate total revenue generated from all orders using LINQ Sum
            // Aggregates Order.Total values to compute overall sales
            decimal totalSales = odb.GetOrders().Sum(o => o.Total); 

            lblTotalSales.Text = totalSales.ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReports();
        }
        // Export system report to a text file using SaveFileDialog
        // Includes summary of products, orders, customers, and total sales
        private void button1_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog(); // open save dialog for selecting file location
            sfd.Filter = "Text File (*.txt)|*.txt";   // stores only txt file
            sfd.FileName = "CafeReport.txt";

            if (sfd.ShowDialog() == DialogResult.OK) // if user click on save
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("CAFE REPORT");
                sb.AppendLine("----------------------");

                sb.AppendLine("Total Products: " + lblTotalProducts.Text);
                sb.AppendLine("Total Orders: " + lblTotalOrders.Text);
                sb.AppendLine("Total Customers: " + lblTotalCustomers.Text);
                sb.AppendLine("Total Sales: " + lblTotalSales.Text);

                System.IO.File.WriteAllText(sfd.FileName, sb.ToString()); // create and write report data into text file

                MessageBox.Show("Report exported successfully!");
            }
        }
    }
    }

