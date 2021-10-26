using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class DashboardModel
    {
        private DbCNWeb db = null;

        public DashboardModel()
        {
            db = new DbCNWeb();
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Lấy số lượng user
        /// </summary>
        /// <returns></returns>
        public int GetNumberUser()
        {
            return db.Users.Count();
        }

        /// <summary>
        /// Lấy tổng số lượng đơn hàng 
        /// </summary>
        /// <returns></returns>
        public int GetNumberOrder()
        {
            return db.Orders.Count();
        }

    }
}
