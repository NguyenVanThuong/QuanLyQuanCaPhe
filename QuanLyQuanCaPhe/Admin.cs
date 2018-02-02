using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace QuanLyQuanCafe
{
    public partial class Admin : Form
    {

        QuanLyQuanCapheEntities1 db = new QuanLyQuanCapheEntities1();

        public Admin()
        {
            InitializeComponent();           
            loadDateOnCbbFodoCategories();
            dtgvCategory.DataSource = (from f in db.FoodCategories where f.idStatusDelete == 0 select f).ToList();

            dtgvAccount.DataSource = (from f in db.Accounts where f.idStatusDelete == 0 select f).ToList();
            dtgvTable.DataSource = (from f in db.TableFoods where f.idStatusDelete == 0 select f).ToList();

            dtgvFood.DataSource = (from f in db.Foods where f.idStatusDelete == 0 select f).ToList();
            //dtgvDoanhThu.DataSource = db.Bills.ToList();
            LoadBill();
            LoadDateTime();
        }
        void LoadBilbyDate(DateTime dfrom,DateTime dto)
        {
            var bill = (from b in db.Bills where b.DateIn >= dfrom && b.DateIn <= dto && b.DateOut != null select b).ToList();


            List<BillDAO> listbd = new List<BillDAO>();
            foreach (var item in bill)
            {
                BillDAO bd = new BillDAO();

                var tablename = (from t in db.TableFoods where t.id == item.idTable select t).SingleOrDefault();
                bd.Table = tablename.Name;

                bd.Id = item.id;
                bd.Checkin = item.DateIn;
                bd.Checkout = item.DateOut;
                bd.Username = item.userName;
                var tongtien = (from b in db.BillDetails where b.idBill == item.id select b).ToList();
                double tong = 0;
                foreach (var item2 in tongtien)
                {
                    var food = (from f in db.Foods where f.id == item2.idFood select f.Price).SingleOrDefault();
                    tong = tong + (food * item2.COUNT);
                }
                bd.Tongtien = tong;
                listbd.Add(bd);
            }

            dtgvDoanhThu.DataSource = listbd;
        }
        void LoadBill()
        {
            var bill = (from b in db.Bills where b.DateOut != null select b ).ToList();
            
            
            List<BillDAO> listbd = new List<BillDAO>();
            foreach(var item in bill)
            {
                BillDAO bd = new BillDAO();

                var tablename = (from t in db.TableFoods where t.id == item.idTable select t).SingleOrDefault();
                bd.Table = tablename.Name;

                bd.Id = item.id;                
                bd.Checkin = item.DateIn;
                bd.Checkout = item.DateOut;
                bd.Username = item.userName;
                var tongtien = (from b in db.BillDetails where b.idBill == item.id select b).ToList();
                double tong = 0;
                foreach(var item2 in tongtien)
                {
                    var food = (from f in db.Foods where f.id == item2.idFood select f.Price).SingleOrDefault();
                    tong = tong +( food * item2.COUNT );
                }
                bd.Tongtien = tong;
                listbd.Add(bd);                                                                
            }

            dtgvDoanhThu.DataSource = listbd;
        }

        public void loadDateOnCbbFodoCategories()
        {
            cbFoodCategory.Items.Clear();
            var Check = from s in db.FoodCategories select s;
            foreach (FoodCategory item in Check)
            {
                cbFoodCategory.Items.Add(item.Name);
            }
        }
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
        List<Food> SearchFood(string name)
        {
            List<Food> listFood = new List<Food>();
             listFood = (from d in db.Foods where d.Name.Contains(name) && d.idStatusDelete == 0 select d).ToList<Food>();
            
            return listFood;
        }
        void LoadDateTime()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year,today.Month,1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            dtgvAccount.DataSource = (from f in db.Accounts where f.idStatusDelete == 0 select f).ToList();
        }

        private void dtgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUserName.Text = dtgvAccount.SelectedCells[0].OwningRow.Cells["Username"].Value.ToString();
            txtDisplayName.Text = dtgvAccount.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
            txtPassword.Text = dtgvAccount.SelectedCells[0].OwningRow.Cells["Password"].Value.ToString();
            cbAccountType.Text = dtgvAccount.SelectedCells[0].OwningRow.Cells["Type"].Value.ToString();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
           
            if (txtDisplayName.Text == "" || txtUserName.Text == "" || txtPassword.Text == "" || cbAccountType.Text == "")
            {
                MessageBox.Show("Xin nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
            }

            else
            {
                Account c = new Account();                
                c.Username =txtUserName.Text;
                c.Password = md5(txtPassword.Text);
                c.Name = txtDisplayName.Text;
                string loai = cbAccountType.Text;
                if (loai == "admin")
                    c.Type = 1;
                else
                    c.Type =0;
               
                db.Accounts.Add(c);
               
                db.SaveChanges();
                MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButtons.OK);
                dtgvAccount.DataSource = (from f in db.Accounts where f.idStatusDelete == 0 select f).ToList();
            }
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            if (txtDisplayName.Text == "" || txtUserName.Text == "" || txtPassword.Text == "" || cbAccountType.Text == "")
            {
                MessageBox.Show("nhap thong tin day du");
            }
            else
            {
                txtUserName.Text = dtgvAccount.SelectedCells[0].OwningRow.Cells["Username"].Value.ToString();
                var update = (from s in db.Accounts where s.Username == txtUserName.Text select s).SingleOrDefault();
                update.Username = txtUserName.Text;
                update.Password = md5(txtPassword.Text);
                update.Name = txtDisplayName.Text;
                update.Type =Convert.ToInt32(cbAccountType.Text);
                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công !!", "Thông báo", MessageBoxButtons.OK);
                dtgvAccount.DataSource = (from f in db.Accounts where f.idStatusDelete == 0 select f).ToList();
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            txtUserName.Text = dtgvAccount.SelectedCells[0].OwningRow.Cells["Username"].Value.ToString();
            var delete = (from d in db.Accounts where d.Username==txtUserName.Text select d).SingleOrDefault();
            delete.idStatusDelete = 1;
            db.SaveChanges();
            MessageBox.Show("Xóa thông tin thành công !!", "Thông báo", MessageBoxButtons.OK);
            dtgvAccount.DataSource = (from f in db.Accounts where f.idStatusDelete == 0 select f).ToList();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if(txtTableName.Text=="")
            {
                MessageBox.Show("Xin nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
            }

            else
            {
                TableFood t = new TableFood();
                                
                var trung = (from f in db.TableFoods where f.Name == txtTableName.Text && f.idStatusDelete == 0 select f).SingleOrDefault();
                if(trung != null)
                {
                    MessageBox.Show("trùng tên bàn!!");
                    return;
                }
                t.Name = txtTableName.Text;
                t.stat = "Trống";
                t.idStatusDelete = 0;
                db.TableFoods.Add(t);
                
                db.SaveChanges();
                MessageBox.Show("Thêm bàn thành công", "Thông báo", MessageBoxButtons.OK);
                dtgvTable.DataSource = (from f in db.TableFoods where f.idStatusDelete == 0 select f).ToList();
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            dtgvTable.DataSource = (from f in db.TableFoods where f.idStatusDelete == 0 select f).ToList();
        }

        private void dtgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTableID.Text= dtgvTable.SelectedCells[0].OwningRow.Cells["id"].Value.ToString();
            txtTableName.Text= dtgvTable.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            txtTableID.Text = dtgvTable.SelectedCells[0].OwningRow.Cells["id"].Value.ToString();            
            int id = Convert.ToInt32(txtTableID.Text);
            var delete = (from d in db.TableFoods where d.id == id select d).SingleOrDefault();
            delete.idStatusDelete = 1;
            db.SaveChanges();
            MessageBox.Show("Xóa bàn thành công", "Thông báo", MessageBoxButtons.OK);
            dtgvTable.DataSource = (from f in db.TableFoods where f.idStatusDelete == 0 select f).ToList();
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            if (txtTableName.Text == "")
            {
                MessageBox.Show("Thông tin không tồn tại");
            }
            else
            {
                txtTableID.Text = dtgvTable.SelectedCells[0].OwningRow.Cells["id"].Value.ToString();
                int id = int.Parse(txtTableID.Text);
                var update = (from s in db.TableFoods where s.id==id select s).SingleOrDefault();
                if(update.stat != "Trống")
                {
                    MessageBox.Show("Bàn đang có người không thể sửa!!");
                    return;
                }
                var trung = (from f in db.TableFoods where f.Name == txtTableName.Text && f.idStatusDelete == 0 select f).SingleOrDefault();
                if (trung != null)
                {
                    MessageBox.Show("trùng tên bàn!!");
                    return;
                }
                update.Name = txtTableName.Text;
                db.SaveChanges();
                MessageBox.Show("Sửa bàn thành công", "Thông báo", MessageBoxButtons.OK);
                dtgvTable.DataSource = (from f in db.TableFoods where f.idStatusDelete == 0 select f).ToList();
            }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            dtgvCategory.DataSource = db.FoodCategories.ToList();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text == "" )
            {
                MessageBox.Show("Xin nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
            }

            else
            {
                FoodCategory f = new FoodCategory();
                
                f.Name = txtCategoryName.Text;
                db.FoodCategories.Add(f);
                db.SaveChanges();
                MessageBox.Show("Thêm danh mục thành công", "Thông báo", MessageBoxButtons.OK);
                dtgvTable.DataSource = (from d in db.TableFoods where d.idStatusDelete == 0 select d).ToList();
                loadDateOnCbbFodoCategories();
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            txtCategoryID.Text = dtgvCategory.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString();
            int id = int.Parse(txtCategoryID.Text);
            var delete = (from d in db.FoodCategories where d.id == id select d).SingleOrDefault();
            delete.idStatusDelete = 1;
            db.SaveChanges();
            MessageBox.Show("Xóa danh mục thành công", "Thông báo", MessageBoxButtons.OK);
            dtgvTable.DataSource = (from f in db.TableFoods where f.idStatusDelete == 0 select f).ToList();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text == "" )
            {
                MessageBox.Show("Thông tin không tồn tại");
            }
            else
            {

                txtCategoryID.Text = dtgvCategory.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString();
                int id = int.Parse(txtCategoryID.Text);
                var update = (from d in db.FoodCategories where d.id == id select d).SingleOrDefault();
                update.Name = txtCategoryName.Text;
                db.SaveChanges();
                MessageBox.Show("Sửa danh mục thành công", "Thông báo", MessageBoxButtons.OK);
                dtgvTable.DataSource = (from f in db.TableFoods where f.idStatusDelete == 0 select f).ToList();
            }
        }

        private void dtgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryID.Text = dtgvCategory.SelectedCells[0].OwningRow.Cells["id"].Value.ToString();
            txtCategoryName.Text = dtgvCategory.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
            
        }

        private void tbnShowFood_Click(object sender, EventArgs e)
        {
            dtgvFood.DataSource = (from f in db.Foods where f.idStatusDelete == 0 select f).ToList();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (txtFoodName.Text == "" || cbFoodCategory.Text=="")
            {
                MessageBox.Show("Xin nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
            }

            else
            {
                
                Food f = new Food();             
                
                f.Name = txtFoodName.Text;
                f.Price =Convert.ToDouble(txtPrice.Text);
                var check = (from d in db.FoodCategories where d.Name == cbFoodCategory.Text select d.id).SingleOrDefault();
                f.idCategory = check;
                f.idStatusDelete = 0;
                db.Foods.Add(f);        
                db.SaveChanges();
                MessageBox.Show("Thêm danh mục thành công", "Thông báo", MessageBoxButtons.OK);
                dtgvFood.DataSource = (from d in db.Foods where d.idStatusDelete == 0 select d).ToList();
            }
        }

        private void cbFoodCategory_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void dtgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            txtFoodID.Text = dtgvFood.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString();
            int id = int.Parse(txtFoodID.Text);
            var delete = (from d in db.Foods where d.id == id select d).SingleOrDefault();
            delete.idStatusDelete = 1;
            db.SaveChanges();
            MessageBox.Show("Xóa danh mục thành công", "Thông báo", MessageBoxButtons.OK);
            dtgvFood.DataSource = (from f in db.Foods where f.idStatusDelete == 0 select f).ToList();
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            if (txtFoodName.Text == "" ||cbFoodCategory.Text =="" || txtPrice.Text=="")
            {
                MessageBox.Show("Thông tin không tồn tại");
            }
            else
            {

                txtFoodID.Text = dtgvFood.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString();
                int id = int.Parse(txtFoodID.Text);
                var update = (from d in db.Foods where d.id == id select d).SingleOrDefault();
                update.Name = txtFoodName.Text;
                var check = (from d in db.FoodCategories where d.Name == cbFoodCategory.Text select d);
                foreach (var item in check)
                {
                    if (item.Name == cbFoodCategory.Text)
                    {
                        update.idCategory = item.id;
                    }
                }
                update.Price =Convert.ToDouble(txtPrice.Text);
                db.SaveChanges();
                MessageBox.Show("Sửa danh mục thành công", "Thông báo", MessageBoxButtons.OK);
                dtgvFood.DataSource = (from f in db.Foods where f.idStatusDelete == 0 select f).ToList();
            }
        }

        private void dtgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Food f = new Food();
            FoodCategory fc = new FoodCategory();
            txtFoodID.Text = dtgvFood.SelectedCells[0].OwningRow.Cells["id"].Value.ToString();
            txtFoodName.Text = dtgvFood.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
            int s =Convert.ToInt32(dtgvFood.SelectedCells[0].OwningRow.Cells["idCategory"].Value.ToString());
           txtPrice.Text = dtgvFood.SelectedCells[0].OwningRow.Cells["Price"].Value.ToString();
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            List<Food> listFood = SearchFood(txtSearchFoodName.Text);
            dtgvFood.DataSource = listFood;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DateTime dfrom = dtpkFromDate.Value;
            DateTime dto = dtpkToDate.Value;
            LoadBilbyDate(dfrom, dto);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Chọn tài khoản", "Thông báo", MessageBoxButtons.OK);
                return;
            }
                
            var acc = (from d in db.Accounts where d.Username == txtUserName.Text select d).SingleOrDefault();
            acc.Password = md5("1");
            db.SaveChanges();
            MessageBox.Show("Đặt lại mật khẩu thành công", "Thông báo", MessageBoxButtons.OK);
            dtgvAccount.DataSource = (from f in db.Accounts where f.idStatusDelete == 0 select f).ToList();
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
    }
}
