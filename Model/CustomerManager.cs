using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Flow.Model
{
    // Represents the Customer entity/model in the system
    // Used to store and transfer customer data between UI and database layers
    // Contains basic customer information such as Name, Phone, and Address
    public class CustomerManager
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
