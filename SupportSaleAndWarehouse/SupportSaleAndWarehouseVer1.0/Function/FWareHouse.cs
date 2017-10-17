using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportSaleAndWarehouseVer1._0.FrameWork;

namespace SupportSaleAndWarehouseVer1._0.Function
{
    class FWareHouse
    {
        WareHouseDbContext db = null;
        public FWareHouse()
        {
            db = new WareHouseDbContext();
        }

        public List<WareHouse> Detail()
        {
            return db.WareHouses.ToList();
        }

        public WareHouse getID(long ID)
        {
            try
            {
                WareHouse entity = db.WareHouses.Find(ID);
                return entity;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public bool Insert(WareHouse entity)
        {
            db.WareHouses.Add(entity);
            int isSave = db.SaveChanges();
            if (isSave > 0)
            {
                return true;
            }

            return false;
        }
        public bool Update(WareHouse entity)
        {
            try
            {
                var Pro = db.WareHouses.Find(entity.ID);
                Pro.Warehouse1 = entity.Warehouse1;
                Pro.Location = entity.Location;
                Pro.Quantity = entity.Quantity;
                Pro.Capacity = entity.Capacity;
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
            var entity = db.WareHouses.Find(ID);
            db.WareHouses.Remove(entity);
            db.SaveChanges();
            return true;
        }
       
    }
}
