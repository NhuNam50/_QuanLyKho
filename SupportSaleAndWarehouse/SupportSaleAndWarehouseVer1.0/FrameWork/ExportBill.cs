namespace SupportSaleAndWarehouseVer1._0.FrameWork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExportBill")]
    public partial class ExportBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExportBill()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int ID { get; set; }

        public int? IDWareHouse { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int? IDClient { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Bill { get; set; }

        public int? Quantity { get; set; }

        public double? TotalPrice { get; set; }

        public virtual Client Client { get; set; }

        public virtual WareHouse WareHouse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
