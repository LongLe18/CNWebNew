using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ListOrder
    {
        public int ID { set; get; }
        public int? Status { set; get; }
        public long? CreatedTime { set; get; }
        public int? Quantity { set; get; }
        public string NameFood { set; get; }
        public int? Price { set; get; }
        public string Alias { set; get; }
        public int? ToTalPrice { set; get; }
    }
}
