using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Model.Transaction
{
    public class TransactionDetail
    {
        public int TransactionID { get; set; }

        public string FullName { get; set; }

        public string ProductName { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
