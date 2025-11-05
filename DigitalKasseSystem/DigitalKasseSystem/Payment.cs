using System;
using System.Collections.Generic;
using System.Linq;
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
        public void UpdateAmount(double amount) // Maybe needs to return something later
        {
            this.amount = amount;
        }
    }
}
