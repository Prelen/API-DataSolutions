using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataSolution.Data.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
     
    }
}