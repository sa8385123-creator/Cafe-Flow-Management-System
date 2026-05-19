using Cafe_Flow.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Flow.DataBase
{
    public class OrderDB : DBConnection
    {
        // insert order into DB
        public void AddOrder(OrderManager o) 
        {
            OpenConnection();

            string query = "INSERT INTO Orders(CustomerName, Product, Quantity, Total) VALUES (@cust, @prod, @qty, @total)";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@cust", o.CustomerName);
            cmd.Parameters.AddWithValue("@prod", o.Product);
            cmd.Parameters.AddWithValue("@qty", o.Quantity);
            cmd.Parameters.AddWithValue("@total", o.Total);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }
        // retrieve records from DB
        public List<OrderManager> GetOrders() 
        {
            List<OrderManager> orders = new List<OrderManager>();

            OpenConnection();

            string query = "SELECT * FROM Orders";

            MySqlCommand cmd = new MySqlCommand(query, con);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // read record one by one
            {
                OrderManager o = new OrderManager();

                o.CustomerName = reader["CustomerName"].ToString();
                o.Product = reader["Product"].ToString();
                o.Quantity = Convert.ToInt32(reader["Quantity"]);
                o.Total = Convert.ToDecimal(reader["Total"]);

                orders.Add(o);
            }

            reader.Close();
            CloseConnection();

            return orders;
        }
        // retrieve orders of specific customer and return them as list
        public List<OrderManager> GetCustomerOrders(string customerName)  
        {
            List<OrderManager> orders = new List<OrderManager>();

            OpenConnection();

            string query = "SELECT * FROM Orders WHERE CustomerName=@name";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", customerName);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                OrderManager o = new OrderManager();

                o.CustomerName = reader["CustomerName"].ToString();
                o.Product = reader["Product"].ToString();
                o.Quantity = Convert.ToInt32(reader["Quantity"]);
                o.Total = Convert.ToDecimal(reader["Total"]);

                orders.Add(o);
            }

            reader.Close();
            CloseConnection();

            return orders;
        }
    }
}
