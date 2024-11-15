
-- Kiểm tra xem database có tồn tại hay không
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_BANSACH')
BEGIN
    -- Đóng tất cả các kết nối đến database (nếu có)
    ALTER DATABASE QL_BANSACH SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    -- Xóa database
    DROP DATABASE QL_BANSACH;
END
-- Tạo database mới
CREATE DATABASE QL_BANSACH;

go

USE QL_BANSACH;

-- Thông báo database đã được tạo thành công
PRINT N'Database QL_BANSACH đã tạo thành công.';

-- Bảng Sách
CREATE TABLE Sach (
    id VARCHAR(50) PRIMARY KEY,
    tieude NVARCHAR(255),
	theloai_id VARCHAR(50),
    gia DECIMAL(10, 2),
    soLuongTon INT,
    hinhAnh VARCHAR(max),
    namXuatBan INT,
	MoTa NVARCHAR(max),
    nxb_id VARCHAR(50)
);
-- Bảng account
CREATE TABLE NguoiDung (
    id VARCHAR(50) PRIMARY KEY,
    username NVARCHAR(50) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL, -- Mật khẩu (nên được mã hóa)
    role VARCHAR(10) NOT NULL, -- Ví dụ: 'admin', 'staff', 'customer',
    gioitinh INT,
	NgaySinh DateTime
);


-- Bảng Khách hàng
CREATE TABLE Khachhang (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255),
    diachi NVARCHAR(255),
    sodienthoai VARCHAR(10),
    email VARCHAR(255),
	id_NguoiDung VARCHAR(50) 
);



 
-- Bảng Nhân viên
CREATE TABLE NhanVien (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255),
    chucVu NVARCHAR(255),
    sodienthoai VARCHAR(10),
    email VARCHAR(255),
	id_NguoiDung  VARCHAR(50)  NULL
);
-- Bảng Đơn đặt hàng
CREATE TABLE DonHang (
    id VARCHAR(50) PRIMARY KEY,
    nguoidung_id VARCHAR(50) null,
    trangthaiDH int DEFAULT 0,
    ngayDatHang DATETIME,
    tongTien DECIMAL(10, 2),-- So luong * DonGia
);

-- Bảng Chi tiết đơn hàng
CREATE TABLE ChiTietDonHang (
    id VARCHAR(50) PRIMARY KEY,
    donhang_id VARCHAR(50),
    sach_id VARCHAR(50),
    soLuong INT,
    giaDonVi DECIMAL(10, 2)
);

-- Bảng Thể loại
CREATE TABLE TheLoai (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255)
);

-- Bảng Nhà xuất bản
CREATE TABLE NhaXuatBan (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255),
    diachi NVARCHAR(255),
    sodienthoai VARCHAR(10),
    email VARCHAR(255)
);

-- Bảng Tác giả
CREATE TABLE TacGia (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255),
    ngaySinh DATE,
    quocTich NVARCHAR(255)
);



CREATE TABLE HoaDon (
    id VARCHAR(50) PRIMARY KEY, 
    donhang_id VARCHAR(50) UNIQUE,
    ngayLap DATETIME,          
    tongTien DECIMAL(10, 2), 
    phuongThucTT NVARCHAR(50),
    trangthaiTT NVARCHAR(50),
    email NVARCHAR(255),         -- Email của người nhận
    sodienthoai VARCHAR(10),     -- Số điện thoại của người nhận
    diachi NVARCHAR(255),        -- Địa chỉ của người nhận
    tenNguoiDatHang NVARCHAR(255)   -- Tên của người nhận
);





go
-- Bảng Tác giả - Sách (nhiều - nhiều)
CREATE TABLE TacGia_Sach (
    sach_id VARCHAR(50),
    tacgia_id VARCHAR(50),
    PRIMARY KEY (sach_id, tacgia_id)
);
-- thêm khóa ngoại cho bảng NguoiDung
ALTER TABLE Khachhang
ADD CONSTRAINT FK_Khachhang_NguoiDung
FOREIGN KEY (id_NguoiDung) REFERENCES NguoiDung(id);

ALTER TABLE NhanVien
ADD CONSTRAINT FK_NhanVien_NguoiDung
FOREIGN KEY (id_NguoiDung) REFERENCES NguoiDung(id);
-- thêm

go
ALTER TABLE HoaDon
ADD CONSTRAINT FK_HoaDon_DonHang FOREIGN KEY (donhang_id) REFERENCES DonHang(id);
go


-- Thêm khóa ngoại cho bảng Sách (thể loại và nhà xuất bản)
ALTER TABLE Sach
ADD CONSTRAINT FK_Sach_TheLoai FOREIGN KEY (theloai_id) REFERENCES TheLoai(id),
    CONSTRAINT FK_Sach_NhaXuatBan FOREIGN KEY (nxb_id) REFERENCES NhaXuatBan(id);
go
-- Thêm khóa ngoại cho bảng Đơn hàng (khách hàng và nhân viên)
ALTER TABLE DonHang
ADD CONSTRAINT FK_DonHang_NguoiDung FOREIGN KEY (nguoidung_id) REFERENCES NguoiDung(id);
go
-- Thêm khóa ngoại cho bảng Chi tiết đơn hàng (sách và đơn hàng)
ALTER TABLE ChiTietDonHang
ADD CONSTRAINT FK_ChiTietDonHang_Sach FOREIGN KEY (sach_id) REFERENCES Sach(id),
    CONSTRAINT FK_ChiTietDonHang_DonHang FOREIGN KEY (donhang_id) REFERENCES DonHang(id);
go

-- Thêm khóa ngoại cho bảng Tác giả - Sách (tác giả và sách)
ALTER TABLE TacGia_Sach
ADD CONSTRAINT FK_TacGia_Sach_Sach FOREIGN KEY (sach_id) REFERENCES Sach(id),
    CONSTRAINT FK_TacGia_Sach_TacGia FOREIGN KEY (tacgia_id) REFERENCES TacGia(id);

-- Thêm dữ liệu cho các bảng

-- Thêm dữ liệu vào bảng Nhà xuất bản
INSERT INTO NhaXuatBan (id, ten, diachi, sodienthoai, email) VALUES
('NXB001', N'Kim Đồng', N'Hà Nội', '0912345678', 'kimdong@email.com'),
('NXB002', N'Trẻ', N'TP.HCM', '0987654321', 'tre@email.com'),
('NXB003', N'Giáo dục', N'Hà Nội', '0901234567', 'giaoduc@email.com'),
('NXB004', N'Văn học', N'Hà Nội', '0976543210', 'vanhoc@email.com'),
('NXB005', N'Lao động', N'Hà Nội', '0998765432', 'laodong@email.com'),
('NXB006', N'Nhã Nam', N'Hà Nội', '0912345679', 'nhanam@email.com'),
('NXB007', N'Phụ nữ', N'TP.HCM', '0987654322', 'phunu@email.com'),
('NXB008', N'Hồng Đức', N'Hà Nội', '0901234568', 'hongduc@email.com'),
('NXB009', N'Thanh Niên', N'Hà Nội', '0976543211', 'thanhnien@email.com'),
('NXB010', N'Công an nhân dân', N'Hà Nội', '0998765433', 'congan@email.com'),
('NXB011', N'Quốc gia', N'Hà Nội', '0905123456', 'quocgia@email.com'),
('NXB012', N'Thế giới', N'TP.HCM', '0918273645', 'thegioi@email.com'),
('NXB013', N'Hội nhà văn', N'Hà Nội', '0921354678', 'hoinhavan@email.com'),
('NXB014', N'Tri thức', N'TP.HCM', '0927485963', 'trithuc@email.com'),
('NXB015', N'Đại học quốc gia', N'Hà Nội', '0932567890', 'daihoc@email.com');

-- Thêm dữ liệu vào bảng Thể loại
INSERT INTO TheLoai (id, ten) VALUES
('TL001', N'Truyện tranh'),
('TL002', N'Tiểu thuyết'),
('TL003', N'Sách giáo khoa'),
('TL004', N'Khoa học'),
('TL005', N'Lịch sử'),
('TL006', N'Tâm lý'),
('TL007', N'Kinh dị'),
('TL008', N'Phiêu lưu'),
('TL009', N'Lãng mạn'),
('TL010', N'Trinh thám'),
('TL011', N'Kiếm hiệp'),
('TL012', N'Giả tưởng'),
('TL013', N'Ngôn tình'),
('TL014', N'Du lịch'),
('TL015', N'Sức khỏe');

-- Thêm dữ liệu vào bảng Tác giả
INSERT INTO TacGia (id, ten, ngaySinh, quocTich) VALUES
('TG001', N'Tô Hoài', '1920-09-27', N'Việt Nam'),
('TG002', N'Nguyễn Nhật Ánh', '1955-05-07', N'Việt Nam'),
('TG003', N'J.K. Rowling', '1965-07-31', N'Anh'),
('TG004', N'Stephen King', '1947-09-21', N'Mỹ'),
('TG005', N'Haruki Murakami', '1949-01-12', N'Nhật Bản'),
('TG006', N'Paulo Coelho', '1947-08-24', N'Brazil'),
('TG007', N'Dan Brown', '1964-06-22', N'Mỹ'),
('TG008', N'J.R.R. Tolkien', '1892-01-03', N'Anh'),
('TG009', N'Jane Austen', '1775-12-16', N'Anh'),
('TG010', N'Agatha Christie', '1890-09-15', N'Anh'),
('TG011', N'Kim Dung', '1924-03-10', N'Trung Quốc'),
('TG012', N'George R.R. Martin', '1948-09-20', N'Mỹ'),
('TG013', N'Nicholas Sparks', '1965-12-31', N'Mỹ'),
('TG014', N'Lonely Planet', NULL, NULL),
('TG015', N'Adam Fawer', '1970-05-17', N'Mỹ');

-- Thêm dữ liệu vào bảng Sách với mô tả đầy đủ
INSERT INTO Sach (id, tieude, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id, moTa) VALUES
('S001', N'Dế Mèn Phiêu Lưu Ký', 'TL001', 50000, 100, 'ToBinhYenVeHanhPhuc.jpg', 1941, 'NXB001', N'Câu chuyện về một chú dế mèn đáng yêu và những cuộc phiêu lưu của nó trong thế giới tự nhiên.'),
('S002', N'Kính vạn hoa', 'TL002', 75000, 50, 'Kinhvanhoa.jpg', 1995, 'NXB002', N'Một cuốn sách tuyệt vời với những hình ảnh đẹp và những câu chuyện thú vị.'),
('S003', N'Harry Potter và Hòn đá Phù thủy', 'TL002', 100000, 200, 'hdpt.jpg', 1997, 'NXB003', N'Câu chuyện về một cậu bé phù thủy tên là Harry Potter và những cuộc phiêu lưu kỳ diệu của cậu ấy tại trường Hogwarts.'),
('S004', N'It',  'TL004', 80000, 30, 'it.jpg', 1986, 'NXB004', N'Một câu chuyện kinh dị đáng sợ và hấp dẫn về một nhóm thiếu niên đối mặt với một thực thể đáng sợ.'),
('S005', N'Rừng Na Uy', 'TL002', 90000, 70, 'rungnauy.jpg', 1987, 'NXB005', N'Câu chuyện về cuộc sống của con người trong một khung cảnh thiên nhiên tuyệt đẹp.'),
('S006', N'Nhà giả kim',  'TL006', 60000, 150, 'giakim.jpg', 1988, 'NXB006', N'Một cuốn sách triết lý về cuộc hành trình tìm kiếm hạnh phúc.');



