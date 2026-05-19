using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Flow.Model
{
    // Represents the User entity/model in the system
    // Used for authentication and authorization purposes
    // Stores user login credentials and system role information
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
