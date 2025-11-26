using DigitalKasseSystem.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace DigitalKasseSystem.ViewModels
{
    public class ItemDescriptionViewModel : INotifyPropertyChanged
    {
        private ItemDescription itemDescription;
        void OnPropertyChanged([CallerMemberName] string? n = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));

        public int ItemNumber
        {
            get { return itemDescription.ItemNumber; }
            set { itemDescription.ItemNumber = value; }
        }
        public string Name
        {
            get { return itemDescription.ItemName; }
            set { itemDescription.ItemName = value; }
        }
        public double Price
        {
            get { return itemDescription.Price; }
            set { itemDescription.Price = value; }
        }
        public string PicturePath
        {
            get { return itemDescription.PicturePath; }
            set 
            { 
                itemDescription.PicturePath = value;
                if (!string.IsNullOrEmpty(value))
                {
                    Picture = new BitmapImage(new Uri(value, UriKind.RelativeOrAbsolute));
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Picture));
                }
                else
                {
                    Picture = new BitmapImage(new Uri("C:\\Users\\marti\\OneDrive\\Documents\\Datamatiker\\Git\\GettingReal\\DigitalKasseSystem\\DigitalKasseSystem\\Image\\MissingImageIcon.png"));
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Picture));
                }
            }
        }
        public string Categori
        {
            get { return itemDescription.Category; }
            set { itemDescription.Category = value; }
        }
        public BitmapImage Picture;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ItemDescriptionViewModel(ItemDescription itemDescription)
        {
            this.itemDescription = itemDescription;
            ItemNumber = itemDescription.ItemNumber;
            Name = itemDescription.ItemName;
            Price = itemDescription.Price;
            PicturePath = itemDescription.PicturePath;
            Categori = itemDescription.Category;
        }
    }
}
