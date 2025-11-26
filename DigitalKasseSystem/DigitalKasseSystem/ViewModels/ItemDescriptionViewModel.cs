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
        private ItemDescription itemDescription;

        public int ItemNumber { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PicturePath { get; set; }
        public string Categori { get; set; }

        public ItemDescriptionViewModel(ItemDescription itemDescription)
        {
            this.itemDescription = itemDescription;
            ItemNumber = itemDescription.ItemNumber;
            Name = itemDescription.ItemName;
            Price = itemDescription.Price;
            PicturePath = itemDescription.PicturePath;
            Categori = itemDescription.Category;
        }
    }
}
