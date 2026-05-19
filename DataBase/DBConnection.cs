//using MySql.Data.MySqlClient;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Cafe_Flow.DataBase
//{
//    public class DBConnection
//    {
//        protected MySqlConnection con;

//        public void OpenConnection()
//        {
//            con = new MySqlConnection("server=localhost;database=CafeFlowDB;uid=root;pwd=salman123@");
//            con.Open();
//        }

//        public void CloseConnection()
//        {
//            con.Close();
//        }
//    }
//}
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Cafe_Flow.DataBase
{
    public class DBConnection
    {
        // Shared database connection object accessible by all derived DB classes
        protected MySqlConnection con;

        // Opens a connection to the MySQL database
        // Used before executing any SQL query (SELECT, INSERT, UPDATE, DELETE)
        public void OpenConnection()
        {
            try
            {
                // Connection string used to connect to local MySQL database
                var connectionString = "server=localhost;database=CafeFlowDB;uid=root;pwd=salman123@";

                // Initialize connection object
                con = new MySqlConnection(connectionString);

                // Open connection only if it is not already open
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                // Display error if database connection fails (e.g., server down, wrong credentials)
                MessageBox.Show(
                    "Database Connection Failed!\n\n" + ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Closes the database connection safely after query execution
        // Helps release database resources and avoid connection leaks
        public void CloseConnection()
        {
            try
            {
                // Ensure connection exists and is currently open before closing
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Show error if connection closing fails
                MessageBox.Show(
                    "Error closing database connection!\n\n" + ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}