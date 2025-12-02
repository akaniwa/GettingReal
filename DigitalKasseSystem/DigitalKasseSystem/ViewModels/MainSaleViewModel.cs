using DigitalKasseSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.ViewModels
{
    public class MainSaleViewModel : INotifyPropertyChanged
    {
        public SaleViewModel CurrentSale;
        public ObservableCollection<ItemDescriptionViewModel> ItemDescriptionsVM { get; set; }

        ItemDescriptionRepository itemDescriptionRepository;
        SaleRepository saleRepository;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainSaleViewModel(ItemDescriptionRepository itemDescriptionRepository, SaleRepository saleRepository)
        {
            this.itemDescriptionRepository = itemDescriptionRepository;
            this.saleRepository = saleRepository;
        }


        public void NewSale()
        {
            CurrentSale = new SaleViewModel();
        }

        public void NewItemToSale(int itemNumber)
        {
            ItemDescription item = itemDescriptionRepository.GetItemDescription(itemNumber);
            if (item != null)
            {
                CurrentSale.Basket.Add(new Item(item));
            }
        }

        public void RemoveItemFromSale(int itemNumber)
        {
            if (CurrentSale.Basket.Count > 0)
            {
                Item item = CurrentSale.Basket.Find(item => item.ItemDescription.ItemNumber == itemNumber);
                if (item != null)
                {
                    CurrentSale.Basket.Remove(item);
                }
            }
        }

        public void SetOrderNumber(int orderNumber)
        {
            Sale.OrderNumber = orderNumber;
        }

        public void EndSale(PaymentMethod paymentMethod)
        {
            //int saleNumber = int.Parse(DateTime.Now.ToString("yyyyMMdd") + saleRepository.GetSalesCount().ToString() + Sale.OrderNumber.ToString("D4"));
            int saleNumber = int.Parse($"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{saleRepository.GetSalesCount()}{Sale.OrderNumber.ToString("D2")}");
            Sale sale = new Sale(saleNumber, CurrentSale.Total, CurrentSale.Payment, CurrentSale.StartTime, DateTime.Now, CurrentSale.Basket);
            saleRepository.AddSale(sale);
        }
    }
}
