using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem
{
    internal class ItemDescriptionRepository
    {
        List<ItemDescription> itemDescriptions = new();

        public void AddItemDescription(ItemDescription itemDescription)
        {
            itemDescriptions.Add(itemDescription);
        }

        public ItemDescription GetItemDescription(int itemNumber) 
        { 
            ItemDescription item = itemDescriptions.Find(item => item.ItemNumber == itemNumber);
            return item;
        }
    }
}
