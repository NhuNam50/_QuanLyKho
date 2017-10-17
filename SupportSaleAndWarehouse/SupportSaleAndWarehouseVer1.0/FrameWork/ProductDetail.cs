namespace SupportSaleAndWarehouseVer1._0.FrameWork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDetail")]
    public partial class ProductDetail
    {
        public int ID { get; set; }

        public int? IDProduct { get; set; }

        public int? Quantity { get; set; }

        public int? IDExBill { get; set; }

        public int? IDImBill { get; set; }

        public virtual ExportBill ExportBill { get; set; }

        public virtual ImportBill ImportBill { get; set; }

        public virtual Product Product { get; set; }
    }
}
