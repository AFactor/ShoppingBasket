using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basket.Repositories;
using Basket.Entities;

namespace Basket.Service
{
    /// <summary>
    /// Class for all things related to basket.
    /// I had a dilemma whether I will create a promotionservice for for all things related to promotion.
    /// But in the end I have decided to put it all in basketservice.
    /// </summary>
    public class BasketService
    {

        #region private properties
        
        private IPriceRepository _priceRepository;
        private IPromotionRepository _promotionRepository;
        private decimal? _subTotal;
        private decimal? _total;              
        private readonly List<ShopBasket> _basketList = new List<ShopBasket>();
        #endregion

        #region public methods and constructor

        public BasketService(IPriceRepository priceRepository, IPromotionRepository promoRepository)
        {
            _priceRepository = priceRepository;
            _promotionRepository = promoRepository;
        }

        


        /// <summary>
        /// This is the method to call when users update the basket from console
        /// </summary>
        /// <param name="items"></param>
        public void SetBasket(List<string> items)
        {
            
                if (items == null)
                    throw new ArgumentNullException("Items");
                if (items.Count == 0)
                    throw new ArgumentOutOfRangeException("Items", " Basket item count must be atleast one");
                
                
                foreach(var item in items)
                {
                    int quantity;
                    decimal price;
                    var line=item.Split(AppSettings.Get<char>("quantitySeparator"));
                    
                    //check length
                    if (!line.Length.Equals(2))
                        throw new ArgumentException("Invalid argument format. " + item);
                    //check qty format
                    if(!int.TryParse(line[1],out quantity))
                        throw new ArgumentException("Invalid argument. Quantity is not a valid number. " + item);
                    //check if item is in DB
                    if (_priceRepository.Get(line[0]) == null)
                        throw new System.IO.InvalidDataException(string.Format("Item {0} does not exist", line[0]));
                    else
                        price = _priceRepository.Get(line[0]).LinePrice;
                    //check duplicates.
                    if (_basketList.Exists(b=>b.ProductTitle.Equals(line[0])))
                        throw new ArgumentException(string.Format("Item {0} has appeared more than once", line[0]));
                    else
                        _basketList.Add(new ShopBasket(line[0], quantity, price));
                    
                
                
            }
            
        }
        /// <summary>
        /// Juice of the program.
        /// </summary>
        public void CalculatePromotions()
        {
            //set sub total and total first
            calculateSubTotal();

            var allPromos = _promotionRepository.GetAll();
            //In real life we will filter promotions by date range.            
            
            foreach (Promotion reward in allPromos)
            {

                //Get the combined dataset of Bucket and Basket. Make sure all products in Bucket has matched.
                var combo = from b in reward.BucketList
                        join l in _basketList on  b.ProductTitle.ToUpper() equals l.ProductTitle.ToUpper()
                        where l.Quantity>=b.Quantity 
                        select new {b.ProductTitle,b.Discount,b.Operator,b.Quantity,l.LinePrice,discountQualificationRatio=Math.Floor((decimal) l.Quantity/b.Quantity)};
                //full bucket with basket match check
                var fullBucketMatch = !reward.BucketList.Select(b => b.ProductTitle).Except(combo.Select(c => c.ProductTitle)).Any();
                var discountScale = combo.Count() > 0 && fullBucketMatch ? combo.Min(c => c.discountQualificationRatio) : 0;
                foreach (var line in combo)
                {
                    var absoluteDiscount = 0.0m;
                    switch (line.Operator)
                    {
                        case DiscountOperator.Fraction:
                            absoluteDiscount = line.LinePrice * line.Discount *line.Quantity * discountScale;
                            break;
                        case DiscountOperator.Absolute:
                            absoluteDiscount = line.Discount*discountScale;                           
                            break;
                        case DiscountOperator.ItemReduction:
                            absoluteDiscount = line.LinePrice * line.Discount * discountScale;
                            break;
                    }
                    _total = _total - absoluteDiscount;
                }
            }
        
        }

        #endregion

        #region public properties
        /// <summary>
        /// Encapsulation of Subtotal calculation in a public property
        /// </summary>
        public decimal SubTotal
        {
            get
            {
                if (!_subTotal.HasValue )
                    calculateSubTotal();
                return _subTotal.Value;
            }
        }

       
        public Decimal Total { get { return _total.HasValue ? _total.Value : 0; } }

        public List<ShopBasket> BasketList { get { return _basketList; } }
               

        #endregion


        private void calculateSubTotal()
        {

            _subTotal=_basketList.Sum(line => line.LinePrice * line.Quantity);            
            _total = _subTotal;
            
        }
    }
}