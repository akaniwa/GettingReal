using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem
{
    class Sale
    {
        public int SaleNumber { get; }
        public Payment Payment;
        DateTime startTime;
        DateTime endTime;
        List<Item> basket;
        double total;

        // Constructor for Sale class, starting a new sale
        public Sale(int saleNumber)
        {
            SaleNumber = saleNumber;
            startTime = DateTime.Now;
            basket = new List<Item>();
            total = 0.0;
        }

        // Method to end the sale
        public void EndSale()
        {
            endTime = DateTime.Now;
        }

        // Methods to add items from the basket
        public void AddToBasket(Item item)
        {
            basket.Add(item);
        }

        // Methods to remove items from the basket
        public void RemoveFromBasket(string itemNumber) // Maybe change later to remove instance instead of by item number
        {
            basket.RemoveAll(i => i.ItemNumber.ToString() == itemNumber);
        }

        public override string ToString() // Change later for saving functionality
        {
            return base.ToString();
        }
    }
}
