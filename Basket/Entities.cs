using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Entities
{

    public class Price
    {
        public Price(string productTitle,  decimal linePrice)
        {

            ProductTitle = productTitle;            
            LinePrice = linePrice;

        }


        public  string ProductTitle { get; private set; }
        public  decimal LinePrice { get; private set; }
        
    }

    public class ShopBasket
    {
        public ShopBasket(string productTitle, int quantity, decimal linePrice)
        {
            ProductTitle = productTitle;
            Quantity = quantity;
            LinePrice = linePrice;

        }

        public  string ProductTitle { get; private set; }
        public int Quantity { get; set; }
        public  decimal LinePrice { get; private set; }
    }
    
    
    public class Promotion
    {

        public Promotion(int id, string description)
        {
            Id = id;
            Description = description;            
        }
        public int Id { get; set; }
        public string Description { get; set; }
        
        public List<Bucket> BucketList { get; set; }        
    }
    //Buckets are related products grouped in for a promotion
    //This will satisfy following promotions that I can think of
    //Buy x get y free.
    //1/3 off. 
    //Buy 2 get discount on something else.
    //x% off
    //This is where I have tied to make my solution as much futureproof as possible
    public class Bucket
    {
        public string ProductTitle { get; set; }
        //relative discount on lineprice
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public DiscountOperator Operator { get; set; }

    }

    
    public enum DiscountOperator
    {
        Fraction,
        Absolute,
        ItemReduction
    }

}
