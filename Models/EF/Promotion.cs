namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Promotion()
        {
            PromotionFoodDetails = new HashSet<PromotionFoodDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Alias { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public int? BlogID { get; set; }

        public string Description { get; set; }

        public int? ActiveDay { get; set; }

        public int? EndDay { get; set; }

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

        public int? TypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PromotionFoodDetail> PromotionFoodDetails { get; set; }

        public virtual PromotionType PromotionType { get; set; }
    }
}
