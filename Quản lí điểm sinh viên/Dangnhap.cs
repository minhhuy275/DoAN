using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDiemSinhVien;

namespace Quản_lí_điểm_sinh_viên
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            bool isFound = false;

            foreach (var dn in Data.DangNhapList)
            {
                if (dn.TenDangNhap == username && dn.MatKhau == password)
                {
                    isFound = true;
                    QLDiem f = new QLDiem(dn.TenDangNhap);
                    f.ShowDialog();
                    this.Close();
                    return; 
                }
            }

            // Nếu không có tài khoản đúng, hiện thông báo 1 lần duy nhất
            MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Data.GetAllDangNhap();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
