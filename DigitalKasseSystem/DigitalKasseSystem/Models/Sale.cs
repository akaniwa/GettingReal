using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    public class Sale
    {
        public static int OrderNumber;

        private long saleNumber;
        private double total;
        private PaymentMethod paymentMethod;
        private DateTime startTime;
        private DateTime endTime;
        private List<Item> basket;

        // Constructor for Sale class, starting a new sale
        public Sale(long saleNumber, double total, PaymentMethod payment, DateTime startTime, DateTime endTime, List<Item> basket)
        {
            this.saleNumber = saleNumber;
            this.total = total;
            this.paymentMethod = payment;
            this.startTime = startTime;
            this.endTime = endTime;
            this.basket = basket;
            OrderNumber++;
        }

        public override string ToString()
        {
            string saveString = $"{saleNumber},{total},{paymentMethod},{startTime},{endTime},";
            foreach (Item item in basket)
            {
                saveString += item.ToString() + "-";
            }
            return saveString;
        }
    }
}
