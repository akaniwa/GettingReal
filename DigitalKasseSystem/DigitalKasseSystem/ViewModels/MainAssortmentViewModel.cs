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
        private ItemDescriptionViewModel? selectedItemDescriptionVM;
        public ItemDescriptionViewModel? SelectedItemDescriptionVM
        {
            get => selectedItemDescriptionVM;
            set
            {
                if (selectedItemDescriptionVM != value)
                {
                    selectedItemDescriptionVM = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedItemDescriptionVM));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainAssortmentViewModel(ItemDescriptionRepository itemDescriptionRepository)
        {
            this.itemDescriptionRepository = itemDescriptionRepository;
            ItemDescriptionsVM = new ObservableCollection<ItemDescriptionViewModel>();
            foreach (ItemDescription item in (itemDescriptionRepository.GetItemDescriptions()))
            {
                ItemDescriptionsVM.Add(new ItemDescriptionViewModel(item));
            }
            ItemDescription itemDescription = new ItemDescription();
        }

        public ICommand AddItemDescriptionCommand { get; } = new Commands.AddItemDesciptionCommand();
        public ICommand DeleteItemDescriptionCommand { get; } = new Commands.DeleteItemDesciptionCommand();

        public void SaveAssortment()
        {
            itemDescriptionRepository.SaveToFile();
        }

        public void AddNewItemDescription(ItemDescription itemDescription)
        {
            itemDescriptionRepository.AddItemDescription(itemDescription);
            ItemDescriptionsVM.Add(new ItemDescriptionViewModel(itemDescription));
        }

        public void DeleteItemDescription(ItemDescription itemDescription)
        {
            if (itemDescription != null && SelectedItemDescriptionVM != null)
            {
                ItemDescriptionsVM.Remove(SelectedItemDescriptionVM);
                itemDescriptionRepository.DeleteItemDescription(itemDescription);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
