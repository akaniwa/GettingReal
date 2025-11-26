using DigitalKasseSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.ViewModels
{
    public class SaleViewModel
    {
        public double Total;
        public PaymentMethod Payment;
        public List<Item> Basket = new List<Item>();
        public DateTime StartTime;

        public SaleViewModel()
        {
            StartTime = DateTime.Now;
        }
    }
}
