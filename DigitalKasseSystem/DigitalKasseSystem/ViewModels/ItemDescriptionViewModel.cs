using DigitalKasseSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.ViewModels
{
    public class ItemDescriptionViewModel
    {
        ItemDescription itemDescription;

        public int ItemNumber;
        public string ItemName;
        public double Price;
        public string PicturePath;
        public string Category;

        public ItemDescriptionViewModel(ItemDescription itemDescription)
        {
            this.itemDescription = itemDescription;
            ItemNumber = itemDescription.ItemNumber;
            ItemName = itemDescription.ItemName;
            Price = itemDescription.Price;
            PicturePath = itemDescription.PicturePath;
            Category = itemDescription.Category;
        }
    }
}