INSERT INTO NguoiDung (id, username, password, role,gioitinh,ngaysinh) VALUES
('user123', N'user123', '12345', 'customer',0,'2000-10-12'),
('user456', N'user456', '12345', 'customer',1,'2000-08-23'),
('user789', N'user789', '12345', 'customer',0,'1992-03-17'),

('admin123', N'admin123', '12345', 'admin',1,'1991-01-30'),

('staff123', N'staff123', '12345', 'staff',1,'1999-04-04'),
('staff456', N'staff456', '12345', 'staff',0,'1998-05-25'),
('staff789', N'staff789', '12345', 'staff',0,'2003-07-16');
-- Chèn dữ liệu vào bảng Khachhang
INSERT INTO Khachhang (id, ten, diachi, sodienthoai, email, id_NguoiDung) VALUES
('KH001', N'Nguyễn Văn A', N'123 Đường Trần Hưng Đạo, Hà Nội', '0987654321', 'nguyenvana@example.com', 'user123'),
('KH002', N'Trần Thị B', N'456 Nguyễn Du, Hồ Chí Minh', '0912345678', 'tranthib@example.com', 'user456'),
('KH003', N'Lê Văn C', N'789 Lê Thánh Tôn, Đà Nẵng', '0789456123', 'levanc@example.com', 'user789');

-- Chèn dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (id, ten, chucVu, sodienthoai, email, id_NguoiDung) VALUES
('NV001', N'Phạm Văn D', N'Quản lý', '0901234567', 'phamvand@example.com', 'staff123'),
('NV002', N'Huỳnh Thị E', N'Nhân viên kinh doanh', '0936547890', 'huynhthie@example.com', 'staff456'),
('NV003', N'Đỗ Văn F', N'Nhân viên kỹ thuật', '0775552211', 'dovanf@example.com', 'staff789'),
('NV004', N'Nguyễn Văn G', N'Admin', '0989123456', 'nguyenvang@example.com', 'admin123');
-- Thêm dữ liệu vào bảng Đơn đặt hàng
INSERT INTO DonHang (id, nguoidung_id, trangthaiDH, ngayDatHang, tongTien) VALUES
('DH001', 'user123',0, '2023-03-01', 150000),
('DH002', 'user123', 1, '2023-03-05', 75000),
('DH003', 'admin123', 0, '2023-03-10', 180000),
('DH004', null, 0, '2023-04-12', 110000);


-- Thêm dữ liệu vào bảng Chi tiết đơn hàng
INSERT INTO ChiTietDonHang (id, donhang_id, sach_id, soLuong, giaDonVi) VALUES
('CTDH001', 'DH001', 'S001', 2, 50000),
('CTDH002', 'DH001', 'S002', 1, 75000),
('CTDH003', 'DH002', 'S003', 3, 100000),
('CTDH004', 'DH003', 'S004', 2, 80000),
('CTDH005', 'DH003', 'S005', 1, 90000);


-- Thêm dữ liệu vào bảng Đặt trước

-- Thêm dữ liệu vào bảng Tác giả - Sách
INSERT INTO TacGia_Sach (sach_id, tacgia_id) VALUES
('S001', 'TG001'),
('S002', 'TG002'),
('S003', 'TG003'),
('S004', 'TG004'),
('S005', 'TG005'),
('S006', 'TG006');
-- BACK UP AND RESTORE

-- BACK UP

---Bài 2) Viết câu lệnh backup và restore cho database đủ 3 trạng thái 
--là full-backup, diff-backup và log-backup. Có minh họa ví dụ phục hồi dữ liệu

--phuc hoi du lieu voi mo hinh simple
--chon mo hinh phuc hoi la simple recovery
ALTER DATABASE QL_BANSACH SET RECOVERY SIMPLE;
--FULL BACKUP

backup database QL_BANSACH
TO DISK ='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu_simple\QL_BANSACH_FULLBACKUP.bak'
with init, description ='Backup db QL_BANSACH at 7:00 PM'

--thêm dữ liệu vào bảng sách
INSERT INTO Sach (id, tieude, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id, moTa) VALUES
('S007', N'Cuốn theo chiều gió', 'TL009', 85000, 120, 'cuon_theo_chieu_gio.jpg', 1936, 'NXB008', N'Tác phẩm nổi tiếng về tình yêu và chiến tranh.')
--DIFF-BACKUP
BACKUP DATABASE QL_BANSACH
TO DISK ='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu_simple\QL_BANSACH_DIFFBACKUP.bak'
with differential;
--RESTORE

restore database QL_BANSACH
FROM DISK ='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu_simple\QL_BANSACH_FULLBACKUP.bak'
WITH NORECOVERY

restore database QL_BANSACH
FROM DISK='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu_simple\QL_BANSACH_DIFFBACKUP.bak'
WITH RECOVERY

--chon mo hinh phuc hoi la full recovery
ALTER DATABASE QL_BANSACH SET RECOVERY FULL;
--FULL-BACKUP
backup database QL_BANSACH
TO DISK ='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_FULLBACKUP.bak'
with init, description ='Backup db QL_BANSACH at 7:00 PM'

--thêm dữ liệu vào bảng sách
INSERT INTO Sach (id, tieude, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id, moTa) VALUES
('S007', N'Cuốn theo chiều gió', 'TL009', 85000, 120, 'cuon_theo_chieu_gio.jpg', 1936, 'NXB008', N'Tác phẩm nổi tiếng về tình yêu và chiến tranh.')
--DIFF-BACKUP
BACKUP DATABASE QL_BANSACH
TO DISK ='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_DIFFBACKUP.bak'
with differential;
--them du lieu vao bang sach
INSERT INTO Sach (id, tieude, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id, moTa) VALUES
('S008', N'Hoa vàng trên cỏ xanh', 'TL013', 68000, 90, 'hoa_vang_tren_co_xanh.jpg', 2010, 'NXB001', N'Câu chuyện tuổi thơ ở vùng nông thôn yên bình Việt Nam.');
--LOG-BACKUP
BACKUP LOG QL_BANSACH
TO DISK='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_LOGBACKUP.trn'
WITH DESCRIPTION ='QL_BANSACH log backup'
--them du lieu vao bang sach
INSERT INTO Sach (id, tieude, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id, moTa) VALUES
('S009', N'Bí mật của Phan Thiên Ân', 'TL010', 95000, 100, 'bi_mat_cua_phan_thien_an.jpg', 2003, 'NXB002', N'Cuốn sách chứa những bài học sâu sắc về cuộc sống.');

--LOG-BACKUP
BACKUP LOG QL_BANSACH
TO DISK='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_LOGBACKUP.trn'
WITH DESCRIPTION ='QL_BANSACH log backup'

INSERT INTO Sach (id, tieude, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id, moTa) VALUES
('S0010', N'Bí mật của Phan Thiên Ân', 'TL010', 95000, 100, 'bi_mat_cua_phan_thien_an.jpg', 2003, 'NXB002', N'Cuốn sách chứa những bài học sâu sắc về cuộc sống.');

--sao luu taillog
BACKUP LOG QL_BANSACH
TO DISK ='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_taillog.trn'
with norecovery

--sau khi xay ra su co
--phuc hoi du lieu voi mo hinh full


--phuc hoi file backup gan nhat
restore database QL_BANSACH
FROM DISK ='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_FULLBACKUP.bak'
WITH NORECOVERY
--Phuc hoi file differential backup
restore database QL_BANSACH
FROM DISK='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_DIFFBACKUP.bak'
WITH NORECOVERY
--Phuc hoi cac log backup tu lan differential backup gan nhat
restore database QL_BANSACH
from disk='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_LOGBACKUP.trn'
with norecovery
--phuc hoi file taillog
restore database QL_BANSACH
from disk='C:\LUUDULIEUSINHVIEN\HEQTCLDL\NHOM4\saoluu\QL_BANSACH_taillog.trn'
with recovery




SELECT*FROM Sach

use master 


-- RESTORE


--A) Tạo 3 login với username lần lượt là User_Admin, User_Backup, User_Staff
CREATE LOGIN User_Admin WITH PASSWORD = 'Password@123';
CREATE LOGIN User_Backup WITH PASSWORD = 'Password@123';
CREATE LOGIN User_Staff WITH PASSWORD = 'Password@123';

--B) Từ 3 tài khoản login tạo 3 người dùng User_Admin, User_Backup, User_Staff và phân quyền cho cả 3 chỉ thao tác trên cơ sở dữ liệu QL_BanSach
USE QL_BANSACH;

-- Tạo người dùng trong CSDL QL_BanSach
CREATE USER User_Admin FOR LOGIN User_Admin;
CREATE USER User_Backup FOR LOGIN User_Backup;
CREATE USER User_Staff FOR LOGIN User_Staff;

--C) Phân quyền 1. Phân quyền User_Admin với toàn quyền (FULL quyền) trên Database QL_BanSach
-- Cấp quyền db_owner cho User_Admin
ALTER ROLE db_owner ADD MEMBER User_Admin;

--2. Phân quyền User_Backup chỉ được quyền sao lưu (backup)
-- Cấp quyền BACKUP DATABASE cho User_Backup
GRANT BACKUP DATABASE TO User_Backup;

--3. Phân quyền User_Staff
-- Cấp quyền Insert, Delete, Update cho bảng Sach
GRANT INSERT, DELETE, UPDATE ON Sach TO User_Staff;

-- Cấp quyền Insert, Delete, Update cho bảng Khachhang
GRANT INSERT, DELETE, UPDATE ON Khachhang TO User_Staff;

-- Cấp quyền Insert, Delete, Update cho bảng DonHang
GRANT INSERT, DELETE, UPDATE ON DonHang TO User_Staff;

-- Cấp quyền Insert, Delete, Update cho bảng ChiTietDonHang
GRANT INSERT, DELETE, UPDATE ON ChiTietDonHang TO User_Staff;

-- Cấp quyền Insert, Delete, Update cho bảng HoaDon
GRANT INSERT, DELETE, UPDATE ON HoaDon TO User_Staff;

-- Cấp quyền SELECT cho bảng Sach, Khachhang, DonHang, ChiTietDonHang, HoaDon
GRANT SELECT ON Sach TO User_Staff;
GRANT SELECT ON Khachhang TO User_Staff;
GRANT SELECT ON DonHang TO User_Staff;
GRANT SELECT ON ChiTietDonHang TO User_Staff;
GRANT SELECT ON HoaDon TO User_Staff;

-- Cấp quyền DELETE cho tất cả các bảng trên
GRANT DELETE ON Sach TO User_Staff;
GRANT DELETE ON Khachhang TO User_Staff;
GRANT DELETE ON DonHang TO User_Staff;
GRANT DELETE ON ChiTietDonHang TO User_Staff;
GRANT DELETE ON HoaDon TO User_Staff;

--D) Thay đổi mật khẩu cho User_Admin
ALTER LOGIN User_Admin WITH PASSWORD = 'NewPassword@456';

--test
USE QL_BANSACH;

SELECT 
    dp.name AS user_name,
    dp.type_desc AS user_type,
    p.class_desc,
    o.name AS object_name,
    p.permission_name,
    p.state_desc AS permission_state
FROM 
    sys.database_principals dp
JOIN 
    sys.database_permissions p ON dp.principal_id = p.grantee_principal_id
LEFT JOIN 
    sys.objects o ON p.major_id = o.object_id
WHERE 
    dp.name = 'User_Staff';

--Kiểm tra quyền trên bảng Sach
USE QL_BANSACH;

EXECUTE AS USER = 'User_Staff';

SELECT 
    OBJECT_NAME(major_id) AS object_name,
    permission_name,
    state_desc
FROM 
    sys.database_permissions
WHERE 
    major_id = OBJECT_ID('Sach');

REVERT;

