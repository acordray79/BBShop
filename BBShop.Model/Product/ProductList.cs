using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Model.Product
{
    public class ProductList
    {
        public int ProductID { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int ProductQuantity { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
