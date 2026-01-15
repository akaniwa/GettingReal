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

        // Constructor for Sale class, starting a new sale
        public Sale(long saleNumber, double total, PaymentMethod payment, DateTime startTime, DateTime endTime, List<Item> basket)
        {
            this.SaleNumber = saleNumber;
            this.Total = total;
            this.PaymentMethod = payment;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Basket = basket;
            OrderNumber++;
        }

        // ToString override for easy save funtionallity
        public override string ToString()
        {
            string saveString = $"{SaleNumber},{Total},{PaymentMethod},{StartTime},{EndTime},";
            foreach (Item item in Basket)
            {
                saveString += item.ToString() + "-";
            }
            return saveString;
        }
    }
}
