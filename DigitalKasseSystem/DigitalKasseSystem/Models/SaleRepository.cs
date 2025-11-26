using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKasseSystem.Models
{
    class SaleRepository
    {
        private List<Sale> sales = new List<Sale>();
        ItemDescriptionRepository itemDescriptionRepository;

        public SaleRepository(ItemDescriptionRepository itemDescriptionRepository)
        {
            this.itemDescriptionRepository = itemDescriptionRepository;
            LoadFromFile(DateTime.Now);
        }

        public void AddSale(Sale sale)
        {
            sales.Add(sale);
        }

        public int GetSalesCount()
        {
            return sales.Count;
        }

        public void SaveToFile()
        {
            string dateString = DateTime.Now.ToString("yyyyMMdd");
            StreamWriter writer = new StreamWriter($"Sales{dateString}.txt");
            foreach (Sale sale in sales)
            {
                writer.WriteLine(sale.ToString());
            }
            writer.Close();
        }

        public void LoadFromFile(DateTime date)
        {
            string dateString = date.ToString("yyyyMMdd");
            if (!File.Exists($"Sales{dateString}.txt"))
            {
                return;
            }
            StreamReader reader = new StreamReader($"Sales{dateString}.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                int saleNumber = int.Parse(parts[0]);
                double total = double.Parse(parts[1]);
                PaymentMethod payment = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), parts[2]);
                DateTime startTime = DateTime.Parse(parts[3]);
                DateTime endTime = DateTime.Parse(parts[4]);
                List<Item> items = new List<Item>();
                foreach (string itemPart in parts[5].Split(','))
                {
                    ItemDescription itemDescription = itemDescriptionRepository.GetItemDescription(int.Parse(itemPart));
                    Item item = new Item(itemDescription);
                    items.Add(item);
                }
                Sale sale = new Sale(saleNumber, total, payment, startTime, endTime, items);
            }
        }
    }
}