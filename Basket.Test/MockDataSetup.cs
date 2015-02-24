using System;
using System.Collections.Generic;
using Basket.Entities;
using Basket.Repositories;
using Moq;
namespace Basket.Test
{
    public static class MockDataSetup
    {
        public static Mock<IPriceRepository> SetMockPrices()
        {
            var mockPrice = new Mock<IPriceRepository>();
                        
            var butterLineItem = new Price( "Butter", 0.80m);
            var breadLineItem = new Price( "Bread",  1.00m);
            var milkLineItem = new Price("Milk", 1.15m);


            mockPrice.Setup(mp => mp.Get("Butter")).Returns(butterLineItem);
            mockPrice.Setup(mp => mp.Get("Milk")).Returns(milkLineItem);
            mockPrice.Setup(mp => mp.Get("Bread")).Returns(breadLineItem);

            return mockPrice;
        }
        public static Mock<IPromotionRepository> SetMockPromotions()
        {
            var mockPromotion = new Mock<IPromotionRepository>();
            var butterAndBreadPromo = new Promotion(1, "Buy 2 Butter and get a Bread at 50% off");
            butterAndBreadPromo.BucketList = new List<Bucket>() { new Bucket() { 
                ProductTitle = "Butter", Discount = 0, Quantity = 2, Operator = DiscountOperator.Absolute },
                new Bucket() {ProductTitle = "Bread", Discount = 0.5m, Quantity = 1, Operator = DiscountOperator.Fraction }
            };




            var milkPromo = new Promotion(2, "Buy 3 Milk and get the 4th milk for free");
            milkPromo.BucketList = new List<Bucket>() { 
                new Bucket() { ProductTitle = "milk", Discount = 1, Quantity = 4, Operator = DiscountOperator.ItemReduction } ,
                 };


            mockPromotion.Setup(mpro => mpro.GetAll()).Returns(new List<Promotion>() { butterAndBreadPromo, milkPromo });
            return mockPromotion;
        }
      

        
    }
}