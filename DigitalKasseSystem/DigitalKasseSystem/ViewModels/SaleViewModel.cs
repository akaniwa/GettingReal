using DigitalKasseSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.ViewModels
{
    internal class SaleViewModel
    {
        //backing fields
        private double _total;
        //private PaymentMethod _payment;
        private List<Item> _basket;
        private DateTime _startTime;

        //properties
        public double Total 
        { 
            get { return _total; } 
            set { _total = value; } 
        }
        /*
        public PaymentMethod Payment
        { 
            get { return _payment; } 
            set { _payment = value; } 
        }
        */
        public List<Item> Basket
        { 
            get { return _basket; } 
            set { _basket = value; }
        }
        public DateTime StartTime
        { 
            get { return _startTime; }
        }

        //constructor
        public SaleViewModel()
        {
            _total = 0;
            _basket = new List<Item>();
            _startTime = DateTime.Now;
        }
    }
}
