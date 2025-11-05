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
        string itemName;
        double price;
        string category;
        BitmapImage picture; // Optional - maybe string with path would be better

        // Constructor overloads for different levels of detail
        public ItemDescription(int itemNumber, string itemName, double price, string category, BitmapImage picture)
        {
            ItemNumber = itemNumber;
            this.itemName = itemName;
            this.price = price;
            this.category = category;
            this.picture = picture;
        }

        public ItemDescription(int itemNumber, string itemName, double price, string category)
        {
            ItemNumber = itemNumber;
            this.itemName = itemName;
            this.price = price;
            this.category = category;
        }

        public ItemDescription(int itemNumber, string itemName, double price)
        {
            ItemNumber = itemNumber;
            this.itemName = itemName;
            this.price = price;
            category = "Missing";
        }

        // Override ToString for easy save functionality
        public override string ToString()
        {
            return $"{ItemNumber};{itemName};{price};{category};{picture}";
        }
    }
}
