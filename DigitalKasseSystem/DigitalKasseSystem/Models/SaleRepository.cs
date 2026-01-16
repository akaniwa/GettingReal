using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DigitalKasseSystem.Models
{
    public class SaleRepository
    {
        private int salesCountOnLoad = 0;

        private List<Sale> sales = new List<Sale>();
        ItemDescriptionRepository itemDescriptionRepository;

        public SaleRepository(ItemDescriptionRepository itemDescriptionRepository)
        {
            this.itemDescriptionRepository = itemDescriptionRepository;
            //LoadFromFile(DateTime.Now);
        }

        public void AddSale(Sale sale)
        {
            sales.Add(sale);
        }

        public List<Sale> GetSales()
        {
            return sales;
        }

        public Sale GetSale(long saleNumber)
        {
            foreach (Sale sale in sales)
            {
                if (sale.SaleNumber == saleNumber)
                {
                    return sale;
                }
            }
            return null;
        }

        public void SaveToFile()
        {
            string dateString = DateTime.Now.ToString("yyyyMMdd");
            string filePath = $"Sales{dateString}.csv";
            if (File.Exists(filePath))
            {
                using (StreamWriter outputFile = new StreamWriter(filePath, true))
                {
                    List<Sale> newSales = sales.GetRange(salesCountOnLoad, sales.Count - salesCountOnLoad);
                    foreach (Sale sale in newSales)
                    {
                        StringBuilder itemsBuilder = new StringBuilder();
                        for (int i = 0; i < sale.Basket.Count; i++)
                        {
                            itemsBuilder.Append(sale.Basket[i].ItemDescription.ItemNumber);
                            if (i < sale.Basket.Count - 1)
                            {
                                itemsBuilder.Append(",");
                            }
                        }
                        outputFile.WriteLine($"{sale.SaleNumber};{sale.Total};{sale.PaymentMethod};{sale.StartTime};{sale.EndTime};{sale.delivered};{itemsBuilder}");
                    }
                    outputFile.Close();
                }
            }
            else
            {
                StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine("Ordre nummer;Total;Betalingsmethode;Starttidspunkt;Sluttidspunkt;Udleveret;Vare (varenummere)");
                foreach (Sale sale in sales)
                {
                    StringBuilder itemsBuilder = new StringBuilder();
                    for (int i = 0; i < sale.Basket.Count; i++)
                    {
                        itemsBuilder.Append(sale.Basket[i].ItemDescription.ItemNumber);
                        if (i < sale.Basket.Count - 1)
                        {
                            itemsBuilder.Append(",");
                        }
                    }
                    writer.WriteLine($"{sale.SaleNumber};{sale.Total};{sale.PaymentMethod};{sale.StartTime};{sale.EndTime};{sale.delivered};{itemsBuilder}");
                }
                writer.Close();
            }
        }

        public void LoadFromFile(DateTime date)
        {
            sales.Clear();
            string dateString = date.ToString("yyyyMMdd");
            if (!File.Exists($"Sales{dateString}.csv"))
            {
                return;
            }
            StreamReader reader = new StreamReader($"Sales{dateString}.csv");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                if (parts[0] != "Ordre nummer")
                {
                    long saleNumber = long.Parse(parts[0]);
                    double total = double.Parse(parts[1]);
                    PaymentMethod payment = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), parts[2]);
                    DateTime startTime = DateTime.Parse(parts[3]);
                    DateTime endTime = DateTime.Parse(parts[4]);
                    bool delivered = bool.Parse(parts[5]);
                    List<Item> items = new List<Item>();
                    foreach (string itemPart in parts[6].Split(','))
                    {
                        ItemDescription itemDescription = itemDescriptionRepository.GetItemDescription(int.Parse(itemPart));
                        Item item = new Item(itemDescription);
                        items.Add(item);
                    }
                    Sale sale = new Sale(saleNumber, total, payment, startTime, endTime, items, delivered);
                    AddSale(sale);
                }
            }
            reader.Close();
            salesCountOnLoad = sales.Count;
        }
    }
}