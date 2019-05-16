using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Model.Transaction
{
    public class TransactionUpdate
    {
        public int TransactionID { get; set; }

        public string CustomerID { get; set; }

        public int ProductID { get; set; }

        public string FullName { get; set; }

        public string ProductName { get; set; }
    }
}