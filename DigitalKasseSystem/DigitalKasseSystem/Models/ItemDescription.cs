using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DigitalKasseSystem.Models
{
    public class ItemDescription
    {
        public int ItemNumber;
        public string ItemName;
        public double Price;
        public string Category;
        public string PicturePath; // Optional - maybe string with path would be better
        private static int ItemNumberCount;

        // Constructor overloads for different levels of detail
        public ItemDescription(int itemNumber, string itemName, double price, string category, string picture)
        {
            ItemNumber = itemNumber;
            ItemName = itemName;
            Price = price;
            Category = category;
            PicturePath = picture;
            ItemNumberCount++;
        }

        public ItemDescription()
        {
            ItemNumber = ItemNumberCount;
            ItemName = "Input name";
            Price = 0;
            Category = "No category";
            ItemNumberCount++;
        }

        // Override ToString for easy save functionality
        public override string ToString()
        {
            return $"{ItemNumber};{ItemName};{PicturePath};{Price};{Category}";
        }
    }
}
