﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking
{
    internal class Phone : Asset
    {
        public Phone(string type, string brand, string model, DateTime purchaseDate, double price, string country)
        {
            Type = type;
            Brand = brand;
            Model = model;
            PurchaseDate = purchaseDate;
            Price = price;
            Country = country;
        }
    }
}
