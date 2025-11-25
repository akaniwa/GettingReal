using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DigitalKasseSystem.Models
{
    class ItemDescription
    {
        public int ItemNumber { get; }
        public string ItemName { get; }
        public double Price { get; }
        public string Category { get; }
        public BitmapImage Picture { get; } // Optional - maybe string with path would be better

        // Constructor overloads for different levels of detail
        public ItemDescription(int itemNumber, string itemName, double price, string category, BitmapImage picture)
        {
            ItemNumber = itemNumber;
            ItemName = itemName;
            Price = price;
            Category = category;
            Picture = picture;
        }

        public ItemDescription(int itemNumber, string itemName, double price, string category)
        {
            ItemNumber = itemNumber;
            ItemName = itemName;
            Price = price;
            Category = category;
        }

        public ItemDescription(int itemNumber, string itemName, double price)
        {
            ItemNumber = itemNumber;
            ItemName = itemName;
            Price = price;
            Category = "No category";
        }

        // Override ToString for easy save functionality
        public override string ToString()
        {
            return $"{ItemNumber};{ItemName};{Price};{Category};{Picture}";
        }
    }
}
