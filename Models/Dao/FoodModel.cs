using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class FoodModel
    {
        private DbCNWeb db = null;

        public FoodModel()
        {
            db = new DbCNWeb();
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// List Food Category
        /// </summary>
        /// <returns></returns>
        public List<FoodCategory> ListFoodCategory()
        {
            return db.FoodCategories.ToList();
        }

        /// <summary>
        /// Get Food Category By Id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public FoodCategory GetFoodCateByID(int ID) 
        {
            return db.FoodCategories.SingleOrDefault(x => x.ID == ID);
        }

        /// <summary>
        /// Update Food Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <param name="createddate"></param>
        /// <param name="createdby"></param>
        /// <param name="updateddate"></param>
        /// <param name="updatedby"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateCateFood(int id, string name, int updateddate, string updatedby,
                                    int status)
        {
            try
            {
                var cate = db.FoodCategories.SingleOrDefault(x => x.ID == id);
                cate.Name = name;
                cate.UpdatedDate = updateddate;
                cate.UpdatedBy = updatedby;
                cate.Status = status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add Food Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="createddate"></param>
        /// <param name="createdby"></param>
        /// <param name="updateddate"></param>
        /// <param name="updatedby"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool AddCateFood(string name, int createddate, string createdby, int updateddate, string updatedby,
                                    int status)
        {
            try
            {
                var countRecord = db.FoodCategories.Count();
                var cate = new FoodCategory();
                cate.Name = name;
                cate.CreatedDate = createddate;
                cate.DisplayOrder = countRecord;
                cate.CreatedBy = createdby;
                cate.UpdatedDate = updateddate;
                cate.UpdatedBy = updatedby;
                cate.Status = status;
                db.FoodCategories.Add(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
                //return false;
            }
        }
    }
}
