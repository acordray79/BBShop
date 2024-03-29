﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Model.Product
{
    public class ProductCreate
    {
        public int ProductID { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Product Quantity")]
        public int ProductQuantity { get; set; }
    }
}
