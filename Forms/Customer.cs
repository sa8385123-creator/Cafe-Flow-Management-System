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
    public partial class Customers : Form
    {
       
        public Customers()
        {
            InitializeComponent();
        }
        // Loads all customer records from database and binds them to DataGridView
        // Used after Add, Update, Delete operations to refresh UI state
        void LoadGrid()
        {
            CustomerDB db = new CustomerDB();

            var data = db.GetCustomers();
            // Reset DataGridView binding to ensure fresh data reload
            dgvCustomers.AutoGenerateColumns = true;
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = data;

            dgvCustomers.Refresh();
        }
        void ClearFields()
        {
            txtCustomerName.Clear();
            txtphone.Clear();
            txtAddress.Clear();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate that no input field is empty before inserting record
            if (txtCustomerName.Text.Trim() == "" ||
                txtphone.Text.Trim() == "" ||
                txtAddress.Text.Trim() == "")
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            CustomerManager c = new CustomerManager();

            c.Name = txtCustomerName.Text;
            c.Phone = txtphone.Text;
            c.Address = txtAddress.Text;

            CustomerDB db = new CustomerDB();
            db.AddCustomer(c);

            LoadGrid();
            ClearFields();
            MessageBox.Show("Customer Added Successfully");
        }
        // When user clicks a row, load selected customer data into input fields
        // This enables Update/Delete operations on selected record
        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCustomerName.Text = dgvCustomers.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtphone.Text = dgvCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAddress.Text = dgvCustomers.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "" || txtphone.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Plz click on any row on data grid view to update record","Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                CustomerManager c = new CustomerManager();

                c.Name = txtCustomerName.Text;
                c.Phone = txtphone.Text;
                c.Address = txtAddress.Text;

                CustomerDB db = new CustomerDB();

                db.UpdateCustomer(c, dgvCustomers.CurrentRow.Cells[0].Value.ToString());

                LoadGrid();
                ClearFields();
                MessageBox.Show("Customer Updated Successfully");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "" || txtphone.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Plz click on any row on data grid view to delete record","Missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                CustomerDB db = new CustomerDB();

                db.DeleteCustomer(dgvCustomers.CurrentRow.Cells[0].Value.ToString());

                LoadGrid();
                ClearFields();
                MessageBox.Show("Customer Deleted Successfully");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CustomerDB db = new CustomerDB();
            // Perform search operation and display matching customer records in DataGridView
            dgvCustomers.DataSource = db.SearchCustomers(txtSearch.Text); 
        }
        // Restrict access to Customers form based on user role
        // Only Admin users are allowed to access this form
        private void Customers_Load(object sender, EventArgs e)
        {
            if (Session.Role != "Admin")
            {
                MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                this.Close();
                this.Dispose();
            }
            LoadGrid();
        }
    }
}
