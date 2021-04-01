using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductMaster.Models
{
    public class CustomOrderDetails
    {
        public int OrderId { get; set; }
        public string ProdName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentStatus { get; set; }
    }
}