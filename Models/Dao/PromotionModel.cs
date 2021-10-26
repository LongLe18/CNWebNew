using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class PromotionModel
    {
        private DbCNWeb db = null;

        public PromotionModel()
        {
            db = new DbCNWeb();
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Lấy danh sách khuyến mãi
        /// </summary>
        /// <returns></returns>
        public List<Promotion> ListPromotion(int filter, string search)
        {
            var data = db.Promotions.Where(x => x.Status == filter);
            if (filter == 2 || (filter == 2 && search != "")) // tất cả
            {
                data = db.Promotions.Where(x => x.Name.Contains(search) || x.ID.ToString().Contains(search));
            }
            if (filter != 2 && search != "")
            {
                data = data.Where(x => x.Status == filter && (x.Name.Contains(search) || x.ID.ToString().Contains(search)));
            }
            return data.ToList();
        }

        /// <summary>
        /// Lấy chi tiết khuyến mãi theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idFood"></param>
        /// <returns></returns>
        public IQueryable<ListPromotion> GetDetailPromotionByID(int id)
        {
            var data = (from a in db.Promotions
                        join c in db.PromotionTypes on a.TypeID equals c.ID
                        select new
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Status = a.Status,
                            ActiveDay = a.ActiveDay,
                            EndDay = a.EndDay,
                            Content = a.Content,
                            TypeReduction = c.Name
                        }).AsQueryable().Select(x => new ListPromotion
                        {
                            ID = x.ID,
                            Name = x.Name,
                            Status = x.Status,
                            ActiveDay = x.ActiveDay,
                            EndDay = x.EndDay,
                            Content = x.Content,
                            TypePromotion = x.TypeReduction
                        });
            var detail = data.Where(x => x.ID == id);
            return detail;
        }

        /// <summary>
        /// Kích hoạt hoặc dừng khuyến mãi
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public bool ChangeStatusPromotion(int ID, int status)
        {
            try
            {
                var promotion = db.Promotions.SingleOrDefault(x => x.ID == ID);
                if (status == 0) promotion.Status = 1;
                else promotion.Status = 0;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
