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
        public double total { get; private set; }
        public Payment Payment;
        DateTime startTime;
        DateTime endTime;
        List<Item> basket;

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
        public void AddToBasket(int itemNumber, double price)
        {
            basket.Add(new Item(itemNumber));
            total += price;
        }

        // Methods to remove items from the basket
        public void RemoveFromBasket(int itemNumber) // Maybe change later to remove instance instead of by item number
        {
            basket.RemoveAll(i => i.ItemNumber == itemNumber);
        }

        public override string ToString() // Change later for saving functionality
        {
            return base.ToString();
        }
    }
}
