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





        public void Load_TabProduct()
        {

            BindingdgrvProduct();

        }
        public void BindingdgrvProduct()
        {
            btnAddProduct.Enabled = false;
            //btnEditPro.Enabled = false;

            dgrvProduct.Refresh();
            dgrvProduct.DataSource = null;
            txtIDPro.Enabled = false;
            var IDCom = Convert.ToInt32(cbCom.SelectedValue.ToString());
            List<Product> list = (from pr in db.Products.Where(x => x.IDCompany == IDCom) select pr).ToList();

            dgrvProduct.AutoGenerateColumns = false;

            dgrvProduct.ColumnCount = 5;
            dgrvProduct.Columns[0].Name = "ID";
            dgrvProduct.Columns[0].HeaderText = "ID";
            dgrvProduct.Columns[0].DataPropertyName = "ID";

            dgrvProduct.Columns[1].Name = "Product1";
            dgrvProduct.Columns[1].HeaderText = "Tên s?n ph?m";
            dgrvProduct.Columns[1].DataPropertyName = "Product1";


            dgrvProduct.Columns[2].Name = "Company";
            dgrvProduct.Columns[2].HeaderText = "Mã công ty";
            dgrvProduct.Columns[2].DataPropertyName = "IDCompany";

            dgrvProduct.Columns[3].Name = "Price";
            dgrvProduct.Columns[3].HeaderText = "Giá bán";
            dgrvProduct.Columns[3].DataPropertyName = "Price";

            dgrvProduct.Columns[4].Name = "OrdinaryPrice";
            dgrvProduct.Columns[4].HeaderText = "Giá g?c";
            dgrvProduct.Columns[4].DataPropertyName = "OrdinaryPrice";

            dgrvProduct.DataSource = list;
            //txtIDPro.Text = dgrvProduct.CurrentRow.Cells["ID"].Value.ToString();
            //txtPro.Text = dgrvProduct.CurrentRow.Cells["Product1"].Value.ToString();
            //cbCompany.Text = dgrvProduct.CurrentRow.Cells["Company"].Value.ToString();
            //txtOrdinaryPrice.Text = dgrvProduct.CurrentRow.Cells["OrdinaryPrice"].Value.ToString();
            //txtPrice.Text = dgrvProduct.CurrentRow.Cells["Price"].Value.ToString();
        }

        public void Load_CbCom()
        {
            List<Company> list = (from com in db.Companies select com).ToList();
            cbCom.DataSource = list;
            cbCom.DisplayMember = "Company1";
            cbCom.ValueMember = "ID";
        }
        public void Load_CbCompany()
        {
            List<Company> list = (from com in db.Companies select com).ToList();
            cbCompany.DataSource = list;
            cbCompany.DisplayMember = "Company1";
            cbCompany.ValueMember = "ID";
        }
        public void dgrvProduct_MouseClick(object sender, MouseEventArgs e)
        {
            txtIDPro.Text = dgrvProduct.CurrentRow.Cells["ID"].Value.ToString();
            txtPro.Text = dgrvProduct.CurrentRow.Cells["Product1"].Value.ToString();
            cbCompany.SelectedValue = cbCom.SelectedValue;
            txtOrdinaryPrice.Text = dgrvProduct.CurrentRow.Cells["OrdinaryPrice"].Value.ToString();
            txtPrice.Text = dgrvProduct.CurrentRow.Cells["Price"].Value.ToString();
        }
        private void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingdgrvProduct();
            txtIDPro.Text = dgrvProduct.CurrentRow.Cells["ID"].Value.ToString();
            txtPro.Text = dgrvProduct.CurrentRow.Cells["Product1"].Value.ToString();
            cbCompany.SelectedValue = cbCom.SelectedValue;
            txtOrdinaryPrice.Text = dgrvProduct.CurrentRow.Cells["OrdinaryPrice"].Value.ToString();
            txtPrice.Text = dgrvProduct.CurrentRow.Cells["Price"].Value.ToString();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Product entity = new Product();
            entity.ID = Convert.ToInt32(txtIDPro.Text.ToString());
            entity.Product1 = txtPro.Text;
            entity.IDCompany = Convert.ToInt32(cbCompany.SelectedValue.ToString());
            entity.Price = Convert.ToInt32(txtPrice.Text.ToString());
            entity.OrdinaryPrice = Convert.ToInt32(txtOrdinaryPrice.Text);
            FProduct fProduct = new FProduct();
            fProduct.Insert(entity);

            BindingdgrvProduct();
        }

        private void btnRefreshPro_Click(object sender, EventArgs e)
        {
            txtIDPro.Text = "";
            txtPro.Text = "";
            txtOrdinaryPrice.Text = "";
            txtPrice.Text = "";
        }

        private void btnEditPro_Click(object sender, EventArgs e)
        {
            Product entity = new Product();
            entity.ID = Convert.ToInt32(txtIDPro.Text.ToString());
            entity.Product1 = txtPro.Text;
            entity.IDCompany = Convert.ToInt32(cbCompany.SelectedValue.ToString());
            entity.Price = Convert.ToInt32(txtPrice.Text.ToString());
            entity.OrdinaryPrice = Convert.ToInt32(txtOrdinaryPrice.Text);
            FProduct fProduct = new FProduct();
            fProduct.Update(entity);

            btnAddProduct.Enabled = false;
            //btnEditPro.Enabled = false;

            dgrvProduct.Refresh();
            dgrvProduct.DataSource = null;
            txtIDPro.Enabled = false;
            var IDCom = Convert.ToInt32(cbCom.SelectedValue.ToString());
            List<Product> list = (from pr in db.Products.Where(x => x.IDCompany == IDCom) select pr).ToList();

            dgrvProduct.AutoGenerateColumns = false;

            dgrvProduct.ColumnCount = 5;
            dgrvProduct.Columns[0].Name = "ID";
            dgrvProduct.Columns[0].HeaderText = "ID";
            dgrvProduct.Columns[0].DataPropertyName = "ID";

            dgrvProduct.Columns[1].Name = "Product1";
            dgrvProduct.Columns[1].HeaderText = "Tên s?n ph?m";
            dgrvProduct.Columns[1].DataPropertyName = "Product1";


            dgrvProduct.Columns[2].Name = "Company";
            dgrvProduct.Columns[2].HeaderText = "Mã công ty";
            dgrvProduct.Columns[2].DataPropertyName = "IDCompany";

            dgrvProduct.Columns[3].Name = "Price";
            dgrvProduct.Columns[3].HeaderText = "Giá bán";
            dgrvProduct.Columns[3].DataPropertyName = "Price";

            dgrvProduct.Columns[4].Name = "OrdinaryPrice";
            dgrvProduct.Columns[4].HeaderText = "Giá g?c";
            dgrvProduct.Columns[4].DataPropertyName = "OrdinaryPrice";

            dgrvProduct.DataSource = list;

        }

        private void btnDeletePro_Click(object sender, EventArgs e)
        {
            FProduct fProduct = new FProduct();
            var ID = Convert.ToInt32(dgrvProduct.CurrentRow.Cells["ID"].Value.ToString());
            fProduct.Delete(ID);
            BindingdgrvProduct();
        }
        private void txtOrdinaryPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDPro.Text) | string.IsNullOrWhiteSpace(txtOrdinaryPrice.Text) | string.IsNullOrWhiteSpace(txtPrice.Text) | string.IsNullOrWhiteSpace(txtPro.Text) | string.IsNullOrWhiteSpace(cbCompany.Text))
            {
                btnAddProduct.Enabled = false;
                btnEditPro.Enabled = false;
            }
            else
            {
                btnAddProduct.Enabled = true;
                btnEditPro.Enabled = true;
            }
        }

        private void txtOrdinaryPrice_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDPro.Text) | string.IsNullOrWhiteSpace(txtOrdinaryPrice.Text) | string.IsNullOrWhiteSpace(txtPrice.Text) | string.IsNullOrWhiteSpace(txtPro.Text) | string.IsNullOrWhiteSpace(cbCompany.Text))
            {
                btnAddProduct.Enabled = false;
                btnEditPro.Enabled = false;
            }
            else
            {
                btnAddProduct.Enabled = true;
                btnEditPro.Enabled = true;
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDPro.Text) | string.IsNullOrWhiteSpace(txtOrdinaryPrice.Text) | string.IsNullOrWhiteSpace(txtPrice.Text) | string.IsNullOrWhiteSpace(txtPro.Text) | string.IsNullOrWhiteSpace(cbCompany.Text))
            {
                btnAddProduct.Enabled = false;
                btnEditPro.Enabled = false;
            }
            else
            {
                btnAddProduct.Enabled = true;
                btnEditPro.Enabled = true;
            }
        }


        #endregion
        #region Tab Import Bill





        public void Binding_CbWHImSearch()
        {
            List<WareHouse> lwh = (from wh in db.WareHouses select wh).ToList();
            cbWHImSearch.DataSource = lwh;
            cbWHImSearch.DisplayMember = "Warehouse";
            cbWHImSearch.ValueMember = "ID";


            cbWHImDt.DataSource = lwh;
            cbWHImDt.DisplayMember = "Warehouse";
            cbWHImDt.ValueMember = "ID";
        }
        public void BindingdgrvIm()
        {
            btnEditIm.Enabled = false;
            dgrvIm.Refresh();
            dgrvIm.DataSource = null;
            txtIDIm.Enabled = false;
            txtPriceIm.Enabled = false;
            txtPriceIm.Enabled = false;
            var IDWH = Convert.ToInt32(cbWHImSearch.SelectedValue.ToString());
            List<ImportBill> list = (from im in db.ImportBills.Where(x => x.IDWareHouse == IDWH) select im).ToList();

            dgrvIm.AutoGenerateColumns = false;

            dgrvIm.ColumnCount = 6;
            dgrvIm.Columns[0].Name = "ID";
            dgrvIm.Columns[0].HeaderText = "ID";
            dgrvIm.Columns[0].DataPropertyName = "ID";

            dgrvIm.Columns[1].Name = "Bill";
            dgrvIm.Columns[1].HeaderText = "Tên Phi?u";
            dgrvIm.Columns[1].DataPropertyName = "Bill";


            dgrvIm.Columns[2].Name = "IDWareHouse";
            dgrvIm.Columns[2].HeaderText = "Mã kho";
            dgrvIm.Columns[2].DataPropertyName = "IDWareHouse";

            dgrvIm.Columns[3].Name = "Date";
            dgrvIm.Columns[3].HeaderText = "Ngày nh?p";
            dgrvIm.Columns[3].DataPropertyName = "Date";

            dgrvIm.Columns[4].Name = "Quantity";
            dgrvIm.Columns[4].HeaderText = "S? lu?ng";
            dgrvIm.Columns[4].DataPropertyName = "Quantity";

            dgrvIm.Columns[5].Name = "TotalPrice";
            dgrvIm.Columns[5].HeaderText = "T?ng ti?n";
            dgrvIm.Columns[5].DataPropertyName = "TotalPrice";

            dgrvIm.DataSource = list;
            //txtIDIm.Text = dgrvIm.CurrentRow.Cells["ID"].Value.ToString();
            //txtPro.Text = dgrvIm.CurrentRow.Cells["Product1"].Value.ToString();
            //cbCompany.Text = dgrvIm.CurrentRow.Cells["Company"].Value.ToString();
            //txtOrdinaryPrice.Text = dgrvIm.CurrentRow.Cells["OrdinaryPrice"].Value.ToString();
            //txtPrice.Text = dgrvIm.CurrentRow.Cells["Price"].Value.ToString();

        }

        public void Binding_dgrvDTIm()
        {
            dgrvDTIm.Refresh();
            dgrvDTIm.DataSource = null;
            txtIDPro.Enabled = false;
            var IDBill = Convert.ToInt32(txtIDIm.Text.ToString());
            var list = (from dt in db.ProductDetails.Where(x => x.IDImBill == IDBill).ToList()
                        from pr in db.Products.Where(x => x.ID == dt.IDProduct).ToList()
                        from com in db.Companies.Where(x => x.ID == pr.IDCompany).ToList()
                        select new
                        {
                            ID = pr.ID,
                            Product1 = pr.Product1,
                            Company = com.Company1,
                            Quantity = dt.Quantity,
                            OrdinaryPrice = pr.OrdinaryPrice
                        }
                                  ).ToList();

            dgrvDTIm.AutoGenerateColumns = false;

            dgrvDTIm.ColumnCount = 5;
            dgrvDTIm.Columns[0].Name = "ID";
            dgrvDTIm.Columns[0].HeaderText = "ID";
            dgrvDTIm.Columns[0].DataPropertyName = "ID";

            dgrvDTIm.Columns[1].Name = "Product1";
            dgrvDTIm.Columns[1].HeaderText = "Tên s?n ph?m";
            dgrvDTIm.Columns[1].DataPropertyName = "Product1";


            dgrvDTIm.Columns[2].Name = "Company";
            dgrvDTIm.Columns[2].HeaderText = "Mã công ty";
            dgrvDTIm.Columns[2].DataPropertyName = "Company";

            dgrvDTIm.Columns[3].Name = "Quantity";
            dgrvDTIm.Columns[3].HeaderText = "S? lu?ng";
            dgrvDTIm.Columns[3].DataPropertyName = "Quantity";

            dgrvDTIm.Columns[4].Name = "OrdinaryPrice";
            dgrvDTIm.Columns[4].HeaderText = "Giá g?c";
            dgrvDTIm.Columns[4].DataPropertyName = "OrdinaryPrice";

            dgrvDTIm.DataSource = list;
        }

        private void dgrvIm_MouseClick(object sender, MouseEventArgs e)
        {
            txtIDIm.Text = dgrvIm.CurrentRow.Cells["ID"].Value.ToString();
            txtIm.Text = dgrvIm.CurrentRow.Cells["Bill"].Value.ToString();
            cbWHImDt.SelectedValue = cbWHImSearch.SelectedValue;
            dtpIm.Text = dgrvIm.CurrentRow.Cells["Date"].Value.ToString();
            txtQuantityIm.Text = dgrvIm.CurrentRow.Cells["Quantity"].Value.ToString();
            txtPriceIm.Text = dgrvIm.CurrentRow.Cells["TotalPrice"].Value.ToString();
            Binding_dgrvDTIm();
        }

        private void cbWHImSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingdgrvIm();
            txtIDIm.Text = dgrvIm.CurrentRow.Cells["ID"].Value.ToString();
            txtIm.Text = dgrvIm.CurrentRow.Cells["Bill"].Value.ToString();
            cbWHImDt.SelectedValue = cbWHImSearch.SelectedValue;
            dtpIm.Text = dgrvIm.CurrentRow.Cells["Date"].Value.ToString();
            txtQuantityIm.Text = dgrvIm.CurrentRow.Cells["Quantity"].Value.ToString();
            txtPriceIm.Text = dgrvIm.CurrentRow.Cells["TotalPrice"].Value.ToString();
            Binding_dgrvDTIm();
        }
        private void btnAddIm_Click(object sender, EventArgs e)
        {
            FormImBill formImBill = new FormImBill();
            formImBill.Show();
        }
        private void btnRefreshIm_Click(object sender, EventArgs e)
        {
            txtIDIm.Text = "";
            txtIm.Text = "";
            cbWHImDt.SelectedValue = cbWHImSearch.SelectedValue;
            dtpIm.Text = dgrvIm.CurrentRow.Cells["Date"].Value.ToString();
            txtQuantityIm.Text = "";
            txtPriceIm.Text = "";
        }

        private void btnEditIm_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(txtIDIm.Text.ToString());
            string Bill = txtIm.Text;
            FormEditImBill formImBill = new FormEditImBill(ID, Bill);
            formImBill.Show();
        }

        private void btnDeleteIm_Click(object sender, EventArgs e)
        {
            FImportBill fImportBill = new FImportBill();

            var IDIM1 = Convert.ToInt32(dgrvIm.CurrentRow.Cells["ID"].Value.ToString());
            var list = db.ProductDetails.Where(x => x.IDImBill == IDIM1).ToList();
            foreach (var item in list)
            {
                FProductDetail dt = new FProductDetail();
                dt.Delete(item.ID);
            }
            fImportBill.Delete(IDIM1);
            BindingdgrvIm();
        }

        private void txtIDIm_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDIm.Text))
            {
                btnEditIm.Enabled = false;
            }
            else
            {
                btnEditIm.Enabled = true;
            }
        }

        #endregion
        #region Export Bill



        public void Binding_CbWHExSearch()
        {
            List<WareHouse> lwh = (from wh in db.WareHouses select wh).ToList();
            cbWHExSearch.DataSource = lwh;
            cbWHExSearch.DisplayMember = "Warehouse";
            cbWHExSearch.ValueMember = "ID";


            cbWHExDt.DataSource = lwh;
            cbWHExDt.DisplayMember = "Warehouse";
            cbWHExDt.ValueMember = "ID";
        }
        public void BindingdgrvEx()
        {
            btnEditEx.Enabled = false;
            dgrvEx.Refresh();
            dgrvEx.DataSource = null;
            txtIDEx.Enabled = false;
            txtPriceEx.Enabled = false;
            txtPriceEx.Enabled = false;
            var IDWH = Convert.ToInt32(cbWHExSearch.SelectedValue.ToString());
            List<ExportBill> list = (from ex in db.ExportBills.Where(x => x.IDWareHouse == IDWH) select ex).ToList();

            dgrvEx.AutoGenerateColumns = false;

            dgrvEx.ColumnCount = 6;
            dgrvEx.Columns[0].Name = "ID";
            dgrvEx.Columns[0].HeaderText = "ID";
            dgrvEx.Columns[0].DataPropertyName = "ID";

            dgrvEx.Columns[1].Name = "Bill";
            dgrvEx.Columns[1].HeaderText = "Tên Phi?u";
            dgrvEx.Columns[1].DataPropertyName = "Bill";


            dgrvEx.Columns[2].Name = "IDWareHouse";
            dgrvEx.Columns[2].HeaderText = "Mã kho";
            dgrvEx.Columns[2].DataPropertyName = "IDWareHouse";

            dgrvEx.Columns[3].Name = "Date";
            dgrvEx.Columns[3].HeaderText = "Ngày nh?p";
            dgrvEx.Columns[3].DataPropertyName = "Date";

            dgrvEx.Columns[4].Name = "Quantity";
            dgrvEx.Columns[4].HeaderText = "S? lu?ng";
            dgrvEx.Columns[4].DataPropertyName = "Quantity";

            dgrvEx.Columns[5].Name = "TotalPrice";
            dgrvEx.Columns[5].HeaderText = "T?ng ti?n";
            dgrvEx.Columns[5].DataPropertyName = "TotalPrice";

            dgrvEx.DataSource = list;
            //txtIDEx.Text = dgrvEx.CurrentRow.Cells["ID"].Value.ToString();
            //txtPro.Text = dgrvEx.CurrentRow.Cells["Product1"].Value.ToString();
            //cbCompany.Text = dgrvEx.CurrentRow.Cells["Company"].Value.ToString();
            //txtOrdinaryPrice.Text = dgrvEx.CurrentRow.Cells["OrdinaryPrice"].Value.ToString();
            //txtPrice.Text = dgrvEx.CurrentRow.Cells["Price"].Value.ToString();

        }

        public void Binding_dgrvDTEx()
        {
            dgrvDTEx.Refresh();
            dgrvDTEx.DataSource = null;
            txtIDPro.Enabled = false;
            var IDBill = Convert.ToInt32(txtIDEx.Text.ToString());
            var list = (from dt in db.ProductDetails.Where(x => x.IDExBill == IDBill).ToList()
                        from pr in db.Products.Where(x => x.ID == dt.IDProduct).ToList()
                        from com in db.Companies.Where(x => x.ID == pr.IDCompany).ToList()
                        select new
                        {
                            ID = pr.ID,
                            Product1 = pr.Product1,
                            Company = com.Company1,
                            Quantity = dt.Quantity,
                            OrdinaryPrice = pr.OrdinaryPrice
                        }
                                  ).ToList();

            dgrvDTEx.AutoGenerateColumns = false;

            dgrvDTEx.ColumnCount = 5;
            dgrvDTEx.Columns[0].Name = "ID";
            dgrvDTEx.Columns[0].HeaderText = "ID";
            dgrvDTEx.Columns[0].DataPropertyName = "ID";

            dgrvDTEx.Columns[1].Name = "Product1";
            dgrvDTEx.Columns[1].HeaderText = "Tên s?n ph?m";
            dgrvDTEx.Columns[1].DataPropertyName = "Product1";


            dgrvDTEx.Columns[2].Name = "Company";
            dgrvDTEx.Columns[2].HeaderText = "Mã công ty";
            dgrvDTEx.Columns[2].DataPropertyName = "Company";

            dgrvDTEx.Columns[3].Name = "Quantity";
            dgrvDTEx.Columns[3].HeaderText = "S? lu?ng";
            dgrvDTEx.Columns[3].DataPropertyName = "Quantity";

            dgrvDTEx.Columns[4].Name = "OrdinaryPrice";
            dgrvDTEx.Columns[4].HeaderText = "Giá g?c";
            dgrvDTEx.Columns[4].DataPropertyName = "OrdinaryPrice";

            dgrvDTEx.DataSource = list;
        }



        private void dgrvEx_MouseClick(object sender, MouseEventArgs e)
        {

            txtIDEx.Text = dgrvEx.CurrentRow.Cells["ID"].Value.ToString();
            txtEx.Text = dgrvEx.CurrentRow.Cells["Bill"].Value.ToString();
            cbWHExDt.SelectedValue = cbWHExSearch.SelectedValue;
            dtpEx.Text = dgrvEx.CurrentRow.Cells["Date"].Value.ToString();
            txtQuantityEx.Text = dgrvEx.CurrentRow.Cells["Quantity"].Value.ToString();
            txtPriceEx.Text = dgrvEx.CurrentRow.Cells["TotalPrice"].Value.ToString();
            Binding_dgrvDTEx();


        }

        private void btnAddEx_Click(object sender, EventArgs e)
        {
            FormExBill formExBill = new FormExBill();
            formExBill.Show();
        }

        private void btnRefreshEx_Click(object sender, EventArgs e)
        {
            txtIDEx.Text = "";
            txtEx.Text = "";
            cbWHExDt.SelectedValue = cbWHExSearch.SelectedValue;
            dtpEx.Text = dgrvEx.CurrentRow.Cells["Date"].Value.ToString();
            txtQuantityEx.Text = "";
            txtPriceEx.Text = "";
        }

        private void btnEditEx_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(txtIDEx.Text.ToString());
            string Bill = txtEx.Text;
            FormEditExBill formExBill = new FormEditExBill(ID, Bill);
            formExBill.Show();
        }

        private void btnDeleteEx_Click(object sender, EventArgs e)
        {
            FExportBill fExportBill = new FExportBill();

            var IDEx1 = Convert.ToInt32(dgrvEx.CurrentRow.Cells["ID"].Value.ToString());
            var list = db.ProductDetails.Where(x => x.IDExBill == IDEx1).ToList();
            foreach (var item in list)
            {
                FProductDetail dt = new FProductDetail();
                dt.Delete(item.ID);
            }
            fExportBill.Delete(IDEx1);
            BindingdgrvEx();
        }

        private void txtIDEx_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDEx.Text))
            {
                btnEditEx.Enabled = false;
            }
            else
            {
                btnEditEx.Enabled = true;
            }
        }

        private void cbWHExSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingdgrvEx();
            txtIDEx.Text = dgrvEx.CurrentRow.Cells["ID"].Value.ToString();
            txtEx.Text = dgrvEx.CurrentRow.Cells["Bill"].Value.ToString();
            cbWHExDt.SelectedValue = cbWHExSearch.SelectedValue;
            dtpEx.Text = dgrvEx.CurrentRow.Cells["Date"].Value.ToString();
            txtQuantityEx.Text = dgrvEx.CurrentRow.Cells["Quantity"].Value.ToString();
            txtPriceEx.Text = dgrvEx.CurrentRow.Cells["TotalPrice"].Value.ToString();
            Binding_dgrvDTEx();
        }
        #endregion
    }
}
