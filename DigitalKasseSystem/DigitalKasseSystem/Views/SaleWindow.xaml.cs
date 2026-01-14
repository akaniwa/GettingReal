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
                        Width = 120,
                        Height = 100
                    };
                    TextBlock itemName = new TextBlock();
                    itemName.Width = 120;
                    itemName.Height = 50;
                    itemName.TextWrapping = TextWrapping.Wrap;
                    itemName.FontSize = 18;
                    itemName.Text = ($"{itemVM.ItemNumber}) {itemVM.ItemName}");
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
                mainSaleViewModel.CurrentSale.Basket.Add(item);

                // If type of item is already in basket
                if (mainSaleViewModel.CurrentSale.Basket.Count(basketItem => basketItem.ItemDescription.ItemNumber == item.ItemDescription.ItemNumber) > 1)
                {
                    CurrentOrdreWindow.Children.OfType<Button>().ToList().ForEach(button =>
                    {
                        if (button.Tag is Item buttonItem && buttonItem.ItemDescription.ItemNumber == item.ItemDescription.ItemNumber)
                        {
                            int itemCount = mainSaleViewModel.CurrentSale.Basket.Count(basketItem => basketItem.ItemDescription.ItemNumber == item.ItemDescription.ItemNumber);
                            button.Content = ($"{item.ItemDescription.ItemName} x{itemCount} - { (item.ItemDescription.Price * itemCount).ToString("C2")}");
                        }
                    });
                }
                else
                {
                    // If type of item is not in basket
                    Button button = new Button();
                    button.FontSize = 20;
                    button.Tag = item;
                    button.Content = ($"{item.ItemDescription.ItemName} - {item.ItemDescription.Price.ToString("C2")}");
                    button.Height = 80;
                    button.Click += InCartItem_Click;
                    CurrentOrdreWindow.Children.Add(button);
                }
            }
            UpdateTotalLabel();
        }

        private void InCartItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                Item item = (Item)clickedButton.Tag;
                foreach (Item basketItem in mainSaleViewModel.CurrentSale.Basket.ToList())
                {
                    if (basketItem.ItemDescription.ItemNumber == item.ItemDescription.ItemNumber)
                    {
                        mainSaleViewModel.CurrentSale.Basket.Remove(basketItem);
                        break;
                    }
                }
                if (mainSaleViewModel.CurrentSale.Basket.Count(basketItem => basketItem.ItemDescription.ItemNumber == item.ItemDescription.ItemNumber) >= 1)
                {
                    int itemCount = mainSaleViewModel.CurrentSale.Basket.Count(basketItem => basketItem.ItemDescription.ItemNumber == item.ItemDescription.ItemNumber);
                    if (itemCount == 1)
                        clickedButton.Content = ($"{item.ItemDescription.ItemName} - {item.ItemDescription.Price.ToString("C2")}");
                    else
                        clickedButton.Content = ($"{item.ItemDescription.ItemName} x{itemCount} - { (item.ItemDescription.Price * itemCount).ToString("C2")}");
                }
                else
                {
                    CurrentOrdreWindow.Children.Remove(clickedButton);
                }
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
            Button saleReferenceButton = new Button();
            saleReferenceButton.Click += SaleReferenceButton_Click;
            saleReferenceButton.HorizontalContentAlignment = HorizontalAlignment.Left;
            saleReferenceButton.Margin = new Thickness(5);
            StackPanel sp = new StackPanel();

            TextBlock Titel = new TextBlock();
            Titel.TextAlignment = TextAlignment.Left;
            Titel.Margin = new Thickness(0, 0, 0, 10);
            Titel.FontSize = 20;
            Titel.Text = ($"Ordre #{saleNumber.ToString()}");

            TextBlock mainText = new TextBlock();
            mainText.TextAlignment = TextAlignment.Left;
            mainText.Margin = new Thickness(0, 0, 0, 10);
            mainText.FontSize = 16;
            List<Item> basket = mainSaleViewModel.CurrentSale.Basket;
            foreach (Item item in basket)
            {
                mainText.Text += ($"{item.ItemDescription.ItemName} - {item.ItemDescription.Price} kr.\n");
            }
            mainText.Text += ($"\nTotal: {mainSaleViewModel.CurrentSale.Total.ToString("C2")}");

            sp.Margin = new Thickness(10);
            sp.Children.Add(Titel);
            sp.Children.Add(mainText);
            saleReferenceButton.Content = sp;

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
            changeOrdreDialog.Owner = this;
            changeOrdreDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
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
