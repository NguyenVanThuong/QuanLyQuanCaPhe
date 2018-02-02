using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe
{
    class BillDAO
    {
        
        private DateTime? checkin;
        private double tongtien;
        private int id;
        private string username;
        private string table;
        private DateTime? checkout;

        public int Id { get => id; set => id = value; }
        public string Table { get => table; set => table = value; }
        public DateTime? Checkout { get => checkout; set => checkout = value; }
        public DateTime? Checkin { get => checkin; set => checkin = value; }
        public double Tongtien { get => tongtien; set => tongtien = value; }
        public string Username { get => username; set => username = value; }
    }
}
