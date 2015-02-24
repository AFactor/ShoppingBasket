using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Basket.Entities;
using Basket.Repositories;
using Basket.Service;
using Moq;
using System.Collections.Generic;
namespace Basket.Test
{
    [TestClass]
    public class BasketServiceTest
    {

        BasketService _basketService;
        [TestInitialize]
        public void TestInitialize()
        {
            var mockPrice = MockDataSetup.SetMockPrices();
            var mockPromotion = MockDataSetup.SetMockPromotions();
            _basketService = new BasketService(mockPrice.Object, mockPromotion.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Set_Basket_with_Null()
        {
            _basketService.SetBasket(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Set_Basket_with_No_items()
        {
            _basketService.SetBasket(new List<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Set_Basket_with_Invalid_Format()
        {
            _basketService.SetBasket(new List<string>() { "Milk = 2" });
        }
        [TestMethod]
        [ExpectedException(typeof(System.IO.InvalidDataException))]
        public void Set_Basket_with_Invalid_Item()
        {
            _basketService.SetBasket(new List<string>() { "Potato,3" });
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Check_Basket_Item_appeared_Twice()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Butter,2", "Butter,1" });


        }

        [TestMethod]
        public void Set_Basket_with_Check_Items_Count()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Butter,1", "Milk,2" });
            //assert
            Assert.AreEqual(2, _basketService.BasketList.Count);
        }

        [TestMethod]
        public void Check_Basket_Total_Milk_1_Butter_1_Bread_1()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Milk,1", "Butter,1","Bread,1" });
            _basketService.CalculatePromotions();
            //assert
            Assert.AreEqual(2.95m, _basketService.Total);
        }

        [TestMethod]
        public void Check_Basket_Total_Milk_4()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Milk,4"});
            _basketService.CalculatePromotions();
            //assert
            Assert.AreEqual(3.45m, _basketService.Total);
        }
        [TestMethod]
        public void Check_Basket_Total_Butter_2_Bread_2()
        {
            //act
            _basketService.SetBasket(new List<string>() {  "Butter,2", "Bread,2" });
            _basketService.CalculatePromotions();
            //assert
            Assert.AreEqual(3.10m, _basketService.Total);
        }

        [TestMethod]
        public void Check_Basket_Total_Butter_2_Bread_1_Milk_8()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Butter,2", "Bread,1","Milk,8" });
            _basketService.CalculatePromotions();
            //assert
            Assert.AreEqual(9.00m, _basketService.Total);
        }


        
        


    }
}