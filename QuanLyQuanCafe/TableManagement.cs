using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class TableManagement : Form
    {
        QuanLyQuanCapheEntities1 db = new QuanLyQuanCapheEntities1();
        List<Button> lstButton = new List<Button>();
        

        private string AccountName;
        public string accountName
        {
            get { return AccountName; }
            set { AccountName = value;  }
        }
        public TableManagement(string acc)
        {
            InitializeComponent();
            this.AccountName = acc;
            LoadAdmin();
            LoadTable();
            LoadCategory();
        }

        void LoadAdmin()
        {
            var search = (from d in db.Accounts where d.Username == AccountName select d).SingleOrDefault();
            adminToolStripMenuItem.Enabled = search.Type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += "(" + search.Name + ")";
        }

        #region P
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountInfo a = new AccountInfo(AccountName);
            a.ShowDialog();            
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin ad = new Admin();
            
            ad.ShowDialog();
            LoadCategory();
            LoadTable();
        }

        private void TableManagement_Load(object sender, EventArgs e)
        {
            var search = (from d in db.Accounts where d.Username == AccountName select d).SingleOrDefault();
            adminToolStripMenuItem.Enabled = search.Type == 1;                        
            thôngTinTàiKhoảnToolStripMenuItem.Text += "(" + search.Name + ")";
            
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        #endregion


        #region T

        #region Method
        void LoadCategory()
        {
            var listCategory = (from c in db.FoodCategories where c.idStatusDelete == 0 select c).ToList<FoodCategory>();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListbyCategory(int id)
        {
            var listFood = (from f in db.Foods where f.idCategory == id && f.idStatusDelete == 0 select f).ToList<Food>();
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            var table = from s in db.TableFoods where s.idStatusDelete == 0 select s;
            foreach (var item in table)
            {
                Button btn = new Button() { Width = 80, Height = 80 };
                btn.Text = item.Name + Environment.NewLine + item.stat;
                btn.Click += btn_CLick;
                btn.Tag = item;
                lstButton.Add(btn);
                if (item.stat == "Trống")
                    btn.BackColor = Color.Aqua;
                else
                    btn.BackColor = Color.LightPink;
                flpTable.Controls.Add(btn);
            }
        }

        

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            double totalPrice = 0;
            var bill = (from b in db.Bills where b.idTable == id && b.stat == 0 select b).SingleOrDefault();
            if (bill != null)
            {
                var billinfo = (from bf in db.BillDetails where bf.idBill == bill.id select bf).ToList<BillDetail>();
                foreach (var item in billinfo)
                {
                    string foodname = (from f in db.Foods where f.id == item.idFood select f.Name).SingleOrDefault();
                    ListViewItem lsvItem = new ListViewItem(foodname);
                    lsvItem.SubItems.Add(item.COUNT.ToString());
                    double price = (from p in db.Foods where p.id == item.idFood select p.Price).SingleOrDefault();
                    lsvItem.SubItems.Add(price.ToString());
                    double Price = price * item.COUNT;
                    lsvItem.SubItems.Add(Price.ToString());
                    lsvBill.Items.Add(lsvItem);
                    totalPrice += Price;
                }
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            //Thread.CurrentThread.CurrentCulture = culture;
            lblPrintTotalPrice.Text = totalPrice.ToString("c", culture);
        }

        void InserBill(int idtable)
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            Bill bil = new Bill();
            bil.DateIn = date;
            bil.idTable = idtable;
            bil.stat = 0;
            bil.userName = accountName;
            db.Bills.Add(bil);
            db.SaveChanges();
        }
        void InserBillDetails(int idbill, int idFood, int count)
        {
            int billdetail = (from b in db.BillDetails where b.idBill == idbill select b).Count();
            if (billdetail > 0)
            {
                var billupdate = (from b in db.BillDetails where b.idBill == idbill && b.idFood == idFood select b).SingleOrDefault();
                if (billupdate != null)
                {
                    billupdate.COUNT += count;
                    if (billupdate.COUNT <= 0)
                    {
                        db.BillDetails.Remove(billupdate);
                    }
                    db.SaveChanges();
                }
                else
                {
                    if (count <= 0)
                    {
                        return;
                    }
                    BillDetail billdetails = new BillDetail();
                    billdetails.idBill = idbill;
                    billdetails.idFood = idFood;
                    billdetails.COUNT = count;
                    db.BillDetails.Add(billdetails);
                    db.SaveChanges();
                }

            }
            else
            {
                if (count <= 0)
                    return;
                BillDetail billdetails = new BillDetail();
                billdetails.idBill = idbill;
                billdetails.idFood = idFood;
                billdetails.COUNT = count;
                db.BillDetails.Add(billdetails);
                db.SaveChanges();
            }

        }
        int GetUnCheckBilbyIdTable(int id)
        {
            var bill = (from b in db.Bills where b.idTable == id && b.stat == 0 select b).SingleOrDefault();
            if (bill == null)
                return -1;
            return bill.id;
        }
        int GetMaxIdBill()
        {
            var idbill = (from b in db.Bills select b.id).Max();
            return idbill;
        }

        void CheckOut(int id)
        {
            var updatebill = (from b in db.Bills where b.id == id select b).SingleOrDefault();
            updatebill.DateOut = DateTime.Now;
            updatebill.stat = 1;
            db.SaveChanges();
        }

        void UpdateTableWhenPay(int id)
        {

            foreach (var item in lstButton)
            {
                TableFood tb = item.Tag as TableFood;
                if (tb.id == id)
                {
                    var tablefromdb = (from t in db.TableFoods where t.id == tb.id select t).SingleOrDefault();
                    item.BackColor = Color.Aqua;
                    tablefromdb.stat = "Trống";
                    db.SaveChanges();
                    item.Text = tablefromdb.Name + Environment.NewLine + tablefromdb.stat;

                }
            }
        }

        void UpdateTableWhenOder(int id)
        {
            foreach (var item in lstButton)
            {
                TableFood tb = item.Tag as TableFood;
                if (tb.id == id)
                {
                    var tablefromdb = (from t in db.TableFoods where t.id == tb.id select t).SingleOrDefault();
                    item.BackColor = Color.Pink;
                    tablefromdb.stat = "Có người";
                    db.SaveChanges();
                    item.Text = tablefromdb.Name + Environment.NewLine + tablefromdb.stat;

                }
            }
        }
        void DisplayTable(string name)
        {
            lblBan.Text = name;
        }
        #endregion

        #region Event   
        private void btn_CLick(object sender, EventArgs e)
        {
            int id = ((sender as Button).Tag as TableFood).id;
            lsvBill.Tag = (sender as Button).Tag;
            string nameTable = ((sender as Button).Tag as TableFood).Name;
            DisplayTable(nameTable);
            ShowBill(id);
        }
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            TableFood table = lsvBill.Tag as TableFood;
            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn!!");
                return;
            }


            int idBill = GetUnCheckBilbyIdTable(table.id);
            int foodId = (cbFood.SelectedItem as Food).id;
            int count = (int)nmFoodCount.Value;
            if (idBill == -1)
            {
                if ((int)nmFoodCount.Value <= (int)0)
                    return;
                InserBill(table.id);
                InserBillDetails(GetMaxIdBill(), foodId, count);
                UpdateTableWhenOder(table.id);
            }
            else
            {
                InserBillDetails(idBill, foodId, count);
            }
            ShowBill(table.id);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 1;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            FoodCategory selected = cb.SelectedItem as FoodCategory;
            id = selected.id;
            LoadFoodListbyCategory(id);
        }


        #endregion

        #endregion

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            TableFood table = lsvBill.Tag as TableFood;
            if (table == null)
                return;
            int idBill = GetUnCheckBilbyIdTable(table.id);
            if (idBill != -1)
            {
                if (MessageBox.Show("Thanh toán cho " + table.Name, "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    CheckOut(idBill);
                    UpdateTableWhenPay(table.id);

                    ShowBill(table.id);
                }
            }
        }

        private void TableManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