-- Kiểm tra quyền User_Staff ở cấp độ cơ sở dữ liệu
USE QL_BANSACH;

SELECT 
    dp.name AS user_name,
    dp.type_desc AS user_type,
    p.permission_name,
    p.state_desc
FROM 
    sys.database_principals dp
JOIN 
    sys.database_permissions p ON dp.principal_id = p.grantee_principal_id
WHERE 
    dp.name = 'User_Staff'
    AND p.class_desc = 'DATABASE';

--Kiểm tra các quyền bị từ chối (DENY)
USE QL_BANSACH;

SELECT 
    dp.name AS user_name,
    dp.type_desc AS user_type,
    p.class_desc,
    o.name AS object_name,
    p.permission_name,
    p.state_desc
FROM 
    sys.database_principals dp
JOIN 
    sys.database_permissions p ON dp.principal_id = p.grantee_principal_id
LEFT JOIN 
    sys.objects o ON p.major_id = o.object_id
WHERE 
    dp.name = 'User_Staff'
    AND p.state_desc = 'DENY';

--C�u E

GRANT SELECT, INSERT ON [ViewName] TO User_Staff;
GRANT EXECUTE ON [StoredProcedureName] TO User_Staff;

REVOKE SELECT, INSERT ON DonHang TO User_Staff;
REVOKE EXECUTE ON [StoredProcedureName] TO User_Staff;

--GRANT SELECT, INSERT ON DonHang TO User_Staff;
--GRANT EXECUTE ON  TO User_Staff;

--C�u F

SELECT name, create_date, modify_date 
FROM sys.database_principals 
WHERE name = 'User_Staff';

SELECT 
    dp.name AS UserName,
    dp.type_desc AS UserType,
    o.name AS ObjectName,
    p.permission_name,
    p.state_desc AS PermissionState
FROM 
    sys.database_permissions p
JOIN 
    sys.objects o ON p.major_id = o.object_id
JOIN 
    sys.database_principals dp ON p.grantee_principal_id = dp.principal_id
WHERE 
    dp.name = 'User_Staff';

--SHOW GRANTS FOR  'User_Staff';

--C�u G

DROP USER User_Staff;

--C�u H

DROP LOGIN User_Staff;
-- TRIGGER START
-- INSTEAD OF trigger ngăn chặn hành động trước khi nó xảy ra. AFTER (hoặc FOR) trigger thực hiện kiểm tra sau khi hành động đã diễn ra. 

--1 Trigger kiểm tra khi thêm sách mới có năm xuất bản lớn hơn 1940 và giá sách lớn hơn 0
CREATE TRIGGER TR_KiemTraSachMoi
ON Sach
INSTEAD OF INSERT
AS
BEGIN
    -- Kiểm tra điều kiện cho năm xuất bản và giá sách
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE namXuatBan <= 1940 OR gia <= 0
    )
    BEGIN
        -- Nếu điều kiện không thỏa, in thông báo lỗi và không thêm sách
        RAISERROR(N'Năm xuất bản phải lớn hơn 1940 và giá sách phải lớn hơn 0. Vui lòng nhập giá trị phù hợp ^ v ^', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- Thêm sách mới nếu các điều kiện được thỏa mãn
        INSERT INTO Sach(id, tieude, isbn, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id)
        SELECT id, tieude, isbn, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id
        FROM inserted;
    END
END;

INSERT INTO Sach (id, tieude, isbn, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id)
VALUES ('S0017', N'Sách B', '978-1234567890', 'TL001', -1000, 50, 'image.jpg', 1930, 'NXB001');
--2 Trigger kiểm tra email của Khachhang được cập nhật
create TRIGGER TR_KiemTraEmailKH
ON KhachHang
FOR UPDATE
AS
BEGIN
    IF UPDATE(email)
    BEGIN
        DECLARE @newEmail VARCHAR(255), @count int;
        SELECT @newEmail = email FROM inserted;

        -- Kiểm tra định dạng email hợp lệ
        IF NOT EXISTS (SELECT 1 FROM inserted WHERE email LIKE '%_@__%.__%')
        BEGIN
            print'email khong khong dung cu phap'
            ROLLBACK TRANSACTION;
            RETURN;
        END;
		set @count=(select count(*) from KhachHang where email=@newEmail)
		if (@count>1)
		begin
		print'Email da ton tai'
		ROLLBACK TRANSACTION;
        RETURN;
		end
		 end
END


SELECT * FROM Khachhang
UPDATE Khachhang 
SET	 email='nguyenvana@email.com'
WHERE id='KH002'
--3 Trigger Ngăn Chặn Xóa Sách Đã Được Đặt Hàng 
CREATE TRIGGER TR_KiemTraXoaSach
ON Sach
INSTEAD OF DELETE
AS
BEGIN
    -- Kiểm tra xem sách có xuất hiện trong bảng ChiTietDonHang không
    IF EXISTS (
        SELECT 1
        FROM deleted d
        INNER JOIN ChiTietDonHang ctdh ON d.id = ctdh.sach_id
    )
    BEGIN
        -- Nếu sách đã từng được đặt hàng, in thông báo lỗi và ngăn thao tác xóa
        RAISERROR(N'Sách đã được đặt hàng và không thể xóa.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- Nếu sách chưa được đặt hàng, cho phép xóa sách khỏi bảng Sach
        DELETE FROM Sach
        WHERE id IN (SELECT id FROM deleted);
    END
END;


SELECT * FROM ChiTietDonHang

DROP TRIGGER TR_KiemTraXoaSach ON ALL SERVER
DELETE FROM Sach WHERE id = 'S002';
--4 Trigger xóa chi tiết đơn hàng khi xóa đơn hàng 
CREATE TRIGGER TR_XoaCTDHKhiXoaDH
ON DonHang
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        DELETE FROM ChiTietDonHang
        WHERE donhang_id IN (SELECT id FROM deleted);
		DELETE FROM DonHang WHERE id IN (SELECT id FROM deleted);
    END
END;

SELECT * FROM DonHang
SELECT * FROM ChiTietDonHang
DELETE FROM DonHang WHERE DonHang.id='DH001'
--5 Trigger tự động cập nhật tổng tiền khi thay đổi số lượng sách(thêm xóa sửa) trong chi tiết đơn hang (Dùng if update)

CREATE TRIGGER TR_UpdateTongTienDonHang
ON ChiTietDonHang
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Khai báo biến để lưu giá trị tổng tiền
    DECLARE @donhang_id VARCHAR(50);

    -- Kiểm tra trường hợp INSERT hoặc UPDATE
    IF (UPDATE(soLuong))
    BEGIN
        -- Lấy mã đơn hàng từ bảng inserted (dữ liệu sau khi thay đổi)
        SELECT @donhang_id = i.donhang_id
        FROM inserted i;

        -- Cập nhật tổng tiền của đơn hàng dựa trên tổng số tiền trong chi tiết đơn hàng
        UPDATE DonHang
        SET tongTien = (
            SELECT SUM(c.soLuong * c.giaDonVi)
            FROM ChiTietDonHang c
            WHERE c.donhang_id = @donhang_id
        )
        WHERE id = @donhang_id;
    END

    -- Kiểm tra trường hợp DELETE
    IF NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- Lấy mã đơn hàng từ bảng deleted (dữ liệu trước khi xóa)
        SELECT @donhang_id = d.donhang_id
        FROM deleted d;

        -- Cập nhật tổng tiền của đơn hàng dựa trên tổng số tiền trong chi tiết đơn hàng
        UPDATE DonHang
        SET tongTien = (
            SELECT SUM(c.soLuong * c.giaDonVi)
            FROM ChiTietDonHang c
            WHERE c.donhang_id = @donhang_id
        )
        WHERE id = @donhang_id;
    END
END;
GO
--test
SELECT * FROM DonHang WHERE id = 'DH001';
SELECT * FROM ChiTietDonHang WHERE ChiTietDonHang.donhang_id = 'DH001';


INSERT INTO ChiTietDonHang (id, donhang_id, sach_id, soLuong, giaDonVi)
VALUES ('CT001', 'DH001', 'S001', 2, 150000);

UPDATE ChiTietDonHang
SET soLuong = 3
WHERE id = 'CT001';

DELETE FROM ChiTietDonHang
WHERE id = 'CT001';

--update donhang_id sach_id
-- update hoadon trong bai 1
-- update hoa don - donhang 11
--6 Trigger kiểm tra số lượng tồn kho trước khi thêm/cập nhật Chi tiết đơn hàng (Ngăn chặn việc bán hàng vượt quá số lượng tồn kho)
-- và cập nhật số lượng tồn kho trong Sách khi thêm/xóa/sửa Chi tiết đơn hàng


CREATE TRIGGER TR_QuanLySoLuongTon
ON ChiTietDonHang
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Kiểm tra số lượng tồn cho INSERT và UPDATE
    IF EXISTS(SELECT 1 FROM inserted i JOIN Sach s ON i.sach_id = s.id WHERE i.soLuong > s.soLuongTon) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        RAISERROR(N'Số lượng đặt mua vượt quá số lượng tồn kho.', 16, 1);
        ROLLBACK TRANSACTION;
    END

    -- Xử lý trường hợp INSERT
    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon - i.soLuong
        FROM Sach s
        JOIN inserted i ON s.id = i.sach_id;
    END

    -- Xử lý trường hợp UPDATE
    IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon + d.soLuong - i.soLuong
        FROM Sach s
        JOIN inserted i ON s.id = i.sach_id
        JOIN deleted d ON s.id = d.sach_id;
    END

    -- Xử lý trường hợp DELETE
    IF EXISTS (SELECT 1 FROM deleted) AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon + d.soLuong
        FROM Sach s
        JOIN deleted d ON s.id = d.sach_id;
    END
END;

--TEST TRIGGER

-- Xem số lượng tồn kho hiện tại của sách 'S001':
SELECT soLuongTon FROM Sach WHERE id = 'S001';
-- Kết quả: 100

-- Thêm một chi tiết đơn hàng mới cho sách 'S001' với số lượng 50:
INSERT INTO ChiTietDonHang (id, donhang_id, sach_id, soLuong, giaDonVi)
VALUES ('CTDH016', 'DH001', 'S001', 50, 50000);

-- Kiểm tra lại số lượng tồn kho của sách 'S001' sau khi bán 50 cuốn
SELECT soLuongTon FROM Sach WHERE id = 'S001';
-- Kết quả: 50

--Thử thêm một chi tiết đơn hàng mới với số lượng vượt quá số lượng tồn (60):
INSERT INTO ChiTietDonHang (id, donhang_id, sach_id, soLuong, giaDonVi)
VALUES ('CTDH017', 'DH001', 'S001', 60, 50000);
-- => Hiện thông báo lỗi "Số lượng đặt mua vượt quá số lượng tồn kho."

 SELECT id,soLuongTon FROM Sach WHERE id = 'S001';
UPDATE ChiTietDonHang set soLuong= 80 WHERE ChiTietDonHang.id='CTDH016'

DELETE FROM ChiTietDonHang WHERE ChiTietDonHang.id='CTDH016'
--7 Trigger tự động tạo Hóa đơn khi Đơn hàng chuyển sang trạng thái "Đã hoàn thành":

CREATE TRIGGER TR_TaoHoaDonTuDong
ON DonHang
AFTER UPDATE
AS
BEGIN
    -- Kiểm tra xem có đơn hàng nào chuyển sang trạng thái "Đã hoàn thành" hay không
    IF UPDATE(trangthaiDH) AND EXISTS (SELECT 1 FROM inserted WHERE trangthaiDH =1)
    BEGIN
        -- Duyệt qua các đơn hàng vừa được cập nhật trạng thái
        INSERT INTO HoaDon (id,donhang_id, ngayLap, tongTien, phuongThucTT, trangthaiTT)
        SELECT NEWID(),i.id, GETDATE(), i.tongTien, N'Tiền mặt', N'Chưa thanh toán'
        FROM inserted i
        WHERE i.trangthaiDH = 1 AND NOT EXISTS (
            SELECT 1 FROM HoaDon WHERE donhang_id = i.id
        );
    END;
END;

-- TEST TRIGGER 
SELECT * FROM HOADON
-- tạo hóa đơn cho đơn hàng có mã 'DH012'
UPDATE DonHang
SET trangthaiDH = 1
WHERE id = 'DH012';
-- tự động tạo hóa đơn với đơn hàng id là 'DH012'
SELECT * FROM HoaDon WHERE donhang_id = 'DH012';

-- Xem trigger trên bảng
exec sp_helptrigger ChiTietDonHang


-- Xem nội dung trigger
exec sp_helptext TR_QuanLySoLuongTon
-- Xóa trigger
DROP TRIGGER TR_TaoHoaDonTuDong;
-- Sửa trigger
alter TRIGGER TR_KiemTraEmailKH
ON KhachHang
AFTER UPDATE
AS
BEGIN
    IF UPDATE(email)
    BEGIN
        DECLARE @newEmail VARCHAR(255);
        SELECT @newEmail = email FROM inserted;

        -- Kiểm tra định dạng email hợp lệ
        IF NOT EXISTS (SELECT 1 FROM inserted WHERE email LIKE '%_@__%.__%')
        BEGIN
            print'email khong khong dung cu phap'
            ROLLBACK TRANSACTION;
            RETURN;
        END;

		 IF EXISTS (
        SELECT 1
        FROM Khachhang AS k
        INNER JOIN inserted AS i
            ON k.email = i.email
        WHERE k.id <> i.id
    )
    BEGIN
        -- Nếu có email trùng, hiển thị thông báo lỗi và rollback
        print'Email đa ton tai. Vui long nhap email khac.'
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- Nếu email không trùng, thực hiện cập nhật.
        UPDATE Khachhang
        SET ten = i.ten,
            diachi = i.diachi,
            sodienthoai = i.sodienthoai,
			email = i.email
        FROM inserted AS i
        WHERE Khachhang.id = i.id;
    END
        -- Kiểm tra email có trùng lặp không (trừ bản ghi đang cập nhật)
    END
END

-- Vô hiệu hóa trigger

DISABLE TRIGGER TR_KiemTraEmailKH ON KhachHang;
-- Kích hoạt lại trigger

ENABLE TRIGGER TR_KiemTraEmailKH ON KhachHang;





-- TRIGGER END
-- STORE PROCEDURE START

--C) FUNCTION: Lấy danh sách các đơn hàng chưa thanh toán của khách hàng (trạng thái của hóa đơn là chưa thanh toán)
CREATE FUNCTION GetUnpaidOrdersByCustomer(@customerId VARCHAR(50))
RETURNS TABLE
AS
RETURN 
(
    SELECT dh.id AS DonHangID,
           dh.ngayDatHang AS NgayDatHang,
           dh.tongTien AS TongTien,
           hd.trangthaiTT AS TrangThaiThanhToan
    FROM DonHang dh
    INNER JOIN HoaDon hd ON dh.id = hd.donhang_id
    WHERE dh.nguoidung_id = @customerId
      AND hd.trangthaiTT = N'Chưa thanh toán'
);
--
INSERT INTO DonHang (id, nguoidung_id, trangthaiDH, ngayDatHang, tongTien) VALUES
('DH005', 'user123',0, '2023-03-01', 150000)

INSERT INTO HoaDon (id, donhang_id, ngayLap, tongTien, phuongThucTT, trangthaiTT) VALUES
('HD001', 'DH001', '2023-03-02', 150000, N'Tiền mặt', N'Đã thanh toán'),
('HD002', 'DH002', '2023-03-06', 75000, N'Chuyển khoản', N'Đã thanh toán'),
('HD003', 'DH003', '2023-03-11', 180000, N'Tiền mặt', N'Đã thanh toán'),
('HD004', 'DH004', '2023-04-13', 110000, N'Thẻ tín dụng', N'Chưa thanh toán'),
('HD005', 'DH005', '2023-04-13', 150000, N'Thẻ tín dụng', N'Chưa thanh toán');

--xem ket qua cau c:
SELECT * FROM GetUnpaidOrdersByCustomer('user123');

--G) PROCEDURE: Lấy danh sách khách hàng theo tổng giá trị đơn hàng đã mua.

