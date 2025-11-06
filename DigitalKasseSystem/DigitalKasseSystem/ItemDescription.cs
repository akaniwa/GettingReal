using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DigitalKasseSystem
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
            this.ItemName = itemName;
            this.Price = price;
            this.Category = category;
            this.Picture = picture;
        }

        public ItemDescription(int itemNumber, string itemName, double price, string category)
        {
            ItemNumber = itemNumber;
            this.ItemName = itemName;
            this.Price = price;
            this.Category = category;
        }

        public ItemDescription(int itemNumber, string itemName, double price)
        {
            ItemNumber = itemNumber;
            this.ItemName = itemName;
            this.Price = price;
            Category = "No category";
        }

        // Override ToString for easy save functionality
        public override string ToString()
        {
            return $"{ItemNumber};{ItemName};{Price};{Category};{Picture}";
        }
    }
}
