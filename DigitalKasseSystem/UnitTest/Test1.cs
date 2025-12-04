using DigitalKasseSystem;
using DigitalKasseSystem.Models;
using DigitalKasseSystem.ViewModels;
using System.Globalization;
namespace UnitTest
{
    [TestClass]
    public sealed class Test1
    {
        ItemDescriptionRepository itemDescriptionRepository;
        SaleRepository saleRepository;

        MainSaleViewModel mainSaleViewModel;
        MainAssortmentViewModel mainAssortmentViewModel;

        ItemDescription fritterDescription, polselsvobDescription, kebabmixDescription;

        /*
        List<Item> basket1, basket2, basket3;
        Sale sale1, sale2, sale3;
        */

        [TestInitialize]
        public void Init()
        {
            //Arrange

            //repositories
            itemDescriptionRepository = new ItemDescriptionRepository();
            saleRepository = new SaleRepository(itemDescriptionRepository);

            //MainViewModels
            mainSaleViewModel = new MainSaleViewModel(itemDescriptionRepository, saleRepository);
            mainAssortmentViewModel = new MainAssortmentViewModel(itemDescriptionRepository);

            //ItemDescriptions to be added
            fritterDescription = new ItemDescription(1, "pommesFrits", 23.50, "grillmad", "..\\..\\..\\..\\DigitalKasseSystem\\Image\\Uldum Hal Menu billeder\\pommesFrits.png");
            polselsvobDescription = new ItemDescription(2, "PolselSvob", 30.50, "grillmad", "..\\..\\..\\..\\DigitalKasseSystem\\Image\\Uldum Hal Menu billeder\\PolselSvob.png");
            //ItemDescriptions to be added later
            kebabmixDescription = new ItemDescription(3, "kebabmix", 24.50, "grillmad", "..\\..\\..\\..\\DigitalKasseSystem\\Image\\Uldum Hal Menu billeder\\kebabmix.png");

            //adding of ItemDescriptions
            mainAssortmentViewModel.AddNewItemDescription(fritterDescription);
            mainAssortmentViewModel.AddNewItemDescription(polselsvobDescription);

            //fill MainSaleViewModel CurrentSale with a SaleViewModel instance, this particular object represents an ongoing sale
            mainSaleViewModel.NewSale();

            //add two Item's to CurrentSale
            mainSaleViewModel.NewItemToSale(1);
            mainSaleViewModel.NewItemToSale(2);

            //finally emulate the user currently having highlighted a specific ItemDescription in AssortmentWindow
            mainAssortmentViewModel.SelectedItemDescriptionVM = mainAssortmentViewModel.ItemDescriptionsVM[0];
        }

        [TestMethod]
        public void AddNewItemDescriptionTest()
        {
            //Act
            mainAssortmentViewModel.AddNewItemDescription(kebabmixDescription);

            //Assert
            Assert.AreEqual(kebabmixDescription, itemDescriptionRepository.GetItemDescription(kebabmixDescription.ItemNumber));
            Assert.AreEqual(kebabmixDescription.ItemNumber, mainAssortmentViewModel.ItemDescriptionsVM[2].ItemNumber);
        }

        [TestMethod]
        public void DeleteItemDescriptionTest()
        {
            //Act
            mainAssortmentViewModel.DeleteItemDescription();

            //Assert
            Assert.AreEqual(null, itemDescriptionRepository.GetItemDescription(fritterDescription.ItemNumber));
            Assert.AreEqual(false, mainAssortmentViewModel.ItemDescriptionsVM.Contains(mainAssortmentViewModel.SelectedItemDescriptionVM));
        }

        [TestMethod]
        public void NewItemToSaleTest()
        {
            //Act
            mainSaleViewModel.NewItemToSale(polselsvobDescription.ItemNumber);

            //Assert
            Assert.AreEqual(new Item(polselsvobDescription).ItemDescription, mainSaleViewModel.CurrentSale.Basket[2].ItemDescription);
        }

        [TestMethod]
        public void RemoveItemFromSaleTest()
        {
            //Act
            mainSaleViewModel.RemoveItemFromSale(fritterDescription.ItemNumber);
            mainSaleViewModel.RemoveItemFromSale(polselsvobDescription.ItemNumber);

            //Assert
            Assert.AreEqual(0, mainSaleViewModel.CurrentSale.Basket.Count);
        }

        [TestMethod]
        public void SetOrderNumberTest()
        {
            //Act
            mainSaleViewModel.SetOrderNumber(5);
            mainSaleViewModel.EndSale(PaymentMethod.Kontant);

            //Assert
            Assert.AreEqual(6, Sale.OrderNumber);
        }

        [TestMethod]
        public void EndSaleTest()
        {
            //Act
            mainSaleViewModel.EndSale(PaymentMethod.Kontant);

            //Assert
            Assert.AreEqual(1, saleRepository.GetSalesCount());
        }
    }
}
