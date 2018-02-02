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
    public partial class AccountInfo : Form
    {
        QuanLyQuanCapheEntities1 db = new QuanLyQuanCapheEntities1();

        private string Name;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        public AccountInfo(string acc)
        {
            InitializeComponent();
            this.Name = acc;    
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDisplayName.Text == "" || txtPassWord.Text == "" || txtNewPass.Text == "" ||txtReEnterPass.Text=="")
            {
                MessageBox.Show("nhap thong tin day du");
            }
            else
            {
                var update = (from s in db.Accounts where s.Username == txtUsername.Text select s).SingleOrDefault();
                update.Name = txtDisplayName.Text;
                if(update.Password != md5(txtPassWord.Text))
                {
                    MessageBox.Show("Nhập sai password!", "Thông báo", MessageBoxButtons.OK);
                }
                if(txtNewPass.Text != txtReEnterPass.Text)
                {
                    MessageBox.Show("Phải nhập giống nhau", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    update.Password = md5(txtReEnterPass.Text);
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin thành công !!", "Thông báo", MessageBoxButtons.OK);

                }
                
               

            }
        }

        private void AccountInfo_Load(object sender, EventArgs e)
        {
            var search = (from d in db.Accounts where d.Username == Name select d).SingleOrDefault();
            txtUsername.Text = search.Username;
            txtDisplayName.Text = search.Name;
        }
    }
}
