using Cafe_Flow.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Flow.DataBase
{
    // Handles all database operations related to Product entity
    // Includes CRUD operations (Add, Read, Update, Delete) and Search functionality
    // Inherits database connection handling from DBConnection class
    public class ProductDB : DBConnection
    {
        // Inserts a new product record into the database
        // Uses parameterized query to prevent SQL injection and ensure data security
        // Requires product name, category, and price from ProductManager object
        public void AddProduct(ProductManager p)
        {
            OpenConnection();

            string query = "INSERT INTO Products(Name, Category, Price) VALUES (@name, @cat, @price)";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@cat", p.Category);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        // Retrieves all product records from the database
        // Converts each database row into ProductManager object
        // Returns a list of products for display in UI (DataGridView)
        public List<ProductManager> GetProducts()
        {
            List<ProductManager> products = new List<ProductManager>();

            OpenConnection();

            string query = "SELECT * FROM Products";

            MySqlCommand cmd = new MySqlCommand(query, con);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ProductManager p = new ProductManager();

                p.Name = reader["Name"].ToString();
                p.Category = reader["Category"].ToString();
                p.Price = Convert.ToDecimal(reader["Price"]);

                products.Add(p);
            }

            reader.Close();
            CloseConnection();

            return products;
        }
        // Updates an existing product record in the database
        // Uses product name (oldName) as identifier to locate record
        // Replaces old values with updated product details
        public void UpdateProduct(ProductManager p, string oldName)
        {
            OpenConnection();

            string query = "UPDATE Products SET Name=@name, Category=@cat, Price=@price WHERE Name=@oldName";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@cat", p.Category);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@oldName", oldName);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }
        // Deletes a product record from the database based on product name
        // Permanently removes the selected product from system
        public void DeleteProduct(string name)
        {
            OpenConnection();

            string query = "DELETE FROM Products WHERE Name=@name";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@name", name);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }
        // Searches products using partial name matching (LIKE operator)
        // Allows flexible searching using keyword input
        // Returns list of matching product records
        public List<ProductManager> SearchProducts(string name)
        {
            List<ProductManager> list = new List<ProductManager>();

            OpenConnection();

            string query = "SELECT * FROM Products WHERE Name LIKE @name";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", "%" + name + "%");

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ProductManager p = new ProductManager();

                p.Name = reader["Name"].ToString();
                p.Category = reader["Category"].ToString();
                p.Price = Convert.ToDecimal(reader["Price"]);

                list.Add(p);
            }

            reader.Close();
            CloseConnection();

            return list;
        }
    }
}
