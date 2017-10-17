using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportSaleAndWarehouseVer1._0.FrameWork;
using SupportSaleAndWarehouseVer1._0.Function;

namespace SupportSaleAndWarehouseVer1._0.Function
{
    public class FImportBill
    {
        WareHouseDbContext db = null;
        public FImportBill()
        {
            db = new WareHouseDbContext();
        }

        public List<ImportBill> Detail()
        {
            return db.ImportBills.ToList();
        }

        public ImportBill getID(long ID)
        {
            try
            {
                ImportBill entity = db.ImportBills.Find(ID);
                return entity;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public bool Insert(ImportBill entity)
        {
            db.ImportBills.Add(entity);
            int isSave = db.SaveChanges();
            if (isSave > 0)
            {
                return true;
            }

            return false;
        }
        public bool Update(ImportBill entity)
        {
            try
            {
                var Pro = db.ImportBills.Find(entity.ID);
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
            var entity = db.ImportBills.Find(ID);
            db.ImportBills.Remove(entity);
            var listdt = (from dt in db.ProductDetails.Where(x => x.IDImBill == ID) select dt).ToList();
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
