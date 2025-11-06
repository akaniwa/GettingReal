using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem
{
    class Controller
    {
        Sale currentSale;
        ItemDescriptionRepository itemDescriptionRepository = new();
        SaleRepository saleRepository = new();

        public void NewSale()
        {
            int newSaleNumber = DateTime.Now.Day + DateTime.Now.Month + saleRepository.GetSalesCount(); 
            currentSale = new Sale(newSaleNumber);
        }

        public double NewItemToSale(int itemNumber)
        {
            ItemDescription item = itemDescriptionRepository.GetItemDescription(itemNumber);
            if (item != null)
            {
                currentSale.AddToBasket(itemNumber, item.Price);
                return currentSale.total;
            }
            return 0.0;
        }

        public double ChoosePaymentMethod(PaymentMethod paymentMethod)
        {
            currentSale.Payment = new Payment(paymentMethod);
            return currentSale.Payment.UpdateAmount(currentSale.total);
        }

        public void EndSale()
        {
            currentSale.EndSale();
            saleRepository.AddSale(currentSale);
        }
    }
}
