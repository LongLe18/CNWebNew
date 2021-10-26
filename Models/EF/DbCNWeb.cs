using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.EF
{
    public partial class DbCNWeb : DbContext
    {
        public DbCNWeb()
            : base("name=DbCNWeb")
        {
        }

        public virtual DbSet<BlogCategory> BlogCategories { get; set; }
        public virtual DbSet<BlogComment> BlogComments { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<CartComboDetail> CartComboDetails { get; set; }
        public virtual DbSet<CartFoodDetail> CartFoodDetails { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<ComboDetail> ComboDetails { get; set; }
        public virtual DbSet<Combo> Combos { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<FoodComment> FoodComments { get; set; }
        public virtual DbSet<FoodOption> FoodOptions { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<OrderFoodDetail> OrderFoodDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PromotionFoodDetail> PromotionFoodDetails { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionType> PromotionTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleName> RoleNames { get; set; }
        public virtual DbSet<SlideDetail> SlideDetails { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<BlogCategory>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<BlogCategory>()
                .HasMany(e => e.Blogs)
                .WithOptional(e => e.BlogCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<BlogComment>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<BlogComment>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Blogs)
                .Map(m => m.ToTable("BlogTags").MapLeftKey("BlogID").MapRightKey("TagID"));

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartComboDetails)
                .WithRequired(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartFoodDetails)
                .WithRequired(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Combo>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Combo>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Combo>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Combo>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Combo>()
                .HasMany(e => e.CartComboDetails)
                .WithRequired(e => e.Combo)
                .HasForeignKey(e => e.ComboID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Combo>()
                .HasMany(e => e.CartComboDetails1)
                .WithRequired(e => e.Combo1)
                .HasForeignKey(e => e.ComboID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Combo>()
                .HasMany(e => e.ComboDetails)
                .WithRequired(e => e.Combo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Credential>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<FoodCategory>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<FoodCategory>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FoodCategory>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FoodCategory>()
                .HasMany(e => e.Foods)
                .WithOptional(e => e.FoodCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<FoodComment>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FoodComment>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FoodOption>()
                .HasMany(e => e.CartFoodDetails)
                .WithRequired(e => e.FoodOption)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodOption>()
                .HasMany(e => e.ComboDetails)
                .WithRequired(e => e.FoodOption)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodOption>()
                .HasMany(e => e.OrderFoodDetails)
                .WithRequired(e => e.FoodOption)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Food>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Food>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Food>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.PromotionFoodDetails)
                .WithRequired(e => e.Food)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Foods)
                .Map(m => m.ToTable("FoodTags").MapLeftKey("FoodID").MapRightKey("TagID"));

            modelBuilder.Entity<Image>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Image>()
                .HasMany(e => e.SlideDetails)
                .WithRequired(e => e.Image)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderFoodDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Promotion>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion>()
                .HasMany(e => e.PromotionFoodDetails)
                .WithRequired(e => e.Promotion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PromotionType>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<PromotionType>()
                .HasMany(e => e.Promotions)
                .WithOptional(e => e.PromotionType)
                .HasForeignKey(e => e.TypeID);

            modelBuilder.Entity<Role>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<RoleName>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.RoleName)
                .HasForeignKey(e => e.IDChucVu);

            modelBuilder.Entity<Slide>()
                .HasMany(e => e.SlideDetails)
                .WithRequired(e => e.Slide)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PostalCode)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreatedByUserID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IDUser);
        }
    }
}
