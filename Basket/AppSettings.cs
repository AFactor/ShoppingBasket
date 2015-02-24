using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel;
namespace Basket
{
    public class AppSettings
    {
        public static T Get<T>(string key)
        {
            
                var appSetting = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrWhiteSpace(appSetting)) throw new KeyNotFoundException(key + "not found");

                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)(converter.ConvertFromInvariantString(appSetting));
           
        }
    }
}
