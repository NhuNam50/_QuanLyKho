using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportSaleAndWarehouseVer1._0.FrameWork;

namespace SupportSaleAndWarehouseVer1._0.Function
{
    public class FProduct
    {
        WareHouseDbContext db = null;
        public FProduct()
        {
            db = new WareHouseDbContext();
        }

        public List<Product> Detail()
        {
            return db.Products.ToList();
        }

        public Product getID(long ID)
        {
            try
            {
                Product entity = db.Products.Find(ID);
                return entity;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public bool Insert(Product entity)
        {
            db.Products.Add(entity);
            int isSave = db.SaveChanges();
            if (isSave > 0)
            {
                return true;
            }

            return false;
        }
        public bool Update(Product entity)
        {
            try
            {
                var Pro = db.Products.Find(entity.ID);
                Pro.Product1 = entity.Product1;
                Pro.IDCompany = entity.IDCompany;
                Pro.Price = entity.Price;
                Pro.OrdinaryPrice = entity.OrdinaryPrice;
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
            var entity = db.Products.Find(ID);
            db.Products.Remove(entity);
            db.SaveChanges();
            return true;
        }
      


    }
}
