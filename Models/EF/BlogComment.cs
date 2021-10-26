namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BlogComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? BlogID { get; set; }

        public int? DisplayOrder { get; set; }

        public int? ParentID { get; set; }

        public string Content { get; set; }

        public long? CreatedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        public long? UpdatedDate { get; set; }

        [StringLength(250)]
        public string UpdatedBy { get; set; }

        public int? Status { get; set; }

        public int? UserID { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual User User { get; set; }
    }
}
