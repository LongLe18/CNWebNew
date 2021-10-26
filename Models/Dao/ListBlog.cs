using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ListBlog
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content{ get; set; }
        public long? CreatedDay { set; get; }
        public string UserName { set; get; }
        public string pathImage { set; get; }
        public int? Status { set; get; }
    }
}
