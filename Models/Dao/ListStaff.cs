using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ListStaff
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { set; get; }
        public string City { set; get; }
        public string Country { set; get; }
        public string Career { set; get; }
        public string aChucVu { set; get; }
    }
}