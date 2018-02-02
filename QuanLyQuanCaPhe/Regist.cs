using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe
{
    public partial class Regist : Form
    {
        QuanLyQuanCapheEntities db = new QuanLyQuanCapheEntities();

        public Regist()
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            if (txtName.Text == "" || txtUSname.Text =="" || txtPassword.Text=="" )
            {
                MessageBox.Show("Xin nhập đầy đủ thông tin","Thông báo",MessageBoxButtons.OK);
            }

            else
            {
                Account c = new Account();
               
                c.Username = txtUSname.Text;
                c.Password = md5(txtPassword.Text);
                c.Name = txtName.Text;
                c.Type = 0;
                
                db.Accounts.Add(c);
                
                db.SaveChanges();
                MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButtons.OK);
                this.Hide();
                l.ShowDialog();
                this.Show();
            }
        }
    }
}
