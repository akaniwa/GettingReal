using DigitalKasseSystem.Models;
using DigitalKasseSystem.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AssortmentWindow.xaml
    /// </summary>
    public partial class AssortmentWindow : Window
    {
        MainAssortmentViewModel mavm;

        public AssortmentWindow(ItemDescriptionRepository itemDescriptionRepository)
        {
            InitializeComponent();
            mavm = new MainAssortmentViewModel(itemDescriptionRepository);
            DataContext = mavm;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            mavm.SaveAssortment();
            DialogResult = true;
            this.Close();
        }

        private void ChoosePicButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                if (mavm.SelectedItemDescriptionVM != null)
                {
                    PicturePathBox.Text = openFileDialog.FileName;
                    mavm.SelectedItemDescriptionVM.PicturePath = openFileDialog.FileName;
                }
            }
        }
    }
}
