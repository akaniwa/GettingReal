using DigitalKasseSystem.Models;
using DigitalKasseSystem.ViewModels;
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
    /// Interaction logic for SaleWindow.xaml
    /// </summary>
    public partial class SaleWindow : Window
    {
        ItemDescriptionRepository itemDescriptionRepository;
        SaleRepository saleRepository;
        public List<ItemDescriptionViewModel> ItemDescriptionsVM = new();

        public SaleWindow(ItemDescriptionRepository itemDescriptionRepository, SaleRepository saleRepository)
        {
            this.itemDescriptionRepository = itemDescriptionRepository;
            this.saleRepository = saleRepository;
            InitializeComponent();
            InitializeAssortmentButtons();
        }

        private void InitializeAssortmentButtons()
        {
            if (itemDescriptionRepository != null)
            {
                foreach (ItemDescription item in (itemDescriptionRepository.GetItemDescriptions()))
                {
                    ItemDescriptionsVM.Add(new ItemDescriptionViewModel(item));
                }
                foreach (ItemDescriptionViewModel itemVM in ItemDescriptionsVM)
                {
                    Button btn = new Button();
                    btn.Content = itemVM.ItemName + "\n" + itemVM.Price.ToString("C2");
                    btn.Tag = itemVM;
                    btn.Margin = new Thickness(5);
                    btn.Padding = new Thickness(10);
                    MidWrapPanel.Children.Add(btn);
                }
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
