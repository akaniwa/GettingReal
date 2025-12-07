using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalKasseSystem.Views
{
    /// <summary>
    /// Interaction logic for CashPaymentDialog.xaml
    /// </summary>
    public partial class CashPaymentDialog : Window
    {
        private string paidAmountString = string.Empty;
        public double PaidAmount { get; set; }
        double owedAmount;

        public CashPaymentDialog(double amount)
        {
            owedAmount = amount;
            InitializeComponent();
            TotalAmountLabel.Content = $"Total: {owedAmount.ToString("C2")}";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int pressed = int.Parse(button.Tag.ToString());
                paidAmountString += pressed.ToString();
                InputTextBox.Text = paidAmountString;
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            PaidAmount = double.Parse(paidAmountString);
            if (PaidAmount >= owedAmount)
            {
                if (PaidAmount > owedAmount)
                {
                    AmountReturnDialog returnDialog = new AmountReturnDialog();
                    returnDialog.ReturnAmount = PaidAmount - owedAmount;
                    returnDialog.Owner = this;
                    returnDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    returnDialog.ShowDialog();
                }
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            paidAmountString = paidAmountString.Remove(paidAmountString.Length - 1);
            InputTextBox.Text = paidAmountString;
        }
    }
}
