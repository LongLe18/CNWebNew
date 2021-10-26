namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderFoodDetails = new HashSet<OrderFoodDetail>();
        }

        public int ID { get; set; }

        [StringLength(250)]
        public string CustomerName { get; set; }

        [StringLength(250)]
        public string CustomerAddress { get; set; }

        public string CustomerMessage { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        public int? CreatedTime { get; set; }

        public int? CreatedByUserID { get; set; }

        public int? Status { get; set; }

        public int? IDUser { get; set; }

        public int? ToTalPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderFoodDetail> OrderFoodDetails { get; set; }

        public virtual User User { get; set; }
    }
}
