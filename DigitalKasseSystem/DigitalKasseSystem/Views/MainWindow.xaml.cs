using DigitalKasseSystem.Models;
using DigitalKasseSystem.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalKasseSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ItemDescriptionRepository itemDescriptionRepository;
        private SaleRepository saleRepository;

        public MainWindow()
        {
            itemDescriptionRepository = new ItemDescriptionRepository();
            saleRepository = new SaleRepository(itemDescriptionRepository);
            // set Window icon from embedded resource
            //this.Icon = new BitmapImage(new Uri("pack://application:,,,/DigitalKasseSystem/DigitalKasseSystem/Image/icon.png"));
            InitializeComponent();
        }

        private void SaleButton_Click(object sender, RoutedEventArgs e)
        {
            SaleWindow saleWindow = new SaleWindow(itemDescriptionRepository, saleRepository);
            saleWindow.Show();
        }

        private void AssortmentButton_Click(object sender, RoutedEventArgs e)
        {
            AssortmentWindow assortmentWindow = new AssortmentWindow(itemDescriptionRepository);
            assortmentWindow.Show();
        }

        private void NotImplementedButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Denne funktion er ikke tilgængelig endnu. Kontakt support.", "Ikke tilgængelig!");
        }
    }
}