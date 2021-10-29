using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models.Dao;
using System.Web.Mvc;

namespace CNWeb.API
{
    public class AdminController : ApiController
    {
        // [GET] api/Admin/GetSales
        public IEnumerable<Order> GetSales()
        {
            var dao = new OrderModel();
            var data = dao.Order().ToList();
            return data;
        }

        // [GET] api/Admin/GetNumberUser
        public int GetNumberUser()
        {
            var dao = new DashboardModel();
            var data = dao.GetNumberUser();
            return data;
        }

        // [GET] api/Admin/GetNumberOrder
        public int GetNumberOrder()
        {
            var dao = new DashboardModel();
            var data = dao.GetNumberOrder();
            return data;
        }

        // [GET] api/Admin/GetListBlogs
        public IEnumerable<ListBlog> GetListBlogs(string filter, string search)
        {
            var dao = new BlogModel();
            var blogs = dao.ListBlog(Int32.Parse(filter), search).ToList();
            return blogs;
        }
    }
}
