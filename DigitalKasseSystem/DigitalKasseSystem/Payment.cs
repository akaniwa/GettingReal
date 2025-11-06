using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem
{
    class Payment
    {
        double amount;
        PaymentMethod paymentMethod;

        // Constructor for choosing payment method
        public Payment(PaymentMethod paymentMethod)
        {
            this.paymentMethod = paymentMethod;
        }

        // Method to update the amount to be paid
        public double UpdateAmount(double amount)
        {
            if (paymentMethod == PaymentMethod.Kontant)
            {
                this.amount = amount % 1;
            }
            if (paymentMethod == PaymentMethod.MobilePay)
            {
                this.amount = amount;
            }
            return this.amount;
        }
    }
}
