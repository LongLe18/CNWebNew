using Models.EF;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class BlogModel
    {
        private DbCNWeb db = null;

        public BlogModel()
        {
            db = new DbCNWeb();
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Lấy danh sách blog theo filter
        /// </summary>
        /// <returns></returns>
        public IQueryable<ListBlog> ListBlog(int filter, string search)
        {            
            var data = (from a in db.Blogs
                        join b in db.Users on a.CreatedBy equals b.ID
                        where b.UserName.Contains(search) || a.Name.Contains(search)
                        select new
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Description = a.Description,
                            Status = a.Status,
                            Content = a.Content,
                            CreatedDay = a.CreatedDate,
                            UserName = b.UserName,
                            pathImage = b.Image
                        }).AsQueryable().Select(x => new ListBlog
                        {
                            ID = x.ID,
                            Name = x.Name,
                            Description = x.Description,
                            Status = x.Status,
                            Content = x.Content,
                            CreatedDay = x.CreatedDay,
                            UserName = x.UserName,
                            pathImage = x.pathImage
                        });
            if (filter == Constant.STATUS_WAIT)
            {
                data = (from a in db.Blogs
                            join b in db.Users on a.CreatedBy equals b.ID
                            where a.Status == Constant.STATUS_WAIT && (b.UserName.Contains(search) || a.Name.Contains(search))
                            select new
                            {
                                ID = a.ID,
                                Name = a.Name,
                                Status = a.Status,
                                Description = a.Description,
                                Content = a.Content,
                                CreatedDay = a.CreatedDate,
                                UserName = b.UserName,
                                pathImage = b.Image
                            }).AsQueryable().Select(x => new ListBlog
                            {
                                ID = x.ID,
                                Name = x.Name,
                                Status = x.Status,
                                Description = x.Description,
                                Content = x.Content,
                                CreatedDay = x.CreatedDay,
                                UserName = x.UserName,
                                pathImage = x.pathImage
                            });
            } else if (filter == Constant.STATUS_ACCEPTED)
            {
                data = (from a in db.Blogs
                        join b in db.Users on a.CreatedBy equals b.ID
                        where a.Status == Constant.STATUS_ACCEPTED && (b.UserName.Contains(search) || a.Name.Contains(search))
                        select new
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Description = a.Description,
                            Status = a.Status,
                            Content = a.Content,
                            CreatedDay = a.CreatedDate,
                            UserName = b.UserName,
                            pathImage = b.Image
                        }).AsQueryable().Select(x => new ListBlog
                        {
                            ID = x.ID,
                            Name = x.Name,
                            Status = x.Status,
                            Description = x.Description,
                            Content = x.Content,
                            CreatedDay = x.CreatedDay,
                            UserName = x.UserName,
                            pathImage = x.pathImage
                        });
            } else if (filter == Constant.STATUS_DENIED)
            {
                data = (from a in db.Blogs
                        join b in db.Users on a.CreatedBy equals b.ID
                        where a.Status == Constant.STATUS_DENIED && (b.UserName.Contains(search) || a.Name.Contains(search))
                        select new
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Status = a.Status,
                            Description = a.Description,
                            Content = a.Content,
                            CreatedDay = a.CreatedDate,
                            UserName = b.UserName,
                            pathImage = b.Image
                        }).AsQueryable().Select(x => new ListBlog
                        {
                            ID = x.ID,
                            Name = x.Name,
                            Description = x.Description,
                            Status = x.Status,
                            Content = x.Content,
                            CreatedDay = x.CreatedDay,
                            UserName = x.UserName,
                            pathImage = x.pathImage
                        });
            }
            return data;
        }

        /// <summary>
        /// Lấy chi tiêt blog theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<ListBlog> DetailBlog(int id)
        {
            var data = (from a in db.Blogs
                        join b in db.Users on a.CreatedBy equals b.ID
                        where a.ID == id
                        select new
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Description = a.Description,
                            Status = a.Status,
                            Content = a.Content,
                            CreatedDay = a.CreatedDate,
                            UserName = b.UserName,
                            pathImage = b.Image
                        }).AsQueryable().Select(x => new ListBlog
                        {
                            ID = x.ID,
                            Name = x.Name,
                            Description = x.Description,
                            Status = x.Status,
                            Content = x.Content,
                            CreatedDay = x.CreatedDay,
                            UserName = x.UserName,
                            pathImage = x.pathImage
                        }); 
            return data;
        }

        /// <summary>
        /// Duyệt bài (đổi status)
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool ChangeStatus(int ID, bool check)
        {
            try
            {
                var blog = db.Blogs.SingleOrDefault(x => x.ID == ID);
                if (check == true) blog.Status = Constant.STATUS_ACCEPTED;
                else blog.Status = Constant.STATUS_DENIED;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Duyệt tất cả các bài
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public bool ChangeAllStatus(List<String> ID, bool check)
        {
            try
            {
                for (var i = 0; i < ID.Count; i++)
                {
                    var blog = db.Blogs.SingleOrDefault(x => x.ID == Int32.Parse(ID[i]));
                    if (check == true) blog.Status = Constant.STATUS_ACCEPTED;
                    else blog.Status = Constant.STATUS_DENIED;
                }
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