CREATE PROCEDURE GetCustomersByTotalOrderValue
AS
BEGIN
    SELECT kh.id AS KhachHangID,
           kh.ten AS TenKhachHang,
           kh.diachi AS DiaChi,
           kh.sodienthoai AS SoDienThoai,
           kh.email AS Email,
           SUM(hd.tongTien) AS TongGiaTriDonHang
    FROM Khachhang kh
    INNER JOIN DonHang dh ON kh.id_NguoiDung = dh.nguoidung_id
    INNER JOIN HoaDon hd ON dh.id = hd.donhang_id
    WHERE hd.trangthaiTT = N'Đã thanh toán'
    GROUP BY kh.id, kh.ten, kh.diachi, kh.sodienthoai, kh.email
    ORDER BY TongGiaTriDonHang DESC;
END;

 
 INSERT INTO DonHang (id, nguoidung_id, trangthaiDH, ngayDatHang, tongTien) VALUES
('DH006', 'user789',0, '2023-03-01', 150000),
('DH007', 'user456',0, '2023-03-01', 150000);

 INSERT INTO HoaDon (id, donhang_id, ngayLap, tongTien, phuongThucTT, trangthaiTT) VALUES
('HD006', 'DH006', '2023-04-13', 150000, N'Thẻ tín dụng', N'Đã thanh toán'),
('HD007', 'DH007', '2023-04-13', 150000, N'Thẻ tín dụng', N'Đã thanh toán');

--xem ket qua cau g
EXEC GetCustomersByTotalOrderValue;


--K) Procedure & Cursor: Thống kê doanh thu theo từng tháng
CREATE PROCEDURE sp_ThongKeDoanhThuTheoThang
AS
BEGIN
    DECLARE @Thang INT,
            @Nam INT,
            @TongDoanhThu MONEY
    DECLARE cur_DoanhThu CURSOR FOR
                SELECT MONTH(NgayLap) AS Thang, YEAR(NgayLap) AS Nam, SUM(TongTien) AS TongDoanhThu
                FROM HoaDon
                GROUP BY MONTH(NgayLap), YEAR(NgayLap);

    OPEN cur_DoanhThu;

    FETCH NEXT FROM cur_DoanhThu INTO @Thang, @Nam, @TongDoanhThu;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT 'Tháng ' + CAST(@Thang AS VARCHAR) + ' năm ' + CAST(@Nam AS VARCHAR) + ': ' + CAST(@TongDoanhThu AS VARCHAR);

        FETCH NEXT FROM cur_DoanhThu INTO @Thang, @Nam, @TongDoanhThu;
    END

    CLOSE cur_DoanhThu;
    DEALLOCATE cur_DoanhThu;
END;

--xem ket qua cau k
EXEC sp_ThongKeDoanhThuTheoThang;


--B
CREATE FUNCTION dbo.TinhTongTienDonHang (@donhang_id VARCHAR(50))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @tongTien DECIMAL(18, 2);

    SELECT @tongTien = SUM(soLuong * giaDonVi)
    FROM ChiTietDonHang
    WHERE donhang_id = @donhang_id;

    RETURN ISNULL(@tongTien, 0);

END;
GO

SELECT dbo.TinhTongTienDonHang('DH001') AS TongTienDonHang;

--F

CREATE PROCEDURE dbo.TimKiemSachTheoTheLoaiVaKhoangGia
    @theloai_id VARCHAR(50),
    @giaMin INT,
    @giaMax INT
AS
BEGIN
    
    --SET NOCOUNT ON;

    SELECT 
        id,
        tieude,
        gia,
        soLuongTon,
        namXuatBan,
        moTa
    FROM 
        Sach
    WHERE 
        theloai_id = @theloai_id
        AND gia BETWEEN @giaMin AND @giaMax;

END;
GO

EXEC dbo.TimKiemSachTheoTheLoaiVaKhoangGia 'TL002', 50000, 100000;

--J

CREATE PROCEDURE dbo.BaoCaoSoLuongBanRaCuaTungTacGia
AS
BEGIN

    DECLARE @tacgia_id VARCHAR(50);
    DECLARE @tenTacGia NVARCHAR(255);
    DECLARE @soLuongBanRa INT;

    DECLARE TacGiaCursor CURSOR FOR
    SELECT id, ten
    FROM TacGia;

    OPEN TacGiaCursor;

    FETCH NEXT FROM TacGiaCursor INTO @tacgia_id, @tenTacGia;
    WHILE @@FETCH_STATUS = 0
    BEGIN

        SELECT @soLuongBanRa = SUM(CTDH.soLuong)
        FROM TacGia_Sach TGS
        JOIN ChiTietDonHang CTDH ON TGS.sach_id = CTDH.sach_id
        WHERE TGS.tacgia_id = @tacgia_id;

        PRINT N'Tác giả: ' + @tenTacGia + N' - Số lượng bán ra: ' + ISNULL(CAST(@soLuongBanRa AS NVARCHAR), N'0');

        FETCH NEXT FROM TacGiaCursor INTO @tacgia_id, @tenTacGia;
    END;

    CLOSE TacGiaCursor;
    DEALLOCATE TacGiaCursor;
END;
GO

EXEC dbo.BaoCaoSoLuongBanRaCuaTungTacGia;







-- D) FUNCTION: Tính tổng số lượng sách đã bán của một mã sách cụ thể.
DROP FUNCTION IF EXISTS dbo.FnTinhTongSoLuongSachDaBan;

CREATE FUNCTION dbo.FnTinhTongSoLuongSachDaBan (@SachId VARCHAR(50))
RETURNS INT
AS
BEGIN
    DECLARE @TongSoLuong INT;

    -- Tính tổng số lượng sách đã bán cho mã sách cụ thể khi trạng thái hóa đơn là 1 (thành công)
    SELECT @TongSoLuong = SUM(ctd.soLuong) 
    FROM ChiTietDonHang ctd
    JOIN DonHang dh ON ctd.donhang_id = dh.id
    WHERE ctd.sach_id = @SachId
    AND dh.trangthaiDH = '1';  -- '1' là trạng thái đã thanh toán thành công

    -- Nếu không có đơn hàng nào thì trả về 0
    IF @TongSoLuong IS NULL
    BEGIN
        SET @TongSoLuong = 0;
    END

    RETURN @TongSoLuong;
END;

SELECT dbo.FnTinhTongSoLuongSachDaBan('S004');
-- M) Procedure & Cursor cập nhật số lượng tồn kho khi có đơn hàng hoàn thành ( trạng thái đơn hàng chuyển sang = 1) -> đặt thành công 

CREATE PROCEDURE SP_CapNhatDonHang
    @donhang_id VARCHAR(50)
AS
BEGIN
    DECLARE @sach_id VARCHAR(50), @soLuong INT;

    -- Cập nhật trạng thái đơn hàng
    UPDATE DonHang
    SET trangthaiDH = 1 -- Đã thanh toán
    WHERE id = @donhang_id;

    -- Giảm số lượng tồn kho của từng sách trong đơn hàng
    DECLARE cur CURSOR FOR
        SELECT sach_id, soLuong
        FROM ChiTietDonHang
        WHERE donhang_id = @donhang_id;

    OPEN cur;
    FETCH NEXT FROM cur INTO @sach_id, @soLuong;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon - @soLuong
        WHERE id = @sach_id;

        FETCH NEXT FROM cur INTO @sach_id, @soLuong;
    END;
    CLOSE cur;
    DEALLOCATE cur;
