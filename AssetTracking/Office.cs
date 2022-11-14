using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking
{
    internal class Office
    {
        public string Country { get; set; }

        // Method to get the currency from the specified country.
        public string CurrencyPerCountry(string country)
        {
            switch (country)
            {
                case "sweden":
                    return "SEK";
                case "spain":
                    return "EUR";
                case "usa":
                    return "USD";
                default:
                    return "Invalid country";
            }
        }

        // Method to convert an assets price depending on country.
        public double ConvertedCurrency(string country, double price)
        {
            switch (country)
            {
                case "sweden":
                    return price * 10.37;
                case "spain":
                    return price * 0.97;
                default:
                    return price;
            }
        }
    }
}
