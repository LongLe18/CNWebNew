namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Food
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Food()
        {
            FoodComments = new HashSet<FoodComment>();
            FoodOptions = new HashSet<FoodOption>();
            PromotionFoodDetails = new HashSet<PromotionFoodDetail>();
            Tags = new HashSet<Tag>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Alias { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public int? OriginPrice { get; set; }

        public int? PromotionPrice { get; set; }

        public int? CategoryID { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public long? CreatedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        public long? UpdatedDate { get; set; }

        [StringLength(250)]
        public string UpdatedBy { get; set; }

        public bool? HotFlag { get; set; }

        public int? ViewCount { get; set; }

        public int? Status { get; set; }

        public int? SlideID { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodComment> FoodComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodOption> FoodOptions { get; set; }

        public virtual Slide Slide { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PromotionFoodDetail> PromotionFoodDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
