public class DangNhap
{
    public string TenDangNhap { get; set; }
    public string MatKhau { get; set; }
    public string HoTen { get; set; }

    public DangNhap() { }

    public DangNhap(string tenDangNhap, string matKhau, string hoTen)
    {
        TenDangNhap = tenDangNhap;
        MatKhau = matKhau;
        HoTen = hoTen;
    }
}
