using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class AccountModel
    {
        private DbCNWeb db = null;

        public AccountModel()
        {
            db = new DbCNWeb();
        }

        /// <summary>
        /// Check Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string userName, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0; // tai khoan k ton tai
            } else // tai khoan ton tai
            {
                if (result.Level == 0) // USER
                {
                    if (result.Password == password)
                        return 1; // OK
                    else
                        return -1; // password sai
                } else // ADMIN
                {
                    if (result.Password == password)
                        return 2; // OK
                    else
                        return -1; // password sai
                }                           
            }
        }

        // Get USER by ID
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        /// <summary>
        /// Insert Account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Register(User user)
        {
            // TODO: Kiem tra neu tai khoan da ton tai
            var check = db.Users.SingleOrDefault(x => x.UserName == user.UserName);
            if (check == null) // chua co tai khoan nao giong'
            {
                db.Users.Add(user);
                db.SaveChanges();
                return 1;
            }
            else // da co tai khoan giong
            {
                return -1;
            }

        }

        /// <summary>
        /// Get All ChucVu in database
        /// </summary>
        /// <returns></returns>
        public List<RoleName> ListAllChucVu()
        {
            return db.RoleNames.Where(x => x.ID != 4).ToList();
        }

        /// <summary>
        /// Lấy ra nhóm quyền tương ứng với userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                       join b in db.UserGroups on a.UserGroupID equals b.ID
                       join c in db.Roles on a.RoleID equals c.ID
                       where b.ID == user.Level
                       select new
                       {
                           RoleID = a.RoleID,
                           UserGroupID = a.UserGroupID
                       }).AsEnumerable().Select(x=> new Credential() { 
                           RoleID = x.RoleID,
                           UserGroupID = x.UserGroupID
                       });
            return data.Select(x => x.RoleID).ToList();
        }
        
        public bool ChangeStatusTwoFactor(int id, bool status)
        {
            try { 
                var user = db.Users.SingleOrDefault(x => x.ID == id);
                user.RequiresVerification = !status;
                db.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
