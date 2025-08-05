-- 1. Xóa và tạo lại database
USE master;
ALTER DATABASE QuanLyDiemSinhVien SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE QuanLyDiemSinhVien;

CREATE DATABASE QuanLyDiemSinhVien;
GO
USE QuanLyDiemSinhVien;
GO

-- 2. Tạo bảng MonHoc
CREATE TABLE MonHoc (
    MaMH VARCHAR(10) PRIMARY KEY,
    TenMH NVARCHAR(100),
    SoTinChi INT
);

-- 3. Tạo bảng SinhVien
CREATE TABLE SinhVien (
    MaSV VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100),
    MonHoc NVARCHAR(50),
    Diem DECIMAL(4,2)
);

-- 4. Tạo bảng DangNhap
CREATE TABLE DangNhap (
    TenDangNhap NVARCHAR(100) PRIMARY KEY,
    MatKhau NVARCHAR(100)
);


DROP TABLE IF EXISTS LichSuDiem;

CREATE TABLE LichSuDiem (
    ID INT IDENTITY PRIMARY KEY,
    MaSV VARCHAR(10),
    TenCu NVARCHAR(100),
    TenMoi NVARCHAR(100),
    MonHocCu NVARCHAR(100),
    MonHocMoi NVARCHAR(100),
    DiemCu FLOAT,
    DiemMoi FLOAT,
    ThaoTac NVARCHAR(10),
    ThoiGian DATETIME DEFAULT GETDATE(),
    NguoiThucHien NVARCHAR(50)
   
);

-- Cho phép các cột nhận giá trị NULL
ALTER TABLE LichSuDiem ALTER COLUMN TenMoi NVARCHAR(100) NULL;
ALTER TABLE LichSuDiem ALTER COLUMN MonHocMoi NVARCHAR(100) NULL;
ALTER TABLE LichSuDiem ALTER COLUMN DiemMoi FLOAT NULL;


-- 6. Thêm dữ liệu vào MonHoc
INSERT INTO MonHoc VALUES
('MH001', N'KTPM1', 3),
('MH002', N'PR', 4),
('MH003', N'QHCC', 4),
('MH004', N'IT', 3),
('MH005', N'TDH', 3);

-- 7. Thêm dữ liệu vào SinhVien
INSERT INTO SinhVien VALUES
('SV001', N'Nguyễn Văn An',   N'KTPM1', 8.0),
('SV002', N'Lê Thị Bình',     N'PR', 6.5),
('SV003', N'Trần Văn Cường',  N'QHCC', 9.0),
('SV004', N'Hoàng Thị Duyên', N'IT', 7.0),
('SV005', N'Phạm Minh Đức',   N'TDH', 5.0); 

-- 8. Thêm dữ liệu vào DangNhap
INSERT INTO DangNhap VALUES
(N'Ngô Nhật Huy', 'SV001'),
(N'Trịnh Thảo My', 'SV002'),
(N'Đỗ Quốc Khánh', 'SV003'),
(N'Vũ Thị Yến', 'SV004'),
(N'Lương Tuấn Kiệt', 'SV005');


-- 9. Kiểm tra dữ liệu
SELECT * FROM MonHoc;
SELECT * FROM SinhVien;
SELECT * FROM DangNhap;
SELECT * FROM LichSuDiem;
