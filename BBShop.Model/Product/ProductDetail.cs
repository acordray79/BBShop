using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Model.Product
{
    public class ProductDetail
    {
        public int ProductID { get; set; }

        public Guid OwnerID { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int ProductQuantity { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

