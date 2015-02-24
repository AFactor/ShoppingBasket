﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Service;
using Basket.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
namespace Basket
{
    public static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //resolve reference
                var container = new UnityContainer().LoadConfiguration();
                IPriceRepository priceRepository = container.Resolve<IPriceRepository>();
                IPromotionRepository promoRepository = container.Resolve<IPromotionRepository>();
                IOutputWriter writer = container.Resolve<IOutputWriter>();

                WriteConsoleOutput(args, writer, priceRepository, promoRepository);
            }
            catch(Exception ex)
            {
                Console.WriteLine("An Error has occurred. " + ex.Message);
                
            }
            finally
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            }
        }

        public static void WriteConsoleOutput(string[] args, IOutputWriter writer, IPriceRepository priceRep, IPromotionRepository promoRep)
        {
            //validate argument. 
            string argument=string.Empty;
            if (!args.Count().Equals(1))
            {
                var message = string.Format(AppSettings.Get<string>("MissingArgText"), AppSettings.Get<char>("ItemSeparator"), AppSettings.Get<char>("QuantitySeparator"));    
                writer.WriteLine(message);
                argument = writer.ReadLine();
                
                    
            }
            else
                argument = args[0];
            //prepare data
            if (!string.IsNullOrEmpty(argument) )
            {
                var itemList = argument.Split(AppSettings.Get<char>("ItemSeparator")).ToList();
            
                var output = new StringBuilder();
                BasketService service = new BasketService(priceRep, promoRep);
                service.SetBasket(itemList);
                service.CalculatePromotions();
                writer.WriteLine(string.Format("Total Before discount: {0}", service.SubTotal.ToString("c")));            
                writer.WriteLine("Total: " + service.Total.ToString("c"));
            }


        }
    }
}




 
