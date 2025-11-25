using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    class Sale
    {
        //private fields
        private int saleNumber;
        private double total;
        private PaymentMethod paymentMethod;
        private DateTime startTime;
        private DateTime endTime;
        private List<Item> basket;

        //backing fields
        private static int _orderNumber;

        //properties
        public static int OrderNumber 
        { 
            get { return _orderNumber; } 
            set { _orderNumber = value; } 
        }

        //constructor, finalizing an ongoing sale
        public Sale(int saleNumber, double total, PaymentMethod paymentMethod, DateTime startTime, List<Item> basket)
        {
            this.saleNumber = saleNumber;
            this.total = total;
            this.paymentMethod = paymentMethod;
            this.startTime = startTime;
            endTime = DateTime.Now;
            this.basket = basket;
        }

        public override string ToString()
        {
            string basketContents = "";
            foreach (Item item in basket)
            {
                basketContents += $"{item.ToString()}-";
            }
            basketContents.Remove(basketContents.Length - 1);

            return $"{saleNumber};{total};{paymentMethod};{startTime};{endTime};{basketContents}";
        }
    }
}
