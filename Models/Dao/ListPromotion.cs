using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ListPromotion
    {
        public int ID { get; set; }
        public int IDFood { get; set; }
        public string TypePromotion { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public int? PercentReduction { set; get; }
        public int? ActiveDay { set; get; }
        public int? EndDay { set; get; }
        public string Content { set; get; }
    }
}

