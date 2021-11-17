using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class StaffModel
    {
        private DbCNWeb db = null;

        public StaffModel()
        {
            db = new DbCNWeb();
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Lấy danh sách là nhân viên
        /// </summary>
        /// <returns></returns>
        public IQueryable<ListStaff> ListAllStaff()
        {
            var data = (from a in db.RoleNames
                        join b in db.Users on a.ID equals b.IDChucVu
                        where b.Level != 1
                        select new
                        {
                            ID = b.ID,
                            FullName = b.FullName,
                            Email = b.Email,
                            Address = b.Address,
                            City = b.City,
                            Country = b.Country,
                            Career = b.Career,
                            aChucVu = a.TenChucVu
                        }).AsQueryable().Select(x => new ListStaff
                        {
                            ID = x.ID,
                            FullName = x.FullName,
                            Email = x.Email,
                            Address = x.Address,
                            City = x.City,
                            Country = x.Country,
                            Career = x.Career,
                            aChucVu = x.aChucVu
                        });
            return data;
        }

        /// <summary>
        /// Xóa nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteStaffById(int id)
        {
            var Staff = db.Users.Single(x => x.ID == id);  
            if (Staff == null)
            {
                return 0; // Fail
            }
            else 
            {
                var carts = (from a in db.Carts
                             where a.CreatedByUserID == id
                             select new
                             {
                                 CreatedByUserID = a.CreatedByUserID
                             }).AsEnumerable().Select(x => new
                             {
                                 CreatedByUserID = x.CreatedByUserID
                             });
                var cart = carts.Select(x => x.CreatedByUserID).ToList();
                if (cart.Count > 0) 
                {
                    return 1;
                } else
                {
                    db.Users.Remove(Staff);
                    db.SaveChanges();
                    return 2; // Success
                }
            }
        }


        /// <summary>
        /// Update Tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="fullname"></param>
        /// <param name="email"></param>
        /// <param name="birthday"></param>
        /// <param name="career"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="postalcode"></param>
        /// <param name="position"></param>
        /// <param name="description"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool UpdateInforStaff(int id, string username, string password, string fullname, string email, int birthday, string career, 
                                    string address, string city, string country, string phonenumber, int position, string description, int level)
        {
            try
            {
                var user = db.Users.SingleOrDefault(x => x.ID == id);
                user.UserName = username;
                user.Password = password;
                user.FullName = fullname;
                user.Email = email;
                user.BirthDay = birthday;
                user.Career = career;
                user.Address = address;
                user.City = city;
                user.Country = country;
                user.PhoneNumber = phonenumber;
                user.IDChucVu = position;
                user.Level = level;
                user.Description = description;
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
