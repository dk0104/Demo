using System;
using System.Collections.Generic;

namespace Model
{
    internal class Order
    {
        public Order()
        {
            this.Products=new Dictionary<string, Product>();
        }
        public DateTime DateTime { get; set; }
        public Dictionary<string,Product> Products { get; set; } 
        public Guid SerialNumber { get; set; }
    }
}