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
    public partial class FormExBill : Form
    {
       WareHouseDbContext db = new WareHouseDbContext();
        List<ProductDetail> lprodt;
        List<Product> lpro;
        public FormExBill()
        {
            InitializeComponent();
        }
        private void FormExBill_Load(object sender, EventArgs e)
        {
            Binding_CbWH();
            Load_CbPro();
            lprodt = new List<ProductDetail>();
            lpro = new List<Product>();
            btnAdd.Enabled = false;
            btnAddPro.Enabled = false;
        }

       
   
       
      


        private void btnAddPro_Click(object sender, EventArgs e)
        {
            List<WareHouse> lwh = (from wh in db.WareHouses select wh).ToList();
            cbWH.DataSource = lwh;
            cbWH.DisplayMember = "Warehouse";
            cbWH.ValueMember = "ID";
        }

        private void btnDeletePro_Click(object sender, EventArgs e)
        {
            List<Product> list = (from pro in db.Products select pro).ToList();
            cbPro.DataSource = list;
            cbPro.DisplayMember = "Product1";
            cbPro.ValueMember = "ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
           
        }

        private void txtBillName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void numeric_ValueChanged(object sender, EventArgs e)
        {
         
        }
    }
}
