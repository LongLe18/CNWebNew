using Models.EF;
using Models.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace CNWeb.Areas.Main.Controllers
{
    delegate void TreeVisitor<T>(T nodeData);
    class Ntree<T>
    {
        private T data;
        private LinkedList<Ntree<T>> children;
        public Ntree(T data)
        {
            this.data = data;
            children = new LinkedList<Ntree<T>>();
        }
        public void AddChild(T data)
        {
            children.AddFirst(new Ntree<T>(data));
        }
        public Ntree<T> GetChild(int i)
        {
            foreach (Ntree<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }
        public void Traverse(Ntree<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.data);
            foreach (Ntree<T> kid in node.children)
                Traverse(kid, visitor);
        }
    }
    public class BlogController : Controller
    {
        // GET: Main/Blog
        DbCNWeb db = new DbCNWeb();
        public ActionResult Index(int? page)
        {
               
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
         //   var bl = (from l in db.Blogs select l).OrderBy(x => x.ID);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
           
            List<Blog> blog = new List<Blog>();
          //  List<Blog> blog1 = new List<Blog>();
            ViewBag.blogview = db.Database.SqlQuery<Blog>("Select TOP 3 * FROM BLogs order by ViewCount desc").ToList();
            ViewBag.blognew = db.Database.SqlQuery<Blog>("Select TOP 3 * FROM BLogs order by CreatedDate desc").ToList();
            var blog1 = db.Blogs.Where(s => s.Status != null).ToList();
          //  ViewBag.blog1 = blog1;
            ViewBag.blogcategories = db.BlogCategories.Where(s => s.Status != null).ToList();
            ViewBag.blogtag = db.Tags.ToList();
            return View(blog1.ToPagedList(pageNumber, pageSize));
            //  return View();
        }
        public ActionResult Detail(int id)
        {
            SqlParameter parameter = new SqlParameter("@id", id);
            SqlParameter parameter1 = new SqlParameter("@id", id);
            SqlParameter parameter2 = new SqlParameter("@id", id);
            SqlParameter parameter3 = new SqlParameter("@id", id);
            ViewBag.cmt = from a in db.BlogComments
                      join b in db.Users
                      on a.UserID equals b.ID
                      where a.BlogID == id
                      where a.Status != null
                      select new CommentModel()
                      {
                          content = a.Content,
                          name = b.FullName,
                          image = b.Image,
                          createdDate = a.CreatedDate
                      };
            ViewBag.blogview = db.Database.SqlQuery<Blog>("Select TOP 3 * FROM BLogs order by ViewCount desc").ToList();
            ViewBag.blognew = db.Database.SqlQuery<Blog>("Select TOP 3 * FROM BLogs order by CreatedDate desc").ToList();
            ViewBag.blogdetail = db.Blogs.Where(s => s.ID == id).FirstOrDefault();
            ViewBag.blogcategories = db.Database.SqlQuery<BlogCategory>("select * from BlogCategories inner join blogs on Blogs.CategoryID = BlogCategories.ID and BlogCategories.Status is not null and Blogs.ID = @id", parameter).ToList();
            ViewBag.blogtag = db.Database.SqlQuery<Tag>("select * from tags inner join BlogTags on BlogTags.TagID = tags.ID inner join blogs on blogs.ID = BlogTags.BlogID and blogs.ID = @id", parameter1).ToList();
            var cnt = db.BlogComments.Where(x => x.BlogID == id).Count();
            ViewBag.cnt = cnt;
            return View();
        }
        public ActionResult Categories(int id, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            SqlParameter parameter = new SqlParameter("@id", id);
            List<Blog> blog1 = new List<Blog>();
            ViewBag.blogview = db.Database.SqlQuery<Blog>("Select TOP 3 * FROM BLogs order by ViewCount desc").ToList();
            ViewBag.blognew = db.Database.SqlQuery<Blog>("Select TOP 3 * FROM BLogs order by CreatedDate desc").ToList();
            var blog = db.Database.SqlQuery<Blog>("select * from blogs inner join BlogCategories on blogs.CategoryID = BlogCategories.ID and blogs.CategoryID = @id and blogs.status is not null", parameter).ToList();
            ViewBag.blogcategories = db.BlogCategories.Where(s => s.Status != null).ToList();
            ViewBag.blogtag = db.Tags.ToList();

            return View(blog.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Tag(int id, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            SqlParameter parameter = new SqlParameter("@id", id);
            List<Blog> blog1 = new List<Blog>();
            ViewBag.blogview = db.Database.SqlQuery<Blog>("Select TOP 3 * FROM BLogs order by ViewCount desc").ToList();
            ViewBag.blognew = db.Database.SqlQuery<Blog>("Select TOP 3 * FROM BLogs order by CreatedDate desc").ToList();
            var blog = db.Database.SqlQuery<Blog>("select * from blogs inner join BlogTags on Blogs.ID = BlogTags.BlogID inner join Tags on BlogTags.TagID = Tags.ID and Tags.ID = @id and blogs.status is not null", parameter).ToList();
            ViewBag.blogcategories = db.BlogCategories.Where(s => s.Status != null).ToList();
            ViewBag.blogtag = db.Tags.ToList();
            return View(blog.ToPagedList(pageNumber,pageSize));
        }
    }
}