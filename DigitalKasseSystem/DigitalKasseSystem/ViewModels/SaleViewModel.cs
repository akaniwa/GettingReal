using DigitalKasseSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.ViewModels
{
    public class SaleViewModel : INotifyPropertyChanged
    {
        public double Total
        {
            get { return Basket.Sum(item => item.ItemDescription.Price); }
            set 
            {
                Basket.Sum(item => item.ItemDescription.Price);
                OnPropertyChanged(nameof(Total)); 
            }
        }
        public PaymentMethod Payment;
        public List<Item> Basket = new List<Item>();
        public DateTime StartTime;

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string? n = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));

        public SaleViewModel()
        {
            StartTime = DateTime.Now;
        }
    }
}
