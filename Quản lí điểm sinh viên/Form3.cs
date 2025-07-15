using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver.Core.Configuration;
using QuanLyDiemSinhVien;

namespace Quản_lí_điểm_sinh_viên
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.Load += Form3_Load;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            LoadLichSuDiem();
            string query = "SELECT * FROM LichSuDiem ORDER BY ThoiGian DESC";
            DataTable dt = Data.LoadCSDL(query);
            dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadLichSuDiem()
        {
            string query = "SELECT ID, MaSV, DiemCu, DiemMoi, ThaoTac, ThoiGian, NguoiThucHien FROM LichSuDiem ORDER BY ThoiGian DESC";
            DataTable dt = Data.LoadCSDL(query);
            dataGridView1.DataSource = dt;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
