using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lí_điểm_sinh_viên
{
    public partial class QuanliSV : Form
    {
        public QuanliSV()
        {
            InitializeComponent();
            this.Controls.Add(lvSinhVien);
        }

        private void ThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private string tenDangNhap;

        public QuanliSV(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            this.Controls.Add(lvSinhVien);
        }
        private void ĐiểmSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLDiem formDiem = new QLDiem(tenDangNhap);
            formDiem.Show(); // hiện form mới
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem item = lvSinhVien.SelectedItems[0];
                txtMaSV.Text = item.Text;
                txtHoTen.Text = item.SubItems[1].Text;
                txtGioiTinh.Text = item.SubItems[2].Text;
                txtNgaySinh.Text = item.SubItems[3].Text;
                txtLop.Text = item.SubItems[4].Text;
            }
            lvSinhVien.View = View.Details;
            lvSinhVien.FullRowSelect = true;
            lvSinhVien.GridLines = true;

            lvSinhVien.Columns.Add("Mã SV", 100);
            lvSinhVien.Columns.Add("Họ tên", 150);
            lvSinhVien.Columns.Add("Giới tính", 80);
            lvSinhVien.Columns.Add("Ngày sinh", 100);
            lvSinhVien.Columns.Add("Lớp", 80);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // Kiểm tra các ô nhập liệu
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtGioiTinh.Text) ||
                string.IsNullOrWhiteSpace(txtNgaySinh.Text) ||
                string.IsNullOrWhiteSpace(txtLop.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sinh viên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Tạo một dòng mới cho ListView
            ListViewItem item = new ListViewItem(txtMaSV.Text); // Cột đầu tiên

            // Thêm các cột tiếp theo (SubItems)
            item.SubItems.Add(txtHoTen.Text);
            item.SubItems.Add(txtGioiTinh.Text);
            item.SubItems.Add(txtNgaySinh.Text);
            item.SubItems.Add(txtLop.Text);

            // Thêm dòng vào ListView
            lvSinhVien.Items.Add(item);

            // Xóa nội dung TextBox sau khi lưu (nếu muốn)
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtGioiTinh.Clear();
            txtNgaySinh.Clear();
            txtLop.Clear();

            // Đưa con trỏ về txtMaSV để tiện nhập tiếp
            txtMaSV.Focus();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count > 0)
            {
                lvSinhVien.Items.Remove(lvSinhVien.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void QuanliSV_Load(object sender, EventArgs e)
        {

        }

        private void QuảnLíSinhViênToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            QuanliSV quanliSV = new QuanliSV();
            quanliSV.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvSinhVien.SelectedItems[0];
                selectedItem.Text = txtMaSV.Text;
                selectedItem.SubItems[1].Text = txtHoTen.Text;
                selectedItem.SubItems[2].Text = txtGioiTinh.Text;
                selectedItem.SubItems[3].Text = txtNgaySinh.Text;
                selectedItem.SubItems[4].Text = txtLop.Text;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
