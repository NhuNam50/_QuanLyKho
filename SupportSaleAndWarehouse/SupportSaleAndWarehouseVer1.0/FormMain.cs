using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SupportSaleAndWarehouseVer1._0.FrameWork;
using SupportSaleAndWarehouseVer1._0.Function;
using System.Reflection;

namespace SupportSaleAndWarehouseVer1._0
{
    public partial class FormMain : Form
    {
        
        public FormMain()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           

        }
        #region Tab WareHouse
        #region Datagridview Warehouse
        public void BindingWarehouse()
        {
            dgrvWH.Refresh();
            dgrvWH.DataSource = null;
            txtIDWH.Enabled = false;
            List<WareHouse> list = (from wh in db.WareHouses select wh).ToList();

            dgrvWH.AutoGenerateColumns = false;

            dgrvWH.ColumnCount = 5;
            dgrvWH.Columns[0].Name = "ID";
            dgrvWH.Columns[0].HeaderText = "ID";
            dgrvWH.Columns[0].DataPropertyName = "ID";

            dgrvWH.Columns[1].Name = "Warehouse1";
            dgrvWH.Columns[1].HeaderText = "Tên kho";
            dgrvWH.Columns[1].DataPropertyName = "Warehouse1";


            dgrvWH.Columns[2].Name = "Location";
            dgrvWH.Columns[2].HeaderText = "Địa chỉ";
            dgrvWH.Columns[2].DataPropertyName = "Location";

            dgrvWH.Columns[3].Name = "Quantity";
            dgrvWH.Columns[3].HeaderText = "Đã chứa";
            dgrvWH.Columns[3].DataPropertyName = "Quantity";

            dgrvWH.Columns[4].Name = "Capacity";
            dgrvWH.Columns[4].HeaderText = "Sức chứa";
            dgrvWH.Columns[4].DataPropertyName = "Capacity";
            dgrvWH.DataSource = list;

            txtIDWH.Text = dgrvWH.CurrentRow.Cells["ID"].Value.ToString();
            txtLocationWH.Text = dgrvWH.CurrentRow.Cells["Location"].Value.ToString();
            txtWH.Text = dgrvWH.CurrentRow.Cells["Warehouse1"].Value.ToString();
            txtCapacityWH.Text = dgrvWH.CurrentRow.Cells["Capacity"].Value.ToString();
            txtQuantityWH.Text = dgrvWH.CurrentRow.Cells["Quantity"].Value.ToString();
        }

