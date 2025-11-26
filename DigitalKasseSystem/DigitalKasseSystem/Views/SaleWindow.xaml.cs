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
        public List<ItemDescriptionViewModel> ItemDescriptionsVM;

        public SaleWindow(ItemDescriptionRepository itemDescriptionRepository, SaleRepository saleRepository, List<ItemDescriptionViewModel> itemDescriptionViewModels)
        {
            this.itemDescriptionRepository = itemDescriptionRepository;
            this.saleRepository = saleRepository;
            this.ItemDescriptionsVM = itemDescriptionViewModels;
            InitializeComponent();
            InitializeAssortmentButtons();
        }

        private void InitializeAssortmentButtons()
        {
            if (itemDescriptionRepository != null)
            {
                foreach (ItemDescriptionViewModel itemVM in ItemDescriptionsVM)
                {
                    Button btn = new Button();
                    btn.Content = itemVM.Name + "\n" + itemVM.Price.ToString("C2");
                    btn.Tag = itemVM;
                    btn.Margin = new Thickness(5);
                    btn.Padding = new Thickness(10);
                    btn.Click click = ItemButton_Click;
                    MidWrapPanel.Children.Add(btn);
                }
            }
        }

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                //Name of button would be ItemButton_ + ItemNumber
                string[] itemParts = clickedButton.Name.ToString().Split('_');
                Item item = new Item(itemDescriptionRepository.GetItemDescription(int.Parse(itemParts[1])));
                MainSaleViewModel mainSaleVM = this.DataContext as MainSaleViewModel;
                mainSaleVM.CurrentSale.Basket.Add(item);

                Button button = new Button();
                button.Content = item.ItemDescription.ItemName;
                CurrentOrdreWindow.Children.Add(button);
            }
        }


        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EndSaleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotEndSaleButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
