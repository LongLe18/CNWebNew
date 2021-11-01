using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class OrderModel
    {
        private DbCNWeb db = null;

        public OrderModel()
        {
            db = new DbCNWeb();
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Lấy danh sách đơn đặt hàng
        /// </summary>
        /// <returns></returns>
        public List<Order> ListOrder(int filter, string search)
        {
            var data = db.Orders.Where(x => x.Status == filter);
            if (filter == 6 || (filter == 6 && search != "")) // tất cả
            {
                data = db.Orders.Where(x => x.CustomerName.Contains(search) || x.ID.ToString().Contains(search));
            }
            if (filter != 6 && search != "")
            {
                data = data.Where(x => x.Status == filter && (x.CustomerName.Contains(search) || x.ID.ToString().Contains(search)));
            }
            return data.ToList();
        }
        public List<Order> SearchOrder_(string id, int userid)
        {
            return db.Orders.Where(i => i.ID.ToString().Contains(id)).Where(j=>j.CreatedByUserID == userid).ToList() ;
        }
        public List<Order> Order()
        {
            return db.Orders.ToList();
        }

        /// <summary>
        /// Cập nhật trạng thái đơn hàng
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool ChangeStatusOrder(int ID, int status)
        {
            try
            {
                var order = db.Orders.SingleOrDefault(x => x.ID == ID);
                order.Status = status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa đơn hàng
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int DeleteOrder(int ID)
        {
            var Order = db.Orders.Single(x => x.ID == ID);
            if (Order == null)
            {
                return 0; // Fail
            }
            else
            {
                db.Orders.Remove(Order);
                db.SaveChanges();
                return 1; // Success
            }
        }
        
        /// <summary>
        /// Lấy chi tiết hóa đơn bằng ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IQueryable<ListOrder> GetDetailOrderByID(int ID)
        {
            var data = (from a in db.Orders
                        join b in db.OrderFoodDetails on a.ID equals b.OrderID
                        join c in db.FoodOptions on b.FoodOptionID equals c.ID
                        join d in db.Foods on c.FoodID equals d.ID
                        where a.ID == ID
                        select new
                        {
                            ID = a.ID,
                            Status = a.Status,
                            Name = d.Name,
                            CreatedTime = a.CreatedTime,
                            Quantity = b.Quantity,
                            Price = d.OriginPrice,
                            Alias = d.Alias,
                            ToTalPrice = a.ToTalPrice
                        }).AsQueryable().Select(x => new ListOrder
                        {
                            ID = x.ID,
                            Status = x.Status,
                            NameFood = x.Name,
                            CreatedTime = x.CreatedTime,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            Alias = x.Alias,
                            ToTalPrice = x.ToTalPrice
                        });
            return data;
        }
    }
}