END;


EXEC CapNhatDonHang ‘DH003’





H) PROCEDURE: tạo hóa đơn cho đơn hàng  khi đơn hàng hoàn thành -> chuyển trạng thái từ 0 sang 1
CREATE SEQUENCE dbo.Seq_HoaDon
    AS INT
    START WITH 1
    INCREMENT BY 1;

CREATE OR ALTER PROCEDURE SP_ThanhToanDonHang
    @DonHangId VARCHAR(50),
    @PhuongThucTT NVARCHAR(50),
    @Email NVARCHAR(255),
    @SoDienThoai VARCHAR(10),
    @DiaChi NVARCHAR(255),
    @TenNguoiDatHang NVARCHAR(255),
    @TongTien DECIMAL(10, 2) OUTPUT
AS
BEGIN
    -- Bắt đầu transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Kiểm tra xem đơn hàng có tồn tại và chưa thanh toán
        IF EXISTS (SELECT 1 FROM DonHang WHERE id = @DonHangId AND trangthaiDH = 0)
        BEGIN
            -- Cập nhật trạng thái đơn hàng thành đã thanh toán
            UPDATE DonHang
            SET trangthaiDH = 1
            WHERE id = @DonHangId;

            -- Lấy tổng tiền của đơn hàng và trả về qua tham số đầu ra
            -- SELECT @TongTien = tongTien FROM DonHang WHERE id = @DonHangId;

            -- Tạo ID cho hóa đơn với tiền tố "HD" và số tự động tăng
            DECLARE @HoaDonId VARCHAR(50);
            SET @HoaDonId = 'HD' + RIGHT('000' + CAST(NEXT VALUE FOR dbo.Seq_HoaDon AS VARCHAR), 4);

            -- Tạo mới hóa đơn cho đơn hàng
            INSERT INTO HoaDon (
                id,
                donhang_id,
                ngayLap,
                tongTien,
                phuongThucTT,
                trangthaiTT,
                email,
                sodienthoai,
                diachi,
                tenNguoiDatHang
            )
            VALUES (
                @HoaDonId,
                @DonHangId,
                GETDATE(),
                @TongTien,
                @PhuongThucTT,
                N'Đặt hàng thành ông',
                @Email,
                @SoDienThoai,
                @DiaChi,
                @TenNguoiDatHang
            );

            -- Xác nhận giao dịch
            COMMIT TRANSACTION;
            PRINT 'Thanh toán thành công và hóa đơn đã được tạo.';
        END
        ELSE
        BEGIN
            -- Nếu đơn hàng không tồn tại hoặc đã thanh toán, báo lỗi
            RAISERROR ('Đơn hàng không tồn tại hoặc đã thanh toán.', 16, 1);
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        -- Bắt lỗi nếu có vấn đề
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;



-- STORE PROCEDURE END
-- Khai báo biến cục bộ
DECLARE @bien1 INT;
DECLARE @bien2 NVARCHAR(50);

-- Gán giá trị cho biến
SET @bien1 = 10;
SET @bien2 = 'Hello SQL';

-- In ra giá trị của biến
PRINT @bien1;
PRINT @bien2;

-- Sử dụng biến toàn cục
SELECT @@VERSION AS 'Phiên bản SQL Server';
SELECT @@LANGUAGE AS 'Ngôn ngữ hiện tại';

-- Cấu trúc If...Else
DECLARE @age INT = 20;

IF @age >= 18
    PRINT 'Người lớn';
ELSE
    PRINT 'Trẻ em';

-- Cấu trúc If Exists
IF EXISTS (SELECT * FROM Khachhang WHERE ten = 'Nguyen Van A')
    PRINT 'Khách hàng tồn tại';
ELSE
    PRINT 'Khách hàng không tồn tại';


	-- Cấu trúc Case...When trong câu SELECT
SELECT tieude,
    CASE
        WHEN gia >= 500000 THEN 'Đắt'
        WHEN gia >= 200000 THEN 'Trung bình'
        ELSE 'Rẻ'
    END AS 'Phân loại giá'
FROM Sach;


-- Hàm thống kê
SELECT 
    SUM(gia) AS TongGia, 
    AVG(gia) AS GiaTrungBinh, 
    COUNT(*) AS TongSoSach, 
    MAX(gia) AS GiaCaoNhat, 
    MIN(gia) AS GiaThapNhat
FROM Sach;


-- Hàm xử lý chuỗi
SELECT 
    LEN(ten) AS 'Độ dài tên', 
    UPPER(ten) AS 'Viết hoa', 
    LOWER(ten) AS 'Viết thường', 
    LEFT(ten, 3) AS 'Lấy 3 ký tự đầu', 
    RIGHT(ten, 3) AS 'Lấy 3 ký tự cuối',
    REPLACE(ten, 'a', 'A') AS 'Thay thế a bằng A'
FROM Khachhang;


-- Hàm toán học
SELECT 
    ABS(-10) AS 'Giá trị tuyệt đối',
    POWER(2, 3) AS 'Lũy thừa 2^3',
    SQRT(16) AS 'Căn bậc hai của 16',
    ROUND(123.456, 2) AS 'Làm tròn đến 2 chữ số thập phân';


	-- Hàm xử lý ngày giờ
SELECT 
    GETDATE() AS 'Ngày giờ hiện tại',
    YEAR(GETDATE()) AS 'Năm hiện tại',
    MONTH(GETDATE()) AS 'Tháng hiện tại',
    DAY(GETDATE()) AS 'Ngày hiện tại',
    DATEADD(DAY, 7, GETDATE()) AS 'Ngày sau 7 ngày',
    DATEDIFF(DAY, '2024-01-01', GETDATE()) AS 'Số ngày đã trôi qua trong năm';


	go


SELECT COUNT(maphieunhap) AS mangay
FROM PHIEUNHAP
WHERE CAST(ngaynhap AS DATE) = CAST(GETDATE() AS DATE);

SELECT COUNT(maphieunhap) AS mangay
FROM PHIEUNHAP
WHERE CONVERT(DATE, ngaynhap) = CONVERT(DATE, GETDATE());

SELECT COUNT(maphieunhap) AS mangay
FROM PHIEUNHAP
WHERE DATEDIFF(DAY, ngaynhap, GETDATE()) = 0;



-- PROC
-- Dang Nhap



-- STORE PROC XOA NGUOI DUNG
CREATE or alter PROCEDURE SP_XoaNguoiDungVaLienQuan
    @NguoiDungId VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Role VARCHAR(10);

    -- Lấy role của người dùng
    SELECT @Role = role FROM NguoiDung WHERE id = @NguoiDungId;

    -- Kiểm tra nếu người dùng không tồn tại thì thoát
    IF @Role IS NULL
    BEGIN
        PRINT 'Người dùng không tồn tại.';
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Xóa các bản ghi trong bảng ChiTietDonHang nếu tồn tại đơn hàng liên quan
        IF EXISTS (SELECT 1 FROM DonHang WHERE nguoidung_id = @NguoiDungId)
        BEGIN
            DELETE FROM ChiTietDonHang
            WHERE donhang_id IN (SELECT id FROM DonHang WHERE nguoidung_id = @NguoiDungId);
        END

        -- Xóa các bản ghi trong bảng HoaDon nếu tồn tại đơn hàng liên quan
        IF EXISTS (SELECT 1 FROM DonHang WHERE nguoidung_id = @NguoiDungId)
        BEGIN
            DELETE FROM HoaDon
            WHERE donhang_id IN (SELECT id FROM DonHang WHERE nguoidung_id = @NguoiDungId);
        END

        -- Xóa các bản ghi trong bảng DonHang nếu tồn tại
        IF EXISTS (SELECT 1 FROM DonHang WHERE nguoidung_id = @NguoiDungId)
        BEGIN
            DELETE FROM DonHang
            WHERE nguoidung_id = @NguoiDungId;
        END

        -- Xóa khách hàng hoặc nhân viên dựa trên role của người dùng
        IF @Role = 'customer'
        BEGIN
            DELETE FROM KhachHang WHERE id_NguoiDung = @NguoiDungId;
        END
        ELSE IF @Role = 'staff' OR @Role = 'admin'
        BEGIN
            DELETE FROM NhanVien WHERE id_NguoiDung = @NguoiDungId;
        END

        -- Xóa người dùng
        DELETE FROM NguoiDung WHERE id = @NguoiDungId

        -- Commit transaction nếu không có lỗi
        COMMIT TRANSACTION;

        PRINT 'Xóa người dùng và các bản ghi liên quan thành công.';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;
        PRINT 'Lỗi xảy ra trong quá trình xóa người dùng. Đã rollback.';
    END CATCH
END;
GO

-- STORE PROC LẤY TẤT CẢ THỂ LOẠI
CREATE or alter PROCEDURE SP_Book_GetAllType
AS
BEGIN
	select *
	from TheLoai
END
GO
-- STORE PROC LẤY THỂ LOẠI theo id
CREATE or alter PROCEDURE SP_LayThongTinTheLoai @idTL VARCHAR(10)
AS
BEGIN
	select *
	from TheLoai tl
	where tl.id=@idTL
END
GO

CREATE or alter PROCEDURE  SP_LayTrangThaiDonHang  @TrangThai int,@idND VARCHAR(50)
as
begin
select * from DonHang dh  where  dh.trangthaiDH=@TrangThai AND @idND =dh.nguoidung_id
end

exec SP_LayTrangThaiDonHang 0, 'user123'
-- lay chi tietdon hang
CREATE OR ALTER PROCEDURE SP_LayChiTietDonHangTheoSachId
    @OrderID NVARCHAR(50),
    @SachId NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM ChiTietDonHang 
    WHERE donhang_id = @OrderID AND sach_id = @SachId;
END

--  Lấy Chi Tiết Đơn Hàng Theo Đơn Hàng ID

CREATE OR ALTER PROCEDURE SP_LayChiTietDonHangTheoDH
	@OrderID varchar(50)

AS
BEGIN
	SELECT *
	FROM ChiTietDonHang ct
	WHERE ct.donhang_id = @OrderID
END


--  Lấy Chi Tiết Đơn Hàng Theo Chi Tiết Đơn Hàng ID

CREATE OR ALTER PROCEDURE SP_LayChiTietDonHangTheoID
	@OrderDetailID varchar(50)

AS
BEGIN
	SELECT *
	FROM ChiTietDonHang ct
	WHERE ct.id = @OrderDetailID
END
GO
-- lấy chi tiết thông tin sách
CREATE OR ALTER PROCEDURE SP_ThongTinSachDuocDatCuaKhTheoId
    @SachId VARCHAR(50)
AS
BEGIN
    SELECT 
        S.id AS SachId,
        S.tieude AS TenSach,
		S.HinhAnh,
		S.Gia,
		S.SoLuongTon,
		S.NamXuatBan,
		S.MoTa,
		S.theloai_id,
        TL.ten AS TenTheLoai,
		STRING_AGG(TG.ten, ', ') AS TenTacGias, -- Sử dụng STRING_AGG
		STRING_AGG(TG.id, ', ') AS TacGiaIds, -- Sử dụng STRING_AGG
		NXB.ten as TenNhaXuatBan,
		NXB.id as NhaXuatBanId
    FROM 
        Sach S
        INNER JOIN TheLoai TL ON S.theloai_id = TL.id
        INNER JOIN TacGia_Sach TGS ON S.id = TGS.sach_id
        INNER JOIN TacGia TG ON TGS.tacgia_id = TG.id
        INNER JOIN NhaXuatBan NXB ON NXB.id = S.nxb_id
    WHERE 
        S.id = @SachId
	GROUP BY
		S.id, S.tieude, S.HinhAnh, S.Gia, S.SoLuongTon, S.NamXuatBan, S.MoTa, S.theloai_id, TL.ten, NXB.ten, NXB.id;
