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
    /// Interaction logic for AmountReturnDialog.xaml
    /// </summary>
    public partial class AmountReturnDialog : Window
    {
        public AmountReturnDialog()
        {
            InitializeComponent();
        }

        public double ReturnAmount
        {
            get { return double.Parse(ReturnAmountLabel.Content.ToString().Replace("Return Amount: ", "").TrimEnd('$')); }
            set { ReturnAmountLabel.Content = $"Byttepenge: {value.ToString("C2")}"; }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
