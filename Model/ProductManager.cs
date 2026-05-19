using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Flow.Model
{
    // Represents the Product entity/model in the system
    // Used to store and transfer product data between UI and database layers
    // Contains basic product details such as name, category, and price
    public class ProductManager
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
