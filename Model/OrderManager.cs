using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Flow.Model
{
    // Represents the Order entity/model in the system
    // Used to transfer order data between UI and database layers
    // Contains order details such as customer, product, quantity, and total amount   
    public class OrderManager
    {
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
