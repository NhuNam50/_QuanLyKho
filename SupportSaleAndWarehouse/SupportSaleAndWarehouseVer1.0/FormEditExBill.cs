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

namespace SupportSaleAndWarehouseVer1._0
{
    public partial class FormEditExBill : Form
    {
        WareHouseDbContext db = new WareHouseDbContext();
        List<ProductDetail> lprodt;
        List<Product> lpro;
        public int IDExBill;
        public FormEditExBill(int ID, string Bill)
        {
            InitializeComponent();
            IDExBill = ID;
            txtBillName.Text = Bill;
        }

        private void FormEditExBill_Load(object sender, EventArgs e)
        {
            Binding_CbWH();
            Load_CbPro();
            lprodt = new List<ProductDetail>();
            lpro = new List<Product>();
            btnEdit.Enabled = false;
            btnAddPro.Enabled = false;
            Binding_list();
            Binding_dgrvPro();
        }
        private void Binding_CbWH()
        {
            List<WareHouse> lwh = (from wh in db.WareHouses select wh).ToList();
            cbWH.DataSource = lwh;
            cbWH.DisplayMember = "Warehouse";
            cbWH.ValueMember = "ID";
        }
        private void Load_CbPro()
        {
            List<Product> list = (from pro in db.Products select pro).ToList();
            cbPro.DataSource = list;
            cbPro.DisplayMember = "Product1";
            cbPro.ValueMember = "ID";
        }


        public void Binding_list()
        {
            var listolder = (from dt in db.ProductDetails.Where(x => x.IDExBill == IDExBill).ToList()
                             from pr in db.Products.Where(x => x.ID == dt.IDProduct).ToList()
                             from com in db.Companies.Where(x => x.ID == pr.IDCompany).ToList()
                             select new
                             {
                                 IDProductDetail = dt.ID,
                                 ID = pr.ID,
                                 Product1 = pr.Product1,
                                 Quantity = dt.Quantity,
                                 OrdinaryPrice = pr.OrdinaryPrice
                             }
                           ).ToList();
            foreach (var item in listolder)
            {
                var search1 = lpro.Find(x => x.ID == item.ID);
                if (search1 == null)
                {
                    ProductDetail entity = new ProductDetail();
                    entity.ID = 1;
                    entity.IDExBill = IDExBill;
                    entity.Quantity = item.Quantity;
                    entity.IDProduct = item.ID;

                    // var ID = Convert.ToInt32(cbPro.SelectedValue.ToString());
                    lprodt.Add(entity);

                    Product pro = db.Products.Where(x => x.ID == item.ID).SingleOrDefault();
                    lpro.Add(pro);
                }
                else
                {
                    foreach (var dt in lprodt.Where(x => x.IDProduct == item.ID))
                    {
                        dt.Quantity = dt.Quantity + item.Quantity;
                    }
                }

            }
        }

        private void SumPriceAndQuantity()
        {
            var list = (from item1 in lpro
                        join item2 in lprodt
                        on item1.ID equals item2.IDProduct
                        orderby item1.ID
                        select new
                        {
                            Product1 = item1.Product1,
                            Quantity = item2.Quantity,
                            OrdinaryPrice = item1.OrdinaryPrice
                        }).ToList();
            var listPrice = (from item in list
                             select new
                             {
                                 Money = item.Quantity * item.OrdinaryPrice
                             }
                             ).ToList();
            var sumPrice = listPrice.Sum(x => x.Money);
            var sumQuantity = list.Sum(x => x.Quantity);
            txtMoney.Text = sumPrice.ToString();
            txtQuantity.Text = sumQuantity.ToString();
        }

        private void Binding_dgrvPro()
        {
            dgrvPro.DataSource = null;
            dgrvPro.AutoGenerateColumns = false;


            dgrvPro.ColumnCount = 3;

            dgrvPro.Columns[0].Name = "Product1";
            dgrvPro.Columns[0].HeaderText = "Sản phẩm";
            dgrvPro.Columns[0].DataPropertyName = "Product1";

            dgrvPro.Columns[1].Name = "Quantity";
            dgrvPro.Columns[1].HeaderText = "Số lượng";
            dgrvPro.Columns[1].DataPropertyName = "Quantity";

            dgrvPro.Columns[2].Name = "OrdinaryPrice";
            dgrvPro.Columns[2].HeaderText = "Giá gốc";
            dgrvPro.Columns[2].DataPropertyName = "OrdinaryPrice";
            var list = (from item1 in lpro
                        join item2 in lprodt
                        on item1.ID equals item2.IDProduct
                        orderby item1.ID
                        select new
                        {
                            Product1 = item1.Product1,
                            Quantity = item2.Quantity,
                            OrdinaryPrice = item1.OrdinaryPrice
                        }).ToList();

            dgrvPro.DataSource = list;
            txtBillName.Text = db.ExportBills.Where(x => x.ID == IDExBill).SingleOrDefault().Bill.ToString();
            dateTimePicker.Text = db.ExportBills.Where(x => x.ID == IDExBill).SingleOrDefault().Date.ToString();
            SumPriceAndQuantity();
        }

