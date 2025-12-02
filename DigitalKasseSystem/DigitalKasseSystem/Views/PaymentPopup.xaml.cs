using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalKasseSystem.Views
{
    /// <summary>
    /// Interaction logic for PaymentPopup.xaml
    /// </summary>
    public partial class PaymentPopup : Window
    {
        double amount { get; set; }

        public PaymentPopup(double amount)
        {
            this.amount = amount;
            InitializeComponent();
            TotalFromCurrentSale.Content = $"Total: {amount.ToString("C2")}";
        }

        private void CashButton_Click(object sender, RoutedEventArgs e)
        {
            CashPaymentDialog cashPaymentDialog = new CashPaymentDialog(amount);
            cashPaymentDialog.Owner = this;
            cashPaymentDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            cashPaymentDialog.ShowDialog();
            if (cashPaymentDialog.DialogResult == true)
            {
                DialogResult = true;
                Close();
            }
            else if (cashPaymentDialog.DialogResult == false)
            {
                amount -= cashPaymentDialog.PaidAmount;
                TotalFromCurrentSale.Content = $"Total: {amount.ToString("C2")}";
            }
        }

        private void MobilPayButton_Click(object sender, RoutedEventArgs e)
        {
            MobilPayPaymentDialog mobilPayPaymentDialog = new MobilPayPaymentDialog();
            mobilPayPaymentDialog.Owner = this;
            mobilPayPaymentDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            mobilPayPaymentDialog.ShowDialog();
            if (mobilPayPaymentDialog.DialogResult == true)
            {
                DialogResult = true;
                Close();
            }
            else if (mobilPayPaymentDialog.DialogResult == false)
            {
                DialogResult = false;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
