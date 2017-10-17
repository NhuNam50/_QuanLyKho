using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportSaleAndWarehouseVer1._0.FrameWork;
using SupportSaleAndWarehouseVer1._0.Function;

namespace SupportSaleAndWarehouseVer1._0.Function
{
    class FExportBill
    {
        WareHouseDbContext db = null;
        public FExportBill()
        {
            db = new WareHouseDbContext();
        }
        public List<ExportBill> Detail()
        {
            return db.ExportBills.ToList();
        }

        public ExportBill getID(long ID)
        {
            try
            {
                ExportBill entity = db.ExportBills.Find(ID);
                return entity;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public bool Insert(ExportBill entity)
        {
            db.ExportBills.Add(entity);
            int isSave = db.SaveChanges();
            if (isSave > 0)
            {
                return true;
            }

            return false;
        }
        public bool Update(ExportBill entity)
        {
            try
            {
                var Pro = db.ExportBills.Find(entity.ID);
                Pro.Bill = entity.Bill;
                Pro.IDWareHouse = entity.IDWareHouse;
                Pro.TotalPrice = entity.TotalPrice;
                Pro.Quantity = entity.Quantity;
                Pro.Date = entity.Date;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }
        public bool Delete(long ID)
        {
            var entity = db.ExportBills.Find(ID);
            db.ExportBills.Remove(entity);
            var listdt = (from dt in db.ProductDetails.Where(x => x.IDExBill == ID) select dt).ToList();
            foreach (var item in listdt)
            {
                FProductDetail f = new FProductDetail();
                f.Delete(item.ID);
            }
            db.SaveChanges();
            return true;
        }
    }
}
