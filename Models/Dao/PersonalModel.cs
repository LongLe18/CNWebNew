using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class PersonalModel
    {
        private DbCNWeb db = null;

        public PersonalModel()
        {
            db = new DbCNWeb();
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Get Information Staff By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetStaffById(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }

        /// <summary>
        /// Update Info Staff
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInforStaff(User entity)
        {
            try
            {
                var user = db.Users.SingleOrDefault(x => x.ID == entity.ID);
                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.FullName = entity.FullName;
                user.Email = entity.Email;
                user.BirthDay = entity.BirthDay;
                user.Career = entity.Career;
                user.Address = entity.Address;
                user.City = entity.City;
                user.Country = entity.Country;
                user.PhoneNumber = entity.PhoneNumber;
                user.IDChucVu = entity.IDChucVu;
                user.Level = entity.Level;
                user.Description = entity.Description;
                db.SaveChanges();
                return true;
            } catch 
            {
                return false;
            }           
        }
    }
}
