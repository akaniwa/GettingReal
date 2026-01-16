using DigitalKasseSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    public class Sale
    {
        // Attributes
        public static int OrderNumber = 1;
        public List<Item> Basket;
        public long SaleNumber;
        public double Total;
        public PaymentMethod PaymentMethod;
        public DateTime StartTime;
        public DateTime EndTime;
        public bool delivered;

        // Constructor for Sale class, starting a new sale
        public Sale(long saleNumber, double total, PaymentMethod payment, DateTime startTime, DateTime endTime, List<Item> basket, bool delivered)
        {
            this.SaleNumber = saleNumber;
            this.Total = total;
            this.PaymentMethod = payment;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Basket = basket;
            this.delivered = delivered;
            OrderNumber++;
        }

        // ToString override for easy save funtionallity
        public override string ToString()
        {
            string saveString = $"{SaleNumber},{Total},{PaymentMethod},{StartTime},{EndTime},{delivered},";
            foreach (Item item in Basket)
            {
                saveString += item.ToString() + ",";
            }
            return saveString;
        }

        public SaleViewModel ToSaleViewModel()
        {
            SaleViewModel saleVM = new SaleViewModel();
            saleVM.Total = this.Total;
            saleVM.Payment = this.PaymentMethod;
            saleVM.Basket = this.Basket;
            saleVM.StartTime = this.StartTime;
            saleVM.Total = this.Total;
            saleVM.EndTime = this.EndTime;
            saleVM.delivered = this.delivered;
            saleVM.SaleNumber = this.SaleNumber;
            return saleVM;
        }
    }
}
