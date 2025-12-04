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
    /// Interaction logic for MobilPayPaymentDialog.xaml
    /// </summary>
    public partial class MobilPayPaymentDialog : Window
    {
        double amountOwed;
        public MobilPayPaymentDialog(double amount)
        {
            amountOwed = amount;
            InitializeComponent();
            mobilpayImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mobilpay_qr.png"));
            TotalLabel.Content = $"Total: {amountOwed.ToString("C2")}";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