END;

exec SP_ThongTinSachDuocDatCuaKhTheoId 'S001'
-- LẤY SÁCH THEO ĐƠN HÀNG ID
CREATE OR ALTER PROCEDURE SP_LaySachTheoIdDonHang
    @OrderID VARCHAR(50) -- Tham số đầu vào: ID đơn hàng
AS
BEGIN
    SELECT 
        S.id AS SachId,
        S.tieude AS TenSach,
        S.HinhAnh,
        S.Gia,
        CD.soLuong AS SoLuongDat,
        S.NamXuatBan,
        S.MoTa,
        TL.ten AS TenTheLoai,
        STRING_AGG(TG.ten, ', ') AS TenTacGias, -- Kết hợp tên tác giả bằng dấu phẩy
        NXB.ten AS TenNhaXuatBan,
        NXB.id AS NhaXuatBanId
    FROM 
        ChiTietDonHang CD
        INNER JOIN Sach S ON CD.sach_id = S.id
        INNER JOIN TheLoai TL ON S.theloai_id = TL.id
        INNER JOIN TacGia_Sach TGS ON S.id = TGS.sach_id
        INNER JOIN TacGia TG ON TGS.tacgia_id = TG.id
        INNER JOIN NhaXuatBan NXB ON S.nxb_id = NXB.id
    WHERE 
        CD.donhang_id = @OrderID -- Lọc theo OrderID
    GROUP BY
        S.id, S.tieude, S.HinhAnh, S.Gia, CD.soLuong, S.NamXuatBan, S.MoTa, S.theloai_id, TL.ten, NXB.ten, NXB.id
    ORDER BY
        CD.soLuong DESC; -- Sắp xếp theo số lượng đặt giảm dần
END;


-- Lấy sách theo số lượng tồn
CREATE OR ALTER PROCEDURE SP_SapXepSachTheoSoLuongTon
AS
BEGIN
    SELECT 
	     S.id AS SachId,
        S.tieude AS TenSach,
		S.HinhAnh,
		S.Gia,
		S.SoLuongTon,
		S.NamXuatBan,
		S.MoTa,
		S.theloai_id,
        TL.ten AS TenTheLoai,
		STRING_AGG(TG.ten, ', ') AS TenTacGias, -- Sử dụng STRING_AGG
		STRING_AGG(TG.id, ', ') AS TacGiaIds, -- Sử dụng STRING_AGG
		NXB.ten as TenNhaXuatBan,
		NXB.id as NhaXuatBanId

    FROM 
        Sach S
        INNER JOIN TheLoai TL ON S.theloai_id = TL.id
        INNER JOIN TacGia_Sach TGS ON S.id = TGS.sach_id
        INNER JOIN TacGia TG ON TGS.tacgia_id = TG.id
        INNER JOIN NhaXuatBan NXB ON NXB.id = S.nxb_id

		GROUP BY
		S.id, S.tieude, S.HinhAnh, S.Gia, S.SoLuongTon, S.NamXuatBan, S.MoTa, S.theloai_id, TL.ten, NXB.ten, NXB.id
		order by soLuongTon DESC;

END;
-- lấy chi tiết thông tin sách theo thể loại

CREATE or alter PROCEDURE SP_DanhSachSachTheoTheLoai
AS
BEGIN
    SELECT 
        S.id AS SachId,
        S.tieude AS TenSach,
		S.HinhAnh,
		S.Gia,
		S.SoLuongTon,
		S.NamXuatBan,
		S.MoTa,
		S.theloai_id,
        TL.ten AS TenTheLoai,
		STRING_AGG(TG.ten, ', ') AS TenTacGias, -- Sử dụng STRING_AGG
		STRING_AGG(TG.id, ', ') AS TacGiaIds, -- Sử dụng STRING_AGG
		NXB.ten as TenNhaXuatBan,
		NXB.id as NhaXuatBanId

    FROM 
        Sach S
        INNER JOIN TheLoai TL ON S.theloai_id = TL.id
        INNER JOIN TacGia_Sach TGS ON S.id = TGS.sach_id
        INNER JOIN TacGia TG ON TGS.tacgia_id = TG.id
        INNER JOIN NhaXuatBan NXB ON NXB.id = S.nxb_id

    WHERE 
        S.theloai_id = @TheLoaiId
	GROUP BY
		S.id, S.tieude, S.HinhAnh, S.Gia, S.SoLuongTon, S.NamXuatBan, S.MoTa, S.theloai_id, TL.ten, NXB.ten, NXB.id;
END;


-- Lấy tất cả sách

CREATE or alter PROCEDURE SP_DanhSachSach
AS
BEGIN
    SELECT 
	     S.id AS SachId,
        S.tieude AS TenSach,
		S.HinhAnh,
		S.Gia,
		S.SoLuongTon,
		S.NamXuatBan,
		S.MoTa,
		S.theloai_id,
        TL.ten AS TenTheLoai,
		STRING_AGG(TG.ten, ', ') AS TenTacGias, -- Sử dụng STRING_AGG
		STRING_AGG(TG.id, ', ') AS TacGiaIds, -- Sử dụng STRING_AGG
		NXB.ten as TenNhaXuatBan,
		NXB.id as NhaXuatBanId

    FROM 
        Sach S
        INNER JOIN TheLoai TL ON S.theloai_id = TL.id
        INNER JOIN TacGia_Sach TGS ON S.id = TGS.sach_id
        INNER JOIN TacGia TG ON TGS.tacgia_id = TG.id
        INNER JOIN NhaXuatBan NXB ON NXB.id = S.nxb_id

		GROUP BY
		S.id, S.tieude, S.HinhAnh, S.Gia, S.SoLuongTon, S.NamXuatBan, S.MoTa, S.theloai_id, TL.ten, NXB.ten, NXB.id;

END;



-- PROC THANH TOÁN
CREATE SEQUENCE dbo.Seq_HoaDon
    AS INT
    START WITH 1
    INCREMENT BY 1;

CREATE OR ALTER PROCEDURE SP_ThanhToanDonHang
    @DonHangId VARCHAR(50),
    @PhuongThucTT NVARCHAR(50),
    @Email NVARCHAR(255),
    @SoDienThoai VARCHAR(10),
    @DiaChi NVARCHAR(255),
    @TenNguoiDatHang NVARCHAR(255),
    @TongTien DECIMAL(10, 2) OUTPUT
AS
BEGIN
    -- Bắt đầu transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Kiểm tra xem đơn hàng có tồn tại và chưa thanh toán
        IF EXISTS (SELECT 1 FROM DonHang WHERE id = @DonHangId AND trangthaiDH = 0)
        BEGIN
            -- Cập nhật trạng thái đơn hàng thành đã thanh toán
            UPDATE DonHang
            SET trangthaiDH = 1
            WHERE id = @DonHangId;

            -- Lấy tổng tiền của đơn hàng và trả về qua tham số đầu ra
            -- SELECT @TongTien = tongTien FROM DonHang WHERE id = @DonHangId;

            -- Tạo ID cho hóa đơn với tiền tố "HD" và số tự động tăng
            DECLARE @HoaDonId VARCHAR(50);
            SET @HoaDonId = 'HD' + RIGHT('000' + CAST(NEXT VALUE FOR dbo.Seq_HoaDon AS VARCHAR), 4);

            -- Tạo mới hóa đơn cho đơn hàng
            INSERT INTO HoaDon (
                id,
                donhang_id,
                ngayLap,
                tongTien,
                phuongThucTT,
                trangthaiTT,
                email,
                sodienthoai,
                diachi,
                tenNguoiDatHang
            )
            VALUES (
                @HoaDonId,
                @DonHangId,
                GETDATE(),
                @TongTien,
                @PhuongThucTT,
                N'Đặt hàng thành công',
                @Email,
                @SoDienThoai,
                @DiaChi,
                @TenNguoiDatHang
            );

            -- Xác nhận giao dịch
            COMMIT TRANSACTION;
            PRINT 'Thanh toán thành công và hóa đơn đã được tạo.';
        END
        ELSE
        BEGIN
            -- Nếu đơn hàng không tồn tại hoặc đã thanh toán, báo lỗi
            RAISERROR ('Đơn hàng không tồn tại hoặc đã thanh toán.', 16, 1);
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        -- Bắt lỗi nếu có vấn đề
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;

-- PROCEDURE: Cập nhật trạng thái đơn hàng và số lượng tồn kho khi đơn hàng được thanh toán.


CREATE PROCEDURE SP_CapNhatDonHang
    @donhang_id VARCHAR(50)
AS
BEGIN
    DECLARE @sach_id VARCHAR(50), @soLuong INT;

    -- Cập nhật trạng thái đơn hàng
    UPDATE DonHang
    SET trangthaiDH = 1 -- Đã thanh toán
    WHERE id = @donhang_id;

    -- Giảm số lượng tồn kho của từng sách trong đơn hàng
    DECLARE cur CURSOR FOR
        SELECT sach_id, soLuong
        FROM ChiTietDonHang
        WHERE donhang_id = @donhang_id;

    OPEN cur;
    FETCH NEXT FROM cur INTO @sach_id, @soLuong;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon - @soLuong
        WHERE id = @sach_id;

        FETCH NEXT FROM cur INTO @sach_id, @soLuong;
    END;
    CLOSE cur;
    DEALLOCATE cur;
END;


-- Stored Procedure để cập nhật đơn hàng, đặt ngày đặt hàng là ngày hiện tại

CREATE OR ALTER PROCEDURE SP_UpdateDonHang
    @id VARCHAR(50),
    @nguoidung_id VARCHAR(50) = NULL,
    @trangthaiDH INT = NULL,
    @tongTien DECIMAL(10, 2) = NULL
AS
BEGIN
    -- Kiểm tra xem đơn hàng tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM DonHang WHERE id = @id)
    BEGIN
        RAISERROR('Đơn hàng không tồn tại.', 16, 1)
        RETURN 0
    END

    -- Cập nhật các trường dữ liệu
	-- Đặt ngày đặt hàng là ngày hiện tại
	DECLARE @ngayDatHang DATETIME = GETDATE();

    UPDATE DonHang
    SET
        nguoidung_id = ISNULL(@nguoidung_id, nguoidung_id),
        trangthaiDH = ISNULL(@trangthaiDH, trangthaiDH),
        ngayDatHang = @ngayDatHang,  -- Sử dụng biến @ngayDatHang
        tongTien = ISNULL(@tongTien, tongTien)
    WHERE
        id = @id;



    RETURN 1;
END;
-- Stored Procedure để cập nhật chi tiết đơn hàng
CREATE PROCEDURE SP_UpdateChiTietDonHang
    @id VARCHAR(50),
    @donhang_id VARCHAR(50) = NULL,
    @sach_id VARCHAR(50) = NULL,
    @soLuong INT = NULL,
    @giaDonVi DECIMAL(10, 2) = NULL
