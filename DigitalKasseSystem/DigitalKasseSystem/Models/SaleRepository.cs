using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    class SaleRepository
    {
        private List<Sale> sales = new List<Sale>();
        
        public void AddSale(Sale sale)
        {
            sales.Add(sale);
        }

        public int GetSalesCount()
        {
            return sales.Count;
        }
    }
}
