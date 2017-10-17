using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportSaleAndWarehouseVer1._0.FrameWork;


namespace SupportSaleAndWarehouseVer1._0.Function
{
    class FProductDetail
    {
         WareHouseDbContext db = null;
        public FProductDetail()
        {
            db = new WareHouseDbContext();
        }

        public List<ProductDetail> Detail()
        {
            return db.ProductDetails.ToList();
        }

        public ProductDetail getID(long ID)
        {
            try
            {
                ProductDetail entity = db.ProductDetails.Find(ID);
                return entity;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public bool Insert(ProductDetail entity)
        {
            db.ProductDetails.Add(entity);
            int isSave = db.SaveChanges();
            if (isSave > 0)
            {
                return true;
            }

            return false;
        }
        public bool Update(ProductDetail entity)
        {
            try
            {
                var Pro = db.ProductDetails.Find(entity.ID);
                
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
            var entity = db.ProductDetails.Find(ID);
            db.ProductDetails.Remove(entity);
            db.SaveChanges();
            return true;
        }
    }
}
