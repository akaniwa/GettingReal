using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using IOPath = System.IO.Path;

namespace DigitalKasseSystem.Views
{
    public partial class MobilPayPaymentDialog : Window
    {
        double amountOwed;
        public MobilPayPaymentDialog(double amount)
        {
            amountOwed = amount;
            InitializeComponent();
            string picPath = IOPath.Combine(Directory.GetCurrentDirectory(), "Image", "mobilpayUldumHal.jpg");
            mobilpayImage.Source = new BitmapImage(new Uri(picPath));
            TotalLabel.Content = $"Total: {amountOwed.ToString("C2")}";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
