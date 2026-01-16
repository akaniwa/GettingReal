using DigitalKasseSystem.Models;
using DigitalKasseSystem.ViewModels;
using DigitalKasseSystem.Views;
using System.IO;
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
using IOPath = System.IO.Path;

namespace DigitalKasseSystem
{
    public partial class MainWindow : Window
    {
        private ItemDescriptionRepository itemDescriptionRepository;
        private SaleRepository saleRepository;
        public List<ItemDescriptionViewModel> ItemDescriptionsVM;

        AssortmentWindow assortmentWindow;
        SaleWindow saleWindow;

        public MainWindow()
        {
            string iconPath = IOPath.Combine(Directory.GetCurrentDirectory(), "Image", "icon.png");
            itemDescriptionRepository = new ItemDescriptionRepository();
            saleRepository = new SaleRepository(itemDescriptionRepository);
            ItemDescriptionsVM = new List<ItemDescriptionViewModel>();
            this.Icon = new BitmapImage(new Uri(iconPath));
            InitializeComponent();
            itemDescriptionRepository.LoadFromFile();
            saleRepository.LoadFromFile(DateTime.Now);
            foreach (ItemDescription item in (itemDescriptionRepository.GetAllDescriptions()))
            {
                ItemDescriptionsVM.Add(new ItemDescriptionViewModel(item));
            }

            SaleButton.Width = 350;
            SaleButton.Content = new Image
            {
                Source = new BitmapImage(new Uri(IOPath.Combine(Directory.GetCurrentDirectory(), "Image", "salgButtonImage.png"))),
                Width = 350,
                Height = 350
            };

            AssortmentButton.Width = 350;
            AssortmentButton.Content = new Image
            {
                Source = new BitmapImage(new Uri(IOPath.Combine(Directory.GetCurrentDirectory(), "Image", "sortimentButtonImage.png"))),
                Width = 350,
                Height = 350
            };
        }

        private void SaleButton_Click(object sender, RoutedEventArgs e)
        {
            ItemDescriptionsVM.Clear();
            foreach (ItemDescription item in (itemDescriptionRepository.GetAllDescriptions()))
            {
                ItemDescriptionsVM.Add(new ItemDescriptionViewModel(item));
            }
            saleWindow = new SaleWindow(itemDescriptionRepository, saleRepository, ItemDescriptionsVM);
            saleWindow.ShowDialog();
        }

        private void AssortmentButton_Click(object sender, RoutedEventArgs e)
        {
            //ItemDescriptionsVM.Clear();
            //foreach (ItemDescription item in (itemDescriptionRepository.GetAllDescriptions()))
            //{
            //    ItemDescriptionsVM.Add(new ItemDescriptionViewModel(item));
            //}
            assortmentWindow = new AssortmentWindow(itemDescriptionRepository);
            assortmentWindow.ShowDialog();
        }

        private void NotImplementedButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Denne funktion er ikke tilgængelig endnu. Kontakt support.", "Ikke tilgængelig!");
        }
    }
}