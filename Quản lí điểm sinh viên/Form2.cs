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
    public partial class QLDiem : Form
    {
        private string tenDangNhap;
        DataTable dt = new DataTable();
        public QLDiem(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LoadTableSinhVien();
            // tăng kích thước bảng sinh viên  
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Size = new Size(903, 312);


        }

        private void LoadTableSinhVien()
        {
            string query = " select * from SinhVien ";
            dt.Clear();
            dt = Data.LoadCSDL(query);
            dataGridView1.DataSource = dt;

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

    

        private void quảnLíToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnableControls(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc, btnLuu });
            UnEnableControls(new List<Control> { btnXoa, btnSua });
         ResetText(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc});
            txtHovsTenSV.Focus();
        }
        private void EnableControls(List<Control> controls )
        {
            foreach (Control control in controls) { 
            
            
              control.Enabled = true;
            }
        }
        private void UnEnableControls(List<Control> controls)
        {
            foreach (Control control in controls) {
                control.Text = "";

               
            }
        }

        private void ResetText(List<Control> controls)
        {
            foreach (Control control in controls)
                control.Text = "";
        }
        private void DisableControls(List<Control> controls)
        {
            foreach (Control control in controls)
                control.Enabled = false;
        }
        private void QLDiem_Load(object sender, EventArgs e)
        {
            DisableControls(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc, btnLuu });
        
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string hoTen = txtHovsTenSV.Text;
            string maSV = txtMa.Text;
            string diemStr = txtDiem.Text;
            string monHoc = txtMonHoc.Text;

            if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(diemStr) || string.IsNullOrWhiteSpace(monHoc))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            decimal diemMoi;
            if (!decimal.TryParse(diemStr.Replace(',', '.'), out diemMoi))
            {
                MessageBox.Show("Điểm không hợp lệ.");
                return;
            }

            // Lưu vào bảng SinhVien
            string query = $"INSERT INTO SinhVien (HoTen, MaSV, Diem, MonHoc) VALUES (N'{hoTen}', '{maSV}', {diemMoi.ToString().Replace(',', '.')}, N'{monHoc}')";
            int kq = Data.ThaoTacCSDL(query);

            if (kq > 0)
            {
                // Ghi lịch sử điểm: chỉ có thông tin mới, không có thông tin cũ
                Data.GhiLichSu(
                    maSV,
                    "", // Tên cũ
                    hoTen, // Tên mới
                    "", // Môn học cũ
                    monHoc, // Môn học mới
                    0, // Điểm cũ
                    diemMoi, // Điểm mới
                    "Thêm",
                    tenDangNhap
                );

                MessageBox.Show("Thêm thành công");
                LoadTableSinhVien();

                UnEnableControls(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc, btnLuu });
                ResetText(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc, btnLuu });
                btnLuu.Enabled = true;

            }
            else
            {
                MessageBox.Show("Thêm không thành công");
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             if(dataGridView1.SelectedRows.Count > 0)
            {
                // lấy dòng được chọn 
                var dongDuocChon = dataGridView1.SelectedRows[0];
                // truyền vô txt
                txtHovsTenSV.Text = dongDuocChon.Cells["HoTen"].Value.ToString();
                txtDiem.Text= dongDuocChon.Cells["Diem"].Value.ToString();
                txtMa.Text = dongDuocChon.Cells["MaSV"].Value.ToString();
                txtMonHoc.Text = dongDuocChon.Cells["MonHoc"].Value.ToString();

                // hiển thị text box  lên 
                EnableControls(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc ,btnXoa,btnSua });
                   
                txtMa.Enabled=false;
                    
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSV = txtMa.Text;
            string hoTenMoi = txtHovsTenSV.Text;
            string monHocMoi = txtMonHoc.Text;
            decimal diemMoi = decimal.Parse(txtDiem.Text.Replace(',', '.'));

            // Tìm dòng sinh viên cũ từ DataTable
            DataRow svCu = dt.AsEnumerable().FirstOrDefault(row => row["MaSV"].ToString() == maSV);

            if (svCu == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên cần sửa.");
                return;
            }

            // Lấy thông tin cũ
            string hoTenCu = svCu["HoTen"].ToString();
            string monHocCu = svCu["MonHoc"].ToString();
            decimal diemCu = Convert.ToDecimal(svCu["Diem"]);

            // Ghi lịch sử điểm
            Data.GhiLichSu(maSV, hoTenCu, hoTenMoi, monHocCu, monHocMoi, diemCu, diemMoi, "Sửa", tenDangNhap);

            // Cập nhật bảng SinhVien
            string query = $@"
        UPDATE SinhVien SET 
            HoTen = N'{hoTenMoi}', 
            Diem = {diemMoi.ToString().Replace(',', '.')}, 
            MonHoc = N'{monHocMoi}'
        WHERE MaSV = '{maSV}'";

            int kq = Data.ThaoTacCSDL(query);
            if (kq > 0)
            {
                MessageBox.Show("Sửa thành công");
                LoadTableSinhVien();
                UnEnableControls(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc, btnLuu, btnSua, btnXoa });
                ResetText(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc, btnLuu });
              

            }
            else
            {
                MessageBox.Show("Sửa không thành công");
            }
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSV = txtMa.Text;

            if (string.IsNullOrWhiteSpace(maSV))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên để xoá.");
                return;
            }

            // Lấy thông tin cũ trước khi xóa
            string hoTenCu = null, monHocCu = null;
            decimal? diemCu = null;

            foreach (DataRow row in dt.Rows)
            {
                if (row["MaSV"].ToString() == maSV)
                {
                    hoTenCu = row["HoTen"].ToString();
                    monHocCu = row["MonHoc"].ToString();
                    diemCu = Convert.ToDecimal(row["Diem"]);
                    break;
                }
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            // Xoá khỏi bảng SinhVien
            string query = $"DELETE FROM SinhVien WHERE MaSV = '{maSV}'";
            int kq = Data.ThaoTacCSDL(query);

            if (kq > 0)
            {
                // Ghi lịch sử điểm: chỉ có dữ liệu cũ
                Data.GhiLichSu(
                    maSV,
                    hoTenCu, null,         // Tên cũ, tên mới
                    monHocCu, null,        // Môn học cũ, mới
                    diemCu, null,          // Điểm cũ, mới
                    "Xóa",
                    tenDangNhap
                );

                MessageBox.Show("Xóa thành công");
                LoadTableSinhVien();
                UnEnableControls(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc, btnLuu, btnSua, btnXoa });
                ResetText(new List<Control> { txtDiem, txtHovsTenSV, txtMa, txtMonHoc });
            }
            else
            {
                MessageBox.Show("Xóa không thành công");
            }
        }



        private void btnTim_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTuKhoa.Text;
            string query = $"SELECT * from SinhVien WHERE HoTen LIKE N'%{tukhoa}%'";
            dt.Clear();
            dt = Data.LoadCSDL(query);
            dataGridView1.DataSource = dt;
        }

        private void lịchSửĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void txtTuKhoa_TextChanged(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
