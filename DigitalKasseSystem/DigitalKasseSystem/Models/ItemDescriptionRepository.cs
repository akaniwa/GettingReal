using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    internal class ItemDescriptionRepository
    {
        //private fields
        private List<ItemDescription> itemDescriptions = new List<ItemDescription>();

        //methods
        public void AddItemDescription(ItemDescription itemDescription)
        {
            itemDescriptions.Add(itemDescription);
        }

        public void DeleteItemDescription(ItemDescription itemDescription)
        {
            itemDescriptions.Remove(itemDescription);
        }

        //this particular method invalidates itemDescriptions being a private field
        public List<ItemDescription> GetItemDescriptions() 
        { 
            return itemDescriptions;
        }

        public void SaveToFile()
        {
            throw new NotImplementedException();
        }

        public void LoadFromFile()
        {
            throw new NotImplementedException();
        }
    }
}
