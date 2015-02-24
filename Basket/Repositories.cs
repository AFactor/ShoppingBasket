using System;
using System.Collections.Generic;
using System.Linq;
using Basket.Entities;

namespace Basket.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private List<Price> _prices;

        public PriceRepository()
        {
            _prices = new List<Price>()
            {new Price( "Butter",0.80m),
            new Price( "Milk", 1.15m),            
             new Price("Bread",  1.00m)};
        }

        public Entities.Price Get(string identifier)
        {
            return _prices.FirstOrDefault(p => p.ProductTitle.Equals(identifier.Trim(),StringComparison.CurrentCultureIgnoreCase));
        }

        public List<Entities.Price> GetAll()
        {
            return _prices;

        }

    }
    public class PromotionRepository : IPromotionRepository
    {
        private List<Promotion> _promotions;

        public PromotionRepository()
        {

            var butterAndBreadPromo = new Promotion(1, "Buy 2 Butter and get a Bread at 50% off");
            butterAndBreadPromo.BucketList = new List<Bucket>() { new Bucket() { 
                ProductTitle = "Butter", Discount = 0, Quantity = 2, Operator = DiscountOperator.Absolute },
                new Bucket() {ProductTitle = "Bread", Discount = 0.5m, Quantity = 1, Operator = DiscountOperator.Fraction }
            };




            var milkPromo = new Promotion(2, "Buy 3 Milk and get the 4th milk for free");
            milkPromo.BucketList = new List<Bucket>() { 
                new Bucket() { ProductTitle = "milk", Discount = 1, Quantity = 4, Operator = DiscountOperator.ItemReduction } ,
                 };

            _promotions = new List<Promotion>() { butterAndBreadPromo, milkPromo };

        }

        public Entities.Promotion Get(int identifier)
        {
            throw new NotImplementedException();
        }

        public List<Entities.Promotion> GetAll()
        {
            return _promotions;
        }

    }
}
