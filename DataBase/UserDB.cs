using Cafe_Flow.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Flow.DataBase
{
    public class UserDB : DBConnection
    {
        // retrieve user details from databse using username
        public User GetUser(string username) 
        {
            OpenConnection();

            string query = "SELECT * FROM Users WHERE Username=@username";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@username", username); // parameterized to prevent SQL injection

            MySqlDataReader reader = cmd.ExecuteReader();

            User u = null;

            if (reader.Read())
            {
                u = new User();

                u.Username = reader["Username"].ToString();
                u.PasswordHash = reader["PasswordHash"].ToString();
                u.Role = reader["Role"].ToString();
            }

            reader.Close();
            CloseConnection();

            return u;
        }
        public void AddUser(User u) // insert user into database
        {
            OpenConnection();

            string query = "INSERT INTO Users(Username, PasswordHash, Role) VALUES(@username,@password,@role)";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@username", u.Username);
            cmd.Parameters.AddWithValue("@password", u.PasswordHash);
            cmd.Parameters.AddWithValue("@role", u.Role);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }
    }
}
