using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Infrastructure;
using Quản_lí_điểm_sinh_viên;

namespace QuanLyDiemSinhVien
{
    public class Data
    {
        public static List<DangNhap> DangNhapList = new List<DangNhap>();

        private const string ConnectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=QuanLyDiemSinhVien;Integrated Security=True";


        private static SqlConnection connection;

        public static void OpenConnection()
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
        }

        public static void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        public static void GetAllDangNhap()
        {
            DangNhapList.Clear(); // XÓA dữ liệu cũ trước khi nạp mới

            try
            {
                OpenConnection();
                string query = "SELECT * FROM DangNhap";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    DangNhap dangNhap = new DangNhap
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                    };
                    DangNhapList.Add(dangNhap);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi truy vấn CSDL: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public static  DataTable LoadCSDL ( string query)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand (query ,connection) ;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { CloseConnection(); }
            return dt;
        }

        public static int ThaoTacCSDL(string query)
        {
            int kq = 0;
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(query,connection);
                kq = cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { CloseConnection(); }
            return kq ;
        }

        public static void GhiLichSuDiem(
     string maSV,
     string tenCu,
     string tenMoi,
     string monHocCu,
     string monHocMoi,
     decimal? diemCu,
     decimal? diemMoi,
     string thaoTac,
     string nguoiThucHien)
        {
            try
            {
                OpenConnection();

                string queryInsert = @"
            INSERT INTO LichSuDiem 
            (MaSV, TenCu, TenMoi, MonHocCu, MonHocMoi, DiemCu, DiemMoi, ThaoTac, ThoiGian, NguoiThucHien)
            VALUES 
            (@MaSV, @TenCu, @TenMoi, @MonHocCu, @MonHocMoi, @DiemCu, @DiemMoi, @ThaoTac, GETDATE(), @NguoiThucHien)";

                SqlCommand cmdInsert = new SqlCommand(queryInsert, connection);

                cmdInsert.Parameters.AddWithValue("@MaSV", maSV ?? (object)DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@TenCu", tenCu ?? (object)DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@TenMoi", tenMoi ?? (object)DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@MonHocCu", monHocCu ?? (object)DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@MonHocMoi", monHocMoi ?? (object)DBNull.Value);

                if (diemCu.HasValue)
                    cmdInsert.Parameters.AddWithValue("@DiemCu", diemCu.Value);
                else
                    cmdInsert.Parameters.AddWithValue("@DiemCu", DBNull.Value);

                if (diemMoi.HasValue)
                    cmdInsert.Parameters.AddWithValue("@DiemMoi", diemMoi.Value);
                else
                    cmdInsert.Parameters.AddWithValue("@DiemMoi", DBNull.Value);

                cmdInsert.Parameters.AddWithValue("@ThaoTac", thaoTac ?? (object)DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@NguoiThucHien", nguoiThucHien ?? (object)DBNull.Value);

                cmdInsert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi lịch sử điểm: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

    }

}
