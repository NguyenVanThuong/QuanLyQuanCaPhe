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
    public partial class Login : Form
    {
         public string Phu;
         static  string UserName;
         static int Type;
        QuanLyQuanCapheEntities db = new QuanLyQuanCapheEntities();

        public Login()
        {
            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            string userName = txtUsername.Text;
            string passWord = md5(txtPassword.Text);
            var check = (from s in db.Accounts where s.Username == userName && s.Password == passWord && s.idStatusDelete==0 select s).SingleOrDefault();
            if(check != null)
            {
                
                string AccountName = check.Username;
                TableManagement t = new TableManagement(AccountName);
                this.Hide();
                t.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Nhập sai tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK);
            }
                
         
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không?","Thông báo",MessageBoxButtons.OKCancel)!= DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
       
                Regist r = new Regist();
                this.Hide();
                r.ShowDialog();
            
            
        }
    }
}
