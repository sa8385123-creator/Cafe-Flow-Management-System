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
    public partial class Products : Form
    {
       // public static List<ProductManager> productList = new List<ProductManager>();
        public Products()
        {
            InitializeComponent();
        }
        // Loads all product records from database and binds them to DataGridView
        // Called after Add, Update, and Delete operations to refresh UI
        void LoadGrid()
        {
            ProductDB db = new ProductDB();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = db.GetProducts();
        }
        // Validate all required product fields before inserting into database
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text == "" || cmbCategory.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Please fill all Fields", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ProductManager p = new ProductManager();

                p.Name = txtProductName.Text;
                p.Category = cmbCategory.Text;
                p.Price = Convert.ToDecimal(txtPrice.Text);
               ProductDB dB = new ProductDB();
                dB.AddProduct(p);
                LoadGrid();
                ClearFields();
                MessageBox.Show("Product Added Successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // Load selected product row data into input fields for Update/Delete operations
        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtProductName.Text = dgvProducts.Rows[e.RowIndex].Cells[0].Value.ToString();
                cmbCategory.Text = dgvProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPrice.Text = dgvProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text == "" || cmbCategory.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Plz click on any row of Data grid view for update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ProductManager p = new ProductManager();

                p.Name = txtProductName.Text;
                p.Category = cmbCategory.Text;
                p.Price = Convert.ToDecimal(txtPrice.Text);

                ProductDB db = new ProductDB();

                db.UpdateProduct(p, dgvProducts.CurrentRow.Cells[0].Value.ToString());

                LoadGrid();
                ClearFields();
                MessageBox.Show("Product Updated Successfully");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text == "" || cmbCategory.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Plz click on any row of Data grid view for deletion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            { 
            ProductDB db = new ProductDB();

            db.DeleteProduct(dgvProducts.CurrentRow.Cells[0].Value.ToString());

            LoadGrid();
            ClearFields();
            MessageBox.Show("Product Deleted Successfully");
        }
        }
        void ClearFields()
        {
            txtProductName.Clear();
            cmbCategory.SelectedIndex = -1;
            txtPrice.Clear();
        }
        // Perform product search based on user input and display matching results in DataGridView
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text=="")
            {
                MessageBox.Show("Plz write in textbox for searching","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                ProductDB db = new ProductDB();
                dgvProducts.DataSource = db.SearchProducts(txtSearch.Text);
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            // Restrict access to Products form based on user role
            // Only Admin users are authorized to manage product inventory
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
