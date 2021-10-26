using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.EF
{
    public class CommentModel
    {
        public string name { get; set; }
        public string image { get; set; }
        public string content { get; set; }
        public long? createdDate { get; set; }
    }
}