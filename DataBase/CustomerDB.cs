using Cafe_Flow.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe_Flow.DataBase
{
    // Handles all database operations related to Customer entity
    // Includes CRUD operations: Add, Read, Update, Delete, and Search
    // Inherits database connection from DBConnection class
    public class CustomerDB : DBConnection
    {
        // Inserts a new customer record into the database using parameterized query
        // Ensures safe data insertion and prevents SQL injection
        public void AddCustomer(CustomerManager c) 
        {
            try
            {
                OpenConnection();

                string query = "INSERT INTO Customers(Name, Phone, Address) VALUES (@name, @phone, @address)";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", c.Name);
                cmd.Parameters.AddWithValue("@phone", c.Phone);
                cmd.Parameters.AddWithValue("@address", c.Address);

                int rows = cmd.ExecuteNonQuery();

                MessageBox.Show("Rows inserted: " + rows);

                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }
        // Retrieves all customer records from database and maps them into CustomerManager objects
        public List<CustomerManager> GetCustomers() 
        {
            List<CustomerManager> customers = new List<CustomerManager>();

            OpenConnection();

            string query = "SELECT * FROM Customers";

            MySqlCommand cmd = new MySqlCommand(query, con);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CustomerManager c = new CustomerManager();

                c.Name = reader["Name"].ToString();
                c.Phone = reader["Phone"].ToString();
                c.Address = reader["Address"].ToString();

                customers.Add(c);
            }

            reader.Close();
            CloseConnection();

            return customers;
        }
        // Updates existing customer record based on unique identifier (Name used as key)
        // Replaces old customer data with updated values
        public void UpdateCustomer(CustomerManager c, string oldName)
        {
            OpenConnection();

            string query = "UPDATE Customers SET Name=@name, Phone=@phone, Address=@address WHERE Name=@oldName";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@name", c.Name);
            cmd.Parameters.AddWithValue("@phone", c.Phone);
            cmd.Parameters.AddWithValue("@address", c.Address);
            cmd.Parameters.AddWithValue("@oldName", oldName);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }
        // Deletes customer record from database based on Name identifier
        // WARNING: Deletion is permanent and cannot be undone
        public void DeleteCustomer(string name)
        {
            OpenConnection();

            string query = "DELETE FROM Customers WHERE Name=@name";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@name", name);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }
        // Searches customer records using partial name matching (LIKE operator)
        // Supports flexible search functionality in UI
        public List<CustomerManager> SearchCustomers(string name)
        {
            List<CustomerManager> customers = new List<CustomerManager>();

            OpenConnection();

            string query = "SELECT * FROM Customers WHERE Name LIKE @name";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", "%" + name + "%");

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CustomerManager c = new CustomerManager();

                c.Name = reader["Name"].ToString();
                c.Phone = reader["Phone"].ToString();
                c.Address = reader["Address"].ToString();

                customers.Add(c);
            }

            reader.Close();
            CloseConnection();

            return customers;

        }
    }
}