        public void Binding_dgrDetailWH()
        {
            dgrvDetailWH.Refresh();
            dgrvDetailWH.DataSource = null;
            var ID = Convert.ToInt32(dgrvWH.CurrentRow.Cells["ID"].Value.ToString());
            List<Product> lpro = new List<Product>();

            lpro = (from wh in db.WareHouses.Where(x => x.ID == ID).ToList()
                    from ib in db.ImportBills.Where(x => x.IDWareHouse == wh.ID).ToList()
                    from dt in db.ProductDetails.Where(x => x.IDImBill == ib.ID).ToList()
                    from pr in db.Products.Where(x => x.ID == dt.IDProduct).ToList()
                    from com in db.Companies.Where(x => x.ID == pr.IDCompany).ToList()
                    select new Product
                    {
                        ID = pr.ID,
                        Product1 = pr.Product1,
                        IDCompany = pr.IDCompany,
                        Price = pr.Price,
                        OrdinaryPrice = pr.OrdinaryPrice
                    }

                    ).ToList();
            List<Product> lpro2 = new List<Product>();
            foreach (var item in lpro)
            {
                var search = lpro2.Find(x => x.ID == item.ID);
                if (search == null)
                {
                    lpro2.Add(item);
                }

            }
            List<ProductDetail> ldt = new List<ProductDetail>();
            ldt = (from wh in db.WareHouses.Where(x => x.ID == ID).ToList()
                   from ib in db.ImportBills.Where(x => x.IDWareHouse == wh.ID).ToList()
                   from dt in db.ProductDetails.Where(x => x.IDImBill == ib.ID).ToList()
                   from pr in db.Products.Where(x => x.ID == dt.IDProduct).ToList()
                   from com in db.Companies.Where(x => x.ID == pr.IDCompany).ToList()
                   select new ProductDetail
                   {
                       IDProduct = dt.IDProduct,
                       Quantity = dt.Quantity,
                   }

           ).ToList();
            List<ProductDetail> ldt2 = new List<ProductDetail>();
            foreach (var item in lpro2)
            {
                var Quantity = ldt.Where(x => x.IDProduct == item.ID).Sum(x => x.Quantity);
                ProductDetail news = new ProductDetail();
                news.IDProduct = item.ID;
                news.Quantity = Quantity;
                ldt2.Add(news);

            }
            var list = (from pr in lpro2.ToList()
                        from dt in ldt2.Where(x => x.IDProduct == pr.ID).ToList()
                        from com in db.Companies.Where(x => x.ID == pr.IDCompany).ToList()
                        select new
                        {

                            ID = pr.ID,
                            Product1 = pr.Product1,
                            IDCompany = pr.IDCompany,
                            Price = pr.Price,
                            OrdinaryPrice = pr.OrdinaryPrice,
                            Quantity = dt.Quantity,
                            Company1 = com.Company1
                        }
                ).ToList();
            dgrvDetailWH.AutoGenerateColumns = false;

            dgrvDetailWH.ColumnCount = 6;

            dgrvDetailWH.Columns[0].Name = "ID";
            dgrvDetailWH.Columns[0].HeaderText = "ID";
            dgrvDetailWH.Columns[0].DataPropertyName = "ID";

            dgrvDetailWH.Columns[1].Name = "Product1";
            dgrvDetailWH.Columns[1].HeaderText = "Sản phẩm";
            dgrvDetailWH.Columns[1].DataPropertyName = "Product1";

            dgrvDetailWH.Columns[2].Name = "Company1";
            dgrvDetailWH.Columns[2].HeaderText = "Công ty";
            dgrvDetailWH.Columns[2].DataPropertyName = "Company1";

            dgrvDetailWH.Columns[3].Name = "Quantity";
            dgrvDetailWH.Columns[3].HeaderText = "Số lượng";
            dgrvDetailWH.Columns[3].DataPropertyName = "Quantity";

            dgrvDetailWH.Columns[4].Name = "Price";
            dgrvDetailWH.Columns[4].HeaderText = "Giá bán";
            dgrvDetailWH.Columns[4].DataPropertyName = "Price";

            dgrvDetailWH.Columns[5].Name = "OrdinaryPrice";
            dgrvDetailWH.Columns[5].HeaderText = "Giá gốc";
            dgrvDetailWH.Columns[5].DataPropertyName = "OrdinaryPrice";

            dgrvDetailWH.DataSource = list;

            txtIDWH.Text = dgrvWH.CurrentRow.Cells["ID"].Value.ToString();
            txtLocationWH.Text = dgrvWH.CurrentRow.Cells["Location"].Value.ToString();
            txtWH.Text = dgrvWH.CurrentRow.Cells["Warehouse1"].Value.ToString();
            txtCapacityWH.Text = dgrvWH.CurrentRow.Cells["Capacity"].Value.ToString();
            txtQuantityWH.Text = dgrvWH.CurrentRow.Cells["Quantity"].Value.ToString();
        }
        private void dgrvWH_MouseClick(object sender, MouseEventArgs e)
        {
            Binding_dgrDetailWH();
        }
        #region Button WH
        private void Add_Condition()
        {
            if (txtCapacityWH.Text == "" || txtIDWH.Text == "" || txtLocationWH.Text == "" || txtQuantityWH.Text == "" || txtWH.Text == "")
            {
                btnAddWH.Enabled = false;
            }
            else
            {
                btnAddWH.Enabled = true;
            }
        }
        private void btnAddWH_Click(object sender, EventArgs e)
        {
            WareHouse entity = new WareHouse();
            entity.ID = Convert.ToInt32(txtIDWH.Text.ToString());
            entity.Location = txtLocationWH.Text;
            entity.Quantity = Convert.ToInt32(txtQuantityWH.Text.ToString());
            entity.Capacity = Convert.ToInt32(txtCapacityWH.Text.ToString());
            entity.Warehouse1 = txtWH.Text;
            FWareHouse fWareHouse = new FWareHouse();
            fWareHouse.Insert(entity);

            BindingWarehouse();

        }

