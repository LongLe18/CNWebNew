using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Dao;

namespace CNWeb.Models
{
    public class ViewDetailBlogModel
    {
        public IList<ListBlog> data { get; set; }
    }
}