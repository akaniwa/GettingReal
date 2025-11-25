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
        //private field
        private static int itemNumberCount = 0;

        //properties
        public int ItemNumber { get; set; }
        public string ItemName { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        // Constructor, missing overloads, preferably via chaining?
        public ItemDescription(List<ItemDescription> itemDescriptions, string itemName, double price, string category, string picture)
        {
            List<int> itemNumbers = new List<int>();

            foreach (ItemDescription itemDescription in itemDescriptions)
            {
                itemNumbers.Add(itemDescription.ItemNumber);
            }

            while (!itemNumbers.Contains(itemNumberCount))
            {
                itemNumberCount++;
            }

            ItemNumber = itemNumberCount;
            ItemName = itemName;
            Price = price;
            Category = category;
            Picture = picture;
        }

        // Override ToString for easy save functionality
        public override string ToString()
        {
            return $"{ItemNumber}_{ItemName}_{Picture}_{Price}_{Category}";
        }
    }
}
