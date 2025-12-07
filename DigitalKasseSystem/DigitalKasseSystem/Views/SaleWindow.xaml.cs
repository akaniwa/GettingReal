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
        MainSaleViewModel mainSaleViewModel;
        public List<ItemDescriptionViewModel> ItemDescriptionsVM;

        public SaleWindow(ItemDescriptionRepository itemDescriptionRepository, SaleRepository saleRepository, List<ItemDescriptionViewModel> itemDescriptionViewModels)
        {
            this.itemDescriptionRepository = itemDescriptionRepository;
            this.saleRepository = saleRepository;
            this.ItemDescriptionsVM = itemDescriptionViewModels;
            mainSaleViewModel = new MainSaleViewModel(itemDescriptionRepository, saleRepository);
            InitializeComponent();
            InitializeAssortmentButtons();
            saleRepository.LoadFromFile(DateTime.Now);
            foreach (Sale sale in saleRepository.GetSales())
            {
                QuickOrderInstanisiate();
            }
            DataContext = mainSaleViewModel;
            mainSaleViewModel.NewSale();
            UpdateTotalLabel();
        }

        private void InitializeAssortmentButtons()
        {
            if (itemDescriptionRepository != null)
            {
                foreach (ItemDescriptionViewModel itemVM in ItemDescriptionsVM)
                {
                    Button btn = new Button();
                    StackPanel sp = new StackPanel();
                    btn.Name = "ItemButton_" + itemVM.ItemNumber.ToString();
                    Image itemPic = new Image
                    {
                        Source = new BitmapImage(new Uri(itemVM.PicturePath, UriKind.RelativeOrAbsolute)),
                        Width = 100,
                        Height = 100
                    };
                    Label itemName = new Label();
                    itemName.FontSize = 16;
                    itemName.Content = ($"{itemVM.ItemNumber}) {itemVM.ItemName}");
                    sp.Children.Add(itemPic);
                    sp.Children.Add(itemName);
                    btn.Content = sp;
                    btn.Tag = itemVM;
                    btn.Margin = new Thickness(5);
                    btn.Padding = new Thickness(10);
                    btn.Click += ItemButton_Click;
                    MidWrapPanel.Children.Add(btn);
                }
            }
        }

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                //ItemName of button would be ItemButton_ + ItemNumber
                string[] itemParts = clickedButton.Name.ToString().Split('_');
                Item item = new Item(itemDescriptionRepository.GetItemDescription(int.Parse(itemParts[1]))); // Change to ItemDesciptionVM

                Button button = new Button();
                button.FontSize = 20;
                button.Tag = item;
                button.Content = ($"{item.ItemDescription.ItemName} - {item.ItemDescription.Price.ToString("C2")}");
                button.Height = 80;
                button.Click += InCartItem_Click;
                mainSaleViewModel.CurrentSale.Basket.Add(item);
                CurrentOrdreWindow.Children.Add(button);
            }
            UpdateTotalLabel();
        }

        private void InCartItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                Item item = (Item)clickedButton.Tag;
                mainSaleViewModel.CurrentSale.Basket.Remove(item);
                CurrentOrdreWindow.Children.Remove(clickedButton);
            }
            UpdateTotalLabel();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void UpdateTotalLabel()
        {
            TotalLabel.Content = ($"Total: {mainSaleViewModel.CurrentSale.Total.ToString("C2")}");
            CurrentOrdreLabel.Content = ($"Ordre #{Sale.OrderNumber.ToString("D2")}");
        }

        // Makes new button for the completed ordre
        private void QuickOrderInstanisiate()
        {
            string saleNumber = Sale.OrderNumber.ToString("D2");
            List<Item> basket = mainSaleViewModel.CurrentSale.Basket;
            Button saleReferenceButton = new Button();
            saleReferenceButton.FontSize = 16;
            saleReferenceButton.Content = ($"Ordre #{saleNumber.ToString()}\n");
            saleReferenceButton.Click += SaleReferenceButton_Click;
            foreach (Item item in basket)
            {
                saleReferenceButton.Content += ($"{item.ItemDescription.ItemName} - {item.ItemDescription.Price} kr.\n");
            }
            saleReferenceButton.Content += ($"\nTotal: {mainSaleViewModel.CurrentSale.Total.ToString("C2")}");
            QuickOrderWindow.Children.Add(saleReferenceButton);
            QuickOrderWindow.Children.Add(new Separator());
        }

        private void SaleReferenceButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int separator = QuickOrderWindow.Children.IndexOf(button);
                QuickOrderWindow.Children.RemoveAt(separator + 1);
                QuickOrderWindow.Children.Remove(button);
            }
        }

        private void EndSaleButton_Click(object sender, RoutedEventArgs e)
        {
            PaymentPopup paymentDialog = new PaymentPopup(mainSaleViewModel.CurrentSale.Total);
            paymentDialog.Owner = this;
            paymentDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            paymentDialog.ShowDialog();
            if (paymentDialog.DialogResult == true) // If they paid full
            {
                int saleNumber = int.Parse(DateTime.Now.ToString("ddMMyy") + +Sale.OrderNumber); // Missing internal number
                double total = mainSaleViewModel.CurrentSale.Total;
                PaymentMethod paymentMethod = mainSaleViewModel.CurrentSale.Payment;
                DateTime startTime = mainSaleViewModel.CurrentSale.StartTime;
                DateTime endTime = DateTime.Now;
                List<Item> basket = mainSaleViewModel.CurrentSale.Basket;
                QuickOrderInstanisiate();
                Sale sale = new Sale(saleNumber, total, paymentMethod, startTime, endTime, basket);
                saleRepository.AddSale(sale);

                saleRepository.SaveToFile();

                mainSaleViewModel.CurrentSale = new SaleViewModel();
                CurrentOrdreWindow.Children.Clear();
                UpdateTotalLabel();
            }
            else if (paymentDialog.DialogResult == false) // If missing amount to pay
            {
                MessageBox.Show("Beløbet betalt er mindre end total beløbet.", "Mangler betaling!");
            }
        }

        // Method for cancel button
        private void NotEndSaleButton_Click(object sender, RoutedEventArgs e)
        {
            mainSaleViewModel.CurrentSale = new SaleViewModel();
            CurrentOrdreWindow.Children.Clear();
            UpdateTotalLabel();
        }

        private void ChangeCurrentOrdreNumberButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeOrdreDialog changeOrdreDialog = new ChangeOrdreDialog();
            changeOrdreDialog.ShowDialog();
            if (changeOrdreDialog.DialogResult == true)
            {
                Sale.OrderNumber = changeOrdreDialog.newCurrentOrdreNumber;
                UpdateTotalLabel();
            }
            else
            {
                MessageBox.Show("Indtastet nummer er højere end 99, tjek korrekte nummer er indtastet", "Indtastet er højere end forventet");
            }
        }
    }
}
