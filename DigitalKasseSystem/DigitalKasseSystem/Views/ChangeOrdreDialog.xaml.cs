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
    /// Interaction logic for ChangeOrdreDialog.xaml
    /// </summary>
    public partial class ChangeOrdreDialog : Window
    {
        string newOrdreNumber = string.Empty;
        public int newCurrentOrdreNumber;

        public ChangeOrdreDialog()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int pressed = int.Parse(button.Tag.ToString());
                newOrdreNumber += pressed.ToString();
                InputTextBox.Text = newOrdreNumber;
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            newCurrentOrdreNumber = int.Parse(newOrdreNumber);
            if (newCurrentOrdreNumber < 100)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                DialogResult = false;
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            newOrdreNumber = newOrdreNumber.Remove(newOrdreNumber.Length - 1);
            InputTextBox.Text = newOrdreNumber;
        }
    }
}
