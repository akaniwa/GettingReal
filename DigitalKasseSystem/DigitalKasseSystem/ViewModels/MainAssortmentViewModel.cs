using DigitalKasseSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DigitalKasseSystem.ViewModels
{
    public class MainAssortmentViewModel : INotifyPropertyChanged
    {
        ItemDescriptionRepository itemDescriptionRepository;
        public ObservableCollection<ItemDescriptionViewModel> ItemDescriptionsVM { get; set; }
        private ItemDescriptionViewModel? _selectedItemDescriptionVM;
        public ItemDescriptionViewModel? SelectedItemDescriptionVM
        {
            get => _selectedItemDescriptionVM;
            set
            {
                    _selectedItemDescriptionVM = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedItemDescriptionVM));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainAssortmentViewModel(ItemDescriptionRepository itemDescriptionRepository)
        {
            ItemDescription.ItemNumberCount = 0;
            this.itemDescriptionRepository = itemDescriptionRepository;
            this.itemDescriptionRepository.LoadFromFile();
            ItemDescriptionsVM = new ObservableCollection<ItemDescriptionViewModel>();
            foreach (ItemDescription item in (itemDescriptionRepository.GetAllDescriptions()))
            {
                ItemDescriptionsVM.Add(new ItemDescriptionViewModel(item));
            }
        }

        public ICommand AddItemDescriptionCommand { get; } = new Commands.AddItemDesciptionCommand();
        public ICommand DeleteItemDescriptionCommand { get; } = new Commands.DeleteItemDesciptionCommand();

        public int SaveAssortment()
        {
            List<int> usedItemNumbers = new List<int>();
            foreach (ItemDescription item in (itemDescriptionRepository.GetAllDescriptions()))
            {
                if (usedItemNumbers.Contains(item.ItemNumber))
                {
                    return item.ItemNumber;
                }
                usedItemNumbers.Add(item.ItemNumber);
            }
            itemDescriptionRepository.SaveToFile();
            return 0;
        }

        public void AddNewItemDescription(ItemDescription itemDescription)
        {
            itemDescriptionRepository.AddItemDescription(itemDescription);
            ItemDescriptionsVM.Add(new ItemDescriptionViewModel(itemDescription));
        }

        public void DeleteItemDescription()
        {
            if (SelectedItemDescriptionVM != null)
            {
                ItemDescription foundItem = itemDescriptionRepository.GetItemDescription(SelectedItemDescriptionVM.ItemNumber);
                ItemDescriptionsVM.Remove(SelectedItemDescriptionVM);
                itemDescriptionRepository.DeleteItemDescription(foundItem);
                ItemDescription.ItemNumberCount--;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
