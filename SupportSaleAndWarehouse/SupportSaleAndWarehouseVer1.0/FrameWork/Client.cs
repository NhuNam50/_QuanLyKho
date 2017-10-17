namespace SupportSaleAndWarehouseVer1._0.FrameWork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            ExportBills = new HashSet<ExportBill>();
        }

        public int ID { get; set; }

        [Column("Client")]
        [StringLength(50)]
        public string Client1 { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public double? Money { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExportBill> ExportBills { get; set; }
    }
}