        private void btnRefreshWH_Click(object sender, EventArgs e)
        {

            txtLocationWH.Text = "";
            txtWH.Text = "";
            txtCapacityWH.Text = "";
            txtQuantityWH.Text = "";
        }

        private void btnDeleteWH_Click(object sender, EventArgs e)
        {
            FWareHouse fWareHouse = new FWareHouse();
            var ID = Convert.ToInt32(dgrvWH.CurrentRow.Cells["ID"].Value.ToString());
            fWareHouse.Delete(ID);
            BindingWarehouse();
        }
        private void btnEditWH_Click(object sender, EventArgs e)
        {
            WareHouse entity = new WareHouse();
            entity.ID = Convert.ToInt32(txtIDWH.Text.ToString());
            entity.Location = txtLocationWH.Text;
            entity.Quantity = Convert.ToInt32(txtQuantityWH.Text.ToString());
            entity.Capacity = Convert.ToInt32(txtCapacityWH.Text.ToString());
            entity.Warehouse1 = txtWH.Text;
            FWareHouse fWareHouse = new FWareHouse();
            fWareHouse.Update(entity);
            dgrvWH.Refresh();
            dgrvWH.DataSource = null;
            txtIDWH.Enabled = false;
            List<WareHouse> list = (from wh in db.WareHouses select wh).ToList();

            dgrvWH.AutoGenerateColumns = false;

            dgrvWH.ColumnCount = 5;
            dgrvWH.Columns[0].Name = "ID";
            dgrvWH.Columns[0].HeaderText = "ID";
            dgrvWH.Columns[0].DataPropertyName = "ID";

            dgrvWH.Columns[1].Name = "Warehouse";
            dgrvWH.Columns[1].HeaderText = "Tên kho";
            dgrvWH.Columns[1].DataPropertyName = "Warehouse";


            dgrvWH.Columns[2].Name = "Location";
            dgrvWH.Columns[2].HeaderText = "Địa chỉ";
            dgrvWH.Columns[2].DataPropertyName = "Location";

            dgrvWH.Columns[3].Name = "Quantity";
            dgrvWH.Columns[3].HeaderText = "Đã chứa";
            dgrvWH.Columns[3].DataPropertyName = "Quantity";

            dgrvWH.Columns[4].Name = "Capacity";
            dgrvWH.Columns[4].HeaderText = "Sức chứa";
            dgrvWH.Columns[4].DataPropertyName = "Capacity";
            dgrvWH.DataSource = list;

            txtIDWH.Text = dgrvWH.CurrentRow.Cells["ID"].Value.ToString();
            txtLocationWH.Text = dgrvWH.CurrentRow.Cells["Location"].Value.ToString();
            txtWH.Text = dgrvWH.CurrentRow.Cells["Warehouse"].Value.ToString();
            txtCapacityWH.Text = dgrvWH.CurrentRow.Cells["Capacity"].Value.ToString();
            txtQuantityWH.Text = dgrvWH.CurrentRow.Cells["Quantity"].Value.ToString();

        }


