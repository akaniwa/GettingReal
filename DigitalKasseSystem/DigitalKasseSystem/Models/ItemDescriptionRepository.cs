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
        // Private fields
        private List<ItemDescription> itemDescriptions = new List<ItemDescription>();

        // Methods
        // Adds ItemDesciption to list
        public void AddItemDescription(ItemDescription itemDescription)
        {
            itemDescriptions.Add(itemDescription);
        }

        // Removes ItemDesciption from list
        public void DeleteItemDescription(ItemDescription itemDescription)
        {
            if (itemDescription != null)
            {
                itemDescriptions.Remove(itemDescription);
            }
        }
        
        // Return single ItemDesciption using the item number to find the specific
        public ItemDescription GetItemDescription(int itemNumber)
        {
            ItemDescription item = itemDescriptions.Find(item => item.ItemNumber == itemNumber);
            return item; 
        } 

        // Returns list of ItemDesciptions
        public List<ItemDescription> GetAllDescriptions()
        {
            return itemDescriptions;
        }

        // Saves all ItemDesciptions to ItemDesciptions.csv for later use
        public void SaveToFile()
        {
            StreamWriter writer = new StreamWriter("ItemDescriptions.csv");
            foreach (ItemDescription item in itemDescriptions)
            {
                writer.WriteLine(item.ToString());
            }
            writer.Close();
        }

        // Loads all files from ItemDesciptions.csv
        public void LoadFromFile()
        {
            itemDescriptions.Clear();
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
                    string picturePath = parts[3];
                    string category = parts[4];
                    ItemDescription itemDescription = new ItemDescription(itemNumber, itemName, price, category, picturePath);
                    itemDescriptions.Add(itemDescription);
                }
                reader.Close();
            }
        }
    }
}
