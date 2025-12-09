using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    public class Item
    {
        // Backing field
        private ItemDescription _itemDescription;

        // Properties
        public ItemDescription ItemDescription 
        { 
            get { return _itemDescription; }
        }

        // Constructor, setting a reference to an ItemDescription in an existing ItemDescriptionRepository
        public Item(ItemDescription itemDescription)
        {
            _itemDescription = itemDescription;
        }

        // Override ToString for easy save functionality
        public override string ToString()
        {
            return $"{ItemDescription.ToString}";
        }
    }
}
