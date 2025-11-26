using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    public class Item
    {
        // Item number property with only a getter
        public ItemDescription ItemDescription { get; }

        // Constructor setting item number
        public Item(ItemDescription itemDescription)
        {
            ItemDescription = itemDescription;
        }

        // Override ToString for easy save functionality
        public override string ToString()
        {
            return $"{ItemDescription.ItemNumber}";
        }
    }
}
