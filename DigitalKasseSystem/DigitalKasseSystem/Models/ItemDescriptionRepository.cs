using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    public class ItemDescriptionRepository
    {
        List<ItemDescription> itemDescriptions = new();

        public void AddItemDescription(ItemDescription itemDescription)
        {
            itemDescriptions.Add(itemDescription);
        }

        public void DeleteItemDescription(ItemDescription itemDescription)
        {
            if (itemDescription != null)
            {
                itemDescriptions.Remove(itemDescription);
            }
        }

        public ItemDescription GetItemDescription(int itemNumber)
        {
            ItemDescription item = itemDescriptions.Find(item => item.ItemNumber == itemNumber);
            return item;
        }

        public List<ItemDescription> GetItemDescriptions()
        {
            return itemDescriptions;
        }

        public void SaveToFile()
        {
            StreamWriter writer = new StreamWriter("ItemDescriptions.txt");
            foreach (ItemDescription item in itemDescriptions)
            {
                writer.WriteLine(item.ToString());
            }
            writer.Close();
        }

        public void LoadFromFile()
        {
            if (File.Exists("ItemDescriptions.csv"))
            {
                StreamReader reader = new StreamReader("ItemDescriptions.csv");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    int itemNumber = int.Parse(parts[0]);
                    string itemName = parts[1];
                    double price = double.Parse(parts[2]);
                    string category = parts[3];
                    string picturePath = parts[4];
                    ItemDescription itemDescription = new ItemDescription(itemNumber, itemName, price, category, picturePath);
                    itemDescriptions.Add(itemDescription);
                }
                reader.Close();
            }
        }
    }
}
