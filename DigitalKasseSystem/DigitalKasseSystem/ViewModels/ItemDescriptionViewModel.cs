using DigitalKasseSystem.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace DigitalKasseSystem.ViewModels
{
    public class ItemDescriptionViewModel : INotifyPropertyChanged
    {
        private readonly ItemDescription itemDescription;

        private int itemNumber;
        private string name = string.Empty;
        private double price;
        private string picturePath = string.Empty;
        private string category = string.Empty;
        private BitmapImage? picture;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ItemDescriptionViewModel(ItemDescription itemDescription)
        {
            this.itemDescription = itemDescription ?? throw new ArgumentNullException(nameof(itemDescription));

            // copy model -> vm backing fields
            itemNumber = itemDescription.ItemNumber;
            name = itemDescription.ItemName ?? string.Empty;
            price = itemDescription.Price;
            category = itemDescription.Category ?? string.Empty;
            picturePath = itemDescription.PicturePath ?? string.Empty;

            UpdatePicture(picturePath);
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public int ItemNumber
        {
            get => itemNumber;
            set
            {
                if (itemNumber == value) return;
                itemNumber = value;
                itemDescription.ItemNumber = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;
                name = value;
                itemDescription.ItemName = value;
                OnPropertyChanged();
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (price.Equals(value)) return;
                price = value;
                itemDescription.Price = value;
                OnPropertyChanged();
            }
        }

        public string PicturePath
        {
            get => picturePath;
            set
            {
                if (picturePath == value) return;
                picturePath = value;
                itemDescription.PicturePath = value;
                UpdatePicture(picturePath);
                OnPropertyChanged();
                OnPropertyChanged(nameof(Picture));
            }
        }

        public string Category
        {
            get => category;
            set
            {
                if (category == value) return;
                category = value;
                itemDescription.Category = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage? Picture
        {
            get => picture;
            private set
            {
                if (picture == value) return;
                picture = value;
                OnPropertyChanged();
            }
        }

        private void UpdatePicture(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    Picture = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    Picture = null;
                }
            }
            catch
            {
                Picture = null;
            }
        }

        // Create a new immutable ItemDescription from current VM values.
        // Call this when you want to persist changes back to your repository.
        public ItemDescription ToModel()
        {
            return new ItemDescription(ItemNumber, Name, Price, Category, PicturePath);
        }
    }
}
