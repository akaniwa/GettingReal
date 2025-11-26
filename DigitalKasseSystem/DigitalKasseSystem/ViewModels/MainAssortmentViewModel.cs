using DigitalKasseSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.ViewModels
{
    public class MainAssortmentViewModel
    {
        ItemDescriptionRepository itemDescriptionRepository;
        public List<ItemDescriptionViewModel> ItemDescriptionsVM;
        public ItemDescriptionViewModel SelectedItemDesciprtionVM;

        public MainAssortmentViewModel(ItemDescriptionRepository itemDescriptionRepository)
        {
            this.itemDescriptionRepository = itemDescriptionRepository;
            ItemDescriptionsVM = new List<ItemDescriptionViewModel>();
            ItemDescription itemDescription = new ItemDescription();

        }

        public void SaveAssortment()
        {
            itemDescriptionRepository.SaveToFile();
        }

        public void AddNewItemDescription(ItemDescriptionViewModel itemDescriptionVM)
        {
            ItemDescription itemDescription = new ItemDescription(itemDescriptionVM.ItemNumber, itemDescriptionVM.ItemName, itemDescriptionVM.Price, itemDescriptionVM.Category, itemDescriptionVM.PicturePath);
            itemDescriptionRepository.AddItemDescription(itemDescription);
            ItemDescriptionsVM.Add(itemDescriptionVM);
        }

        public void DeleteItemDescription(ItemDescription itemDescription)
        {
            if (itemDescription != null)
            {
                ItemDescriptionsVM.Remove(SelectedItemDesciprtionVM);
                itemDescriptionRepository.DeleteItemDescription(itemDescription);
            }
        }
    }
}
