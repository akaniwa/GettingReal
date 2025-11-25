using DigitalKasseSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.ViewModels
{
    class MainSaleViewModel
    {
        //private fields, repositories
        private ItemDescriptionRepository itemDescriptionRepository;
        private SaleRepository saleRepository;

        //backing fields, ViewModels
        private SaleViewModel _currentSale;
        private List<ItemDescriptionViewModel> _itemDescriptionsVM;

        //properties, ViewModels
        public SaleViewModel CurrentSale 
        { 
            get { return _currentSale; }
            set { _currentSale = value; }
        }
        public List<ItemDescriptionViewModel> ItemDescriptionsVM 
        { 
            get { return _itemDescriptionsVM; }
            set { _itemDescriptionsVM = value; } 
        }

        //methods
        public void NewSale()
        {
            CurrentSale = new SaleViewModel();
        }

        public void SetOrderNumber(int orderNumber)
        {
            Sale.OrderNumber = orderNumber;
        }

        public void NewItemToSale(int itemNumber)
        {
            foreach (ItemDescription itemDescription in itemDescriptionRepository.GetItemDescriptions())
            {
                if (itemNumber == itemDescription.ItemNumber)
                {
                    CurrentSale.Total += itemDescription.Price;
                    CurrentSale.Basket.Add(new Item(itemDescription));
                }
            }
        }

        public void RemoveItemFromSale(int itemNumber)
        {
            for (int basketIndex = 0; basketIndex < CurrentSale.Basket.Count; basketIndex++)
            {
                if (itemNumber == CurrentSale.Basket[basketIndex].ItemDescription.ItemNumber)
                {
                    CurrentSale.Total -= CurrentSale.Basket[basketIndex].ItemDescription.Price;
                    CurrentSale.Basket.RemoveAt(basketIndex);
                    break;
                }
            }
        }

        public void EndSale(PaymentMethod paymentMethod)
        {
            int saleNumber = int.Parse($"{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}{saleRepository.GetSalesCount()}{Sale.OrderNumber}");
            saleRepository.AddSale(new Sale(saleNumber, CurrentSale.Total, paymentMethod, CurrentSale.StartTime, CurrentSale.Basket));

            saleRepository.SaveToFile();

            Sale.OrderNumber++;
        }

        //constructor
        public MainSaleViewModel
            (
            List<ItemDescriptionViewModel> itemDescriptionsVM, 
            ItemDescriptionRepository itemDescriptionRepository, 
            SaleRepository saleRepository
            )
        {
            _itemDescriptionsVM = itemDescriptionsVM;
            this.itemDescriptionRepository = itemDescriptionRepository;
            this.saleRepository = saleRepository;
        }
    }
}
