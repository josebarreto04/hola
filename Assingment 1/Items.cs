using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assingment_1
{
    public class Items
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ID { get; set; }
        public int Quantity { get; set; }

        public Items(string Name, string Description, decimal Price, int ID, int Quantity)
        {
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.ID = ID;
            this.Quantity = Quantity;
        }

    }
}
