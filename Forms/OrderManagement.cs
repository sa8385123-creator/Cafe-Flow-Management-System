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
    public partial class Orders : Form
    {
        
        public Orders()
        {
            InitializeComponent();
        }
        // Loads order data into DataGridView based on current user role
        // If user is Customer → shows only their orders
        // If user is Admin → shows all system orders
        void LoadGrid(OrderDB db)  
        {
            // Automatically assign logged-in username to customer field
            txtCustomerName.Text = Session.Username;
            if (Session.Role == "Customer")
            {
                dgvOrders.DataSource = db.GetCustomerOrders(Session.Username);
            }
            else
            {
                dgvOrders.DataSource = db.GetOrders();
            }
        }
        private void Orders_Load(object sender, EventArgs e)
        {
           
            OrderDB db = new OrderDB();
            ProductDB dba = new ProductDB();
            // Load available products into ComboBox for order selection
            // Initialize order grid based on logged-in user role
            cmbProduct.DataSource = dba.GetProducts(); 
            cmbProduct.DisplayMember = "Name";

            LoadGrid(db);
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex != -1)
            {
                ProductManager p = (ProductManager)cmbProduct.SelectedItem; // get selected object from combobox 
                // Calculate total price dynamically when quantity changes
                // Formula: Product Price × Quantity
                txtTotal.Text = (p.Price * numQuantity.Value).ToString();
            }
            else
            { 
                MessageBox.Show("You have not yet selected any product ");
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() == "" || cmbProduct.Text == "" || numQuantity.Value <= 0 || txtTotal.Text=="")
            {
                MessageBox.Show("Plz fill all fields for order");
                return;
            }
            else
            {
                OrderManager o = new OrderManager();
                o.CustomerName = Session.Username;
                o.Product = cmbProduct.Text;
                o.Quantity = (int)numQuantity.Value;
                o.Total = Convert.ToDecimal(txtTotal.Text);
                OrderDB db = new OrderDB();
                db.AddOrder(o);
                LoadGrid(db);
                MessageBox.Show("Order Added Successfully");
            }
        }
       
        private void btnOrderClear_Click(object sender, EventArgs e)  // clear the fields
        {
            txtCustomerName.Clear();
            cmbProduct.SelectedIndex = 0;
            numQuantity.Value = 1;
            txtTotal.Clear();
        }
    }
}