AS
BEGIN
    -- Kiểm tra xem chi tiết đơn hàng tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM ChiTietDonHang WHERE id = @id)
    BEGIN
        RAISERROR('Chi tiết đơn hàng không tồn tại.', 16, 1)
        RETURN
    END

    -- Cập nhật các trường dữ liệu nếu giá trị truyền vào khác NULL
    UPDATE ChiTietDonHang
    SET
        donhang_id = ISNULL(@donhang_id, donhang_id),
        sach_id = ISNULL(@sach_id, sach_id),
        soLuong = ISNULL(@soLuong, soLuong),
        giaDonVi = ISNULL(@giaDonVi, giaDonVi)
    WHERE
        id = @id;
    
    -- Kiểm tra dữ liệu sau cập nhật
    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('Không có thay đổi được thực hiện.', 16, 1)
        RETURN 1;  -- Trả về mã lỗi khác 0
    END

    -- Trả về thông tin chi tiết đơn hàng cập nhật (tùy chọn)
    SELECT *
    FROM ChiTietDonHang
    WHERE id = @id;

    RETURN 0; -- Trả về 0 nếu cập nhật thành công
END;
-- Store procedure xóa hóa đơn
CREATE OR ALTER PROCEDURE SP_DeleteHoaDonVaCapNhatDonHang
    @hoaDonId VARCHAR(50)
AS
BEGIN
    -- Bắt đầu một giao dịch để đảm bảo tính toàn vẹn dữ liệu
    BEGIN TRANSACTION;

    -- Lấy DonHangId từ hóa đơn trước khi xóa
    DECLARE @donHangId VARCHAR(50);
    SELECT @donHangId = donhang_id FROM HoaDon WHERE id = @hoaDonId;

    -- Kiểm tra nếu hóa đơn tồn tại và lấy các chi tiết đơn hàng (sách và số lượng)
    DECLARE @SachId VARCHAR(50);
    DECLARE @SoLuong INT;

    -- Duyệt qua các chi tiết của hóa đơn để trả lại số lượng sách vào kho
    DECLARE ChiTietCursor CURSOR FOR
        SELECT ct.sach_id, ct.soLuong
        FROM ChiTietDonHang ct
        JOIN HoaDon hd ON ct.donhang_id = hd.donhang_id
        WHERE hd.id = @hoaDonId;

    OPEN ChiTietCursor;
    FETCH NEXT FROM ChiTietCursor INTO @SachId, @SoLuong;

    -- Cập nhật số lượng tồn kho của sách
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Tăng số lượng tồn kho của sách
        UPDATE Sach
        SET soLuongTon = soLuongTon + @SoLuong
        WHERE id = @SachId;

        FETCH NEXT FROM ChiTietCursor INTO @SachId, @SoLuong;
    END

    -- Đóng con trỏ
    CLOSE ChiTietCursor;
    DEALLOCATE ChiTietCursor;

    -- Xóa hóa đơn
    DELETE FROM HoaDon WHERE id = @hoaDonId;

    -- Cập nhật trạng thái đơn hàng thành 0 (chưa thanh toán) nếu đơn hàng có tồn tại
    IF @donHangId IS NOT NULL
    BEGIN
        UPDATE DonHang
        SET trangthaiDH = 0
        WHERE id = @donHangId;
    END

    -- Kiểm tra và hoàn thành giao dịch
    IF @@ERROR = 0
    BEGIN
        COMMIT TRANSACTION;
    END
    ELSE
    BEGIN
        ROLLBACK TRANSACTION;
    END
END

-- store procedure xóa ctdh
CREATE PROCEDURE SP_DeleteChiTietDonHang
    @id VARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM ChiTietDonHang WHERE id = @id)
        RETURN 1;  -- Lỗi: Không tồn tại

    DELETE FROM ChiTietDonHang WHERE id = @id;
    IF @@ROWCOUNT = 0
        RETURN 2;  -- Lỗi: Không có dòng nào bị xóa

    RETURN 0; -- Thành công
END;
-- PROC TAO DON HANG

CREATE OR ALTER PROCEDURE SP_TaoDonHang
    @IDND NVARCHAR(50),
    @OrderPrice DECIMAL(18, 2),
    @OrderStatus INT,
    @NGAYDATHANG DATETIME = NULL -- tham số ngày đặt hàng
AS
BEGIN
    -- Nếu @NGAYDATHANG không được truyền vào, sử dụng ngày hiện tại
    IF @NGAYDATHANG IS NULL
    BEGIN
        SET @NGAYDATHANG = GETDATE(); -- Lấy ngày giờ hiện tại
    END

    DECLARE @NewOrderID NVARCHAR(50);
    DECLARE @MaxID NVARCHAR(50);

    -- Lấy giá trị id lớn nhất hiện tại trong bảng DonHang
    SELECT @MaxID = MAX(id) FROM DonHang WHERE id LIKE 'DH%';

    -- Kiểm tra nếu không có id nào, đặt giá trị khởi tạo là 'DH001'
    IF @MaxID IS NULL
    BEGIN
        SET @NewOrderID = 'DH001';
    END
    ELSE
    BEGIN
        -- Lấy phần số trong id, tăng số lên 1 và tạo lại id mới
        SET @NewOrderID = 'DH' + RIGHT('000' + CAST(CAST(SUBSTRING(@MaxID, 3, LEN(@MaxID)) AS INT) + 1 AS VARCHAR), 3);
    END

    -- Thực hiện INSERT vào bảng DonHang
    INSERT INTO DonHang (id, nguoidung_id, tongTien, trangthaiDH, ngayDatHang)
    VALUES (@NewOrderID, @IDND, @OrderPrice, @OrderStatus, @NGAYDATHANG);
    
END;

exec SP_TaoDonHang 'admin123',0,0
-- PROC TẠO CTHDH
CREATE PROCEDURE SP_TaoChiTietDonHang
    @OrderID NVARCHAR(50),
    @SachId NVARCHAR(50),
    @SoLuong INT,
    @GiaDonVi DECIMAL(10, 2)
AS
BEGIN
    -- Tạo ID mới cho ChiTietDonHang, bắt đầu với 'CTDH' và theo sau là giá trị số tự động
    DECLARE @NewId NVARCHAR(50);
    SET @NewId = 'CTDH' + CAST((SELECT ISNULL(MAX(CAST(SUBSTRING(Id, 5, LEN(Id)) AS INT)), 0) + 1 FROM ChiTietDonHang) AS NVARCHAR(50));

    -- Thực hiện INSERT vào bảng ChiTietDonHang
    INSERT INTO ChiTietDonHang (Id, donhang_id, sach_id, soLuong, giaDonVi)
    VALUES (@NewId, @OrderID, @SachId, @SoLuong, @GiaDonVi);
END;

-- PROC THEM SACH
CREATE OR ALTER PROCEDURE SP_ThemSach
    @TieuDe NVARCHAR(255),
    @TheLoaiID VARCHAR(50),
    @Gia DECIMAL(10, 2),
    @SoLuongTon INT,
    @HinhAnh VARCHAR(MAX),
    @NamXuatBan INT,
    @MoTa NVARCHAR(MAX),
    @NXB_ID VARCHAR(50),
    @TacGiaIDList NVARCHAR(MAX) -- Danh sách các ID tác giả, phân cách bởi dấu phẩy
AS
BEGIN
    SET NOCOUNT ON;

    -- Tạo ID cho sách tự động
    DECLARE @SachID VARCHAR(50);
    DECLARE @MaxSachID INT;

    -- Lấy số lượng sách hiện tại và xác định ID sách tiếp theo
    SELECT @MaxSachID = COUNT(*) + 1 FROM Sach; -- Tính số sách hiện tại và thêm 1 cho ID tiếp theo

    -- Tạo SachID theo định dạng SP001, SP002, ...
    SET @SachID = 'SP' + RIGHT('000' + CAST(@MaxSachID AS VARCHAR), 3);

    -- Thêm thông tin sách vào bảng Sach
    INSERT INTO Sach (id, tieude, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, MoTa, nxb_id)
    VALUES (@SachID, @TieuDe, @TheLoaiID, @Gia, @SoLuongTon, @HinhAnh, @NamXuatBan, @MoTa, @NXB_ID);

    -- Sử dụng CURSOR để thêm các tác giả vào bảng TacGia_Sach
    DECLARE @TacGiaID VARCHAR(50);
    DECLARE TacGiaCursor CURSOR FOR
    SELECT value
    FROM STRING_SPLIT(@TacGiaIDList, ',');

    -- Mở CURSOR
    OPEN TacGiaCursor;

    -- Lặp qua từng tác giả trong danh sách và thêm vào TacGia_Sach
    FETCH NEXT FROM TacGiaCursor INTO @TacGiaID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO TacGia_Sach (sach_id, tacgia_id)
        VALUES (@SachID, @TacGiaID);

        FETCH NEXT FROM TacGiaCursor INTO @TacGiaID;
    END

    -- Đóng và hủy CURSOR
    CLOSE TacGiaCursor;
    DEALLOCATE TacGiaCursor;

    PRINT 'Thêm sách thành công với ID: ' + @SachID;
END;


-- PROC CAP NHAT SACH
CREATE PROCEDURE SP_UpdateThongTinSach
    @SachID VARCHAR(50),
    @TieuDe NVARCHAR(255),
    @TheLoaiID VARCHAR(50),
    @Gia DECIMAL(10, 2),
    @SoLuongTon INT,
    @HinhAnh VARCHAR(MAX),
    @NamXuatBan INT,
    @MoTa NVARCHAR(MAX),
    @NXB_ID VARCHAR(50),
    @TacGiaIDList NVARCHAR(MAX) -- Danh sách các ID tác giả, phân cách bởi dấu phẩy
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem sách có tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM Sach WHERE id = @SachID)
    BEGIN
        PRINT 'Sách không tồn tại.';
        RETURN;
    END

    -- Cập nhật thông tin chi tiết của sách trong bảng Sach
    UPDATE Sach
    SET 
        tieude = @TieuDe,
        theloai_id = @TheLoaiID,
        gia = @Gia,
        soLuongTon = @SoLuongTon,
        hinhAnh = @HinhAnh,
        namXuatBan = @NamXuatBan,
        MoTa = @MoTa,
        nxb_id = @NXB_ID
    WHERE 
        id = @SachID;

    -- Xóa các tác giả hiện tại của sách trong bảng TacGia_Sach
    DELETE FROM TacGia_Sach WHERE sach_id = @SachID;

    -- Sử dụng CURSOR để thêm các tác giả mới vào bảng TacGia_Sach
    DECLARE @TacGiaID VARCHAR(50);
    DECLARE TacGiaCursor CURSOR FOR
    SELECT value
    FROM STRING_SPLIT(@TacGiaIDList, ',');

    -- Mở CURSOR
    OPEN TacGiaCursor;

    -- Lặp qua từng tác giả trong danh sách và thêm vào TacGia_Sach
    FETCH NEXT FROM TacGiaCursor INTO @TacGiaID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO TacGia_Sach (sach_id, tacgia_id)
        VALUES (@SachID, @TacGiaID);

        FETCH NEXT FROM TacGiaCursor INTO @TacGiaID;
    END

    -- Đóng và hủy CURSOR
    CLOSE TacGiaCursor;
    DEALLOCATE TacGiaCursor;

    PRINT 'Cập nhật thông tin sách thành công.';
END;
-- PROC XOA SACH
CREATE PROCEDURE SP_XoaSach
    @SachID VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem sách có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Sach WHERE id = @SachID)
    BEGIN
        PRINT 'Sách không tồn tại.';
        RETURN;
    END

    -- Xóa các tác giả liên quan đến sách trong bảng TacGia_Sach
    DELETE FROM TacGia_Sach WHERE sach_id = @SachID;

    -- Xóa sách trong bảng Sach
    DELETE FROM Sach WHERE id = @SachID;

    PRINT 'Xóa sách thành công.';
END;

