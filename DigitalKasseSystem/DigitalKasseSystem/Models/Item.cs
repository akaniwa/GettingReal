using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    class Item
    {
        // Item number property with only a getter
        public int ItemNumber { get; }

        // Constructor setting item number
        public Item(int itemNumber)
        {
            ItemNumber = itemNumber;
        }

        // Override ToString for easy save functionality
        public override string ToString()
        {
            return $"{ItemNumber}";
        }
    }
}
