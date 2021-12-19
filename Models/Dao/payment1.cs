using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
   public class payment1
    {
        public int Wallet { get; set; }
        public int OriginPrice { get; set; }

        public int? Discount { get; set; }
        public int? FinalPrice { get; set; }
        public int Shipping { get; set; }
        public int VAT { get; set; }
        public int? Balance { get; set; }
    }
}