-- PROC ĐẶT HÀNG THÀNH CÔNG
CREATE PROCEDURE SP_DatHangThanhCong
    @donhang_id VARCHAR(50)
AS
BEGIN
    -- Bắt đầu transaction để đảm bảo tính toàn vẹn dữ liệu
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Bước 1: Cập nhật trạng thái đơn hàng thành "1" (Đã đặt hàng thành công)
        UPDATE DonHang
        SET trangthaiDH = 1
        WHERE id = @donhang_id;

        -- Bước 2: Khai báo các biến để lưu mã sách và số lượng sách trong từng dòng chi tiết của đơn hàng
        DECLARE @sach_id VARCHAR(50);
        DECLARE @soLuong INT;

        -- Bước 3: Khai báo cursor để duyệt qua từng dòng trong bảng ChiTietDonHang của đơn hàng
        DECLARE sach_cursor CURSOR FOR
        SELECT sach_id, soLuong
        FROM ChiTietDonHang
        WHERE donhang_id = @donhang_id;

        OPEN sach_cursor;

        -- Bước 4: Duyệt qua từng dòng và cập nhật số lượng tồn kho của sách
        FETCH NEXT FROM sach_cursor INTO @sach_id, @soLuong;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            -- Kiểm tra xem sách có đủ tồn kho không
            IF (SELECT soLuongTon FROM Sach WHERE id = @sach_id) >= @soLuong
            BEGIN
                -- Nếu đủ tồn kho, tiến hành trừ số lượng tồn kho
                UPDATE Sach
                SET soLuongTon = soLuongTon - @soLuong
                WHERE id = @sach_id;
            END
            ELSE
            BEGIN
                -- Nếu không đủ tồn kho, thông báo lỗi và rollback transaction
                RAISERROR ('Số lượng tồn kho của sách %s không đủ để thực hiện đơn hàng', 16, 1, @sach_id);
                ROLLBACK TRANSACTION;
                RETURN;
            END

            -- Tiếp tục duyệt qua dòng tiếp theo
            FETCH NEXT FROM sach_cursor INTO @sach_id, @soLuong;
        END;

        -- Đóng và giải phóng cursor
        CLOSE sach_cursor;
        DEALLOCATE sach_cursor;

        -- Nếu tất cả đều thành công, commit transaction
        COMMIT TRANSACTION;
        
        PRINT 'Đặt hàng thành công và cập nhật số lượng tồn kho.';
    END TRY
    BEGIN CATCH
        -- Xử lý lỗi và rollback nếu có lỗi
        ROLLBACK TRANSACTION;
        
        -- Lấy thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Thông báo lỗi
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
-- FUNTION
CREATE FUNCTION fn_TongTienDonHang(@donhang_id NVARCHAR(50))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @TongTien DECIMAL(18, 2);

    SELECT @TongTien = SUM(cd.soLuong * cd.giaDonVi)
    FROM ChiTietDonHang cd
    WHERE cd.donhang_id = @donhang_id;

    RETURN ISNULL(@TongTien, 0);
END;

CREATE FUNCTION FnKiemTraSachSapHetHang()
RETURNS TABLE
AS
RETURN
(
    SELECT id, tieude, soLuongTon
    FROM Sach
    WHERE soLuongTon < 10
);
-- D) FUNCTION: Tính tổng số lượng sách đã bán của một mã sách cụ thể.
DROP FUNCTION IF EXISTS dbo.FnTinhTongSoLuongSachDaBan;

CREATE FUNCTION dbo.FnTinhTongSoLuongSachDaBan (@SachId VARCHAR(50))
RETURNS INT
AS
BEGIN
    DECLARE @TongSoLuong INT;

    -- Tính tổng số lượng sách đã bán cho mã sách cụ thể khi trạng thái hóa đơn là 1 (thành công)
    SELECT @TongSoLuong = SUM(ctd.soLuong) 
    FROM ChiTietDonHang ctd
    JOIN DonHang dh ON ctd.donhang_id = dh.id
    WHERE ctd.sach_id = @SachId
    AND dh.trangthaiDH = '1';  -- '1' là trạng thái đã thanh toán thành công

    -- Nếu không có đơn hàng nào thì trả về 0
    IF @TongSoLuong IS NULL
    BEGIN
        SET @TongSoLuong = 0;
    END

    RETURN @TongSoLuong;
END;

SELECT dbo.FnTinhTongSoLuongSachDaBan('S004');


-- PROC LOGIN
CREATE or alter PROCEDURE SP_LOGIN
    @Username NVARCHAR(50),
    @Password NVARCHAR(255),
    @UserRole VARCHAR(10) OUTPUT,
    @FullName NVARCHAR(255) OUTPUT,
	@idND VARCHAR(50) OUTPUT

AS
BEGIN
    SELECT  @UserRole = role,
            @FullName = CASE
                            WHEN role = 'customer' THEN k.ten
                            WHEN role = 'staff' THEN n.ten
                            WHEN role = 'admin' THEN n.ten

                            ELSE NULL
                        END,
						   @idND = NguoiDung.id

    FROM NguoiDung
    LEFT JOIN Khachhang k ON NguoiDung.id = k.id_NguoiDung
    LEFT JOIN NhanVien n ON NguoiDung.id = n.id_NguoiDung
    WHERE username = @Username AND password = @Password;
    
    IF @UserRole IS NULL
    BEGIN
        RAISERROR('Tên người dùng hoặc mật khẩu không chính xác.', 16, 1);
        RETURN;
    END

    IF @FullName IS NULL
    BEGIN
        RAISERROR('Không tìm thấy thông tin.', 16, 1);
        RETURN;
    END

    PRINT N'Đăng nhập thành công.';
END;


-- procedure lấy danh sách nhà xuất bản

CREATE PROCEDURE SP_DanhSachNXB
AS
BEGIN
    SELECT id, ten, diachi, sodienthoai, email
    FROM NhaXuatBan;
END;

-- procedure lấy danh sách tác giá

CREATE PROCEDURE SP_DanhSachTacGia
AS
BEGIN
    SELECT * 
    FROM TacGia;
END;


exec SP_DanhSachTacGia


-- procedure

-- 


-- thêm kh
CREATE PROCEDURE ThemKH
    @id VARCHAR(50),
    @ten VARCHAR(255),
    @diachi VARCHAR(255),
    @sodienthoai VARCHAR(10),
    @email VARCHAR(255)
AS
BEGIN
    INSERT INTO Khachhang (id, ten, diachi, sodienthoai, email)
    VALUES (@id, @ten, @diachi, @sodienthoai, @email);
END;



-- cập nhật hàng tồn kho

CREATE PROCEDURE UpdateTonKho
    @sach_id VARCHAR(50),
    @soLuong INT
AS
BEGIN
    UPDATE Sach
    SET soLuongTon = soLuongTon + @soLuong
    WHERE id = @sach_id;
END;


-- lấy thông tin sách theo id

CREATE PROCEDURE HienThiSachId
    @id VARCHAR(50)
AS
BEGIN
    SELECT * FROM Sach
    WHERE id = @id;
END;


-- Lấy Thông Tin Khách Hàng Và Đơn Hàng Của Họ
CREATE PROCEDURE ThongTinKhVaDH
    @khachhang_id VARCHAR(50)
AS
BEGIN
    SELECT k.id AS KhachHangID, k.ten AS TenKhachHang, d.id AS DonHangID, d.trangthaiDH AS TrangThaiDonHang, d.tongTien AS TongTien
    FROM Khachhang k
    LEFT JOIN DonHang d ON k.id = d.khachhang_id
    WHERE k.id = @khachhang_id;
END;

-- Lay sach duoc dat nhieu nhat



-- Tìm Sách Theo Từ Khóa Trong Tiêu Đề
CREATE PROCEDURE TimSachTheoTuKhoa
    @keyword NVARCHAR(255)
AS
BEGIN
    SELECT * FROM Sach
    WHERE tieude LIKE '%' + @keyword + '%';
END;

--  Tính Tổng Số Sách Theo Thể Loại
CREATE PROCEDURE TongSachTL
    @genre_id VARCHAR(50)
AS
BEGIN
    SELECT COUNT(*) AS TongSoSach
    FROM Sach
    WHERE theloai_id = @genre_id;
END;


-- lay doanh thu theo tháng

--  Lấy Các Đơn Hàng Chưa Thanh Toán Trong Khoảng Thời Gian
-------------------------------- CURSOR




-- Lấy thông tin tài khoản



-- FUNCTION: Tính tổng doanh thu từ tất cả các đơn hàng đã thanh toán.
CREATE FUNCTION fn_TongDoanhThu()
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @tongDoanhThu DECIMAL(10, 2);

    SELECT @tongDoanhThu = SUM(tongTien)
    FROM DonHang
    WHERE trangthaiDH = 1; -- Đã thanh toán

    RETURN @tongDoanhThu;
END;


-- Tìm kiếm sách theo thể loại và khoảng giá.

CREATE PROCEDURE sp_TimSachTheoTheLoaiGia
    @theloai_id VARCHAR(50),
    @giaMin DECIMAL(10, 2),
    @giaMax DECIMAL(10, 2)
AS
BEGIN
    SELECT *
    FROM Sach
    WHERE theloai_id = @theloai_id
      AND gia BETWEEN @giaMin AND @giaMax;
END;


-- PROCEDURE: Lấy danh sách khách hàng theo tổng giá trị đơn hàng đã mua.

CREATE PROCEDURE sp_LayDanhSachKhachHangTheoGiaTri
    @minValue DECIMAL(10, 2),
    @maxValue DECIMAL(10, 2)
AS
BEGIN
    SELECT k.ten, k.diachi, k.sodienthoai, SUM(d.tongTien) AS TongGiaTriMua
    FROM Khachhang k
    JOIN DonHang d ON k.id = d.khachhang_id
    WHERE d.trangthaiDH = 1 -- Đã thanh toán
    GROUP BY k.ten, k.diachi, k.sodienthoai
    HAVING SUM(d.tongTien) BETWEEN @minValue AND @maxValue;
END;


-- Kiểm tra số lượng tồn kho của sách.
CREATE FUNCTION fn_KiemTraTonKho(@sach_id VARCHAR(50))
RETURNS INT
AS
BEGIN
    DECLARE @soLuongTon INT;

    SELECT @soLuongTon = soLuongTon
    FROM Sach
    WHERE id = @sach_id;

    RETURN @soLuongTon;
END;


-- CURSOR: Tạo danh sách top 5 sách bán chạy nhất.

CREATE PROCEDURE sp_TopSachBanChay
AS
BEGIN
    DECLARE @sach_id VARCHAR(50), @tongSoLuong INT;

    DECLARE cur CURSOR FOR
        SELECT TOP 5 sach_id, SUM(soLuong) AS tongSoLuong
        FROM ChiTietDonHang
        GROUP BY sach_id
        ORDER BY tongSoLuong DESC;

    OPEN cur;
    FETCH NEXT FROM cur INTO @sach_id, @tongSoLuong;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT 'Sách ID: ' + @sach_id + ', Số lượng đã bán: ' + CAST(@tongSoLuong AS VARCHAR);

        FETCH NEXT FROM cur INTO @sach_id, @tongSoLuong;
    END;
    CLOSE cur;
    DEALLOCATE cur;
END;



   SELECT hd.id, hd.donhang_id, hd.ngayLap, hd.tongTien, hd.phuongThucTT, 
          hd.trangthaiTT, hd.email, hd.sodienthoai, hd.diachi, hd.tenNguoiDatHang
   FROM HoaDon hd
   INNER JOIN DonHang dh ON hd.donhang_id = dh.id
   WHERE dh.nguoidung_id = @hoadonid