        private void txtWH_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWH.Text) | string.IsNullOrWhiteSpace(txtIDWH.Text) | string.IsNullOrWhiteSpace(txtLocationWH.Text) | string.IsNullOrWhiteSpace(txtQuantityWH.Text) | string.IsNullOrWhiteSpace(txtCapacityWH.Text))
            {
                btnAddWH.Enabled = false;
            }
            else
            {
                btnAddWH.Enabled = true;
            }
        }

        private void txtIDWH_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWH.Text) | string.IsNullOrWhiteSpace(txtIDWH.Text) | string.IsNullOrWhiteSpace(txtLocationWH.Text) | string.IsNullOrWhiteSpace(txtQuantityWH.Text) | string.IsNullOrWhiteSpace(txtCapacityWH.Text))
            {
                btnAddWH.Enabled = false;
            }
            else
            {
                btnAddWH.Enabled = true;
            }
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWH.Text) | string.IsNullOrWhiteSpace(txtIDWH.Text) | string.IsNullOrWhiteSpace(txtLocationWH.Text) | string.IsNullOrWhiteSpace(txtQuantityWH.Text) | string.IsNullOrWhiteSpace(txtCapacityWH.Text))
            {
                btnAddWH.Enabled = false;
            }
            else
            {
                btnAddWH.Enabled = true;
            }
        }

        private void txtCapacity_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWH.Text) | string.IsNullOrWhiteSpace(txtIDWH.Text) | string.IsNullOrWhiteSpace(txtLocationWH.Text) | string.IsNullOrWhiteSpace(txtQuantityWH.Text) | string.IsNullOrWhiteSpace(txtCapacityWH.Text))
            {
                btnAddWH.Enabled = false;
            }
            else
            {
                btnAddWH.Enabled = true;
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWH.Text) | string.IsNullOrWhiteSpace(txtIDWH.Text) | string.IsNullOrWhiteSpace(txtLocationWH.Text) | string.IsNullOrWhiteSpace(txtQuantityWH.Text) | string.IsNullOrWhiteSpace(txtCapacityWH.Text))
            {
                btnAddWH.Enabled = false;
            }
            else
            {
                btnAddWH.Enabled = true;
            }
        }

        private void txtCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion
        #endregion


        private void btnDeleteDTWH_Click(object sender, EventArgs e)
        {
            FProductDetail fprodt = new FProductDetail();
            var IDwh = Convert.ToInt32(dgrvWH.CurrentRow.Cells["ID"].Value.ToString());
            var IDpro = Convert.ToInt32(dgrvDetailWH.CurrentRow.Cells["ID"].Value.ToString());
            var IDprodt = (from wh in db.WareHouses.Where(x => x.ID == IDwh).ToList()
                           from prodt in db.ProductDetails.Where(z => z.IDProduct == IDpro).ToList()
                           select new
                           {
                               ID = prodt.ID
                           }
                ).ToList();
            foreach (var item in IDprodt)
            {
                var a = Convert.ToInt32(item.ID.ToString());
                fprodt.Delete(a);
            }

            Binding_dgrDetailWH();
        }

        private void btnAddPro_Click(object sender, EventArgs e)
        {
            FormImBill formImBill = new FormImBill();
            formImBill.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormExBill formImBill = new FormExBill();
            formImBill.Show();
        }
        #endregion

        #region Tab Product





        public void dgrvProduct_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        private void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRefreshPro_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEditPro_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDeletePro_Click(object sender, EventArgs e)
        {
          
        }
        private void txtOrdinaryPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtOrdinaryPrice_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
           
        }


        #endregion
        #region Tab Import Bill
       
    

      

        private void dgrvIm_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void cbWHImSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void btnAddIm_Click(object sender, EventArgs e)
        {
           
        }
        private void btnRefreshIm_Click(object sender, EventArgs e)
        {
           
        }

        private void btnEditIm_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDeleteIm_Click(object sender, EventArgs e)
        {
        }

        private void txtIDIm_TextChanged(object sender, EventArgs e)
        {
           
        }

        #endregion
        #region Export Bill
        
       

        private void dgrvEx_MouseClick(object sender, MouseEventArgs e)
        {

         


        }

        private void btnAddEx_Click(object sender, EventArgs e)
        {
          
        }

        private void btnRefreshEx_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEditEx_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDeleteEx_Click(object sender, EventArgs e)
        {
           
        }

        private void txtIDEx_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cbWHExSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        #endregion
    }
}