        private void btnAddPro_Click(object sender, EventArgs e)
        {
            var ite = cbPro.SelectedValue.ToString();
            var search = lpro.Find(x => x.ID == Convert.ToInt32(ite));
            if (search == null)
            {
                ProductDetail entity = new ProductDetail();
                entity.Quantity = Convert.ToInt32(numeric.Value.ToString());
                entity.IDProduct = Convert.ToInt32(cbPro.SelectedValue.ToString());
                entity.IDExBill = IDExBill;


                var ID = Convert.ToInt32(cbPro.SelectedValue.ToString());
                lprodt.Add(entity);

                Product pro = db.Products.Where(x => x.ID == ID).SingleOrDefault();
                lpro.Add(pro);
            }
            else
            {
                foreach (var dt in lprodt.Where(x => x.IDProduct == Convert.ToInt32(cbPro.SelectedValue.ToString())))
                {
                    dt.Quantity = dt.Quantity + Convert.ToInt32(numeric.Value.ToString());
                }
            }


            var list = (from item1 in lpro
                        join item2 in lprodt
                        on item1.ID equals item2.IDProduct
                        orderby item1.ID
                        select new
                        {
                            Product1 = item1.Product1,
                            Quantity = item2.Quantity,
                            OrdinaryPrice = item1.OrdinaryPrice
                        }).ToList();


            dgrvPro.DataSource = null;
            dgrvPro.AutoGenerateColumns = false;

            dgrvPro.ColumnCount = 3;

            dgrvPro.Columns[0].Name = "Product1";
            dgrvPro.Columns[0].HeaderText = "Sản phẩm";
            dgrvPro.Columns[0].DataPropertyName = "Product1";

            dgrvPro.Columns[1].Name = "Quantity";
            dgrvPro.Columns[1].HeaderText = "Số lượng";
            dgrvPro.Columns[1].DataPropertyName = "Quantity";

            dgrvPro.Columns[2].Name = "OrdinaryPrice";
            dgrvPro.Columns[2].HeaderText = "Giá gốc";
            dgrvPro.Columns[2].DataPropertyName = "OrdinaryPrice";

            dgrvPro.DataSource = list;


            SumPriceAndQuantity();
            Binding_dgrvPro();
            btnEdit.Enabled = true;
        }

        private void btnDeletePro_Click(object sender, EventArgs e)
        {
            var CurrentPro = dgrvPro.CurrentRow.Cells["Product1"].Value.ToString();
            var ID = db.Products.Where(x => x.Product1 == CurrentPro).SingleOrDefault().ID;
            lprodt.RemoveAll(x => x.IDProduct == ID);
            lpro.RemoveAll(x => x.ID == ID);
            var list = (from item1 in lpro
                        join item2 in lprodt
                        on item1.ID equals item2.IDProduct
                        orderby item1.ID
                        select new
                        {
                            Product1 = item1.Product1,
                            Quantity = item2.Quantity,
                            OrdinaryPrice = item1.OrdinaryPrice
                        }).ToList();
            dgrvPro.DataSource = null;
            dgrvPro.AutoGenerateColumns = false;

            dgrvPro.ColumnCount = 3;

            dgrvPro.Columns[0].Name = "Product1";
            dgrvPro.Columns[0].HeaderText = "Sản phẩm";
            dgrvPro.Columns[0].DataPropertyName = "Product1";

            dgrvPro.Columns[1].Name = "Quantity";
            dgrvPro.Columns[1].HeaderText = "Số lượng";
            dgrvPro.Columns[1].DataPropertyName = "Quantity";

            dgrvPro.Columns[2].Name = "OrdinaryPrice";
            dgrvPro.Columns[2].HeaderText = "Giá gốc";
            dgrvPro.Columns[2].DataPropertyName = "OrdinaryPrice";

            dgrvPro.DataSource = list;
            SumPriceAndQuantity();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ExportBill entity = new ExportBill();
            entity.ID = IDExBill;
            entity.Bill = txtBillName.Text;
            entity.IDWareHouse = Convert.ToInt32(cbWH.SelectedValue);
            entity.Date = dateTimePicker.Value;
            entity.Quantity = Convert.ToInt32(txtQuantity.Text);
            entity.TotalPrice = Convert.ToInt32(txtMoney.Text);
            FExportBill Ex = new FExportBill();
            var result = Ex.Update(entity);
            var listolder = (from dt in db.ProductDetails.Where(x => x.IDExBill == IDExBill).ToList()
                             from pr in db.Products.Where(x => x.ID == dt.IDProduct).ToList()
                             from com in db.Companies.Where(x => x.ID == pr.IDCompany).ToList()
                             select new
                             {
                                 IDProductDetail = dt.ID,
                                 ID = pr.ID,
                                 Product1 = pr.Product1,
                                 Quantity = dt.Quantity,
                                 OrdinaryPrice = pr.OrdinaryPrice
                             }
                           ).ToList();
            foreach (var item in lprodt)
            {
                var search = listolder.Find(x => x.ID == item.IDProduct);
                if (search == null)
                {
                    FProductDetail dt = new FProductDetail();

                    dt.Insert(item);
                }
                else
                {
                    item.ID = search.IDProductDetail;
                    FProductDetail dt = new FProductDetail();
                    dt.Update(item);
                }
            }
            if (result == true)
            {
                MessageBox.Show("Sửa phiếu nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBillName.Text = "";
            txtMoney.Text = "";
            txtQuantity.Text = "";
            dgrvPro.DataSource = null;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBillName_TextChanged(object sender, EventArgs e)
        {
            if (txtBillName.Text != "")
            {
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
            }
        }

        private void numeric_ValueChanged(object sender, EventArgs e)
        {
            if (numeric.Value != 0)
            {
                btnAddPro.Enabled = true;
            }
            else
            {
                btnAddPro.Enabled = false;
            }
        }
    }
}